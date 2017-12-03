using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGui : MonoBehaviour {

    [TextArea]
    [SerializeField] string victoryText; // Text showed when player complete mission
    [TextArea]
    [SerializeField] string defeadText; // Text showed when player defeat mission

    [SerializeField] Button backToMenuButton; // Back to menu button
    [SerializeField] GameObject missionEndPanel; //Panel showed when player end mission
    [SerializeField] GameObject wavePanel; //panel showed pass waves

    // Use this for initialization
    void Start () {
        backToMenuButton.onClick.AddListener (ReturnToMenu); // add back to menu listener
    }

    public void Defeat()
    {
        ShowMissionEndPanel (defeadText); 
    }

    private void ShowMissionEndPanel(string missionResultText)
    {
        wavePanel.SetActive (false); //hide wave panel
        missionEndPanel.SetActive (true); //show mission result panel
        var textComp = missionEndPanel.GetComponentInChildren<Text> (); // get mission text...
        textComp.text = missionResultText; // set it to mission result text
        

        var camera = GameObject.FindObjectOfType<CameraFollow> (); //find camera and...
        camera.GameOver (this.transform);//center it to mission world space result canvas 
    }

    public void Victory()
    {
        ShowMissionEndPanel (victoryText);

        WorldData.NextMission.SetCompleted (); // Set next mission avaible to play
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene ("Menu"); // load menu scene
    }

}
