using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
struct Dialogue
{
    [SerializeField] private DialogueData dialogue;
    [SerializeField] private UnityEvent finishEvent;
    [SerializeField] private float finishDelay;

    public DialogueData GetDialogue() { return dialogue; }
    public UnityEvent GetFinishEvent() { return finishEvent; }
    public float GetFinishDelay() { return finishDelay; }
}

[RequireComponent(typeof(TextDisplay))]
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<Dialogue> dialogueList; //A List of Story Data that is used within the specific order
    [SerializeField] private int dialogueIndex = 0; //Index for which Dialogue we are on
    private Dialogue currentDialogue;

    private TextDisplay _output;
    private BeatData _currentBeat;

    private bool dialogueFinished;

    [Header("Text Display")]
    [SerializeField] private bool autoDisplay; //Should the text automatically display
    [SerializeField] private bool canUpdate; //Should the Dialogue Manager Update. Only True if currently in focus

    private void Awake()
    {
        _output = GetComponent<TextDisplay>();
        _currentBeat = null;
    }

    private void Start()
    {
        if (autoDisplay)
        {
            Write();
        }
    }

    private void Update()
    {
        if (canUpdate)
        {
            if (_output.IsBusy)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _output.SpeedUpText();
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    _output.DefaultTextSpeed();
                }
            }

            if (_output.IsIdle)
            {
                if (_currentBeat != null)
                {
                    UpdateInput();
                }
            }

            if (_output.IsIdle && dialogueFinished)
            {
                dialogueList[dialogueIndex].GetFinishEvent().Invoke();
                dialogueFinished = false;
                dialogueIndex++;
            }
        }
    }

    private void UpdateInput()
    {
        KeyCode alpha = KeyCode.Alpha1;
        KeyCode keypad = KeyCode.Keypad1;

        for (int count = 0; count < _currentBeat.Decision.Count; ++count)
        {
            if (alpha <= KeyCode.Alpha9 && keypad <= KeyCode.Keypad9)
            {
                if (Input.GetKeyDown(alpha) || Input.GetKeyDown(keypad))
                {
                    ChoiceData choice = _currentBeat.Decision[count];
                    DisplayBeat(choice.NextID);
                    break;
                }
            }

            ++alpha;
            ++keypad;
        }
    }

    private void DisplayBeat(int id)
    {
        BeatData data = currentDialogue.GetDialogue().GetBeatById(id);
        StartCoroutine(DoDisplay(data, currentDialogue.GetFinishDelay()));
        _currentBeat = data;
    }

    private IEnumerator DoDisplay(BeatData data, float finishDelay)
    {
        _output.Clear();

        while (_output.IsBusy)
        {
            yield return null;
        }

        //If the Dialogue is meant to end after displayed
        if (data.DisplayText.Contains("END DIALOGUE"))
        {         
            //Removes the "END DIALOGUE" part of the text
            string newText = data.DisplayText;
            newText = newText.Replace("END DIALOGUE", "");
            _output.Display(newText);

            yield return new WaitForSeconds(finishDelay);
            dialogueFinished = true;

        }
        else
            _output.Display(data.DisplayText);


        while (_output.IsBusy)
        {
            yield return null;
        }

        for (int count = 0; count < data.Decision.Count; ++count)
        {
            ChoiceData choice = data.Decision[count];
            _output.Display(string.Format("{0}: {1}", (count + 1), choice.DisplayText));

            while (_output.IsBusy)
            {
                yield return null;
            }
        }

        _output.ShowWaitingForInput();
    }

    public void Write()
    {       
        if (_output.IsIdle)
        {
            if (_currentBeat == null)
            {
                //Check If there is any Dialogue
                if (dialogueList.Count != 0)
                {
                    if (dialogueIndex <= dialogueList.Count - 1)
                    {              
                        currentDialogue = dialogueList[dialogueIndex];
                        DisplayBeat(1);
                    }
                }
                else
                {
                    Debug.LogWarning("No Dialogue Inputted");
                    return;
                }
            }
        }
    }

    public void NextDialogue() //Moves onto the Next Dialogue And Writes it
    {
        Clear();
        Write();
    }

    public void Clear()
    {
        _currentBeat = null;
        _output.ResetDisplay();
    }

    public bool CanUpdate { set { canUpdate = value; } }
}
