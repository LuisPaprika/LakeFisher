using System;
using UnityEngine;

public class BedInteract : InteractableBase
{
    [SerializeField] private AudioSource bedSFX;
    public static event Action onSleep;
    public override void Interact()
    {
        onSleep.Invoke();
    }

    void Awake()
    {
        DayController.onSleep += playSound;
    }

    private void playSound()
    {
        bedSFX.Play();
    }

    void OnDestroy()
    {
        DayController.onSleep -= playSound;
    }
}
