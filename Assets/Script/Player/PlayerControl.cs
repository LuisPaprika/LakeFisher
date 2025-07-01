using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform playerCameraTransform;
    public static Transform PlayerCamera;

    [Header("Player Movement")]
    public float moveSpeed = 7.5f;

    [Header("Camera Look")]
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;

    [Header("Physics")]
    public float gravity = -15.0f;

    private Vector3 playerVelocity;
    private bool isGrounded;
    private float rotationX = 0;

    private Vector2 moveInput;
    private Vector2 lookInput;
    public static InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        SetActionMapByName("Player");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        PlayerCamera = playerCameraTransform;
    }

    void Update()
    {
        if (characterController == null || playerCameraTransform == null)
        {
            Debug.LogWarning("Character Controller or Player Camera Transform not assigned.");
            return;
        }


        if (inputActions.Player.enabled)
        {
            HandleGrounded();
            HandleMovement();
            HandleGravity();
            HandleCameraLook();
        }


    }

    public static void SetActionMapByName(string ActionMapName)
    {
        inputActions.Disable();
        var actionMap = inputActions.asset.FindActionMap(ActionMapName);
        actionMap.Enable();
        Debug.Log("Set map to " + ActionMapName);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private void HandleGrounded()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleGravity()
    {
        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleCameraLook()
    {
        float mouseX = lookInput.x * lookSpeed;
        float mouseY = lookInput.y * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}