using Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CameraUI { 

    /// <summary>
    /// Anmiate text in game.
    /// </summary>
    public class TextAnimation : MonoBehaviour {

        [SerializeField] Text mainTextToAnmiate;  // first text
        [SerializeField] Text additionTextToAnmiate; //second text
                                                     // may have diffrent format

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
            yield return new WaitForSeconds (5); // wait before next wave

            mainTextToAnmiate.transform.root.gameObject.SetActive (false);

            var waveSystem = GetComponent<WaveSystem> (); // start next wave after end of animation
            waveSystem.NextWave ();
            StopAllCoroutines ();
        }


    }
}