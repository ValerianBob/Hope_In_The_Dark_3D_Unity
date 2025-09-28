using System.Collections;
using TMPro;
using UnityEngine;

public class SosFlireEvent : MonoBehaviour
{
    public GameObject ZombiePref;
    public TextMeshProUGUI timerText;
    public GameObject HelicopterToEscape;

    public string FlareInfo;
    public string PressInfo;

    private ParticleSystem ps;
    private Light Light;

    private Vector3[] spawnPoint =
    {
        new Vector3(-270.99f, 638.93f, 501.17f),
        new Vector3(-270.99f, 638.93f, 535.6f),
        new Vector3(-270.99f, 638.93f, 446.4f),
    };

    private float timeRemaining = 120f;

    private bool timerRunning = false;

    private void Start()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        Light = transform.GetChild(1).GetComponent<Light>();
    }

    public void StartEvacuation()
    {
        ps.gameObject.SetActive(true);
        Light.gameObject.SetActive(true);

        HelicopterToEscape.gameObject.SetActive(true);

        timerRunning = true;

        timerText.gameObject.SetActive(true);

        StartCoroutine("SpawnZombie");
    }

    private void Update()
    {
        StartTimer();
    }

    private IEnumerator SpawnZombie()
    {
        while (true)
        {
            GameObject zombie = Instantiate(ZombiePref, spawnPoint[Random.Range(0, 3)], ZombiePref.transform.rotation);
            zombie.gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);
        }
    }

    private void StartTimer()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                timerText.text = "00:00";
                Debug.Log("Timer finished!");
            }
        }
    }
}
