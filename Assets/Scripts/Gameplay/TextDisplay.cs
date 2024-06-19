using System.Collections;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public enum State { Initialising, Idle, Busy }

    [SerializeField] private TMP_Text _displayText;
    private string _displayString;
    private WaitForSeconds _quickWait;
    private WaitForSeconds _shortWait;
    private WaitForSeconds _currentWaitTime;
    private WaitForSeconds _longWait;
    [SerializeField] private State _state = State.Initialising;

    public bool IsIdle { get { return _state == State.Idle; } }
    public bool IsBusy { get { return _state != State.Idle; } }

    private AudioSource AudioSource;

    private void Awake()
    {
        _quickWait = new WaitForSeconds(0.01f);
        _shortWait = new WaitForSeconds(0.05f);
        _currentWaitTime = _shortWait;
        _longWait = new WaitForSeconds(0.8f);

        _displayText.text = string.Empty;
        _state = State.Idle;

        AudioSource = GetComponent<AudioSource>();
    }

    private IEnumerator DoShowText(string text)
    {
        _currentWaitTime = _shortWait;

        int currentLetter = 0;
        char[] charArray = text.ToCharArray();

        while (currentLetter < charArray.Length)
        {
            _displayText.text += charArray[currentLetter++];
            PlayLetterSound();
            yield return _currentWaitTime;
        }

        _displayText.text += "\n";
        _displayString = _displayText.text;
        _state = State.Idle;
    }

    private IEnumerator DoAwaitingInput()
    {
        bool on = true;

        while (enabled)
        {
            _displayText.text = string.Format("{0}> {1}", _displayString, (on ? "|" : " "));
            on = !on;
            yield return _longWait;
        }
    }

    private IEnumerator DoClearText()
    {
        int currentLetter = 0;
        char[] charArray = _displayText.text.ToCharArray();

        while (currentLetter < charArray.Length)
        {
            if (currentLetter > 0 && charArray[currentLetter - 1] != '\n')
            {
                charArray[currentLetter - 1] = ' ';
            }

            if (charArray[currentLetter] != '\n')
            {
                charArray[currentLetter] = '_';
            }

            _displayText.text = charArray.ArrayToString();
            ++currentLetter;
            yield return null;
        }

        _displayString = string.Empty;
        _displayText.text = _displayString;
        _state = State.Idle;
    }

    private void PlayLetterSound()
    {
        if (AudioSource != null)
        {
            AudioSource.pitch = Random.Range(0.8f, 1.2f); //Changes the Pitch for each Letter
            AudioSource.Play();
        }
    }

    public void Display(string text)
    {
        if (_state == State.Idle)
        {
            StopAllCoroutines();
            _state = State.Busy;
            StartCoroutine(DoShowText(text));
        }
    }

    public void ShowWaitingForInput()
    {
        if (_state == State.Idle)
        {
            StopAllCoroutines();
            StartCoroutine(DoAwaitingInput());
        }
    }

    public void Clear()
    {
        if (_state == State.Idle)
        {
            StopAllCoroutines();
            _state = State.Busy;
            StartCoroutine(DoClearText());
        }
    }

    public void ResetDisplay()
    {
        _displayString = string.Empty;
        _displayText.text = string.Empty;
        _state = State.Idle;
    }

    public void SpeedUpText()
    {
        _currentWaitTime = _quickWait;
    }

    public void DefaultTextSpeed()
    {
        _currentWaitTime = _shortWait;
    }
}
