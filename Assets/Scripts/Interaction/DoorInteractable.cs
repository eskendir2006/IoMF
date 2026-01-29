using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        StartCoroutine(RotateDoor(openAngle));
    }
    public void Interact(InteractComponent interactor)
    {
        
    }

    public float openAngle = 90f; // Angle to open the door (e.g., 90 degrees)
    public float closeAngle = 0f; // Original angle (closed)
    public float rotationSpeed = 2f; // Speed of opening/closing
    private bool isOpen = false;

    IEnumerator RotateDoor(float targetAngle)
    {
        Debug.Log("Door opening wow");

        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(0, targetAngle, 0);
        float timeElapsed = 0;

        while (timeElapsed < (1 / rotationSpeed))
        {
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed * rotationSpeed);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }
        transform.localRotation = endRotation; // Ensure it reaches the exact target rotation
    }
}