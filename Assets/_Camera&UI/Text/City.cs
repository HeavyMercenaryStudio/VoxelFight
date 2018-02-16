using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CameraUI
{ 
    /// <summary>
    /// Describe particular city
    /// </summary>
    public class City : MonoBehaviour {

        [SerializeField] string cityName; // name of city 
        [SerializeField] List<Mission> cityMissions; //avaible missions in city

        public string CityName{get{ return cityName; }} 

        public List<Mission> GetMissions()
        {
            return cityMissions;
        }
    }
}
