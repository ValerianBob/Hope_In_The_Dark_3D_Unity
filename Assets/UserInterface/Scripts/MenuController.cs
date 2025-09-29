using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Camera MenuCamera;

    public Canvas MenuCanvas;
    public Canvas GameCanvas;
    
    public GameObject Player;

    public Button StartGameButton;
    public Button ExitGameButton;

    //Show-Hide menu
    public Image panel;
    private Color color;

    private float fadeSpeed = 6f;
    private float timer = 0f;

    private bool fadingOut = false;
    private bool fadingIn = false;

    private bool isGameStarted = false;

    void Start()
    {
        StartGameButton.onClick.AddListener(StartGame);
        ExitGameButton.onClick.AddListener(ExitGame);

        color = panel.color;

        ShowMenu();
    }

    void Update()
    {
        ShowOrHidePanel();
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
            HideMenu();
            SoundsController.Instance.PlayMenu(0, MenuCamera.transform.position);
            Invoke("LoadLevel", 10f);

            isGameStarted = true;
        }
    }

    private void LoadLevel()
    {
        Player.SetActive(true);
        GameCanvas.gameObject.SetActive(true);

        MenuCanvas.gameObject.SetActive(false);
        MenuCamera.gameObject.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
