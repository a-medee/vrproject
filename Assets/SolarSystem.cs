using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f; // Gravitational constant (tuned for Unity physics)
    GameObject[] celestials;  // Array of objects tagged "Celestial"
    GameObject centralObject; // The object tagged "StillAndNotFall" around which the items orbit

    void Start()
    {
        // Find the central object tagged "StillAndNotFall"
        centralObject = GameObject.FindGameObjectWithTag("DoNot");

        // Find all celestial objects tagged "Celestial" (everyday items in this case)
        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        // Set initial velocity to make celestial objects orbit around the central object
        SetInitialVelocity();
        // Start the coroutine to make celestial objects fall sequentially onto the "FlatGround"

	StartCoroutine(SequentialFall());
    }

    void FixedUpdate()
    {
        // Keep celestial objects orbiting the central object if they are not falling
        ApplyGravityToCelestials();
    }

void SetInitialVelocity()
{
    foreach (GameObject a in celestials)
    {
        Rigidbody rb = a.GetComponent<Rigidbody>();

        // Disable gravity initially to avoid falling immediately
        rb.useGravity = false;

        Vector3 directionToCentralObject = (centralObject.transform.position - a.transform.position).normalized;

        float r = Vector3.Distance(a.transform.position, centralObject.transform.position);
        float m2 = centralObject.GetComponent<Rigidbody>().mass;
        float orbitalSpeed = Mathf.Sqrt(G * m2 / r);

        Vector3 orbitalVelocity = Vector3.Cross(directionToCentralObject, Vector3.up).normalized * orbitalSpeed;

        rb.velocity = orbitalVelocity;
    }
}

    void ApplyGravityToCelestials()
    {
        // Apply gravitational force from the central object to each celestial object
        foreach (GameObject a in celestials)
        {
            Rigidbody rb = a.GetComponent<Rigidbody>();
            if (rb.useGravity == false) // Only apply gravity if it's not already falling
            {
                // Mass of the celestial object
                float m1 = a.GetComponent<Rigidbody>().mass;
                // Mass of the central object
                float m2 = centralObject.GetComponent<Rigidbody>().mass;
                // Distance between celestial and central object
                float r = Vector3.Distance(a.transform.position, centralObject.transform.position);

                // Calculate gravitational force direction
                Vector3 forceDirection = (centralObject.transform.position - a.transform.position).normalized;
                // Gravitational force using the formula F = G * (m1 * m2) / r^2
                Vector3 gravityForce = forceDirection * (G * (m1 * m2) / (r * r));

                // Apply the gravitational force
                rb.AddForce(gravityForce);
            }
        }
    }

    IEnumerator SequentialFall()
    {
        // Sequentially make celestial objects fall onto the ground
	    yield return new WaitForSeconds(20f);
        foreach (GameObject celestial in celestials)
        {
            // Make the current celestial object start falling
            StartFalling(celestial);

            // Wait for 20 seconds before making the next object fall
            yield return new WaitForSeconds(20f);
        }
    }

    void StartFalling(GameObject celestial)
    {
        Rigidbody rb = celestial.GetComponent<Rigidbody>();

        // Stop orbital movement by setting velocity to zero
        rb.velocity = Vector3.zero;

        // Enable gravity to make the object fall
        rb.useGravity = true; // This will apply downward force due to gravity
    }


    // This script will be attached to each celestial object (everyday item)
// for handling collision with the flat surface

    void OnCollisionEnter(Collision collision)
    {
        // Check if the celestial object collides with the "FlatGround"
        if (collision.gameObject.CompareTag("Sol"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Stop all movement when it hits the ground
            rb.velocity = Vector3.zero;

            // Disable gravity (it’s now on the ground)
            rb.useGravity = false;
        }
    }

}
