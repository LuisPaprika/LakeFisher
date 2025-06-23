using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100f))
        {
            if (inputActions.Player.Interact.WasPerformedThisFrame())
            {
                if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
