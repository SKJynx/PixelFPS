using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCurrentWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject weaponHolder;
    Image displayedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        displayedWeapon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        displayedWeapon.sprite = weaponHolder.GetComponentInChildren<GunScript>().m_scriptableWeapon.weaponSprite;
    }
}
