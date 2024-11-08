using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] celestials;
    GameObject centralObject;
    float timer = 0f;
    bool isFalling = false;

    void Start()
    {
        // Find the central object with the "StillAndNotFall" tag
        centralObject = GameObject.FindGameObjectWithTag("DoNot");

        // Find all celestial objects with the "Celestial" tag
        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        SetInitialVelocity();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Check if 20 seconds have passed
        if (timer >= 20f && !isFalling)
        {
            StartFalling();
            isFalling = true;
        }
    }

    void FixedUpdate()
    {
        if (!isFalling)
        {
            ApplyGravityToCelestials();
        }
    }

    void SetInitialVelocity()
    {
        // Apply initial velocity to make celestial objects revolve around the central object
        foreach (GameObject a in celestials)
        {
            float m2 = centralObject.GetComponent<Rigidbody>().mass;
            float r = Vector3.Distance(a.transform.position, centralObject.transform.position);

            a.transform.LookAt(centralObject.transform);
            a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
        }
    }

    void ApplyGravityToCelestials()
    {
        // Apply gravity from the central object to each celestial object
        foreach (GameObject a in celestials)
        {
            float m1 = a.GetComponent<Rigidbody>().mass;
            float m2 = centralObject.GetComponent<Rigidbody>().mass;
            float r = Vector3.Distance(a.transform.position, centralObject.transform.position);

            // Calculate the gravitational force towards the central object
            Vector3 forceDirection = (centralObject.transform.position - a.transform.position).normalized;
            Vector3 gravityForce = forceDirection * (G * (m1 * m2) / (r * r));

            a.GetComponent<Rigidbody>().AddForce(gravityForce);
        }
    }

    void StartFalling()
    {
        // Stop the custom gravitational force and enable downward fall
        foreach (GameObject a in celestials)
        {
            Rigidbody rb = a.GetComponent<Rigidbody>();

            // Reset velocity and enable Unity's gravity
            rb.velocity = Vector3.zero;
            rb.useGravity = true;
        }
    }
}
