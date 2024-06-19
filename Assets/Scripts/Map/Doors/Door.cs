using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Parameters")]
    [SerializeField] private float doorSpeed = 1;
    [SerializeField] private float openPercentage = 0.0f;
    [SerializeField] private bool open = false;
    private bool isMoving; //States if the door is currently opening or closing

    [Header("Door Positions")]
    [SerializeField] private Vector3 closePosition;
    [SerializeField] private Vector3 openPosition;

    [SerializeField] private Transform secondDoor; //Varaible for the Second Door to open if there is one
    [SerializeField] private Vector3 sDoorClosePosition;
    [SerializeField] private Vector3 sDoorOpenPosition;

    private void Start()
    {
        if (open)
        {
            openPosition = transform.localPosition;
            if (secondDoor)
                sDoorOpenPosition = secondDoor.localPosition;
        }
        else
        {
            closePosition = transform.localPosition;
            if (secondDoor)
                sDoorClosePosition = secondDoor.localPosition;
        }
    }

    public void OpenDoor()
    {
        if(!open)
            StartCoroutine(DoorAnimation(true));
    }

    public void CloseDoor()
    {
        if(open)
            StartCoroutine(DoorAnimation(false));
    }

    IEnumerator DoorAnimation(bool shouldOpen)
    {
        if (shouldOpen)
        {
            while (transform.localPosition != openPosition)
            {
                isMoving = true;
                openPercentage += doorSpeed * Time.deltaTime;
                transform.localPosition = Vector3.Lerp(closePosition, openPosition, openPercentage);
                if (secondDoor != null)
                    secondDoor.transform.localPosition = Vector3.Lerp(sDoorClosePosition, sDoorOpenPosition, openPercentage);

                yield return null;
            }

            open = true;
            isMoving = false;
        }
        else
        {
            while (transform.localPosition != closePosition)
            {
                isMoving = true;
                openPercentage -= doorSpeed * Time.deltaTime;
                transform.localPosition = Vector3.Lerp(closePosition, openPosition, openPercentage);
                if (secondDoor != null)
                    secondDoor.transform.localPosition = Vector3.Lerp(sDoorClosePosition, sDoorOpenPosition, openPercentage);
                yield return null;
            }

            open = false;
            isMoving = false;
        }
    }

    public bool IsOpen() { return open; }
    public bool IsMoving() { return isMoving; }
    public bool HasSecondDoor() { if (secondDoor != null) { return true; } else { return false; } }
}
