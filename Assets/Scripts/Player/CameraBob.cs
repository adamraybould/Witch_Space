using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    [Header("Trasnform References")]
    public Transform headTransform;
    public Transform cameraTransform;

    [Header("Head Bobbing")]
    public float bobFrequency = 5.0f;
    public float bobHorizonalAmplitude = 0.1f;
    public float bobVerticalAmplitude = 0.1f;
    [Range(0, 1)] public float headBobSmoothing = 0.1f;

    public bool isWalking;
    private float walkingTime;
    private Vector3 targetCameraPosition;

    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontalOffset = 0;
        float verticalOffset = 0;
        Vector3 offset = Vector3.zero;

        if (t > 0)
        {
            horizontalOffset = Mathf.Cos(t * bobFrequency) * bobHorizonalAmplitude;
            verticalOffset = Mathf.Sin(t * bobFrequency * 2) * bobVerticalAmplitude;

            offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
        }

        return offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWalking)
            walkingTime = 0.0f;
        else
            walkingTime += Time.deltaTime;
        
        //Calculate The Camera's target position
        targetCameraPosition = headTransform.position + CalculateHeadBobOffset(walkingTime);

        //Interpolate Position
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headBobSmoothing);
        if ((cameraTransform.position - targetCameraPosition).magnitude <= 0.001f)
            cameraTransform.position = targetCameraPosition;
    }
}
