using System;
using UnityEngine;

public class DoorInteract : InteractableBase
{
    [Header("Animator")]
    [SerializeField] Animator doorAnimator;
    [SerializeField] AudioSource openDoorSFX;
    [SerializeField] AudioSource closeDoorSFX;
    private bool interacted = false;
    public override void Interact()
    {
        interacted = !interacted;
        if (interacted)
        {
            doorAnimator.SetTrigger("OpenDoor");
            openDoorSFX.Play();
        }
        else
        {
            doorAnimator.SetTrigger("CloseDoor");
            closeDoorSFX.Play();
        }

    }

}
