using System;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    [Header("Shown prompt when hovered")]
    [SerializeField] string ShownPrompt = "Interact";
    public static event Action<string> OnHover;
    public abstract void Interact();
    public void Hovered()
    {
        OnHover?.Invoke(ShownPrompt);
    }
}
