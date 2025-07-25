using System;
using UnityEngine;

public class PersonInteract : InteractableBase
{
    [Header("Dialogue of this character")]
    [SerializeField] DialogueSO dialogue;
    public static event Action<DialogueSO, string> OnTalk;
    public override void Interact()
    {
        OnTalk?.Invoke(dialogue, "Player");
    }
}
