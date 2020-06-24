using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoStockpile : MonoBehaviour
{
    [System.Serializable]
    public struct AmmoEntry
    {
        // Include name field for editing convenience only - not needed in-game.
#if UNITY_EDITOR
        [HideInInspector]
        public string name;
#endif
        public int Stockpile;
        public int currentMag;
    }

    // Keep a private list of ammo currentMags, so we can enforce capacity rules.
    [SerializeField]
    List<AmmoEntry> _inventory = new List<AmmoEntry>();


    public enum AmmoType
    {
        Light,
        Magnum,
        Assault,
        Heavy,
        Sniper,
        Explosive,
        Shell,
        Energy,
        Grenade,
        None
    }
    public AmmoType ammoType;

    public WeaponSelection weaponSelection;

    private void Start()
    {
        weaponSelection = GetComponent<WeaponSelection>();
    }


    public int SyncCurrentMag(AmmoType type)
    {
        AmmoEntry held = _inventory[(int)type];
        int syncAmount = weaponSelection.weaponSlots[weaponSelection.selectedSlot].gameObject.GetComponentInChildren<GunScript>().currentMag;
        held.currentMag = syncAmount;
        _inventory[(int)type] = held;
        return syncAmount;
    }

    // Since our enum is "really" an integer, we can use it
    // as an index to jump straight to the entry we want.
    public int GetCurrentMag(AmmoType type)
    {
        return _inventory[(int)type].currentMag;
    }

    public int GetStockpile(AmmoType type)
    {
        return _inventory[(int)type].Stockpile;
    }

    // Returns amount collected, so you can choose to not consume
    // pickups if you're already full (ie. return value is zero).
    public int Collect(AmmoType type, int amount)
    {
        AmmoEntry held = _inventory[(int)type];
        int collect = Mathf.Min(amount, held.Stockpile - held.currentMag);
        held.currentMag += collect;
        _inventory[(int)type] = held;
        return collect;
    }

    // Returns the amount actually spent, in case firing a full round
    // would drop us below 0 ammo, you can scale down the last shot.
    // You could also implement a TrySpend that aborts for insufficient ammo.
    public int Spend(AmmoType type, int amount)
    {
        AmmoEntry held = _inventory[(int)type];
        int spend = Mathf.Min(amount, held.currentMag);
        held.currentMag -= spend;
        _inventory[(int)type] = held;
        return spend;
    }

    public int Reload(AmmoType type, int amount)
    {
        AmmoEntry held = _inventory[(int)type];
        int reloadAmount = Mathf.Min(amount, held.Stockpile);
        held.Stockpile -= reloadAmount;
        held.currentMag += reloadAmount;
        _inventory[(int)type] = held;
        return reloadAmount;
    }

    // Ensure our inventory list always matches the enum in the event of code changes.
    // You could also use a custom editor to maintain this more efficiently.
#if UNITY_EDITOR
    void Reset() { OnValidate(); }
    void OnValidate()
    {
        var ammoNames = System.Enum.GetNames(typeof(AmmoType));
        var inventory = new List<AmmoEntry>(ammoNames.Length);
        for (int i = 0; i < ammoNames.Length; i++)
        {
            var existing = _inventory.Find(
                (entry) => { return entry.name == ammoNames[i]; });
            existing.name = ammoNames[i];
            existing.currentMag = Mathf.Min(existing.currentMag, existing.Stockpile);
            inventory.Add(existing);
        }
        _inventory = inventory;
    }
#endif

}
