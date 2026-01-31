using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform playerHead;
    [SerializeField] private Animator animator;
    [SerializeField] private CameraShaker cameraShaker;


    [Header("Movement")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float crouchSpeed = 5f;

    [SerializeField] private float defaultHeight = 1.8f;
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float mouseSensitivity = 0.1f;
    private bool isSprinting;
    private bool isCrouching;
    private string shakeType;
    private bool isMoving;
    private float getSpeed;
    private CharacterController controller;
    private Vector3 velocity;
    private Vector2 moveInput;



    float xRotation = 0f;
    float yRotation = 0f;
    Vector2 lookInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValueAsButton();
    }

    void Update()
    {
        cameraShaker.ShakeCamera(shakeType);
        HandleStates();
        HandleMovement();
    }

    void HandleMovement()
    {
        // Movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * getSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Look
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        playerBody.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    void HandleStates()
    {
        animator.SetBool("isCrouching", isCrouching);
        
        if (moveInput == Vector2.zero)
        {
            shakeType = "Idle";
            return;
        }
        
        if (isSprinting)
        {
            shakeType = "Sprint";
            getSpeed = sprintSpeed;
        }
        else if (isCrouching)
        {
            shakeType = "Walk";
            getSpeed = crouchSpeed;
        }
        else
        {
            shakeType = "Walk";
            getSpeed = speed;
        }
    }
}
