using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameObjectSoundPlayer : MonoBehaviour
{
    protected AudioSource _AudioSource;

    protected float _PitchRandomcess = 0.05f;
    protected float _BasePitch;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _BasePitch = _AudioSource.pitch;
    }

    protected void PlayClipWithRanomPitch(AudioClip Clip)
    {
        float RandomPitch = Random.Range(-_PitchRandomcess, _PitchRandomcess);
        _AudioSource.pitch = _BasePitch + RandomPitch;

    }

    protected void PlayClip(AudioClip Clip)
    {
        _AudioSource.Stop();
        _AudioSource.clip = Clip;
        _AudioSource.Play();
    }
}
