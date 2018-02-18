using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WayPoints : MonoBehaviour {

    [HideInInspector]
    public List<GameObject> list = new List<GameObject>();
    public List<GameObject> GetAllPoints(){
        return list;
    }
    
    public void AddPoint()
    {
        Vector3 startPos = Vector3.zero;

        if (list.Count > 0) {
            var p = list[list.Count - 1];
            startPos = new Vector3(p.transform.position.x + 0.25f, p.transform.position.y, p.transform.position.z + 0.25f);
        }
        else
            startPos = transform.position;

        var point = new GameObject("WayPoint: " + list.Count);
        var pos = startPos;
        point.transform.position = pos;
        point.transform.SetParent(transform);
        list.Add(point);
    }

    public void RemoveLastPointPoint()
    {
        if (list.Count == 0) return;

        var index = list.Count - 1;
        DestroyImmediate(list[index]);
        list.RemoveAt(index);
    }


    void OnDrawGizmos()
    {
        if (list.Count == 0) return;

        //Draw Points
        Gizmos.color = Color.red;
        for (int i = 0; i < list.Count; i++){
            Gizmos.DrawCube(list[i].transform.position, new Vector3(0.25f,1, 0.25f));
        }

        //Draw Lines
        Gizmos.color = Color.green;
        for (int i = 0; i < list.Count-1; i++){
            Gizmos.DrawLine(list[i].transform.position, list[i + 1].transform.position);
        }

        //Draw end line
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(list[list.Count-1].transform.position, list[0].transform.position);
    }
}
