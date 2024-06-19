using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private List<BeatData> _beats;
 
    public BeatData GetBeatById( int id )
    {
        return _beats.Find(b => b.ID == id);
    }
}

