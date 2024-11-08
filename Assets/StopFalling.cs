using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFalling : MonoBehaviour
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
        // Check if the celestial object has hit the ground
        if (collision.gameObject.CompareTag("Sol"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Stop all movement and disable gravity to stop falling
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }
}
