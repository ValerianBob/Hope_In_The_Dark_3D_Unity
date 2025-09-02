using UnityEngine;
using UnityEngine.InputSystem;

public class ScarH1_Controller : MonoBehaviour
{
    public Camera Camera;

    public GameObject spawnPosition;
    public GameObject bullet;

    private Ray ray;
    private RaycastHit hit;
    
    private float fireRate = 0.1f;

    private float nextFireTime = 0f;

    //bullets
    private const int bulletsInMagasine = 20;

    public int currentBulletsInMagasine = 0;
    public int bulletsInInventory = 90;

    void Start()
    {

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
        ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            hit.collider.gameObject.GetComponent<TargetController>().Death();
        }
        else
        {
            Debug.Log("No hit");
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
        }

        SoundsController.Instance.PlayGunShot(0, transform.position);
    }
}
