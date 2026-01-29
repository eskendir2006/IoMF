using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public int gridWidth = 4;
    public int gridHeight = 3;
    public float cellSize = 64f;
    public RectTransform gridRoot;
    public GameObject itemPrefab;

    
    
    InventoryGrid grid;
    public InventoryGrid Grid => grid;

    void Start()
    {
        grid = new InventoryGrid(gridWidth, gridHeight);
    }

    public bool AddItem(ItemData data)
    {
        if (grid.TryAdd(data, out InventoryItem item))
        {
            CreateItemUI(item);
            return true;
        }

        return false; // inventory full
    }


    void CreateItemUI(InventoryItem item)
    {
        GameObject go = Instantiate(itemPrefab, gridRoot);
        DraggableItem ui = go.GetComponent<DraggableItem>();
        ui.Init(item, this);
    }

    public Vector2 GridToWorld(int x, int y)
    {
        return new Vector2(x * cellSize, -y * cellSize);
    }
    public Vector2Int WorldToGrid(Vector2 pos)
    {
        int x = Mathf.FloorToInt(pos.x / cellSize);
        int y = Mathf.FloorToInt(-pos.y / cellSize);
        return new Vector2Int(x, y);
    }

}
