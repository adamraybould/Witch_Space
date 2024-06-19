using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public struct Notification
{
    public string text;
    public int ID;
}

public class NotificationManager : MonoBehaviour
{
    private static NotificationManager _instance;
    public static NotificationManager Instance { get { return _instance; } }

    [SerializeField] private NotificationBox NotificationBox;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    public void DisplayNotification(string text)
    {
        Debug.Log(text);
        GetComponent<TextDisplay>().ResetDisplay();
        GetComponent<TextDisplay>().Display(text);
        NotificationBox.DisplayBox(2.0f);
    }
}
