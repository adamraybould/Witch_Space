using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueManager))]
public class Terminal : FocusableObject
{
    private DialogueManager DialogueManager;

    [Header("Terminal Access")]
    [SerializeField] private bool locked;
    [SerializeField] private DialogueData lockedDialogue;

    private void Awake()
    {
        DialogueManager = GetComponent<DialogueManager>();
    }

    public override void Focus()
    {
        base.Focus();

        DialogueManager.CanUpdate = true;
        DialogueManager.Write();
    }

    public bool Locked { get { return locked; } set { DialogueManager.Clear(); locked = value; } }
}
