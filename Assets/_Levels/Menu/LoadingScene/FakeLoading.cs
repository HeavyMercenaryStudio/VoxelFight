using CameraUI;
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeLoading : MonoBehaviour {

    [SerializeField] Image loadingBar;
    [SerializeField] Text loadingText;
    [SerializeField] float fakeLoadTime = 5f;

    float progress;
    GameObject gui;
    // Use this for initialization
    void Start () {
        fakeLoadTime = WorldData.InifinityModeSize * 0.2f + 2.2f;

        gui = FindObjectOfType<GameGui>().gameObject;
        gui.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        progress += Time.deltaTime;
        if(loadingBar) loadingBar.fillAmount = progress / fakeLoadTime;
        if(loadingText) loadingText.text = ((int)(progress/fakeLoadTime * 100f)).ToString() + "%";
        if(progress > fakeLoadTime)
        {
            gui.SetActive(true);
            Destroy(this.gameObject);
        }

       
	}
}
