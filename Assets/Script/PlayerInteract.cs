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
        inputActions.Player.Attack.performed += ctx => Debug.Log("Attack");
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Interact.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        Debug.Log("Interacted");
    }
}
