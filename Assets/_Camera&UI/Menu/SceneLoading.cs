using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Data;

public class SceneLoading : MonoBehaviour {

    [SerializeField] Image loadingBar;
    [SerializeField] Text loadingText;

    // Use this for initialization
    void Start () {
        StartCoroutine(LoadAsync());
    }

	
    IEnumerator LoadAsync()
    {
        var operation = SceneManager.LoadSceneAsync(WorldData.SceneIndex);

        while (!operation.isDone)
        {
            float progres = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBar.fillAmount = progres;
            loadingText.text = (progres * 100).ToString() + "%";

            yield return null;
        }
    }
	
}
