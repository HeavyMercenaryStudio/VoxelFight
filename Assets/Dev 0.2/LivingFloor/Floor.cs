using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    [SerializeField] GameObject floorElementPrefab;
    [SerializeField] int amount;
    private float size;
    private float offset;
    private int rowCount;
    private int animationCycle = 0;
    private float animationStepper;
    private List<GameObject> elements=new List<GameObject>();
    private List<Vector2> conObj = new List<Vector2>();
    [SerializeField]
    GameObject actor;

    // Use this for initialization
    void Start () {
        animationStepper = 0f;
        rowCount = Mathf.RoundToInt(Mathf.Sqrt(amount));
        size = floorElementPrefab.GetComponent<Renderer>().bounds.size.x;
        offset = size;
        var col = 0f;
        var row = 0f;
        var cycle = 0;
        Debug.Log(rowCount);
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                elements.Add((GameObject)Instantiate(floorElementPrefab));
                elements[elements.Count - 1].transform.position = new Vector3(col, 0, row);
                col += offset;
                ++cycle;
                if(cycle>rowCount-1)
                {
                    col = 0f;
                    row += offset;
                    cycle = 0;
                }
            }
        }
        setSurface();
	}
	

    void setSurface()
    {
        var iterator = 0;
        var cycle = 0;
        var x = 0f;
        var y = 0f;
        var step = 0.2f;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                var equation = (Mathf.Sin(x) + Mathf.Cos(y))/10;
                //Debug.Log(equation);
                var pos = elements[iterator].transform.position;
                elements[iterator].transform.position=new Vector3(pos.x, equation, pos.z);
                x += step;
                ++cycle;
                ++iterator;
                if (cycle > rowCount - 1)
                {
                    x = 0f;
                    y += step;
                    cycle = 0;
                }
            }
        }
    }

    void animSurf(float os)
    {
        var iterator = 0;
        var cycle = 0;
        var x = 0f;
        var y = 0f;
        var step = 0.2f;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                var equation = (Mathf.Sin(x+os) + Mathf.Cos(y+os)) / 10;
                //Debug.Log(equation);
                var pos = elements[iterator].transform.position;
                if (!conObj.Contains(elements[iterator].transform.position))
                    elements[iterator].transform.position = new Vector3(pos.x, equation, pos.z);
                else
                    elements[iterator].transform.position = new Vector3(pos.x,0,pos.z);
                x += step;
                ++cycle;
                ++iterator;
                if (cycle > rowCount - 1)
                {
                    x = 0f;
                    y += step;
                    cycle = 0;
                }
            }
        }
    }


    // Update is called once per frame
    void LateUpdate () {
        //Collider[] hitColliders = Physics.OverlapSphere(actor.transform.position, 0.5f);
        //Debug.Log(hitColliders[0].gameObject.name);
        //for (int i = 0; i < hitColliders.Length; i++)
        //{
        //    hitColliders[i].gameObject.GetComponent<Steering>().Contact();
        //}
        conObj.Clear();
        conObj = actor.GetComponent<ContactDetection>().getConObj();
        Debug.Log("Contacts: "+conObj.Count);


        animSurf(animationStepper);
        ++animationCycle;
        animationStepper += 0.1f;
        if(animationCycle==64)
        {
            animationCycle = 0;
            animationStepper = 0f;
        }
    

	}
}
