using UnityEngine;

public class AmmoBoxController : MonoBehaviour
{
    public string ammoInfo;
    public string LootInfo;

    public int ammoAmount;

    public void TakeAmmo()
    {
        Destroy(gameObject);
    }
}
