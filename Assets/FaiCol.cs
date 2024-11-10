using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FaiCol : MonoBehaviour
{
    public AudioClip collisionSound;
    private AudioSource audioSource;
    private FallenNotification notification;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Sol").GetComponent<AudioSource>();
        notification = FindObjectOfType<FallenNotification>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Ensure AudioSource component is not null
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource not found on ground object. Make sure the ground has an AudioSource component.");
        }

        // Register grab events
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sol")) // Ground object
        {
            // Play collision sound and show notification
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
            notification?.ShowNotification("A celestial object has fallen!");
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Stop physics and make it easier to carry
        rb.isKinematic = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Re-enable physics when dropped
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
