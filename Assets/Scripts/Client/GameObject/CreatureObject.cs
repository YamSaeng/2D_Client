﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class CreatureObject : CBaseObject
{
    [HideInInspector]
    public Vector2 _WeaponPosition;

    [HideInInspector]
    public bool _IsChattingFocus;
    [HideInInspector]
    public GameObject SpeechBubblePosition;
    [HideInInspector]
    public UI_SpeechBubble _SpeechBubbleUI;

    [HideInInspector]
    public UI_HPBar _HPBarUI;    

    [HideInInspector]
    public UI_SkillCastingBar _SkillCastingBarUI;
    [HideInInspector]
    public UI_GatheringBar _GatheringBar;

    [HideInInspector]
    public Inventory _Inventory;

    [HideInInspector]
    public EquipmentBox _EquipmentBox { get; set; }
    [HideInInspector]
    public UI_EquipmentBox _EquipmentBoxUI { get; set; } // 장비 UI

    [HideInInspector]
    public UI_SkillBox _SkillBoxUI { get; set; } // 기술 UI    
     
    // 내가 선택한 오브젝트 정보
    public st_GameObjectInfo _SelectTargetObjectInfo;

    [field: SerializeField]
    public UnityEvent OnSpawnEvent { get; set; }    

    [field: SerializeField]
    public UnityEvent OnHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDieEvent { get; set; }

    [field: SerializeField]
    public UnityEvent<bool> OnDieAnimationEvent { get; set; }    

    public override void Init()
    {
        base.Init();

        if(_EquipmentBox == null)
        {
            _EquipmentBox = new EquipmentBox();
        }        

        if(_GameObjectInfo.ObjectId != Managers.NetworkManager._PlayerDBId)
        {
            if (_Inventory == null)
            {
                _Inventory = new Inventory();
                _Inventory.InventoryCreate(10, 10);                
            }
        }       

        switch (_GameObjectInfo.ObjectType)
        {
            case en_GameObjectType.OBJECT_PLAYER:
                if (gameObject.transform.Find("SpeechBubblePosition") != null)
                {
                    SpeechBubblePosition = gameObject.transform.Find("SpeechBubblePosition").gameObject;
                }

                _SpeechBubbleUI = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SPEECH_BUBBLE, SpeechBubblePosition.transform).GetComponent<UI_SpeechBubble>();
                _SpeechBubbleUI.SetOwner(this);
                _SpeechBubbleUI.ShowCloseUI(false);
                               
                break;
            case en_GameObjectType.OBJECT_GOBLIN:                
                StartCoroutine(SpawnEventCoroutine());
                break;
        }        

        if(_HPBarUI != null)
        {
            UpdateHPBar();
        }
    }

    //체력바 추가 ( 오브젝트 따위는 추가 안하기 위해 함수로 뺌 )
    protected void AddHPBar(float HPBarPositionX, float HPBarPositionY)
    {
        //유니티에서 생성한 프리팹 HPBar 가져와서 복제
        //기본 위치는 소환한 크리처 좌표보다 약간 위쪽으로        
        GameObject HPBarGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_HP_BAR, transform);
        HPBarGo.transform.localPosition = new Vector3(HPBarPositionX, HPBarPositionY, 0);
        HPBarGo.name = "UI_HPBar";

        //체력 업데이트 하기 위해서 스크립트 추출해서 넣어둠
        _HPBarUI = HPBarGo.GetComponent<UI_HPBar>();        

        //체력바 업데이트
        UpdateHPBar();

        _HPBarUI.gameObject.SetActive(false);
    }

    protected void AddSkillCastingBar(float SkillCastingBarPositionX, float SkillCastingBarPositionY)
    {
        GameObject SkillCastingBarGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_CASTING_BAR, transform);
        SkillCastingBarGo.transform.localPosition = new Vector3(SkillCastingBarPositionX, SkillCastingBarPositionY, 0);
        SkillCastingBarGo.name = "UI_SkillCastingBar";

        _SkillCastingBarUI = SkillCastingBarGo.GetComponent<UI_SkillCastingBar>();
    }   

    protected void AddGatheringBar(float GatheringBarPositionX, float GatheringBarPositionY)
    {
        GameObject GatheringUIGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_GATHERING_BAR, transform);
        GatheringUIGo.transform.localPosition = new Vector3(0, 0.7f, 0);
        GatheringUIGo.name = "UI_GatheringBar";

        _GatheringBar = GatheringUIGo.GetComponent<UI_GatheringBar>();
        _GatheringBar.Init(GatheringBarPositionX, GatheringBarPositionY);
    }

    public void OnDamaged()
    {
        OnHit?.Invoke();
    }

    public void UpdateHPBar()
    {
        //_HPBar가 null이면 리턴
        if (_HPBarUI == null)
        {
            return;
        }

        //HP비율 계산
        float HPRatio = 0.0f;
        if (StatInfo.MaxHP > 0)
        {
            HPRatio = ((float)_HP) / StatInfo.MaxHP;
        }

        //변경된 HP적용
        _HPBarUI.SetHPBar(HPRatio);
    }   

    public void EquipmentBoxUICreate()
    {
        GameObject EquipmentBoxGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_EQUIPMENT_BOX, _GameSceneUI.transform);
        _EquipmentBoxUI = EquipmentBoxGO.GetComponent<UI_EquipmentBox>();
        _EquipmentBoxUI.GetComponent<RectTransform>().localPosition = new Vector3(-600.0f, 100.0f, 0.0f);
        _EquipmentBoxUI.Binding();
        _EquipmentBoxUI._OwnerObject = this;

        _EquipmentBoxUI.EquipmentBoxUICreate(6);        

        _GameSceneUI._EquipmentBoxUI = _EquipmentBoxUI;
        _GameSceneUI._EquipmentBoxUI.ShowCloseUI(false);

        EquipmentBoxCreate();
    }

    public void EquipmentBoxCreate()
    {
        _EquipmentBox.Init(this);
    }

    public void EquipmentBoxUIItemCreate(st_ItemInfo[] EquipmentItemInfos)
    {
        foreach (st_ItemInfo EquipmentItemInfo in EquipmentItemInfos)
        {
            _EquipmentBox.OnEquipmentItem(EquipmentItemInfo);            
        }                
    }

    public void SkillBoxUICreate()
    {
        GameObject SkillBoxGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_BOX, _GameSceneUI.transform);
        _SkillBoxUI = SkillBoxGO.GetComponent<UI_SkillBox>();
        _SkillBoxUI.Binding();

        _GameSceneUI._SkillBoxUI = _SkillBoxUI;        
    }

    public void CreatureShowClose(bool IsShowClose)
    {
        CreatureSpriteShowClose(IsShowClose);
        CreatureObjectWeaponShowClose(IsShowClose);
        CreatureObjectNameShowClose(IsShowClose);
        CreatureObjectHPBarShowClose(IsShowClose);
    }    

    public void CreatureObjectWeaponShowClose(bool IsShowClose)
    {
        if(_EquipmentBox != null)
        {
            switch (_GameObjectInfo.ObjectPositionInfo.State)
            {
                case en_CreatureState.ROOTING:
                case en_CreatureState.DEAD:
                    _EquipmentBox.WeaponItemShowClose(false);
                    break;
                default:
                    _EquipmentBox.WeaponItemShowClose(IsShowClose);
                    break;
            }
        }        
    }    

    public void CreatureObjectNameShowClose(bool IsShowClose)
    {
        if(_NameUI != null)
        {
            _NameUI.gameObject.SetActive(IsShowClose);
        }        
    }

    public void CreatureObjectHPBarShowClose(bool IsShowClose)
    {
        if(_HPBarUI != null)
        {
            _HPBarUI.gameObject.SetActive(IsShowClose);
        }        
    }           

    public void S2C_InventoryInsertItem(st_ItemInfo[] InventoryItems, long Coin)
    {
        foreach(st_ItemInfo InventoryItem in InventoryItems)
        {
            _Inventory.PlaceItem(InventoryItem, InventoryItem.ItemTileGridPositionX, InventoryItem.ItemTileGridPositionY);   
        }       
    }

    public void SkillCastingStart(string SkillCastingName, float SkillCastingTime, float SkillCastingSpeed)
    {
        if (_SkillCastingBarUI != null)
        {
            _SkillCastingBarUI.SkillCastingStart(SkillCastingName, SkillCastingTime, SkillCastingSpeed);
        }
        else
        {
            Debug.Log("SkillCasting Bar를 찾을 수 없습니다.");
        }
    }

    IEnumerator SpawnEventCoroutine()
    {
        yield return new WaitForSeconds(0.1f);        
        OnSpawnEvent?.Invoke();             
    }
}
