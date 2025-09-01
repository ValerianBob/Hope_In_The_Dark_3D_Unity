using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;

    public Camera mainCamera;

    //Mouse Look
    private Vector2 mouseDelta;
    private Vector2 rotation = Vector2.zero;

    //Character Move
    private Vector3 moveDir = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private const float gravity = -9.81f;

    private float horizontal = 0f;
    private float vertical = 0f;

    public float sensitivity;
    public float movingSpeed;
    public float jumpHeight;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetMouseInput();
        CameraMovement();
        Movement();

        //Gravity
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        //Running();
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        movingSpeed = 5f;

        if (Keyboard.current.leftShiftKey.isPressed && characterController.isGrounded)
        {
            movingSpeed = 10f;
        }

        //Jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (characterController.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        //Running();
        //Gravity();
        //Jump();
    }

    private void GetMouseInput()
    {
        mouseDelta = Mouse.current.delta.ReadValue();

        rotation.x += mouseDelta.x * sensitivity;
        rotation.y -= mouseDelta.y * sensitivity;
    }

    private void CameraMovement()
    {
        rotation.y = Mathf.Clamp(rotation.y, -80f, 80f);

        mainCamera.transform.localRotation = Quaternion.Euler(rotation.y, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, rotation.x, 0f);
    }

    private void Movement()
    {
        vertical = 0f;
        horizontal = 0f;

        if (Keyboard.current.wKey.isPressed)
        {
            vertical = 1f;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            vertical = -1f;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;
        }

        moveDir = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(moveDir * movingSpeed * Time.deltaTime);
    }
    
    private void Running()
    {
        movingSpeed = 5f;
        
        characterController.Move(velocity * Time.deltaTime);

        if (Keyboard.current.leftShiftKey.isPressed && characterController.isGrounded)
        {
            movingSpeed = 10f;
        }
    }

    private void Jump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (characterController.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
    }

    private void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
