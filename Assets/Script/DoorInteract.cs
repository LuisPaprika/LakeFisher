using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Door Interacted");
    }
}
