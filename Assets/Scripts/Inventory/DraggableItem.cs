using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;

    InventoryItem item;
    InventoryUI ui;
    RectTransform rect;
    Vector2 startPos;
    bool dragging;

    public void Init(InventoryItem i, InventoryUI inventory)
    {
        item = i;
        ui = inventory;
        rect = GetComponent<RectTransform>();
        icon.sprite = item.data.icon;

        UpdateVisual();
    }

    public void OnRotate()
    {
        if (!dragging) return;

        item.rotated = !item.rotated;
        UpdateVisual();
    }


    void UpdateVisual()
    {
        rect.sizeDelta = new Vector2(
            item.Width * ui.cellSize,
            item.Height * ui.cellSize
        );

        rect.anchoredPosition = ui.GridToWorld(item.x, item.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        startPos = rect.anchoredPosition;
        ui.Grid.Remove(item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;

        if (!RectTransformUtility.RectangleContainsScreenPoint(ui.gridRoot, Input.mousePosition))
        {
            DropToWorld();
            Destroy(gameObject);
            return;
        }

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            ui.gridRoot, Input.mousePosition, null, out localPos);

        Vector2Int gridPos = ui.WorldToGrid(localPos);

        

        if (ui.Grid.CanPlace(item, gridPos.x, gridPos.y))
        {
            ui.Grid.Place(item, gridPos.x, gridPos.y);
            rect.anchoredPosition = ui.GridToWorld(item.x, item.y);
        }
        else
        {
            // return to original
            ui.Grid.Place(item, item.x, item.y);
            rect.anchoredPosition = startPos;
        }
    }
    void DropToWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Instantiate(item.data.worldPrefab, hit.point, Quaternion.identity);
        }
    }

}
