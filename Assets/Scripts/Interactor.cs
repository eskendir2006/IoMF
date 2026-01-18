using UnityEngine;
interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactorSource;
    [SerializeField] private float interactorRange;

    public void OnInteract()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactorRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
            }
        }
    }
}
