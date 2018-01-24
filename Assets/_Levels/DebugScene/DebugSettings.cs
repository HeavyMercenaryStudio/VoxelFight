using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using CameraUI;
using Characters;
using System.Linq;

public class DebugSettings : MonoBehaviour {

    private void Awake()
    {
        WorldData.NumberOfPlayers = 0;
    }

    private void Start()
    {
        var camera = FindObjectOfType<CameraFollow>();

        var ta = FindObjectsOfType<PlayerController>();

        List<Transform> newT = new List<Transform>();
        foreach (var t in ta)
        {
            newT.Add(t.transform);
        }

        camera.SetTransformTargets(newT);
    }
}
