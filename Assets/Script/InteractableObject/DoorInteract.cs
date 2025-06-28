using System;
using UnityEngine;

public class DoorInteract : InteractableBase
{
    [Header("Animator")]
    [SerializeField] Animator doorAnimator;
    private bool interacted = false;
    public override void Interact()
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
