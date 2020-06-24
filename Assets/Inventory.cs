using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{

    public GameObject[] weaponsList;

    void Start()
    {
        weaponsList = Resources.LoadAll<GameObject>("WeaponPrefabs");

        if (weaponsList != null)
        {
            foreach (var weapon in weaponsList)
            {
                print(weapon.name);
            }
        }
        else
        {
            print("No weapons were found in the Resources/WeaponPrefabs folder.");
        }
    }

}
