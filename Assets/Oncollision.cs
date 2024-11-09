using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oncollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Stop movement and disable gravity when hitting the ground
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }
}
