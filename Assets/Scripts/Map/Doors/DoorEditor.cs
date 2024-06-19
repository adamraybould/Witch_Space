using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Door doorScript = (Door)target;
        if (GUILayout.Button("Open Door"))
        {
            doorScript.OpenDoor();
        }

        if (GUILayout.Button("Close Door"))
        {
            doorScript.CloseDoor();
        }
    }
}
#endif