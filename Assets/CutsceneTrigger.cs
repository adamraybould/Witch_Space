using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableAsset cutscene;
    [SerializeField] private PlayableDirector PlayableDirector;
    [SerializeField] private bool activated; //Is the Cutscene already activated?

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            activated = true;
            PlayableDirector.Play(cutscene);
        }
    }
}
