using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += OnPlayerInteract;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Interact.performed -= OnPlayerInteract;
    }

    private void OnPlayerInteract(InputAction.CallbackContext ctx)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100f))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactObj))
            {
                interactObj.Interact();
            }
        }
    }
}
