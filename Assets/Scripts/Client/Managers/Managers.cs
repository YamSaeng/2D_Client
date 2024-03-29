﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryController))]
public class Managers : MonoBehaviour
{
    static Managers _Instance;
    public static Managers GetInstance
    {
        get { Init(); return _Instance; }
    }

    #region ContentsManagers
    MapManager _MapManager = new MapManager();
    ObjectManager _ObjectManager = new ObjectManager();
    InventoryController _InventoryController;    
    SkillBoxManager _SkillBoxManager = new SkillBoxManager();
    QuickSlotBarManager _QuickSlotBarManager = new QuickSlotBarManager();
    CraftingBoxManager _CraftingBoxManager = new CraftingBoxManager();
    KeyManager _KeyManager = new KeyManager();  
    SpriteManager _SpriteManager = new SpriteManager();
    StringManager _StringManager = new StringManager();
    CameraManager _CameraManager = new CameraManager();
    GameMessageManager _GameMessageManager = new GameMessageManager();
    MouseManager _MouseManager = new MouseManager();
    TileManager _TileManager = new TileManager();

    public static MapManager Map
    {
        get { return GetInstance._MapManager; }
    }

    public static ObjectManager Object
    {
        get { return GetInstance._ObjectManager; }
    }

    public static InventoryController MyInventory
    {
        get { return GetInstance._InventoryController; }
    }    
    
    public static SkillBoxManager SkillBox
    {
        get { return GetInstance._SkillBoxManager; }
    }

    public static QuickSlotBarManager QuickSlotBar
    {
        get { return GetInstance._QuickSlotBarManager; }
    }

    public static CraftingBoxManager CraftingBox
    {
        get { return GetInstance._CraftingBoxManager; }
    }

    public static KeyManager Key
    {
        get { return GetInstance._KeyManager; }
    }

    public static SpriteManager Sprite
    {
        get { return GetInstance._SpriteManager; }
    }    

    public static StringManager String
    {
        get { return GetInstance._StringManager; }
    }

    public static CameraManager Camera
    {
        get { return GetInstance._CameraManager; }
    }

    public static GameMessageManager GameMessage
    {
        get { return GetInstance._GameMessageManager; }
    }

    public static MouseManager Mouse
    {
        get { return GetInstance._MouseManager; }
    }

    public static TileManager MapTile
    {
        get { return GetInstance._TileManager; }
    }


    #endregion

    #region CoreManagers    
    DataManager _DataManager = new DataManager();
    ResourceManager _ResourceManager = new ResourceManager();
    SceneManagerEx _SceneManager = new SceneManagerEx();
    BGMSoundManager _SoundManager = new BGMSoundManager();
    UIManager _UIManager = new UIManager();        

    public static DataManager Data
    {
        get { return GetInstance._DataManager; }
    }

    public static ResourceManager Resource
    {
        get { return GetInstance._ResourceManager; }
    }

    public static SceneManagerEx Scene
    {
        get { return GetInstance._SceneManager; }
    }

    public static BGMSoundManager Sound
    {
        get { return GetInstance._SoundManager; }
    }

    public static UIManager UI
    {
        get { return GetInstance._UIManager; }
    }
    #endregion

    #region ServerManager
    CNetworkManager _NetworkManager = new CNetworkManager();
    #endregion

    public static CNetworkManager NetworkManager
    {
        get { return GetInstance._NetworkManager; }
    }

    void Start()
    {        
        Init();
    }

    void Update()
    {
        _NetworkManager.Update();        
    }

    static void Init()
    {
        if (_Instance == null)
        {            
            GameObject GOManagers = GameObject.Find("@Managers");
            if (GOManagers == null)
            {
                GOManagers = new GameObject { name = "@Managers" };
                //Managers 스크립트를 Managers 오브젝트에 붙임
                GOManagers.AddComponent<Managers>();
                GOManagers.AddComponent<ClickObject>();
            }

            // 백그라운드에서도 실행 하도록 함
            Application.runInBackground = true;

            //사라지지 Managers 오브젝트가 사라지지 않도록 해줌
            DontDestroyOnLoad(GOManagers);
            _Instance = GOManagers.GetComponent<Managers>();
                        
            _Instance._ResourceManager.Init();
            _Instance._ObjectManager.Init();
            _Instance._SoundManager.Init();
            _Instance._CameraManager.Init();            

            _Instance._InventoryController = GOManagers.GetComponent<InventoryController>();

            _Instance._SpriteManager.Init();

            _Instance._StringManager.Init();

            _Instance._SkillBoxManager.Init();

            _Instance._GameMessageManager.Init();

            _Instance._MouseManager.Init();

            _Instance._TileManager.Init();

            _Instance._InventoryController.Binding();
        }
    }

    public static void Clear()
    {        
        Scene.Clear();
        UI.Clear();
        
        QuickSlotBar.Clear();
        Object.Clear();
    }
}
