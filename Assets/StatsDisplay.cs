using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField]
    HealthScript healthScript;

    Text healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        healthDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = $"{healthScript.health}";
    }
}
