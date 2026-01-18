using UnityEngine;

public class Pop : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted");
        Destroy(gameObject);
    }
}
