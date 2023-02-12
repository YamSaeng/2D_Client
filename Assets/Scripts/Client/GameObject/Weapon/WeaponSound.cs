using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : GameObjectSoundPlayer
{
    [SerializeField]
    private AudioClip _WooshBlade;

    public void PlayWooshSound()
    {
        PlayClip(_WooshBlade);
    }
}
