using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationTest : MonoBehaviour
{
    private Animator testAnim;
    void Start()
    {
        testAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            testAnim.SetTrigger("walk");
        }
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            testAnim.SetTrigger("fire");
        }
    }
}
