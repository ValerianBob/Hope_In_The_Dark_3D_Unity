using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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
            Shot();

            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shot()
    {
        GameObject newBullet = Instantiate(bullet, spawnPosition.transform.position, bullet.transform.rotation);
        newBullet.GetComponent<BulletController>().speed = 7000f * Time.deltaTime;
        newBullet.GetComponent<BulletController>().direction = Camera.transform.forward;

        _audioSource.PlayOneShot(_audioSource.clip);
    }
}

//newBullet.transform.rotation = Quaternion.LookRotation(Camera.transform.forward);
