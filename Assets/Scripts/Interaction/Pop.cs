using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Pop : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted");
        Destroy(gameObject);
    }
    public void Interact(InteractComponent interactor)
    {
        
    }
}
