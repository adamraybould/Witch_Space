using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Notifcation", menuName = "Notification")]
public class NotificationData : ScriptableObject
{
    [SerializeField] private string text; //Text for the Nofitication
    [SerializeField] private Vector3 objectivePosition; //Position to the Objective that the Notification is referring too

    public string GetText() { return text; }
    public Vector3 GetObjectivePosition() { return objectivePosition; }
}
