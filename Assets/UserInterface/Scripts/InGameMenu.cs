using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public GameObject InGameMenuPanel;

    public CharacterMovement cm;

    public Button ContinueButton;
    public Button ExitButton;

    [HideInInspector]
    public bool isMenuOpened = false;

    void Start()
    {
        ContinueButton.onClick.AddListener(Continue);
        ExitButton.onClick.AddListener(Exit);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !cm.isDead)
        {
            isMenuOpened = !isMenuOpened;

            if (isMenuOpened)
            {
                InGameMenuPanel.SetActive(true);
                Time.timeScale = 0f;

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                InGameMenuPanel.SetActive(false);
                Time.timeScale = 1f;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void Continue()
    {
        isMenuOpened = !isMenuOpened;
        InGameMenuPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
