using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{

    AmmoStockpile ammoStockpile;

    public GameObject[] weaponSlots;

    public int selectedSlot;

    // Start is called before the first frame update
    void Start()
    {
        ammoStockpile = GetComponent<AmmoStockpile>();
        selectedSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {


        ChangeWeapon();

        weaponSlots[selectedSlot].gameObject.GetComponentInChildren<GunScript>().GetAmmoType();

        if (selectedSlot == 0)
        {
            weaponSlots[0].SetActive(true);
            weaponSlots[1].SetActive(false);
            weaponSlots[2].SetActive(false);
            weaponSlots[3].SetActive(false);
        }

        if (selectedSlot == 1)
        {
            weaponSlots[0].SetActive(false);
            weaponSlots[1].SetActive(true);
            weaponSlots[2].SetActive(false);
            weaponSlots[3].SetActive(false);
        }

        if (selectedSlot == 2)
        {
            weaponSlots[0].SetActive(false);
            weaponSlots[1].SetActive(false);
            weaponSlots[2].SetActive(true);
            weaponSlots[3].SetActive(false);
        }

        if (selectedSlot == 3)
        {
            weaponSlots[0].SetActive(false);
            weaponSlots[1].SetActive(false);
            weaponSlots[2].SetActive(false);
            weaponSlots[3].SetActive(true);
        }
    }

    void ChangeWeapon()
    {

        ammoStockpile.SyncCurrentMag(ammoStockpile.ammoType);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSlot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSlot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedSlot = 3;
        }
    }
}
