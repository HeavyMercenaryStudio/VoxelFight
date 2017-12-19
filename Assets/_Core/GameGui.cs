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

    [SerializeField] GameObject missionEndPanel; //Panel showed when player end mission
    [SerializeField] GameObject wavePanel; //panel showed pass waves

    [SerializeField] Text score;


    public void Defeat()
    {
        ShowMissionEndPanel ();

        var textComp = missionEndPanel.GetComponentInChildren<Text> (); // get mission text...
        textComp.text = defeadText; // set it to mission result text
        textComp.color = Color.red;
    }

    private void ShowMissionEndPanel()
    {
        wavePanel.SetActive (false); //hide wave panel
        missionEndPanel.SetActive (true); //show mission result panel

        StartCoroutine (ReturnToMenu ());
    }

    public void Victory()
    {
        ShowMissionEndPanel ();
        var textComp = missionEndPanel.GetComponentInChildren<Text> (); // get mission text...
        textComp.text = victoryText; // set it to mission result text
        textComp.color = Color.green;

        WorldData.NextMission.SetCompleted (); // Set next mission avaible to play
    }

    private IEnumerator ReturnToMenu()
    {

        yield return new WaitForSeconds (2);

        SceneManager.LoadScene ("Menu"); // load menu scene
    }
}
