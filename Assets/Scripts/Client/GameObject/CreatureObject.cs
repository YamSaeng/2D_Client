using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureObject : BaseObject
{
    [HideInInspector]
    public GameObject SpeechBubblePosition;
    [HideInInspector]
    public UI_SpeechBubble _SpeechBubbleUI;
    
    [HideInInspector]
    public UI_HPBar _HpBar;
    [HideInInspector]
    public UI_Name _NameUI;

    [HideInInspector]
    public UI_SpellBar _SpellBar;
    [HideInInspector]
    public UI_GatheringBar _GatheringBar;

    [HideInInspector]
    public UI_Inventory _InventoryUI { get; set; } // 가방 UI

    [HideInInspector]
    public LineRendererController _LineRendererController;

    // 내가 선택한 오브젝트 정보
    public st_GameObjectInfo _SelectTargetObjectInfo;

    public override void Init()
    {
        base.Init();

        _LineRendererController = GetComponent<LineRendererController>();
        if (_LineRendererController != null)
        {
            _LineRendererController.SetUpOwnPlayer(this);
        }

        if (gameObject.transform.Find("SpeechBubblePosition") != null)
        {
            SpeechBubblePosition = gameObject.transform.Find("SpeechBubblePosition").gameObject;
        }

        switch (_GameObjectInfo.ObjectType)
        {
            case en_GameObjectType.OBJECT_PLAYER:
                _SpeechBubbleUI = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SPEECH_BUBBLE, SpeechBubblePosition.transform).GetComponent<UI_SpeechBubble>();
                _SpeechBubbleUI.SetOwner(this);
                _SpeechBubbleUI.ShowCloseUI(false);
                break;
        }
    }

    //체력바 추가 ( 오브젝트 따위는 추가 안하기 위해 함수로 뺌 )
    public void AddHPBar(float HPBarPositionX, float HPBarPositionY)
    {
        //유니티에서 생성한 프리팹 HPBar 가져와서 복제
        //기본 위치는 소환한 크리처 좌표보다 약간 위쪽으로        
        GameObject HPBarGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_HP_BAR, transform);
        HPBarGo.transform.localPosition = new Vector3(0, 0.6f, 0);
        HPBarGo.name = "UI_HPBar";

        //체력 업데이트 하기 위해서 스크립트 추출해서 넣어둠
        _HpBar = HPBarGo.GetComponent<UI_HPBar>();
        _HpBar.Init(HPBarPositionX, HPBarPositionY);        

        //체력바 업데이트
        UpdateHPBar();

        _HpBar.gameObject.SetActive(false);
    }

    public void AddSpellBar(float SpellBarPositionX, float SpellBarPositionY)
    {
        GameObject SpellBarGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SPELL_BAR, transform);
        SpellBarGo.transform.localPosition = new Vector3(0, -0.9f, 0);
        SpellBarGo.name = "UI_SpellBar";

        _SpellBar = SpellBarGo.GetComponent<UI_SpellBar>();
        _SpellBar.Init(SpellBarPositionX, SpellBarPositionY);
    }

    public void AddNameBar(float NameBarPositionX, float NameBarPositionY)
    {
        GameObject NameUIGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_NAME, transform);
        NameUIGo.transform.localPosition = new Vector3(0, 0.7f, 0);
        NameUIGo.name = "UI_NameBar";

        _NameUI = NameUIGo.GetComponent<UI_Name>();
        _NameUI.Init(_GameObjectInfo.ObjectName, NameBarPositionX, NameBarPositionY);
    }

    public void AddGatheringBar(float GatheringBarPositionX, float GatheringBarPositionY)
    {
        GameObject GatheringUIGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_GATHERING_BAR, transform);
        GatheringUIGo.transform.localPosition = new Vector3(0, 0.7f, 0);
        GatheringUIGo.name = "UI_GatheringBar";

        _GatheringBar = GatheringUIGo.GetComponent<UI_GatheringBar>();
        _GatheringBar.Init(GatheringBarPositionX, GatheringBarPositionY);
    }

    public void UpdateHPBar()
    {
        //_HPBar가 null이면 리턴
        if (_HpBar == null)
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
        _HpBar.SetHPBar(HPRatio);        
    }

    public void InventoryCreate(int InventoryWidth,
        int InventoryHeight,
        CItem[] InventoryItems,
        long GoldCoinCount, short SliverCoinCount, short BronzeCoinCount)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if(GameSceneUI != null)
        {
            GameObject InventoryBodyGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INVENTORY_BOX, GameSceneUI.transform);
            InventoryBodyGO.GetComponent<RectTransform>().localPosition = new Vector3(600.0f, 100.0f, 0.0f);

            GameObject InentoryEdgeGO = InventoryBodyGO.transform.Find("InventoryEdge").gameObject;
            GameObject InentoryGO = InentoryEdgeGO.transform.Find("Inventory").gameObject;
            UI_Inventory InventorUI = InentoryGO.GetComponent<UI_Inventory>();
            _InventoryUI = InventorUI;
            _InventoryUI.Binding();
            _InventoryUI._InventoryRectTransform = InentoryGO.GetComponent<RectTransform>();

            _InventoryUI.InventoryCreate(InventoryWidth, InventoryHeight);

            for (byte i = 0; i < InventoryItems.Length; i++)
            {
                GameObject InventoryItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INVENTORY_ITEM, GameSceneUI.transform);
                
                UI_InventoryItem InventoryItem = InventoryItemGO.GetComponent<UI_InventoryItem>();
                InventoryItem.GetComponent<RectTransform>().SetAsLastSibling();
                InventoryItem.Set(InventoryItems[i]._ItemInfo);
                InventoryItem.Binding();
                InventoryItem.SetParentGridInventory(_InventoryUI);

                _InventoryUI.PlaceItem(InventoryItem, InventoryItem._ItemInfo.ItemTileGridPositionX, InventoryItem._ItemInfo.ItemTileGridPositionY);                
            }

            _InventoryUI.MoneyUIUpdate(GoldCoinCount, SliverCoinCount, BronzeCoinCount);

            _InventoryUI.ShowCloseUI(false);

            Managers.MyInventory.SelectedInventory = InventorUI;
        }
    }   

    public virtual void OnDamaged()
    {

    }

    public virtual void OnDead()
    {
        //죽음 상태로 바꿔주고
        State = en_CreatureState.DEAD;

        //자리에 이펙트를 출력해준다.
        //GameObject Effect = Managers.Resource.Instantiate("Effect/DieEffect");
        //Effect.transform.position = transform.position;
        //Effect.GetComponent<Animator>().Play("START");        

        GameObject.Destroy(gameObject, 1.5f);
    }
  
    public void SpellStart(string SpellName, float SpellCastingTime, float SpellSpeed)
    {
        if (_SpellBar != null)
        {
            _SpellBar.SpellStart(SpellName, SpellCastingTime, SpellSpeed);
        }
        else
        {
            Debug.Log("Spell Bar를 찾을 수 없습니다.");
        }
    } 
}
