using System;
using UnityEngine;

public class BedInteract : InteractableBase
{
    public static event Action onSleep;
    public override void Interact()
    {
        onSleep.Invoke();
    }
}
