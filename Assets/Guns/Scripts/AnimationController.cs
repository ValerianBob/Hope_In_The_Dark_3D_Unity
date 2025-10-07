using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    public CharacterMovement CharacterMovementScript;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!CharacterMovementScript.isMoving)
        {
            _animator.SetTrigger("Idle");
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _animator.SetTrigger("Fire");
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _animator.SetTrigger("Reload");
        }

        if (CharacterMovementScript.isMoving)
        {
            _animator.SetTrigger("Walk");
        }
    }
}
