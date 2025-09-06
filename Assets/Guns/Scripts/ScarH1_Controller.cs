using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScarH1_Controller : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    private float nextFireTime = 0f;

    private bool isReloading = false;

    public Camera Camera;

    public CharacterMovement characterMovement;

    public InventoryController inventory;

    [Header("Bullets Text Settings")]
    public TextMeshProUGUI bulletsInMagazineText;
    public TextMeshProUGUI bulletsInInventoryText;

    [Header("FireRate Settings")]
    public float fireRate = 0.1f;

    [Header("Recoil Settings")]
    public float verticalRecoil = 1f;
    public float horizontalRecoil = 1f;

    [Header("Bullets Settings")]
    public int maxBulletsInMagazine = 20;
    public int currentBulletsInMagasine = 0;

    [Header("Reload Settings")]
    public int reloadTime = 2;

    void Start()
    {
        bulletsInMagazineText.text = maxBulletsInMagazine.ToString();
        bulletsInInventoryText.text = inventory.Ammo7_62.ToString();

        currentBulletsInMagasine = 20;
    }

    void Update()
    {
        bulletsInMagazineText.text = currentBulletsInMagasine.ToString();
        bulletsInInventoryText.text = inventory.Ammo7_62.ToString();

        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime && !isReloading && currentBulletsInMagasine > 0)
        {
            Shot();

            nextFireTime = Time.time + fireRate;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame && currentBulletsInMagasine != maxBulletsInMagazine && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shot()
    {
        ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (currentBulletsInMagasine != 0)
        {
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                Debug.DrawLine(ray.origin, hit.point, Color.green);

                if (hit.collider.gameObject.GetComponent<TargetController>() != null)
                {
                    hit.collider.gameObject.GetComponent<TargetController>().Death();
                }

                currentBulletsInMagasine -= 1;
                bulletsInMagazineText.text = currentBulletsInMagasine.ToString();

                SoundsController.Instance.PlayGunShot(0, transform.position);
            }
            else
            {
                currentBulletsInMagasine -= 1;
                bulletsInMagazineText.text = currentBulletsInMagasine.ToString();

                Debug.Log("Hit nothing");
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                SoundsController.Instance.PlayGunShot(0, transform.position);
            }
        }
        characterMovement.ApplyRecoil(verticalRecoil, horizontalRecoil);
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        int bulletsNeeded = maxBulletsInMagazine - currentBulletsInMagasine;

        if (inventory.Ammo7_62 >= bulletsNeeded)
        {
            currentBulletsInMagasine += bulletsNeeded;
            inventory.Ammo7_62 -= bulletsNeeded;
        }
        else
        {
            currentBulletsInMagasine += inventory.Ammo7_62;
            inventory.Ammo7_62 = 0;
        }

        bulletsInMagazineText.text = currentBulletsInMagasine.ToString();
        bulletsInInventoryText.text = inventory.Ammo7_62.ToString();

        isReloading = false;
    }
}
