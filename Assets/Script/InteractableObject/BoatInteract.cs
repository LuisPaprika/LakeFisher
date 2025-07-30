using System;
using UnityEngine;
public class BoatInteract : InteractableBase
{
    [Header("Target Scene Name")]
    [SerializeField] string sceneName;
    [SerializeField] private AudioSource boatPaddleSFX;
    public static event Action<string> onBoatInteract;
    public override void Interact()
    {
        boatPaddleSFX.Play();
        onBoatInteract.Invoke(sceneName);
    }

}
