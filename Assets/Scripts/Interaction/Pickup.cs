using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    public ItemData data;
    public void Interact(InteractComponent interactor)
    {
        bool added = interactor.inventory.AddItem(data);

        if (added)
            Destroy(gameObject);
    }
    public void Interact()
    {
        // Not used
    }

}
