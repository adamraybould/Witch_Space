using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : SelectableObject
{
    [Header("KeyCard")]
    [SerializeField] private DoorController doorController; //The Door to Unlock when picked up

    protected override void Select()
    {
        doorController.Unlock();
        Destroy(gameObject);
    }
}
