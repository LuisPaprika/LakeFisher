using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] GameObject UI;
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
