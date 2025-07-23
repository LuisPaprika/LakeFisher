using System;
using UnityEngine;
public class BoatInteract : InteractableBase
{
    [Header("Target Scene Name")]
    [SerializeField] string sceneName;
    public static event Action<string> onBoatInteract;
    public override void Interact()
    {
        onBoatInteract.Invoke(sceneName);
    }

}
