using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour {

    [SerializeField] string cityName;
    [SerializeField] List<Mission> cityMissions;

    public string CityName{get{ return cityName; }}

    public List<Mission> GetMissions()
    {
        return cityMissions;
    }
}
