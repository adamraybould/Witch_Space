using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.050f, 0, Space.World);
        transform.Translate(0, 0, 0.40f, Space.World);
    }
}
