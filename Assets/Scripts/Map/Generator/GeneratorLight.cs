using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLight : MonoBehaviour
{
    [SerializeField] private bool hit; //If the Light been selected within the Generator minigame

    public void Hit()
    {
        hit = true;
        enabled = true;
    }

    //This will either switch the light off or on depending on it's current state. Used for flashing the lights
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public bool IsHit { get { return hit; } }
}
