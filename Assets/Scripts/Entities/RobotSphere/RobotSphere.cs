using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSphere : MonoBehaviour
{
    Animator anim;

    public bool isActivated;

    [SerializeField] public bool shouldProject;
    public bool wasOpen;
    [SerializeField] private Projection projection;

    void Awake()
    {
        anim = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            anim.SetTrigger("Activate");
        } 
    }
}
