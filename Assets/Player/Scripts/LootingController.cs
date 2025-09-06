using System.Collections;
using System.Security.Cryptography;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootingController : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    private float textFadeDuration = 0.3f;

    private float alpha = 0f;
    private bool isVisible = false;

    public Camera Camera;

    private InventoryController inventory;

    public float lootingRange;

    public LayerMask hitMask;

    public TextMeshProUGUI ItemInfo;
    public TextMeshProUGUI ActionText;

    private void Start()
    {
        inventory = GetComponent<InventoryController>();
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

        //Debug.DrawRay(ray.origin, ray.direction * lootingRange, Color.yellow);
    }

    private void InteractLootingText()
    {
        if (hit.collider.CompareTag("Ammo"))
        {
            isVisible = true;

            LootAmmo();
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
            hit.collider.gameObject.GetComponent<AmmoBoxController>().TakeAmmo();

            SoundsController.Instance.PlayLooting(0, transform.position);
        }
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
