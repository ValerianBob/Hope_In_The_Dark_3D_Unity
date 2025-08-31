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
