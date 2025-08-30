using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        _animator.SetTrigger("Idle");

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _animator.SetTrigger("Fire");
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            _animator.SetTrigger("CheckSkin");
        }
    }

}
