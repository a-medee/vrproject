using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FocusAndGrab : MonoBehaviour
{
    public Transform cameraTransform;  // Assign your main camera here

    private Transform originalParent;

    private void OnEnable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Store the object's original parent so we can restore it on release
        originalParent = transform.parent;

        // Parent the object to the camera
        transform.SetParent(cameraTransform, true); // 'true' keeps the object in its current position relative to the camera
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Restore the object's original parent
        transform.SetParent(originalParent, true);
    }
}
