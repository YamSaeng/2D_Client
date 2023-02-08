using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSoundManager
{
    private AudioSource _BGMAudioSource = new AudioSource();
    private Dictionary<en_SoundClip, AudioClip> _AudioClips = new Dictionary<en_SoundClip,AudioClip>();

    // MP3 Player -> AudioSource
    // MP3 음원   -> AudioClip
    // 관객(귀)   -> AudioListener (보통 MainCamera에 붙어 있음)

    public void Init()
    {
        //맵에서 @Sound게임오브젝트 찾은 후 없으면 만들어주고        
        GameObject SoundRoot = GameObject.Find("@Sound");
        if (SoundRoot == null)
        {
            SoundRoot = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(SoundRoot);
            
            GameObject BGMGo = new GameObject { name = "BGM" };
            _BGMAudioSource = BGMGo.AddComponent<AudioSource>();
            BGMGo.transform.parent = SoundRoot.transform;
            _BGMAudioSource.loop = true;            
        }

        LoadAudioClip();
    }

    private void LoadAudioClip()
    {
        _AudioClips.Add(en_SoundClip.SOUND_CLIP_LOGIN, Managers.Resource.Load<AudioClip>("Sounds/winds_rm"));
        _AudioClips.Add(en_SoundClip.SOUND_CLIP_FOREST, Managers.Resource.Load<AudioClip>("Sounds/Music/01.부여성"));
    }
    
    public void Clear()
    {
        _BGMAudioSource.clip = null;
        _BGMAudioSource.Stop();        

        _AudioClips.Clear();
    }

    public void PlayBGM(en_SoundClip SoundCliptype, float Volume = 1.0f, float Pitch = 1.0f)
    {
        if(_BGMAudioSource.isPlaying == true)
        {
            _BGMAudioSource.Stop();
        }

        _BGMAudioSource.volume = Volume;
        _BGMAudioSource.pitch = Pitch;
        _BGMAudioSource.clip = _AudioClips[SoundCliptype];
        _BGMAudioSource.Play();

        //_BGMAudioSource.PlayOneShot();
    }   
}
