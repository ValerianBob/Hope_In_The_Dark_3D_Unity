using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private CharacterMovement _movement;
    private GunController _gunController;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponentInParent<CharacterMovement>();

        _gunController = GetComponent<GunController>();
    }

    private void Update()
    {
        UpdateMovement();
        UpdateReload();
        UpdateShooting();
    }

    private void UpdateMovement()
    {
        _animator.SetBool("isWalking", _movement.isMoving);
        _animator.SetBool("Idle", !_movement.isMoving);
    }

    private void UpdateShooting()
    {
        if (_gunController != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame &&
            !_gunController.isReloading &&
            _gunController.currentBulletsInMagasine > 0)
            {
                _animator.SetTrigger("Fire");
            }
        }
        else
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _animator.SetTrigger("Fire");
            }
        }
        
    }

    private void UpdateReload()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _animator.SetTrigger("Reload");
        }
    }
}

