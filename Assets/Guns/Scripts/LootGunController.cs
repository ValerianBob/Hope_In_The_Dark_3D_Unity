using UnityEngine;
using UnityEngine.UI;

public class LootGunController : MonoBehaviour
{
    public GameObject gunObject;

    public string GunInfo;
    public string LootInfo;

    public RawImage gunIcon;

    public bool isMainGun;

    public void TakeGun()
    {
        gameObject.SetActive(false);
    }
}
