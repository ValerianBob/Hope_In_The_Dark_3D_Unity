using UnityEngine;
using UnityEngine.UI;

public class LootGunController : MonoBehaviour
{
    public GameObject gunObject;

    public string GunInfo;

    public RawImage gunIcon;

    public bool isMainGun;

    public void TakeGun()
    {
        Destroy(gameObject);
    }
}
