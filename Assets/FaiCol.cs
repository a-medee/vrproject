using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaiCol : MonoBehaviour
{
	public AudioClip collisionSound; // The sound to play on collision
    private AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
	// Get the AudioSource component from the central object or ground
        audioSource = GameObject.FindGameObjectWithTag("Sol").GetComponent<AudioSource>();

        // Ensure the AudioSource component is not null
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource not found on ground object. Make sure the ground has an AudioSource component.");
        }
        
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

            // Stop all movement when it hits the ground
            rb.velocity = Vector3.zero;

            // Disable gravity (it’s now on the ground)
            rb.useGravity = true;

            // Play the collision sound
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }


}
