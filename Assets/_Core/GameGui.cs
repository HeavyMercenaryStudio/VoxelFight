using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Data;
using Menagers;

namespace CameraUI { 

    /// <summary>
    /// Control in Game interface
    /// </summary>
    public class GameGui : MonoBehaviour {

        [TextArea]
        [SerializeField] string victoryText; // Text showed when player complete mission
        [TextArea]
        [SerializeField] string defeadText; // Text showed when player defeat mission
        [SerializeField] Text crystalsText;

        [SerializeField] Button backToMenuButton;
        [SerializeField] GameObject missionEndPanel; //Panel showed when player end mission
        [SerializeField] GameObject wavePanel; //panel showed pass waves

        private void Start()
        {
            backToMenuButton.onClick.AddListener (BackToMenu);
            PlayerDatabase.Instance.crystalValueChanged += UpdateCrystalText;

            UpdateCrystalText(PlayerDatabase.Instance.PlayersCrystals);
        }
        private void BackToMenu()
        {
            var worldMenager = FindObjectOfType<WorldMenager>();
            worldMenager.MissionDefeat(true);
        }
        public void Defeat()
        {
            ShowMissionEndPanel ();

            var textComp = missionEndPanel.GetComponentInChildren<Text> (); // get mission text...
            textComp.text = defeadText; // set it to mission result text
            textComp.color = Color.red;
        }
        private void ShowMissionEndPanel()
        {
           if(wavePanel) wavePanel.SetActive (false); //hide wave panel
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
        public void VictoryWithNoReturn()
        {
            wavePanel.SetActive(false); //hide wave panel
            missionEndPanel.SetActive(true); //show mission result panel

            var textComp = missionEndPanel.GetComponentInChildren<Text>(); // get mission text...
            textComp.text = victoryText; // set it to mission result text
            textComp.color = Color.green;

            WorldData.NextMission.SetCompleted(); // Set next mission avaible to play
        }
        private IEnumerator ReturnToMenu()
        {

            yield return new WaitForSeconds (2);

            SceneManager.LoadScene ("Menu"); // load menu scene
        }

        public void UpdateCrystalText(int value)
        {
            crystalsText.text = value.ToString();
        }

        void OnDestroy()
        {
            PlayerDatabase.Instance.crystalValueChanged -= UpdateCrystalText;
        }

    }
}
