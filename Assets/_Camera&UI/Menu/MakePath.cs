using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePath : MonoBehaviour {


    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    [SerializeField] int verticesAmount;
    [SerializeField] Transform wayPrefab;
    [SerializeField] Transform animationPrefab;
    [SerializeField] float speed;
    List<Transform> objects;
    Transform obj;
    Vector3 TempStart;
    Transform animatedObject;
    float step;
    int point;



    void Start()
    {
        objects = new List<Transform>();
        TempStart = start.transform.position;
        if (verticesAmount == 0)
        {
            verticesAmount = 20;
            step = 0.05f;
        }
        else
            step = (float)1 / verticesAmount;
        if (speed == 0) speed = 0.1f;
        point = 1;
        Draw();
        animatedObject = Instantiate(animationPrefab,objects[0].transform.position,objects[0].transform.rotation);
        animatedObject.GetComponent<Collider>().enabled = false;
        animatedObject.transform.parent = GameObject.Find("MapPlanet").transform;

    }

    void Draw () {
        float t=0;
        for (int i = 0; i < verticesAmount; i++)
        {
            obj = Instantiate(wayPrefab, GetBezierPosition(t, start.transform, end.transform), Quaternion.identity);
            obj.GetComponent<Collider>().enabled = false;
            obj.transform.parent = GameObject.Find("MapPlanet").transform;
           
            objects.Add(obj);
            if (objects[i] != null)
            {
                if (i == 0)
                {
                    //obj.rotation = start.transform.rotation;
                    objects[i].transform.rotation = start.transform.rotation;
                }
                else
                {
                    //obj.LookAt(objects[i - 1]);
                    objects[i].LookAt(objects[i - 1]);
                }
                t += step;
            }
        }

    }

    void Update()
    {
        Animate();
    }

    private void Delete()
    {
        if (start.transform.position != TempStart)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != null)
                    Destroy(objects[i].gameObject);
            }
            objects.Clear();
            Draw();
            TempStart = start.transform.position;
        }
    }

    private Vector3 GetBezierPosition(float t, Transform start1, Transform end1)
    {
        Vector3 p0 = start1.position;
        Vector3 p1 = p0 + start1.right*-2;
        Vector3 p3 = end1.position;
        Vector3 p2 = p3 + end1.right*-2f;
        
        return Mathf.Pow(1f - t, 3f) * p0 + 3f * Mathf.Pow(1f - t, 2f) * t * p1 + 3f * (1f - t) * Mathf.Pow(t, 2f) * p2 + Mathf.Pow(t, 3f) * p3;
    }

    private void Animate()
    {
        if (animatedObject.transform.position == objects[point].transform.position)
            ++point;

        Debug.Log(point);
        //animatedObject.transform.rotation = Quaternion.Slerp(animatedObject.rotation, objects[point].rotation, Time.deltaTime * speed);
        //animatedObject.transform.position = Vector3.Lerp(animatedObject.position,objects[point].position,Time.deltaTime*speed);
        if (point != verticesAmount)
            animatedObject.position = Vector3.MoveTowards(animatedObject.position, objects[point].position, Time.deltaTime * speed);
        else
        {
            animatedObject.position = objects[0].position;
            point = 1;
        }
    }
}
