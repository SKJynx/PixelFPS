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


        if (weaponSlots[selectedSlot].GetComponentInChildren<GunScript>().reloadTimer < 0)
        {
            ammoStockpile.SyncCurrentMag(ammoStockpile.ammoType);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ResetWeapon();
                selectedSlot = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ResetWeapon();
                selectedSlot = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ResetWeapon();
                selectedSlot = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ResetWeapon();
                selectedSlot = 3;
            }
        }

    }

    void ResetWeapon()
    {
        weaponSlots[selectedSlot].GetComponentInChildren<Animator>().Play("Gun_Idle", -1, 0);
        weaponSlots[selectedSlot].GetComponentInChildren<GunScript>().isReloading = false;
        weaponSlots[selectedSlot].GetComponentInChildren<GunScript>().ResetMuzzleAnimation();
    }
}
