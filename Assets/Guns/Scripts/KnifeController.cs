using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeController : MonoBehaviour
{
    public Camera Camera;

    public CharacterMovement cm;
    public InGameMenu menu;

    private Ray ray;

    private RaycastHit hit;

    private float hitRange = 2f;

    private float nextFireTime = 0f;

    public float fireRate = 0.5f;

    public int Damage;

    public LayerMask hitMask;

    public TextMeshProUGUI BulletsInMagazine;
    public TextMeshProUGUI BulletsInInventory;

    public ParticleSystem Blood;

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
            if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime && !cm.isDead && !menu.isMenuOpened)
            {
                if (hit.collider.gameObject.GetComponent<ZombieControler>() != null)
                {
                    hit.collider.gameObject.GetComponent<ZombieControler>().TakeDamage(Damage);

                    Instantiate(Blood, hit.point, transform.rotation);

                    SoundsController.Instance.PlayGunShot(1, transform.position);
                }
                else
                {
                    SoundsController.Instance.PlayGunShot(1, transform.position);
                }

                nextFireTime = Time.time + fireRate;
            }
        }
        //Debug.DrawRay(ray.origin, ray.direction * hitRange, Color.purple);
    }
}
