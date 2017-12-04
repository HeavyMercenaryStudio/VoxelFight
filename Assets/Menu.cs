using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour {

    [SerializeField] List<Button> startButtonList; 

    [SerializeField] Text cityNameText; // Text changing when city change 
    [SerializeField] GameObject cityMissionViewContent; //context of city missions

    List<City> allCites; // all cites on map
    City currentCity; //current selected city

	// Use this for initialization
	void Start () {
        startButtonList[0].onClick.AddListener (SinglePlayer); // add listener to signle player
        startButtonList[1].onClick.AddListener (MultiPlayer); // add listener to multi
        startButtonList[2].onClick.AddListener (Exit); // add listener to on application exit

        allCites = FindObjectsOfType<City> ().ToList(); // get all of citys on map
        AudioMenager.Instance.PlayMenuMusic (); // play menu music
    }

    private void SinglePlayer()
    {
        WorldData.NumberOfPlayers = 1;
        StartCoroutine(RotateCamera ());
        startButtonList[0].transform.parent.gameObject.SetActive (false);
    }
    private void MultiPlayer()
    {
        WorldData.NumberOfPlayers = 2;
        StartCoroutine (RotateCamera ());
        startButtonList[0].transform.parent.gameObject.SetActive (false);
    }
    private void Exit()
    {
        Application.Quit ();
    }
    IEnumerator RotateCamera()
    {
        while (true)
        {
            var cam = Camera.main.transform;
            cam.RotateAround (cam.position, cam.up, 1.5f);

            yield return new WaitForEndOfFrame ();

            if (cam.rotation == Quaternion.identity)
                StopAllCoroutines ();
        }
    }

    private void CityChange(City city)
    {
        foreach (Transform t in cityMissionViewContent.transform){
            Destroy (t.gameObject);
        } //destroy previous missions on contex

        currentCity = city; //change city to clicked city

        cityNameText.text = city.CityName;//set displayed text
        foreach (Mission m in currentCity.GetMissions()) //Add mission to current city
        {
            //Instantiate new mission panel
            GameObject newMissionObject = Instantiate (m.GetMissionPrefab (), cityMissionViewContent.transform);

            //Set instantiated prefab...
            //Mission parametr to current mission in city missions
            var panelMission = newMissionObject.GetComponent<MissionScript> ();
            panelMission.SetMission(m);

            if (!m.IsMissionComplete()) //If mission is not completed yet...
                DisableObject (newMissionObject); // disable iteraction
        }
    }
    private static void DisableObject(GameObject obj)
    {
        var backImage = obj.GetComponent<Image> (); // get background color 
        backImage.color = new Color (0.02f, 0.1f, 0.14f, 0.77f); // set it to dark
        var frontImage = obj.GetComponentsInChildren<Image> (); // get image color 
        frontImage[1].color = new Color (0.2f, 0.2f, 0.2f, 1f); //set it to dark
        var text = obj.GetComponentInChildren<Text> (); // get text color
        text.color = new Color (0.2f, 0.2f, 0.2f, 1f); //set it to dark
       
        obj.GetComponent<Button> ().interactable = false;  //set not iteractable for not completetd  missions
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown (0)) //if left mouse button click..
            RaycastForCity (); //cast ray for city object on map
    }
    private void RaycastForCity()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //create ray

        RaycastHit hit;
        Physics.Raycast (ray, out hit);

        if (hit.collider) //if ray hit collider....
        {
            var gameobjectHit = hit.collider.gameObject; //get collider...
            var cityHit = gameobjectHit.GetComponent<City> ();// try to get city component

            if (cityHit)//if sucess...
                CityChange (cityHit); // change current city to new city
        }
    }
}
