using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LootingController : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    private float textFadeDuration = 0.3f;

    private float alpha = 0f;
    private bool isVisible = false;

    public Camera Camera;

    private InventoryController inventory;
    private CharacterMovement characterMovement;

    public float lootingRange;

    public LayerMask hitMask;

    [Header("Looting Text")]
    public TextMeshProUGUI ItemInfo;
    public TextMeshProUGUI ActionText;

    [Header("Reading Text Window")]
    public GameObject ReadingWindow;
    public TextMeshProUGUI noteText;
    public Button bEnter;

    private void Start()
    {
        inventory = GetComponent<InventoryController>();
        characterMovement = GetComponent<CharacterMovement>();

        bEnter.onClick.AddListener(StopReadingNote);
    }

    void Update()
    {
        ray = new Ray(Camera.transform.position, Camera.transform.forward);

        if (Physics.Raycast(ray, out hit, lootingRange, hitMask))
        {
            InteractLootingText();
            ShowOrHideText();
        }
        else
        {
            isVisible = false;
            ShowOrHideText();
        }

        Debug.DrawRay(ray.origin, ray.direction * lootingRange, Color.black);
    }

    private void InteractLootingText()
    {
        if (hit.collider.CompareTag("Ammo"))
        {
            isVisible = true;

            LootAmmo();
            ShowOrHideText();
        }
        else if (hit.collider.CompareTag("Gun"))
        {
            isVisible = true;

            LootGun();
            ShowOrHideText();
        }
        else if (hit.collider.CompareTag("Laptop"))
        {
            isVisible = true;

            ReadNote();
            ShowOrHideText();
        }
        else if (hit.collider.CompareTag("ElectroBox"))
        {
            isVisible = true;

            CarryCargo();
            ShowOrHideText();
        }
        else if (hit.collider.CompareTag("Door"))
        {
            isVisible = true;

            EnterDoor();
            ShowOrHideText();
        }
        else if (hit.collider.CompareTag("Virus"))
        {
            isVisible = true;

            TakeVirus();
            ShowOrHideText();
        }
        else if (hit.collider.CompareTag("SosFire"))
        {
            isVisible = true;

            OpenSosFire();
            ShowOrHideText();
        }
        else if (hit.collider.CompareTag("Helicopter"))
        {
            isVisible = true;

            Escape();
            ShowOrHideText();
        }
        else
        {
            isVisible = false;
        }
    }

    private void LootAmmo()
    {
        ItemInfo.text = hit.collider.gameObject.GetComponent<AmmoBoxController>().ammoInfo.ToString();
        ActionText.text = hit.collider.gameObject.GetComponent<AmmoBoxController>().LootInfo.ToString();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (hit.collider.gameObject.GetComponent<AmmoBoxController>().ammoInfo == "7.62 ammo")
            {
                inventory.Ammo7_62 += hit.collider.gameObject.GetComponent<AmmoBoxController>().ammoAmount;
            }
            else if (hit.collider.gameObject.GetComponent<AmmoBoxController>().ammoInfo == "9mm Ammo")
            {
                inventory.Ammo9mm += hit.collider.gameObject.GetComponent<AmmoBoxController>().ammoAmount;
            }
            else if (hit.collider.gameObject.GetComponent<AmmoBoxController>().ammoInfo == "ShotGun Ammo")
            {
                inventory.AmmoShotGun += hit.collider.gameObject.GetComponent<AmmoBoxController>().ammoAmount;
            }
            hit.collider.gameObject.GetComponent<AmmoBoxController>().TakeAmmo();

            SoundsController.Instance.PlayLooting(0, transform.position);
        }
    }

    private void LootGun()
    {
        ItemInfo.text = hit.collider.gameObject.GetComponent<LootGunController>().GunInfo.ToString();
        ActionText.text = hit.collider.gameObject.GetComponent<LootGunController>().LootInfo.ToString();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (inventory.MainGun == null && hit.collider.gameObject.GetComponent<LootGunController>().isMainGun)
            {
                inventory.MainGun = hit.collider.gameObject.GetComponent<LootGunController>().gunObject;

                inventory.MainGunObject = hit.collider.gameObject;
                inventory.MainGunIcon = hit.collider.gameObject.GetComponent<LootGunController>().gunIcon;

                hit.collider.gameObject.GetComponent<LootGunController>().gunIcon.gameObject.SetActive(true);
                hit.collider.gameObject.GetComponent<LootGunController>().TakeGun();

                SoundsController.Instance.PlayGunTake(0, transform.position);
            }

            if (inventory.Pistol == null && !hit.collider.gameObject.GetComponent<LootGunController>().isMainGun)
            {
                inventory.Pistol = hit.collider.gameObject.GetComponent<LootGunController>().gunObject;

                inventory.PistolObject = hit.collider.gameObject;
                inventory.PistolIcon = hit.collider.gameObject.GetComponent<LootGunController>().gunIcon;

                hit.collider.gameObject.GetComponent<LootGunController>().gunIcon.gameObject.SetActive(true);
                hit.collider.gameObject.GetComponent<LootGunController>().TakeGun();

                SoundsController.Instance.PlayGunTake(0, transform.position);
            }
        }
    }

    private void ReadNote()
    {
        ItemInfo.text = hit.collider.gameObject.GetComponent<LaptopController>().LaptopInfo.ToString();
        ActionText.text = hit.collider.gameObject.GetComponent<LaptopController>().LootInfo.ToString();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            characterMovement.isReading = true;
            ReadingWindow.SetActive(true);

            noteText.text = hit.collider.gameObject.GetComponent<LaptopController>().NoteText;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            SoundsController.Instance.PlayLooting(1, transform.position);
        }
    }

    private void CarryCargo()
    {
        ItemInfo.text = hit.collider.gameObject.GetComponent<CarryAndDropCargo>().info.ToString();
        ActionText.text = hit.collider.gameObject.GetComponent<CarryAndDropCargo>().PressInfo.ToString();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            hit.collider.gameObject.GetComponent<CarryAndDropCargo>().CarryCargo();
        }
    }

    private void EnterDoor()
    {
        ItemInfo.text = hit.collider.gameObject.GetComponent<EnterDoor>().DoorInfo.ToString();
        ActionText.text = hit.collider.gameObject.GetComponent<EnterDoor>().PressInfo.ToString();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            transform.position = hit.collider.gameObject.GetComponent<EnterDoor>().PlaceToMove;
            gameObject.GetComponent<CharacterController>().enabled = true;

            SoundsController.Instance.PlayEnvironment(4, transform.position);
        }
    }

    private void TakeVirus()
    {
        ItemInfo.text = hit.collider.gameObject.GetComponent<VirusTake>().VirusInfo.ToString();
        ActionText.text = hit.collider.gameObject.GetComponent<VirusTake>().PressInfo.ToString();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Destroy(hit.collider.gameObject);

            SoundsController.Instance.PlayEnvironment(5, transform.position);
        }
    }


    private void OpenSosFire()
    {
        ItemInfo.text = hit.collider.gameObject.GetComponent<SosFlireEvent>().FlareInfo.ToString();
        ActionText.text = hit.collider.gameObject.GetComponent<SosFlireEvent>().PressInfo.ToString();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            hit.collider.gameObject.GetComponent<SosFlireEvent>().StartEvacuation();

            SoundsController.Instance.PlayEnvironment(6, transform.position);
        }
    }

    private void Escape()
    {
        ItemInfo.text = hit.collider.gameObject.GetComponent<Helicopter>().Info.ToString();
        ActionText.text = hit.collider.gameObject.GetComponent<Helicopter>().LootInfo.ToString();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            hit.collider.gameObject.GetComponent<Helicopter>().EndGame();
        }
    }

    private void StopReadingNote()
    {
        characterMovement.isReading = false;
        ReadingWindow.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ShowOrHideText()
    {
        if (isVisible && alpha < 1f)
        {
            alpha += Time.deltaTime / textFadeDuration;
        }
        else if (!isVisible && alpha > 0f)
        {
            alpha -= Time.deltaTime / textFadeDuration;
        }

        alpha = Mathf.Clamp01(alpha);

        Color ItemInfoColor = ItemInfo.color;
        Color ActionTextColor = ActionText.color;

        ItemInfo.color = new Color(ItemInfoColor.r, ItemInfoColor.g, ItemInfoColor.b, alpha);
        ActionText.color = new Color(ActionTextColor.r, ActionTextColor.g, ActionTextColor.b, alpha);
    }
}
