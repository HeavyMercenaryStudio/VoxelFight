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
            GameObject obj = Instantiate (m.GetMissionPrefab (), cityMissionViewContent.transform);

            if (!m.IsMissionComplete())
                DisableObject (obj);
        }

    }

    private static void DisableObject(GameObject obj)
    {
        //Set dark color
        var backImage = obj.GetComponent<Image> (); // get background color 
        backImage.color = new Color (0.02f, 0.1f, 0.14f, 0.77f); // set it to dark
        var frontImage = obj.GetComponentsInChildren<Image> (); // get image color 
        frontImage[1].color = new Color (0.2f, 0.2f, 0.2f, 1f); //set it to dark
        var text = obj.GetComponentInChildren<Text> (); // get text color
        text.color = new Color (0.2f, 0.2f, 0.2f, 1f); //set it to dark

        //
        obj.GetComponent<Button> ().interactable = false;
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

        if (hit.collider) { 
            var gameobjectHit = hit.collider.gameObject;
            var cityHit = gameobjectHit.GetComponent<City> ();

            if (cityHit)
                CityChange (cityHit);
        }

    }
}
