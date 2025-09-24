using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
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

    [Header("Gun Damage")]
    public int GunDamage;

    [Header("Sound index")]
    public int soundIndex;

    [Header("Fire Mode")]
    public bool isAutoFire;
    private bool fireMode;

    [Header("FireRate Settings")]
    public float fireRate;

    [Header("Recoil Settings")]
    public float verticalRecoil;
    public float horizontalRecoil;

    [Header("Bullets Settings")]
    public int maxBulletsInMagazine;
    public int currentBulletsInMagasine;
    public int bulletCaliberIndex;

    [Header("Reload Settings")]
    public int reloadTime;

    [Header("Gun Flame Settings")]
    public Light gunLight;

    void Start()
    {
        bulletsInMagazineText.text = maxBulletsInMagazine.ToString();

        if (bulletCaliberIndex == 0)
        {
            bulletsInInventoryText.text = inventory.Ammo9mm.ToString();
        }
        else if (bulletCaliberIndex == 1)
        {
            bulletsInInventoryText.text = inventory.Ammo7_62.ToString();
        }
        else if (bulletCaliberIndex == 2)
        {
            bulletsInInventoryText.text = inventory.AmmoShotGun.ToString();
        }

        currentBulletsInMagasine = maxBulletsInMagazine;
    }

    void Update()
    {
        bulletsInMagazineText.text = currentBulletsInMagasine.ToString();

        if (bulletCaliberIndex == 0)
        {
            bulletsInInventoryText.text = inventory.Ammo9mm.ToString();
        }
        else if (bulletCaliberIndex == 1)
        {
            bulletsInInventoryText.text = inventory.Ammo7_62.ToString();
        }
        else if (bulletCaliberIndex == 2)
        {
            bulletsInInventoryText.text = inventory.AmmoShotGun.ToString();
        }

        if (!isAutoFire)
        {
            fireMode = Mouse.current.leftButton.wasPressedThisFrame;
        }
        else
        {
            fireMode = Mouse.current.leftButton.isPressed;
        }

        if (fireMode && Time.time >= nextFireTime && !isReloading && currentBulletsInMagasine > 0)
        {
            if (bulletCaliberIndex == 2)
            {
                GrapeShot();
            }
            else
            {
                Shot();
                
            }

            Invoke("TurnOfFireFlame", 0.1f);
            nextFireTime = Time.time + fireRate;
        }
        else if (fireMode && Time.time >= nextFireTime && !isReloading && currentBulletsInMagasine == 0)
        {
            SoundsController.Instance.PlayGunShot(0, transform.position);

            nextFireTime = Time.time + fireRate;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame && currentBulletsInMagasine != maxBulletsInMagazine && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void TurnOfFireFlame()
    {
        gunLight.gameObject.SetActive(false);
    }

    private void Shot()
    {
        ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (currentBulletsInMagasine != 0)
        {
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);

                if (hit.collider.gameObject.GetComponent<ZombieControler>() != null)
                {
                    hit.collider.gameObject.GetComponent<ZombieControler>().TakeDamage(GunDamage);
                }
            }
            else
            {
                Debug.Log("Hit nothing");
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
            }
        }

        SoundsController.Instance.PlayGunShot(soundIndex, transform.position);
        currentBulletsInMagasine -= 1;
        bulletsInMagazineText.text = currentBulletsInMagasine.ToString();

        gunLight.gameObject.SetActive(true);

        characterMovement.ApplyRecoil(verticalRecoil, horizontalRecoil);
    }

    private void GrapeShot()
    {
        int pellets = 8;
        float spread = 5f;

        for (int i = 0; i < pellets; i++)
        {
            Vector3 direction = Camera.transform.forward;
            direction = Quaternion.Euler(
                Random.Range(-spread, spread),
                Random.Range(-spread, spread),
                0
            ) * direction;

            Vector3 startPos = Camera.transform.position + Camera.transform.forward * 0.1f;

            if (Physics.Raycast(startPos, direction, out RaycastHit hit, 1000f))
            {
                Debug.DrawRay(startPos, direction * hit.distance, Color.green, 2f);

                if (hit.collider.gameObject.CompareTag("Target"))
                {
                    if (hit.collider.gameObject.GetComponent<ZombieControler>() != null)
                    {
                        hit.collider.gameObject.GetComponent<ZombieControler>().TakeDamage(GunDamage);
                    }
                }
            }
            else
            {
                Debug.DrawRay(startPos, direction * 1000f, Color.red, 2f);
            }
        }

        gunLight.gameObject.SetActive(true);

        SoundsController.Instance.PlayGunShot(soundIndex, transform.position);
        currentBulletsInMagasine -= 1;
        bulletsInMagazineText.text = currentBulletsInMagasine.ToString();

        characterMovement.ApplyRecoil(verticalRecoil, horizontalRecoil);
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        int bulletsNeeded = maxBulletsInMagazine - currentBulletsInMagasine;

        if (bulletCaliberIndex == 0)
        {
            if (inventory.Ammo9mm >= bulletsNeeded)
            {
                currentBulletsInMagasine += bulletsNeeded;
                inventory.Ammo9mm -= bulletsNeeded;
            }
            else
            {
                currentBulletsInMagasine += inventory.Ammo9mm;
                inventory.Ammo9mm = 0;
            }
        }
        else if (bulletCaliberIndex == 1)
        {
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
        }
        else if (bulletCaliberIndex == 2)
        {
            if (inventory.AmmoShotGun >= bulletsNeeded)
            {
                currentBulletsInMagasine += bulletsNeeded;
                inventory.AmmoShotGun -= bulletsNeeded;
            }
            else
            {
                currentBulletsInMagasine += inventory.AmmoShotGun;
                inventory.AmmoShotGun = 0;
            }
        }

        //if (inventory.Ammo9mm >= bulletsNeeded)
        //{
        //    currentBulletsInMagasine += bulletsNeeded;
        //    inventory.Ammo9mm -= bulletsNeeded;
        //}
        //else
        //{
        //    currentBulletsInMagasine += inventory.Ammo9mm;
        //    inventory.Ammo9mm = 0;
        //}

        //bulletsInMagazineText.text = currentBulletsInMagasine.ToString();
        //bulletsInInventoryText.text = inventory.Ammo9mm.ToString();

        bulletsInMagazineText.text = currentBulletsInMagasine.ToString();

        if (bulletCaliberIndex == 0)
        {
            bulletsInInventoryText.text = inventory.Ammo9mm.ToString();
        }
        else if (bulletCaliberIndex == 1)
        {
            bulletsInInventoryText.text = inventory.Ammo7_62.ToString();
        }
        else if (bulletCaliberIndex == 2)
        {
            bulletsInInventoryText.text = inventory.AmmoShotGun.ToString();
        }

        isReloading = false;
    }
}

