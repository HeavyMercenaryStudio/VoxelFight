using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenager : MonoBehaviour {

    [SerializeField] SoundMenager inGameMusic;

    AudioSource audioSource;
    public static AudioMenager Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy (this);

        DontDestroyOnLoad (this);
    }
    void Start () {
        audioSource = GetComponent<AudioSource> ();
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot (clip);
    }

    public void PlayMenuMusic()
    {
        var clip = inGameMusic.GetMenuClip ();
        audioSource.clip = clip;
        audioSource.Play ();
    }

    public void PlayGameMusic()
    {
        var clip = inGameMusic.GetGameClip ();
        audioSource.clip = clip;
        audioSource.Play ();
    }


}
