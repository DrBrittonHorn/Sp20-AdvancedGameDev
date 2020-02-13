using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        //Vector3 center = this.transform.position;
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(Vector3.forward * 1000);   
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(Vector3.forward * -1000);
        }
    }
}
