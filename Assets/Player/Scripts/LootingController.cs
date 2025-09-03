using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootingController : MonoBehaviour
{
    public Camera Camera;

    private Ray ray;
    private RaycastHit hit;

    public float lootingRange;

    public LayerMask hitMask;

    public TextMeshProUGUI ItemInfo;

    void Update()
    {
        ray = new Ray(Camera.transform.position, Camera.transform.forward);

        if (Physics.Raycast(ray, out hit, lootingRange, hitMask))
        {
            Debug.Log("Looting Object : " + hit.collider.name);

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (hit.collider.gameObject.CompareTag("Ammo"))
                {
                    ItemInfo.text = hit.collider.gameObject.GetComponent<AmmoBoxController>().ammoInfo.ToString();
                    hit.collider.gameObject.GetComponent<AmmoBoxController>().TakeAmmo();

                    SoundsController.Instance.PlayLooting(0,transform.position);
                }
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * lootingRange, Color.yellow);
    }
}
