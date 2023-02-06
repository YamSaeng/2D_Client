using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class GameScene : BaseScene
{
    UI_GameScene _GameSceneUI;

    private CDay _Day;

    protected override void Init()
    {
        base.Init();

        Managers.Sound.PlayBGM(en_SoundClip.SOUND_CLIP_FOREST, 0.1f);

        _SceneType = Define.en_Scene.GameScene;

        Managers.Map.LoadMap(en_ResourceName.CLIENT_MAP_MAIN_FIELD);
        
        Screen.SetResolution(1366 , 960, false);

        _GameSceneUI = Managers.UI.ShowSceneUI<UI_GameScene>(en_ResourceName.CLIENT_UI_SCENE_GAME);

        GameObject DayGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_DAY, transform);
        _Day = DayGO.GetComponent<CDay>();
    }

    public override void Clear()
    {

    }      

    public CDay GetDay()
    {
        return _Day;
    }
}
