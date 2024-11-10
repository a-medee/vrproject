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
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
        {
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
            notification?.ShowNotification("Un objet vient de tomber!");
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        rb.isKinematic = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
