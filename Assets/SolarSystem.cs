using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
	readonly float G = 100f;
	GameObject[] celestials;
	float time_done = 0;
	// Start is called before the first frame update
	void Start()
	{
	    celestials = GameObject.FindGameObjectsWithTag("Celestial");

	    SetInitialVelocity();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void FixedUpdate()
	{
		Gravity();
	}

	void SetInitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if(!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.transform.LookAt(b.transform);
		    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);

		}
            }
        }
    }

	void Gravity()
	{
		foreach (GameObject a in celestials)
		{
			foreach (GameObject b in celestials)
			{
				if (!a.Equals(b))
				{
					float m1 = a.GetComponent<Rigidbody>().mass;
					float m2 = b.GetComponent<Rigidbody>().mass;
					float r = Vector3.Distance(a.transform.position, b.transform.position);

					a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));
					if ( i == 10)
						
				}
			}
		}
	}


}
