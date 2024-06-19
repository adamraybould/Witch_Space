using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct SoundEffect
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;

    public string GetName() { return name; }
    public AudioClip GetAudio() { return clip; }
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<SoundEffect> soundEffects; //A List of Audio Clips to be used
    private AudioSource AudioSource;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    //Plays a Specific Audio Clip under the passed in name
    public void PlaySoundEffect(string name)
    {
        //Searches through the list of clips for the passed in name and plays it
        foreach (SoundEffect effect in soundEffects)
        {
            if (effect.GetName() == name)
            {
                AudioSource.clip = effect.GetAudio();
                AudioSource.Play();
            }
        }
    }
}
