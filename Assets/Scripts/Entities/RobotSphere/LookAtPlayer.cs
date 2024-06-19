using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 positionOffset; //The Offset from the player position at which to look at

    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private bool lookAt;

    // Update is called once per frame
    void Update()
    {
        if (lookAt)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - (transform.position - positionOffset)), rotationSpeed * Time.deltaTime);
        }
    }

    public void EnableLook() { lookAt = true; }
    public void DisableLook() { lookAt = false; }
}
