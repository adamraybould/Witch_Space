using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneManager : MonoBehaviour
{
    private static CutsceneManager _instance;
    public static CutsceneManager Instance { get { return _instance; } }

    private PlayableDirector director; //The Director that Handles the Cutscenes from CineMachine
    [SerializeField] private PlayerManager playerManager;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        director = GetComponent<PlayableDirector>();
    }

    public void PlayCutscene(PlayableAsset cutscene)
    {
        //Disables Player Movement
        playerManager.TakeControl();

        director.playableAsset = cutscene;
        director.Play();
    }
}
