using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {

    [SerializeField] Text mainTextToAnmiate;
    [SerializeField] Text additionTextToAnmiate;

    private void Start()
    {
        StartCoroutine (MainTextAnimation ());
    }

    IEnumerator MainTextAnimation()
    {
        string text = mainTextToAnmiate.text; //get text from inspector
        int lenght = text.Length;
        int i = 0;
        mainTextToAnmiate.text = "";
        string addText = additionTextToAnmiate.text;
        additionTextToAnmiate.text = "";

        while (i < lenght) // while text is not empty
        {
            mainTextToAnmiate.text += text[i]; //fill text area
            yield return new WaitForSeconds (0.05f); //every 0.05 s
            i++;
        }

        StartCoroutine (AddionTextAnimation (addText));
    }

    IEnumerator AddionTextAnimation(string text)
    {
        int lenght = text.Length;
        int i = 0;
        while (i < lenght) // while text is not empty
        {
            additionTextToAnmiate.text += text[i]; //fill text area
            yield return new WaitForSeconds (0.05f); //every 0.05 s
            i++;
        }

        StartCoroutine (Delay ());
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds (5);

        mainTextToAnmiate.transform.root.gameObject.SetActive (false);
        var waveSystem = GetComponent<WaveSystem> ();
        waveSystem.NextWave ();
        StopAllCoroutines ();
    }


}
