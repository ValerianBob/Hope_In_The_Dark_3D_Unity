using UnityEngine;
using UnityEngine.SceneManagement;

public class Helicopter : MonoBehaviour
{
    public Canvas PlayerCanvas;
    public Canvas CutSceneCanvas;

    public Camera CutSceneCamera;

    public GameObject player;
    public GameObject Developers;

    public string Info;
    public string LootInfo;

    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed = 5f;

    private bool moving = true;
    private bool isCutScene = false;
    private bool isCutSceneEnding = false;

    void Start()
    {
        transform.position = startPoint;

        SoundsController.Instance.PlayHelicopter(7,true);
    }

    void Update()
    {
        if (moving && !isCutScene)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, endPoint) < 0.1f)
            {
                moving = false;
            }
        }

        if (isCutScene && transform.position.y < 670)
        {
            transform.Translate(Vector3.up * 5f * Time.deltaTime);
        }

        if (transform.position.y >= 670)
        {
            if (!isCutSceneEnding)
            {
                Developers.SetActive(true);

                CutSceneCamera.transform.position = new Vector3(-536.73f, 665.0211f, 495.1391f);
                CutSceneCamera.transform.rotation = Quaternion.Euler(-12.084f, -92.269f, -0f);

                transform.position = new Vector3(-543.469971f, 670f, 488.200012f);

                isCutSceneEnding = true;
            }

            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }

    public void EndGame()
    {
        isCutScene = true;

        PlayerCanvas.gameObject.SetActive(false);
        player.gameObject.SetActive(false);

        CutSceneCamera.gameObject.SetActive(true);
        CutSceneCanvas.gameObject.SetActive(true);

        CutSceneCamera.transform.position = new Vector3(-429.680695f, 643.694824f, 535.759888f);
        CutSceneCamera.transform.rotation = Quaternion.Euler(343.154999f, 193.819092f, -0.000120426696f);

        transform.position = new Vector3(-429.540009f, 645.900024f, 493.200012f);
        transform.rotation = Quaternion.Euler(0f, 270f, 0f);

        Invoke(nameof(LoadMenu), 18f);
    }

    private void LoadMenu()
    {
        Debug.Log("EndGame called!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
