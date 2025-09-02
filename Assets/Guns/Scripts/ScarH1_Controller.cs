using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScarH1_Controller : MonoBehaviour
{
    public Camera Camera;

    private Ray ray;
    private RaycastHit hit;

    public TextMeshProUGUI bulletsInMagazineText;
    public TextMeshProUGUI bulletsInInventoryText;

    private float fireRate = 0.1f;

    private float nextFireTime = 0f;

    public int maxBulletsInMagazine = 20;
    public int currentBulletsInMagasine = 0;
    public int bulletsInInventory = 90;
    public int reloadTime = 2;

    private bool isReloading = false;

    void Start()
    {
        bulletsInMagazineText.text = maxBulletsInMagazine.ToString();
        bulletsInInventoryText.text = bulletsInInventory.ToString();

        currentBulletsInMagasine = maxBulletsInMagazine;
    }

    void Update()
    {
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

                SoundsController.Instance.PlayGunShot(0, transform.position);
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        int bulletsNeeded = maxBulletsInMagazine - currentBulletsInMagasine;

        if (bulletsInInventory >= bulletsNeeded)
        {
            currentBulletsInMagasine += bulletsNeeded;
            bulletsInInventory -= bulletsNeeded;
        }
        else
        {
            currentBulletsInMagasine += bulletsInInventory;
            bulletsInInventory = 0;
        }

        bulletsInMagazineText.text = currentBulletsInMagasine.ToString();
        bulletsInInventoryText.text = bulletsInInventory.ToString();

        isReloading = false;
    }
}
