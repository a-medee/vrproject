using UnityEngine;
using TMPro;
using System.Collections;

public class FallenNotification : MonoBehaviour
{
	public TextMeshProUGUI notificationTextTMP;
	public float displayTime = 3f;
	private FallenNotification notification;

	private void Start()
	{
		notificationTextTMP.text = "Welcome";
		notification = FindObjectOfType<FallenNotification>();
	}

	public void ShowNotification(string message)
	{
		notificationTextTMP.text = message;
		StartCoroutine(HideNotificationAfterDelay());
	}

	private IEnumerator HideNotificationAfterDelay()
	{
		yield return new WaitForSeconds(displayTime);
		notificationTextTMP.text = "";
	}
}
