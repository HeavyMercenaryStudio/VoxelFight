using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

    // Use this for initialization
    public int crystals;
	void Start () {
        crystals = 0;
	}
	
    public void addCrystals(int amount)
    {
        if(amount>0)
            crystals += amount;
    }

    public int getCrystals()
    {
        return crystals;
    }
	// Update is called once per frame
	void Update () {
          Debug.Log(this.name+": "+getCrystals());
	}
}
