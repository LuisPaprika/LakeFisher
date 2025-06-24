using System;
using UnityEngine;

public class PersonInteract : InteractableBase
{
    [SerializeField] DialogueSO dialogue;
    public static event Action<DialogueSO> OnTalk;
    public override void Interact()
    {
        OnTalk?.Invoke(dialogue);
    }
}
