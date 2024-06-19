using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : SelectableObject
{
    [Header("Locker Parameters")]
    Animator anim;

    [SerializeField] private bool open;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Select()
    {
        base.Select();

        if (!open)
        {
            anim.SetTrigger("Open");
            open = true;
        }
        else
        {
            anim.SetTrigger("Close");
            open = false;
        }
    }
}
