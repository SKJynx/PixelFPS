using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasic : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float maxSpeed;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCamera");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveToTarget = player.transform.position - transform.position;

        rb.velocity = new Vector3(moveToTarget.x, 0, moveToTarget.z) * maxSpeed;

    }
}
