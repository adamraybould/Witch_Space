using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(DialogueManager))]
public class Projection : MonoBehaviour
{
    private Animator anim;

    private DialogueManager DialogueManager;

    private AudioSource audioSource;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    private bool cutsceneProjection; //Is the Projection activated within a cutscene
    [SerializeField] private PlayableDirector director; //Encase the Projection is within a cutscene

    private void Awake()
    {
        anim = GetComponent<Animator>();
        DialogueManager = GetComponent<DialogueManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateProjection(bool isCutscene)
    {
        anim.SetBool("Project", true);
        PlayAudio(openClip);
        cutsceneProjection = isCutscene;

        DialogueManager.NextDialogue();
    }

    public void DeactivateProjection()
    {
        anim.SetBool("Project", false);
        PlayAudio(closeClip);

        //Check if is within a Cutscene, if yes: resume the cutscene
        if (cutsceneProjection)
            director.Resume();
    }

    private void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
