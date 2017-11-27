using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    [SerializeField] Text cityNameText;
    [SerializeField] GameObject cityMissionViewContent;


    List<City> allCites;
    City currentCity;

	// Use this for initialization
	void Start () {
        allCites = FindObjectsOfType<City> ().ToList();
	}
	
    void CityChange(City city)
    {
        cityNameText.text = city.CityName;

        foreach (Transform t in cityMissionViewContent.transform){
            Destroy (t.gameObject);
        }

        foreach (Mission m in city.GetMissions())
        {
            Instantiate (m.GetMissionPrefab (), cityMissionViewContent.transform);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0))
            RaycastForCity ();
    }

    private void RaycastForCity()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        RaycastHit hit;
        Physics.Raycast (ray, out hit);

        var gameobjectHit = hit.collider.gameObject;
        var cityHit = gameobjectHit.GetComponent<City> ();

        if (cityHit)
        {
            CityChange (cityHit);
        }

    }
}
