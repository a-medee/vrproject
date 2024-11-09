using UnityEngine;

public class CelestialCollisionHandler : MonoBehaviour
{
    public AudioClip collisionSound;
    private AudioSource audioSource;
    private FallenNotification notification; // Reference to the notification script

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Find the notification script on the Canvas in the scene
        notification = FindObjectOfType<FallenNotification>();

        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on celestial object.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Tag ground object as "Ground"
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
            }

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
