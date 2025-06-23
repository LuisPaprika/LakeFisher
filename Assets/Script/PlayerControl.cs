using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    /*
    ------------------------------------------------------
    ตัวแปรสำหรับปรับแต่งค่าต่างๆ
    -------------------------------------------------------
    */

    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform playerCameraTransform;

    [Header("Player Movement")]
    public float moveSpeed = 7.5f;

    [Header("Camera Look")]
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;

    [Header("Physics")]
    public float gravity = -15.0f;

    /*
    ------------------------------------------------------
    ตัวแปรที่ใช้ภายในสคริปต์
    -------------------------------------------------------
    */

    private Vector3 playerVelocity;
    private bool isGrounded;
    private float rotationX = 0;

    private Vector2 moveInput;
    private Vector2 lookInput;

    /*
    ------------------------------------------------------
    ฟังก์ชันของ Unity ที่ทำงานตอนเริ่มต้น
    -------------------------------------------------------
    */

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (characterController == null || playerCameraTransform == null)
        {
            Debug.LogWarning("Character Controller or Player Camera Transform not assigned.");
            return;
        }

        HandleGrounded();
        HandleMovement();
        HandleGravity();
        HandleCameraLook();
    }
    
    /*
    ------------------------------------------------------
    ฟังก์ชันที่ถูกเรียกโดย PlayerInput (Send Messages)
    -------------------------------------------------------
    */

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    /*
    ------------------------------------------------------
    ฟังก์ชันจัดการการทำงานส่วนต่างๆ (ใช้ค่าจาก Input System)
    -------------------------------------------------------
    */

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