using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SoundMenager")]
public class SoundMenager : ScriptableObject {

    [SerializeField] List<AudioClip> hitClips;
    [SerializeField] List<AudioClip> shootClips;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;

    public AudioClip GetHitClip()
    {
        int n = Random.Range (0, hitClips.Count - 1);
        return hitClips[n];
    }

    public AudioClip GetShootClip()
    {
        return shootClips[0];
    }

    public AudioClip GetMenuClip()
    {
        return menuMusic;
    }

    public AudioClip GetGameClip()
    {
        return gameMusic;
    }

}
