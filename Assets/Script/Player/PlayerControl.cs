using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] float rotationSpeed = 0.25f;

    [Header("Physics")]
    public float gravity = -15.0f;

    private Vector3 playerVelocity;
    private bool isGrounded;
    private float rotationX = 0;

    private Vector2 moveInput;
    private Vector2 lookInput;
    public static bool isFishing = false;
    public static bool castLineAtFish = false;
    public static InputSystem_Actions inputActions;
    [SerializeField] string startActionMap = "Player";

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        SetActionMapByName(startActionMap);

        PersonInteract.OnTalk += Look;
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
        }

        if (!inputActions.Conversation.enabled)
        {
            HandleCameraLook();
        }
    }

    void OnDestroy()
    {
        inputActions.Disable();
    }

    private void Look(DialogueSO dialogue, Vector3 positionVector)
    {
        StartCoroutine(RotateToTarget(positionVector, rotationSpeed));
    }

    private IEnumerator RotateToTarget(Vector3 target, float duration)
    {
        Quaternion startRotation = PlayerCamera.rotation;
        Vector3 direction = target - PlayerCamera.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float startTime = 0f;

        while (startTime < duration)
        {
            startTime += Time.deltaTime;
            float t = Mathf.Clamp01(startTime / duration);
            PlayerCamera.rotation = Quaternion.Lerp(startRotation, targetRotation, t); //Slowly turning camera
            yield return null;
        }

        Vector3 finalEuler = PlayerCamera.localRotation.eulerAngles;
        rotationX = finalEuler.x;
        
    }

    public static void SetActionMapByName(string ActionMapName)
    {
        inputActions.Disable();
        var actionMap = inputActions.asset.FindActionMap(ActionMapName);
        actionMap.Enable();
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