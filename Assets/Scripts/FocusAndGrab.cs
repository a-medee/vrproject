using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FocusAndGrab : MonoBehaviour
{
	public Transform cameraTransform;
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
        originalParent = transform.parent;

        transform.SetParent(cameraTransform, true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
            transform.SetParent(originalParent, true);
    }
}
