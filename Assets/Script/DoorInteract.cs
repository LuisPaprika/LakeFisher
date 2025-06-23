using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    [SerializeField] Animator doorAnimator;
    private bool interacted = false;
    public void Interact()
    {
        interacted = !interacted;
        if (interacted)
        {
            doorAnimator.SetTrigger("OpenDoor");
        }
        else
        {
            doorAnimator.SetTrigger("CloseDoor");
        }
            
    }
}
