using UnityEngine;
using UnityEngine.InputSystem;

public class ScarH1_Controller : MonoBehaviour
{
    private AudioSource _audioSource;

    public Camera Camera;

    public GameObject spawnPosition;
    public GameObject bullet;

    private float fireRate = 0.1f;

    private float nextFireTime = 0f;

    //bullets
    private const int bulletsInMagasine = 20;

    public int currentBulletsInMagasine = 0;
    public int bulletsInInventory = 90;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            //Shot();
            DrawRayToCenter();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shot()
    {
        Ray camRay = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Step 2: Get a point along that ray (far away)
        Vector3 targetPoint = camRay.GetPoint(1000f); // 1000 units forward

        // Step 3: Make direction from spawn point to that target
        Vector3 dir = (targetPoint - spawnPosition.transform.position).normalized;

        GameObject newBullet = Instantiate(bullet, spawnPosition.transform.position, bullet.transform.rotation);
        newBullet.GetComponent<BulletController>().speed = 5f;
        newBullet.GetComponent<BulletController>().direction = dir;

        _audioSource.PlayOneShot(_audioSource.clip);
    }

    void DrawRayToCenter()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        Ray screenRay = Camera.main.ScreenPointToRay(screenCenter);

        Vector3 targetPoint = screenRay.GetPoint(1000f);

        Vector3 dir = (targetPoint - spawnPosition.transform.position).normalized;

        Debug.DrawRay(spawnPosition.transform.position, dir * 100f, Color.green, 10f);
    }
}
