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
        if (PlayerControl.inputActions.Player.enabled)
        {
            PlayerControl.inputActions.Player.Disable();
        }
        else if (PlayerControl.inputActions.Fishing.enabled)
        {
            PlayerControl.inputActions.Fishing.Disable();
        }
            
        boatPaddleSFX.Play();
        onBoatInteract.Invoke(sceneName);
    }

    void OnDestroy()
    {
        PlayerControl.inputActions.Player.Enable();
    }

}
