using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 1)]
public class ScriptableWeapons : ScriptableObject
{

    public Sprite weaponSprite;

    public string weaponName;

    public float weaponDamage;

    //Accuracy in degrees
    public float weaponAccuracy;

    //100 = 100% crit chance
    public float critChance;

    //In ticks
    public float reloadTime;
    public int maxAmmo;

    public int startingAmmo;

    public int projectileCount;

    //In ticks 60 ticks = 1 sec
    public float fireRate;
    public float criticalMultiplier;

    //Can the weapon fire full auto?
    public bool automatic;

    public enum WeaponTypes
    {
        Pistol,
        Magnum,
        SMG,
        Shotgun,
        Rifle,
        Sniper,
        Heavy,
        Energy,
        RocketLauncher,
        GrenadeLauncher,
        Melee
    }
    public WeaponTypes weaponType;

    public enum ReloadType
    {
        Magazine,
        Single,
        Charging
    }
    public ReloadType reloadType;

}

