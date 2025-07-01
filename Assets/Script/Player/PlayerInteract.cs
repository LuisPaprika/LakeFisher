using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float InteractRange = 2.5f;
    private Camera playerCamera;
    void Awake()
    {
        playerCamera = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, InteractRange))
        {
            if (hit.collider.TryGetComponent<InteractableBase>(out InteractableBase interactObj))
            {
                interactObj.Hovered();

                if (PlayerControl.inputActions.Player.Interact.WasPerformedThisFrame())
                {
                    interactObj.Interact();
                }
            }

        }
    }

}

