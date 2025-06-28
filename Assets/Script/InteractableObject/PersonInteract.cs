using System;
using UnityEngine;

public class PersonInteract : InteractableBase
{
    [Header("Dialogue of this character")]
    [SerializeField] DialogueSO dialogue;
    [Header("Speed when camera turn to face this character")]
    [SerializeField] float smoothSpeed = 5f;
    public static event Action<DialogueSO> OnTalk;
    public override void Interact()
    {
        OnTalk?.Invoke(dialogue);
    }

    void Update()
    {
        Vector3 direction = gameObject.transform.position - PlayerControl.PlayerCamera.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        PlayerControl.PlayerCamera.rotation = Quaternion.Lerp(PlayerControl.PlayerCamera.rotation, targetRotation, Time.deltaTime * smoothSpeed); //Slowly turning camera
    }
}
