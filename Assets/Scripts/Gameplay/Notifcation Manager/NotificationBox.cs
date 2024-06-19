using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationBox : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 EndPosition;

    private float movePercentage;
    [SerializeField] private float moveSpeed;

    private float notificationTimer; //The Timer to keep track of the time a Notification is displayed for

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void DisplayBox(float notificationDelay)
    {
        StartCoroutine(MoveNotificationBox(notificationDelay));
    }

    private IEnumerator MoveNotificationBox(float notificationDelay)
    {
        while (movePercentage < 1.0f)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, EndPosition, movePercentage);
            movePercentage += moveSpeed * Time.deltaTime;
            yield return null;
        }

        while(notificationTimer < notificationDelay)
        {
            notificationTimer += Time.deltaTime;
            yield return null;
        }
        notificationTimer = 0.0f;

        while (movePercentage > 0.0f)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, EndPosition, movePercentage);
            movePercentage -= moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
