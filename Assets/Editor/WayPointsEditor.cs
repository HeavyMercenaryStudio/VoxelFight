using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoints))]
public class WayPointsEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WayPoints script = (WayPoints)target;

        if (GUILayout.Button("ADD"))
        {
            script.AddPoint();
        }
        else if (GUILayout.Button("SUBB"))
        {
            script.RemoveLastPointPoint();
        }
        
        GUILayout.Label("Number of points: " + script.GetAllPoints().Count);
    }


}
