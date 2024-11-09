using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaiCol : MonoBehaviour
{
	public AudioClip collisionSound; 
	private AudioSource audioSource;
	private FallenNotification notification;
	
    void Start()
    {
	// Get the AudioSource component from the central object or ground
        audioSource = GameObject.FindGameObjectWithTag("Sol").GetComponent<AudioSource>();

	notification = FindObjectOfType<FallenNotification>();
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
        if (collision.gameObject.CompareTag("Sol")) // Tag ground object as "Ground"
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Play collision sound
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }

            // Show the notification
            if (notification != null)
            {
                notification.ShowNotification("A celestial object has fallen!");
            }
        }
    }


}
