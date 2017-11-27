using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

    [SerializeField] string cityName;
    List<Mission> cityMissions;

    public string CityName{get{ return cityName; }}

    public delegate void OnMouseClick(City city);
    public static event OnMouseClick notifyCityChange;

    // Use this for initialization
    void Start () {
		
	}
	
	
}
