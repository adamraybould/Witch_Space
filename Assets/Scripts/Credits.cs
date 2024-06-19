using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private bool shouldScroll;
    [SerializeField] private float scrollSpeed;

    private Transform text;
    private Transform cameraOffset;

    private void Awake()
    {
        text = transform.Find("Text");
        cameraOffset = transform.Find("Camera Offset");
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldScroll)
            text.position += Vector3.up * scrollSpeed * Time.deltaTime;
    }

    public void StartCredits()
    {
        GameObject player = GameObject.Find("First Person Character");
        Transform playerCamera = player.transform.Find("Player Camera");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();

        Debug.Log("Started Credits");
        //Disable the Player and Move them to the Credits
        playerManager.TakeControl();
        playerCamera.position = cameraOffset.position;
        playerCamera.rotation = cameraOffset.rotation;
        shouldScroll = true;
    }
}
