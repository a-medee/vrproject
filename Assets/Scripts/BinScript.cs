using UnityEngine;
using TMPro;
using System.Collections;

public class Bin : MonoBehaviour
{
    public TextMeshProUGUI notificationTextTMP;  // Reference to the UI text component

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Celestial"))
        {
            // Destroy the celestial object
            Destroy(other.gameObject);

            // Show the notification message
            ShowNotification("Object added to the bin!");
        }
    }

    // Function to show notification
    private void ShowNotification(string message)
    {
        notificationTextTMP.text = message;

        // Optionally, hide it after a delay
        StartCoroutine(HideNotificationAfterDelay());
    }

    // Coroutine to hide the notification after a set time
    private IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(3f); // Display for 3 seconds
        notificationTextTMP.text = ""; // Clear the message
    }
}
