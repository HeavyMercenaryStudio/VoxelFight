using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SoundMenager")]
public class SoundMenager : ScriptableObject {

    //IF you want new sound to your weapon need to add in this script
    // and in getweapon function

    [SerializeField] AudioClip pistolSound;
    [SerializeField] AudioClip riffeSound;
    [SerializeField] AudioClip shotugnSound;

    public AudioClip GetWeaponSound(Weapon w)
    {
        if (w is Riffle) return riffeSound;
        if (w is DefaultWeapon) return pistolSound;
        if (w is SniperRiffle) return riffeSound;
        if (w is Shotgun) return shotugnSound;

        return null;
    }
}
