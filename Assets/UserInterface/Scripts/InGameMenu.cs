using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public GameObject InGameMenuPanel;
    public GameObject Settings;

    public TextMeshProUGUI sensitivityNumberText;
    public TextMeshProUGUI soundNumberText;

    public Slider sensitivitySlider;
    public Slider soundSlider;

    public CharacterMovement cm;

    public Button ContinueButton;
    public Button SettingsButton;
    public Button BackButton;
    public Button ExitButton;

    [HideInInspector]
    public bool isMenuOpened = false;

    void Start()
    {
        sensitivitySlider.value = 0.2f;
        soundSlider.value = 1f;

        sensitivityNumberText.text = sensitivitySlider.value.ToString();
        soundNumberText.text = soundSlider.value.ToString();

        ContinueButton.onClick.AddListener(Continue);
        ExitButton.onClick.AddListener(Exit);

        SettingsButton.onClick.AddListener(ShowSettingsMenu);
        BackButton.onClick.AddListener(BackInGameMenu);
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

        PlayerSettings.Instance.PlayerSensitivity = Mathf.Round(sensitivitySlider.value * 10f) / 10f;
        PlayerSettings.Instance.SoundVolume = Mathf.Round(soundSlider.value * 10f) / 10f; 

        sensitivityNumberText.text = (Mathf.Round(sensitivitySlider.value * 10f) / 10f).ToString();
        soundNumberText.text = (Mathf.Round(soundSlider.value * 10f) / 10f).ToString(); 
    }

    private void Continue()
    {
        isMenuOpened = !isMenuOpened;
        InGameMenuPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ShowSettingsMenu()
    {
        Settings.SetActive(true);

        ContinueButton.gameObject.SetActive(false);
        SettingsButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
    }

    private void BackInGameMenu()
    {
        Settings.SetActive(false);

        ContinueButton.gameObject.SetActive(true);
        SettingsButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
    }

    private void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
