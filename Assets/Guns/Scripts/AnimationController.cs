using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetTrigger("Idle");

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _animator.SetTrigger("Fire");
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            _animator.SetTrigger("CheckSkin");
        }
    }
}
