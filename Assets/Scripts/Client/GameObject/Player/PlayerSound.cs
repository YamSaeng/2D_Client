using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : GameObjectSoundPlayer
{
    [SerializeField]
    private AudioClip _StepClip;

    public void PlayStepSound()
    {
        PlayClip(_StepClip);
    }
}
