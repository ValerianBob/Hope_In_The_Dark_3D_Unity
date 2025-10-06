using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private InGameMenu inGameMenu;

    public Camera MenuCamera;
    public Camera CutSceneCamera;

    public Canvas MenuCanvas;
    public Canvas GameCanvas;
    public Canvas CutSceneCanvas;

    public GameObject Player;
    public GameObject RedLightRoom;
    public GameObject Car;

    public Button StartGameButton;
    public Button ExitGameButton;

    //Settings
    public GameObject Settings;
    public Button settingsButton;
    public Button BackButtonFromSettings;

    public Slider sensitivitySlider;
    public Slider soundSlider;

    public TextMeshProUGUI sensitivityNumberText;
    public TextMeshProUGUI soundNumberText;

    //Developers
    public Button DevelopersButton;
    public GameObject DevelopersNames;
    public Button BackButtonFromDevelopers;

    //Show-Hide menu
    public Image panel;
    private Color color;

    private float fadeSpeed = 6f;
    private float timer = 0f;

    [SerializeField]
    private bool fadingOut = false;
    [SerializeField]
    private bool fadingIn = false;

    private bool isGameStarted = false;

    public bool isManeMenuSettingsOn = true;

    void Start()
    {
        inGameMenu = GetComponent<InGameMenu>();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        StartGameButton.onClick.AddListener(StartGame);
        ExitGameButton.onClick.AddListener(ExitGame);

        settingsButton.onClick.AddListener(ShowSettings);
        BackButtonFromSettings.onClick.AddListener(BackFromSettings);

        DevelopersButton.onClick.AddListener(ShowDevelopers);
        BackButtonFromDevelopers.onClick.AddListener(BackFromDevelopers);
        
        color = panel.color;

        sensitivitySlider.value = PlayerSettings.Instance.PlayerSensitivity;
        soundSlider.value = PlayerSettings.Instance.SoundVolume;

        sensitivityNumberText.text = sensitivitySlider.value.ToString();
        soundNumberText.text = soundSlider.value.ToString();

        ShowMenu();
    }

    void Update()
    {
        ShowOrHidePanel();

        SetSettings();
    }

    public void ShowMenu()
    {
        timer = 0f;
        fadingOut = true;
        fadingIn = false;
    }

    public void HideMenu()
    {
        timer = 0f;
        fadingIn = true;
        fadingOut = false;
    }

    private void ShowOrHidePanel()
    {
        if (fadingOut && timer < fadeSpeed)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeSpeed);
            color.a = alpha;
            panel.color = color;

            if (timer >= fadeSpeed) fadingOut = false;
        }

        if (fadingIn && timer < fadeSpeed)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeSpeed);
            color.a = alpha;
            panel.color = color;

            if (timer >= fadeSpeed) fadingIn = false;
        }
    }

    private void StartGame()
    {
        if (!isGameStarted)
        {
            isManeMenuSettingsOn = false;

            HideMenu();
            SoundsController.Instance.PlayMenu(0, MenuCamera.transform.position);
            Invoke("ShowStartCutScene", 10f);

            isGameStarted = true;
        }
    }

    private void ShowStartCutScene()
    {
        MenuCanvas.gameObject.SetActive(false);
        MenuCamera.gameObject.SetActive(false);

        CutSceneCamera.gameObject.SetActive(true);
        CutSceneCanvas.gameObject.SetActive(true);
        RedLightRoom.gameObject.SetActive(true);

        StartCoroutine("MoveCameraForward");

        Invoke("LoadLevel", 12f);
    }

    private IEnumerator MoveCameraForward()
    {
        float timer = 0f;

        while (timer < 7f)
        {
            CutSceneCamera.transform.Translate(Vector3.forward * 0.1f * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        Car.SetActive(true);
        CutSceneCamera.transform.position = new Vector3(-1.06559849f, 0.9f, -170.164986f);
        CutSceneCamera.transform.rotation = Quaternion.Euler(4.81756783f, 109.54863f, 0.282536924f);
    }

    private void LoadLevel()
    {
        MenuCanvas.gameObject.SetActive(false);
        MenuCamera.gameObject.SetActive(false);
        CutSceneCamera.gameObject.SetActive(false);
        CutSceneCanvas.gameObject.SetActive(false);
        RedLightRoom.gameObject.SetActive(false);

        Player.SetActive(true);
        GameCanvas.gameObject.SetActive(true);
    }
    
    private void ShowSettings()
    {
        Settings.SetActive(true);

        StartGameButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        DevelopersButton.gameObject.SetActive(false);
        ExitGameButton.gameObject.SetActive(false);
    }

    private void BackFromSettings()
    {
        Settings.SetActive(false);

        StartGameButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        DevelopersButton.gameObject.SetActive(true);
        ExitGameButton.gameObject.SetActive(true);
    }

    private void ShowDevelopers()
    {
        DevelopersNames.SetActive(true);

        StartGameButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        DevelopersButton.gameObject.SetActive(false);
        ExitGameButton.gameObject.SetActive(false);
    }

    private void BackFromDevelopers()
    {
        DevelopersNames.SetActive(false);

        StartGameButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        DevelopersButton.gameObject.SetActive(true);
        ExitGameButton.gameObject.SetActive(true);
    }

    private void SetSettings()
    {
        PlayerSettings.Instance.PlayerSensitivity = Mathf.Round(sensitivitySlider.value * 10f) / 10f;
        PlayerSettings.Instance.SoundVolume = Mathf.Round(soundSlider.value * 10f) / 10f;

        sensitivityNumberText.text = (Mathf.Round(sensitivitySlider.value * 10f) / 10f).ToString();
        soundNumberText.text = (Mathf.Round(soundSlider.value * 10f) / 10f).ToString();

        if (isManeMenuSettingsOn)
        {
            inGameMenu.sensitivitySlider.value = sensitivitySlider.value;
            inGameMenu.soundSlider.value = soundSlider.value;

            inGameMenu.sensitivityNumberText.text = (Mathf.Round(sensitivitySlider.value * 10f) / 10f).ToString();
            inGameMenu.soundNumberText.text = (Mathf.Round(soundSlider.value * 10f) / 10f).ToString();
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
