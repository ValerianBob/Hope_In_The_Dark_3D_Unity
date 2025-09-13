using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    public GameObject MainGun;
    [HideInInspector] 
    public GameObject Pistol;
    
    public GameObject Knife;

    [HideInInspector]
    public GameObject MainGunObject;
    [HideInInspector]
    public GameObject PistolObject;

    public int Ammo7_62 = 90;
    public int Ammo9mm = 30;
    public int AmmoShotGun = 25;

    public Image[] gunSlots;

    [HideInInspector]
    public RawImage MainGunIcon;
    [HideInInspector]
    public RawImage PistolIcon;

    private Color32 slotColor = new Color32(0, 0, 0, 165);
    private Color32 selectedColor = new Color32(150, 150, 150, 165);

    public TextMeshProUGUI dropGunText;

    public GameObject DropPoint;

    private void Start()
    {
        ChangeCurrentGunSlotUI(0);
        dropGunText.gameObject.SetActive(false);
    }

    void Update()
    {
        ChangeGun();

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (MainGun != null && MainGun.activeInHierarchy)
            {
                // Set Main Gun null
                MainGun.SetActive(false);
                MainGun = null;
                MainGunIcon.gameObject.SetActive(false);
                MainGunIcon = null;

                // Show knife
                ChangeCurrentGunSlotUI(0);
                Knife.SetActive(true);
                dropGunText.gameObject.SetActive(false);

                // Drop Gun
                GameObject gun = Instantiate(MainGunObject, DropPoint.transform.position, MainGunObject.transform.rotation);
                gun.SetActive(true);

                MainGunObject = null;
            }
            else if (Pistol != null && Pistol.activeInHierarchy)
            {
                // Set Main Gun null
                Pistol.SetActive(false);
                Pistol = null;
                PistolIcon.gameObject.SetActive(false);
                PistolIcon = null;

                // Show knife
                ChangeCurrentGunSlotUI(0);
                Knife.SetActive(true);
                dropGunText.gameObject.SetActive(false);

                // Drop Gun
                GameObject gun = Instantiate(PistolObject, DropPoint.transform.position, PistolObject.transform.rotation);
                gun.SetActive(true);

                PistolObject = null;
            }
            else
            {
                Debug.Log("I am holding Knife");
            }
        }
    }

    private void ChangeGun()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame && MainGun != null)
        {
            MainGun.SetActive(true);

            if (Pistol != null)
            {
                Pistol.SetActive(false);
            }

            Knife.SetActive(false);

            ChangeCurrentGunSlotUI(2);
            dropGunText.gameObject.SetActive(true);

            SoundsController.Instance.PlayGunTake(1, transform.position);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame && Pistol != null)
        {
            if (MainGun != null)
            {
                MainGun.SetActive(false);
            }

            Pistol.SetActive(true);

            Knife.SetActive(false);

            ChangeCurrentGunSlotUI(1);
            dropGunText.gameObject.SetActive(true);

            SoundsController.Instance.PlayGunTake(1, transform.position);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            if (MainGun != null)
            {
                MainGun.SetActive(false);
            }

            if (Pistol != null)
            {
                Pistol.SetActive(false);
            }

            Knife.SetActive(true);

            ChangeCurrentGunSlotUI(0);
            dropGunText.gameObject.SetActive(false);

            SoundsController.Instance.PlayGunTake(1, transform.position);
        }
    }

    private void ChangeCurrentGunSlotUI(int index)
    {
        for(int i = 0; i < gunSlots.Length; i++)
        {
            if (i == index)
            {
                gunSlots[index].color = selectedColor;
            }
            else
            {
                gunSlots[i].color = slotColor;
            }
        }
    }
}
