using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private CameraBob cameraBob;

    //Regives control to the player if it was taken away
    public void GiveControl()
    {
        playerMovement.enabled = true;
        playerLook.enabled = true;
        cameraBob.enabled = true;

        Quaternion resetRotation = transform.rotation;
        resetRotation.x = 0.0f;
        resetRotation.z = 0.0f;
        transform.rotation = resetRotation;
    }

    //Takes away all control from the player
    public void TakeControl()
    {
        playerMovement.enabled = false;
        playerLook.enabled = false;
        cameraBob.enabled = false;
    }

    public void TakeMovementControl()
    {
        playerMovement.enabled = false;
    }
}
