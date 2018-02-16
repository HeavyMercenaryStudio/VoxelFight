using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraUI;
using Data;

public class Map : MonoBehaviour {

    [SerializeField] float secondsBetweenMapUpdate;

    Renderer[] playersMarks;
    Renderer[] mapPlanes;

    void Start()
    {
        var players = FindObjectOfType<CameraFollow>().GetPlayers();
        playersMarks = new Renderer[players.Length];
        for (int i = 0; i < players.Length; i++)
            playersMarks[i] = players[i].Find("MapMark").GetComponent<Renderer>();

        mapPlanes = new Renderer[MazeGen.mapPlanes.Count];
        for (int i = 0; i < MazeGen.mapPlanes.Count; i++)
            mapPlanes[i] = MazeGen.mapPlanes[i].GetComponent<Renderer>();

        StartCoroutine(UpdateMap());
    }

    
    IEnumerator UpdateMap()
    {
        while (true)
        {
            for (int i = 0; i < mapPlanes.Length; i++)
                CheckIntersectionForPlane(i);

            yield return new WaitForSeconds(secondsBetweenMapUpdate);
        }
    }

    private bool CheckIntersectionForPlane(int i)
    {
        //for each player 
        foreach (Renderer r in playersMarks)
        {
            //check if plane is able to walk
            var plane = mapPlanes[i].GetComponent<InteractiveMapPlane>();
            if (plane) // if yes check intersection
            {
                if (mapPlanes[i].bounds.Intersects(r.bounds)){
                    mapPlanes[i].material.color = Color.white;
                    return true;
                }
            }
        }

        return false;
    }
}
