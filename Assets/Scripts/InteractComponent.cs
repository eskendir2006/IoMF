using UnityEngine;
interface IInteractable
{
    public void Interact();
    public void Interact(InteractComponent interactor);
}

public class InteractComponent : MonoBehaviour
{
    [SerializeField] private Transform interactorSource;
    [SerializeField] private float interactorRange;
    public InventoryUI inventory;

    public void OnInteract()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactorRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                Debug.Log("Ray succesfully hit on: " + interactObj);
                interactObj.Interact(this);
            }
        }
    }
}
