using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float InteractRange = 2.5f;
    private Camera playerCamera;
    private InputSystem_Actions inputActions;
    void Awake()
    {
        playerCamera = gameObject.GetComponentInChildren<Camera>();

        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    void OnDestroy()
    {
        inputActions.Disable();
    }

    void Update()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, InteractRange))
        {
            if (hit.collider.TryGetComponent<InteractableBase>(out InteractableBase interactObj))
            {
                interactObj.Hovered();

                if (inputActions.Player.Interact.WasPerformedThisFrame())
                {
                    interactObj.Interact();
                }
            }

        }
    }
}
