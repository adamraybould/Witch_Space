using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    private Transform fan;
    [SerializeField] private float fanSpeed = 100.0f;

    private void Awake()
    {
        fan = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        fan.transform.Rotate(0, 0, fanSpeed * Time.deltaTime);
    }
}
