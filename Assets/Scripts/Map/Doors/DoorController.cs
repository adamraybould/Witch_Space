using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : FocusableObject
{
    [Header("Door Controller Parameters")]
    [SerializeField] private Door door;

    [SerializeField] private bool locked;
    [SerializeField] private int lockCode; //The Code to Unlock the Door
    [SerializeField] private int enteredCode;

    private AudioManager AudioManager;

    private void Awake()
    {
        AudioManager = GetComponent<AudioManager>();
    }

    protected override void Select()
    {
        //If Locked and Has no KeyCode
        if (locked && lockCode == 0)
        {
            AudioManager.PlaySoundEffect("KeyCode_Fail");
        }
        else if (locked) //If just locked and has a KeyCode
        {
            Focus();
        }
        else //No Lock
        {
            if (!door.IsMoving())
            {
                AudioManager.PlaySoundEffect("KeyCode_Access");
                if (!door.IsOpen())
                    door.OpenDoor();
                else if (door.IsOpen() && !door.IsMoving())
                    door.CloseDoor();
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (locked && focused)
        {
            //Check the player input
            KeyCode alpha = KeyCode.Alpha0;
            for (int i = 0; i <= 9; i++)
            {           
                if (alpha <= KeyCode.Alpha9)
                {
                    if (Input.GetKeyDown(alpha))
                    {
                        NumberPressed(i); //Passes along the pressed number
                    }
                }
                alpha++;
            }
        }
    }

    private void NumberPressed(int number)
    {
        AudioManager.PlaySoundEffect("KeyCode_Enter");

        string enteredCodeS = enteredCode.ToString();
        enteredCodeS += number.ToString();
        enteredCode = int.Parse(enteredCodeS);

        //Check if the code matches
        if (enteredCode == lockCode)
        {
            Unlock();
            door.OpenDoor();
            UnFocus();
            AudioManager.PlaySoundEffect("KeyCode_Access");
        }
        else if (enteredCode != lockCode && enteredCodeS.Length == 4) //If the Code isn't the same and the length is at max
        {
            enteredCode = 0;
            AudioManager.PlaySoundEffect("KeyCode_Fail");
        }
    }

    public bool IsLocked() { return locked; }
    public void Lock() { locked = true; }
    public void Unlock() { locked = false; }
}
