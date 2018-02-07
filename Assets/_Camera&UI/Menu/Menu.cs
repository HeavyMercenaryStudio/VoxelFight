using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Audio;
using Data;
using Weapons;

namespace CameraUI {

    /// <summary>
    /// Menu functions.
    /// </summary>
    public class Menu : MonoBehaviour {

        [SerializeField] List<Button> startButtonList; //list of main menu options
        [SerializeField] GameObject mainMenuBackground; // main menu background
        [SerializeField] Button backToMainMenuButon; // back to menu button
        [SerializeField] Button characterPanelButon; // character panel button
        [SerializeField] Button missionsPanelButon; // mission panel button

        [SerializeField] Text cityNameText; // Text changing when city change 
        [SerializeField] GameObject cityMissionViewContent; //context of city missions

        [SerializeField] Text tutorialText;

        City currentCity; //current selected city

	    // Use this for initialization
	    void Start ()
        {

            AddButtonsListeners (); // add listeneres
            VisiblePanels (); // hide and visible particlular panels

            AudioMenager.Instance.PlayMenuMusic (); // play menu music

            GameData.ApplicationRunTimes++; // aplication run first time

            CityChange(GetComponent<City>());
        }

        private void VisiblePanels()    
        {
            if(GameData.ApplicationRunTimes > 0) // if it is not first run
            {
                //pass main menu
               // tutorialText.transform.parent.gameObject.SetActive (true); 
                mainMenuBackground.SetActive (false);
                backToMainMenuButon.gameObject.SetActive (true);
                characterPanelButon.gameObject.SetActive(true);
                missionsPanelButon.gameObject.SetActive(true);
               // StartCoroutine (TutorialText ());
            }
            else // else start game with main menu
            { 
               // tutorialText.transform.parent.gameObject.SetActive (false);
                mainMenuBackground.SetActive (true);
                backToMainMenuButon.gameObject.SetActive (false);
                characterPanelButon.gameObject.SetActive(false);
                missionsPanelButon.gameObject.SetActive(false);
            }
        }
        private void AddButtonsListeners()
        {
            startButtonList[0].onClick.AddListener (SinglePlayer); // add listener to signle player
            startButtonList[1].onClick.AddListener (MultiPlayer); // add listener to multi
            startButtonList[2].onClick.AddListener (Exit); // add listener to on application exit
            startButtonList[3].onClick.AddListener(ResetSave); // add listener to on application exit
            backToMainMenuButon.onClick.AddListener (BackToMenu);
            characterPanelButon.onClick.AddListener(ChangeToCharacterPanel);
            missionsPanelButon.onClick.AddListener(ChangeToMissionPanel);
        }

        private void ChangeToMissionPanel()
        {
            StartCoroutine(RotateToMissions());
        }
        private void ChangeToCharacterPanel()
        {
           StartCoroutine(RotateToInventory());
        }
        private void ResetSave()
        {
            PlayerDatabase.Instance.ResetData();
          //  InventoryMenu.Instance.UpdateInventoryGUI();
        }
        private void BackToMenu()
        {
           // tutorialText.transform.parent.gameObject.SetActive (false); // active tutorial test
            mainMenuBackground.SetActive (true); // active main menu options
            backToMainMenuButon.gameObject.SetActive (false); // deactive back button
            characterPanelButon.gameObject.SetActive(false);
            missionsPanelButon.gameObject.SetActive(false);
        }
        private void SinglePlayer()
        {
            WorldData.NumberOfPlayers = 1; // set number of players as single

            StartGame ();
        }
        private void MultiPlayer()
        {
            WorldData.NumberOfPlayers = 2; // set players to two 

            StartGame ();
        }
        private void Exit()
        {
            Application.Quit ();
        }
        IEnumerator RotateToInventory()
        {
           
           // tutorialText.transform.parent.gameObject.SetActive(false);
            characterPanelButon.gameObject.SetActive(false);
            characterPanelButon.gameObject.SetActive(true);
            var cam = Camera.main.transform;

            Quaternion inventoryRotation = new Quaternion(0f, 0.7f, 0, 0.7f);

            float duration = 0.5f;
            
            for(float i = 0; i < duration; i += Time.deltaTime)
            {
                cam.rotation = Quaternion.Lerp(cam.rotation, inventoryRotation, i / duration);
                yield return new WaitForEndOfFrame();
            }
        }
        IEnumerator RotateToMissions()
        {
           
           // tutorialText.transform.parent.gameObject.SetActive(true);
            missionsPanelButon.gameObject.SetActive(false);
            missionsPanelButon.gameObject.SetActive(true);
            var cam = Camera.main.transform;

            Quaternion missionRotation = Quaternion.identity;

            float duration = 0.5f;
            for (float i = 0; i < duration; i += Time.deltaTime)
            {
                cam.rotation = Quaternion.Lerp(cam.rotation, missionRotation, i / duration);
                yield return new WaitForEndOfFrame();
            }
        }

        private void StartGame()
        {
            mainMenuBackground.SetActive (false);
            //tutorialText.transform.parent.gameObject.SetActive (true);
            backToMainMenuButon.gameObject.SetActive (true); // active back to menu button
            characterPanelButon.gameObject.SetActive(true);
            missionsPanelButon.gameObject.SetActive(true);

            //StartCoroutine (TutorialText ());
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

                var modeHit = gameobjectHit.GetComponent<ModeScript>();// try to get city component
                if (modeHit)//if sucess...
                    modeHit.LoadScene();
            }
        }

        IEnumerator TutorialText()
        {
            string text = tutorialText.text; //get text from inspector
            int lenght = text.Length;
            int i = 0;
            tutorialText.text = "";

            while (i < lenght) // while text is not empty
            {
                tutorialText.text += text[i]; //fill text area
                yield return new WaitForSeconds (0.05f); //every 0.15 s
                i++;    
            }
        }



    }
}
