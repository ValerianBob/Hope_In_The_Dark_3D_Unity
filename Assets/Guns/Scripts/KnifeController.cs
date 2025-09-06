using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeController : MonoBehaviour
{
    public Camera Camera;

    private Ray ray;

    private RaycastHit hit;

    private float hitRange = 2f;

    private float nextFireTime = 0f;

    public float fireRate = 0.5f;

    public LayerMask hitMask;

    public TextMeshProUGUI BulletsInMagazine;
    public TextMeshProUGUI BulletsInInventory;

    void Start()
    {
        
    }

    void Update()
    {
        BulletsInMagazine.text = "";
        BulletsInInventory.text = "";

        ray = new Ray(Camera.transform.position, Camera.transform.forward);

        if (Physics.Raycast(ray, out hit, hitRange, hitMask))
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
            {
                if (hit.collider.gameObject.GetComponent<TargetController>() != null)
                {
                    hit.collider.gameObject.GetComponent<TargetController>().Death();

                    SoundsController.Instance.PlayGunShot(1, transform.position);
                }
                else
                {
                    SoundsController.Instance.PlayGunShot(1, transform.position);
                }

                nextFireTime = Time.time + fireRate;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * hitRange, Color.purple);
    }
}
