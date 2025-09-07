using UnityEngine;

public class LootGunController : MonoBehaviour
{
    public string GunInfo;

    public bool isMainGun;

    public void TakeGun()
    {
        Destroy(gameObject);
    }
}
