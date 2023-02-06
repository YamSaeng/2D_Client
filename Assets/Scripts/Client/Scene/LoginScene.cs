using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class LoginScene : BaseScene
{
    UI_LoginScene _LoginSceneUI;

    protected override void Init()
    {
        base.Init();

        Managers.Sound.PlayBGM(en_SoundClip.SOUND_CLIP_LOGIN, 0.1f);

        _SceneType = Define.en_Scene.LoginScene;
                
        //게임 화면 크기 640 480으로 조정
        Screen.SetResolution(800, 600, false);

        _LoginSceneUI = Managers.UI.ShowSceneUI<UI_LoginScene>(en_ResourceName.CLIENT_UI_SCENE_LOGIN);        
    }

    public override void Clear()
    {

    }
}
