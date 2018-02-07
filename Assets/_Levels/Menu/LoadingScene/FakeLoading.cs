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
    // Use this for initialization
    void Start () {
        fakeLoadTime = WorldData.InifinityModeSize * 0.2f + 2.2f;
	}
	
	// Update is called once per frame
	void Update () {

        progress += Time.deltaTime;
        if(loadingBar) loadingBar.fillAmount = progress / fakeLoadTime;
        if(loadingText) loadingText.text = ((int)(progress/fakeLoadTime * 100f)).ToString() + "%";
        if(progress > fakeLoadTime)
        {
            Destroy(this.gameObject);
        }

       
	}
}
