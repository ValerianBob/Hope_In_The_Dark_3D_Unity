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

    public bool isReading = false;

    private float footSoundSpeed = 1.3f;

    private bool isGrounded = false;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isReading)
        {
            GetMouseInput();
            CameraMovement();
            Movement();

            //Gravity
            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);

            if (characterController.isGrounded)
            {
                if (!isGrounded) // just landed
                {
                    SoundsController.Instance.PlayPlayer(2, transform.position);
                }

                isGrounded = true;

                // Small downward force to keep player grounded
                if (velocity.y < 0)
                    velocity.y = -2f;
            }
            else
            {
                isGrounded = false;
            }

            //if (characterController.isGrounded && velocity.y < 0 && !isGrounded)
            //{
            //    velocity.y = -2f;
            //    SoundsController.Instance.PlayPlayer(2, transform.position);
            //    isGrounded = true;
            //}

            //Running();
            movingSpeed = 5f;
            footSoundSpeed = 1.3f;

            if (Keyboard.current.leftShiftKey.isPressed && characterController.isGrounded)
            {
                movingSpeed = 7.5f;
                footSoundSpeed = 1.6f;
            }

            //Jump
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (characterController.isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                    isGrounded = false;

                    SoundsController.Instance.PlayPlayer(1, transform.position);
                }
            }
        }

        HandleFootsteps(footSoundSpeed);
    }

    private void GetMouseInput()
    {
        mouseDelta = Mouse.current.delta.ReadValue();

        rotation.x += mouseDelta.x * sensitivity;
        rotation.y -= mouseDelta.y * sensitivity;

        rotation.y = Mathf.Clamp(rotation.y, -80f, 80f);
    }

    private void CameraMovement()
    {
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

    public void ApplyRecoil(float recoilVertical, float recoilHorizontal)
    {
        rotation.y -= recoilVertical;

        if (Random.Range(0, 2) == 0)
        {
            rotation.x += recoilHorizontal;
        }
        else
        {
            rotation.x -= recoilHorizontal;
        }
    }

    private void HandleFootsteps(float FootSoundSpeed)
    {
        bool isMoving = (horizontal != 0 || vertical != 0);

        if (characterController.isGrounded && isMoving)
        {
            SoundsController.Instance.PlayFootstep(0, true, FootSoundSpeed);
        }
        else
        {
            SoundsController.Instance.StopFootstep();
        }
    }
}
