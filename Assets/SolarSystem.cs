using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] celestials;
    GameObject centralObject;

    void Start()
    {
        centralObject = GameObject.FindGameObjectWithTag("DoNot");

        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        SetInitialVelocity();

	StartCoroutine(SequentialFall());
    }

    void FixedUpdate()
    {
	    celestials = System.Array.FindAll(celestials, celestial => celestial != null);

	    ApplyGravityToCelestials();
    }
void SetInitialVelocity()
{
    foreach (GameObject a in celestials)
    {
        Rigidbody rb = a.GetComponent<Rigidbody>();

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
	    foreach (GameObject a in celestials)
        {
		if (a == null) continue;
            Rigidbody rb = a.GetComponent<Rigidbody>();
	    if (rb == null) continue;
            if (rb.useGravity == false)             {
		    float m1 = a.GetComponent<Rigidbody>().mass;
                float m2 = centralObject.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, centralObject.transform.position);

                Vector3 forceDirection = (centralObject.transform.position - a.transform.position).normalized;
                Vector3 gravityForce = forceDirection * (G * (m1 * m2) / (r * r));

		rb.AddForce(gravityForce);
            }
        }
    }

    IEnumerator SequentialFall()
    {
	    yield return new WaitForSeconds(20f);
        foreach (GameObject celestial in celestials)
        {
		StartFalling(celestial);

	    yield return new WaitForSeconds(20f);
        }
    }

    void StartFalling(GameObject celestial)
    {
        Rigidbody rb = celestial.GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;

        rb.useGravity = true;
    }
}
