using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 100.0f;
    [SerializeField] private Transform body;

    [SerializeField] private float maxRotation = 90.0f;
    [SerializeField] private float xRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxRotation, maxRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        body.Rotate(Vector3.up * mouseX);
    }
}
