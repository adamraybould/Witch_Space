using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusableObject : SelectableObject
{
    [Header("Focus Parameters")]
    [SerializeField] private Transform cameraOffset; //The offset from the object where the players can will be placed when focused
    [SerializeField] protected bool focused;

    private PlayerManager PlayerManager;
    private Transform playerCamera;

    protected override void Start()
    {
        base.Start();

        if (cameraOffset == null)
            Debug.LogError("Camera Offset not assigned");

        PlayerManager = player.GetComponent<PlayerManager>();
        playerCamera = player.Find("Player Camera");
    }

    protected override void Update()
    {
        base.Update();

        if (focused)
        {
            //Unfocuses the camera if escape is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnFocus();
            }
        }
    }

    protected override void Select()
    {
        base.Select();

        Focus();
    }

    public virtual void Focus()
    {
        focused = true;

        //Disables player movement and moves the player camera
        playerCamera.position = cameraOffset.position;
        playerCamera.rotation = cameraOffset.rotation;
        PlayerManager.TakeControl();

        DisableSelection(); //Disables the highlight on the Object
    }

    public virtual void UnFocus()
    {
        focused = false;

        PlayerManager.GiveControl();
        EnableSelection(); //Enables the highlight of the Object
    }
}
