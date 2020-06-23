using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounterText : MonoBehaviour
{
    [SerializeField]
    AmmoStockpile ammoStockpile;

    Text ammoDisplay;


    void Start()
    {
        ammoDisplay = GetComponent<Text>();
    }

    void Update()
    {
        ammoDisplay.text = $"{ammoStockpile.GetCurrentMag(ammoStockpile.ammoType)} | {ammoStockpile.GetStockpile(ammoStockpile.ammoType)}";
    }

}
