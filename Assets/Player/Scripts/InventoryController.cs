using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public List<GameObject> gunsInInventory = new List<GameObject>();

    public int Ammo7_62 = 90;
    public int Ammo9mm = 30;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SelectGun(2);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SelectGun(1);
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            SelectGun(0);
        }
    }

    private void SelectGun(int index)
    {
        if (index < 0 || index >= gunsInInventory.Count)
        {
            return;
        }

        gunsInInventory[index].SetActive(true);

        for (int i = 0; i < gunsInInventory.Count; i++)
        {
            if (i == index)
            {
                continue;
            }
            if (gunsInInventory[i] != null)
            {
                gunsInInventory[i].SetActive(false);
            }   
        }
    }
}
