using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_GameScene : UI_Scene
{
    public GameObject _Object;
    public UI_EquipmentBox _EquipmentBoxUI { get; set; } // 장비창 UI    
    public UI_InventoryItemDivide _InventoryItemDivideUI { get; set; } // 아이템 분배 UI
    public UI_TargetHUD _TargetHUDUI { get; set; } // Target HUD UI
    public UI_ChattingBoxGroup _ChattingBoxGroup { get; private set; } // 채팅창 UI
    public UI_Minimap _Minimap { get; private set; } // 미니맵 UI
    public UI_ItemGainBox _ItemGainBox { get; private set; } // 아이템 얻기 Box UI
    public UI_SkillBox _SkillBoxUI { get; set; } // 스킬창 UI        
    public UI_QuickSlotBarBox _QuickSlotBarBoxUI { get; private set; } // 퀵슬롯 Bar UI
    public UI_MyCharacterHUD _MyCharacterHUDUI { get; set; } // 내 캐릭터 HUD UI    
    public UI_PlayerExperience _PlayerExperienceUI { get; private set; } // Player 경험치 UI
    public UI_SkillExplanation _SkillExplanationUI { get; private set; } // 스킬 설명문 UI
    public UI_ItemExplanation _ItemExplanationUI { get; private set; } // 아이템 설명문 UI
    public UI_Furnace _FurnaceUI { get; private set; } // 용광로 UI
    public UI_Sawmill _SawmillUI { get; private set; } // 제재소 UI         
    public UI_Option _OptionUI { get; private set; } // 옵션 UI    
    // GameScene에 현재 열려잇는 UI를 관리할 배열
    // 모든 UI는 GameSceneUIList에 저장될때 저장되는 인덱스 번호를 가진다.
    // 할당된 인덱스 번호를 기준으로 해서 UI를 닫을때 _GameSceneUIList에서 제거한다.
    private UI_Base[] _GameSceneUIList = new UI_Base[Constants.GameSceneUIListMax];

    public UI_SkillItemDrag _DragSkillItemUI { get; private set; }
    public UI_InventoryItemDrag _DragItemUI { get; private set; }
    public UI_QuickSlotItemDrag _DragQuickSlotItemUI { get; private set; }
    public UI_PlayerOption _PlayerOptionUI { get; private set; }
    public UI_PartyFrame _PartyFrameUI { get; private set; }
    public UI_PartyPlayerOption _PartyPlayerOptionUI { get; private set; }
    public UI_PartyReaction _PartyReactionUI { get; private set; }
    public UI_QuickSlotKey _QuickSlotKeyUI { get; private set; }
    public UI_Interaction _InteractionUI { get; private set; }
    public UI_Menu _MenuUI { get; private set; }
    public UI_Building _BuildingUI { get; private set; }

    public override void ShowCloseUI(bool IsShowClose)
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
        base.Init();

        _Object = new GameObject();
        if (_Object != null)
        {
            _Object.name = "@Object";
        }
    }

    public override void Binding()
    {
        _ChattingBoxGroup = GetComponentInChildren<UI_ChattingBoxGroup>();

        _Minimap = GetComponentInChildren<UI_Minimap>();
        _Minimap.Binding();

        _ItemGainBox = GetComponentInChildren<UI_ItemGainBox>();
        _ItemGainBox.Binding();

        _QuickSlotBarBoxUI = GetComponentInChildren<UI_QuickSlotBarBox>();        

        _PlayerExperienceUI = GetComponentInChildren<UI_PlayerExperience>();
        _PlayerExperienceUI.Binding();

        UI_GlobalMessageBox GlobalMessageBox = GetComponentInChildren<UI_GlobalMessageBox>();
        if (GlobalMessageBox != null)
        {
            GlobalMessageBox.Binding();
        }

        GameObject SkillExplanationGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_EXPLANATION, this.transform);
        _SkillExplanationUI = SkillExplanationGO.GetComponent<UI_SkillExplanation>();

        GameObject ItemExplanationGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_ITEM_EXPLANATION, this.transform);
        _ItemExplanationUI = ItemExplanationGO.GetComponent<UI_ItemExplanation>();
        _ItemExplanationUI.Binding();

        GameObject FurnaceGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_FURNACE, this.transform);
        _FurnaceUI = FurnaceGO.GetComponent<UI_Furnace>();
        _FurnaceUI.Binding();

        GameObject SamillGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SAWMILL, this.transform);
        _SawmillUI = SamillGO.GetComponent<UI_Sawmill>();
        _SawmillUI.Binding();

        GameObject GridInventoryItemDivideGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_ITEM_DIVIDE, this.transform);
        _InventoryItemDivideUI = GridInventoryItemDivideGO.GetComponent<UI_InventoryItemDivide>();
        _InventoryItemDivideUI.Binding();

        GameObject OptionGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_OPTION, this.transform);
        _OptionUI = OptionGO.GetComponent<UI_Option>();
        _OptionUI.Binding();

        GameObject DragSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM_DRAG, this.transform);
        _DragSkillItemUI = DragSkillItemGO.GetComponent<UI_SkillItemDrag>();

        GameObject DragItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INVENTORY_ITEM_DRAG, this.transform);
        _DragItemUI = DragItemGO.GetComponent<UI_InventoryItemDrag>();

        GameObject DragQuickslotItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_QUICK_SLOT_ITEM_DRAG, this.transform);
        _DragQuickSlotItemUI = DragQuickslotItemGO.GetComponent<UI_QuickSlotItemDrag>();

        GameObject MyCharacterHUDGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_MY_CHARACTER_HUD, this.transform);
        _MyCharacterHUDUI = MyCharacterHUDGO.GetComponent<UI_MyCharacterHUD>();
        _MyCharacterHUDUI.Binding();

        GameObject TargetHUDGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_TARGET_HUD, this.transform);
        _TargetHUDUI = TargetHUDGO.GetComponent<UI_TargetHUD>();
        _TargetHUDUI.Binding();
        _TargetHUDUI.TargetHUDOff();

        GameObject PlayerOptionGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_PLAYER_OPTION, this.transform);
        _PlayerOptionUI = PlayerOptionGO.GetComponent<UI_PlayerOption>();
        _PlayerOptionUI.Binding();

        GameObject PartyFrameGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_PARTY, this.transform);
        _PartyFrameUI = PartyFrameGO.GetComponent<UI_PartyFrame>();
        _PartyFrameUI.Binding();

        GameObject PartyPlayerOption = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_PARTY_PLAYER_OPTION, this.transform);
        _PartyPlayerOptionUI = PartyPlayerOption.GetComponent<UI_PartyPlayerOption>();
        _PartyPlayerOptionUI.Binding();

        GameObject PartyReactionGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_PARTY_REACTION, this.transform);
        _PartyReactionUI = PartyReactionGO.GetComponent<UI_PartyReaction>();
        _PartyReactionUI.Binding();

        GameObject QuickSlotKeyGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_QUICK_SLOT_KEY, this.transform);
        _QuickSlotKeyUI = QuickSlotKeyGO.GetComponent<UI_QuickSlotKey>();
        _QuickSlotKeyUI.Binding();

        GameObject InteractionGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INTERACTION, this.transform);
        _InteractionUI = InteractionGO.GetComponent<UI_Interaction>();
        _InteractionUI.Binding();

        GameObject MenuUIGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_MENU, this.transform);
        _MenuUI = MenuUIGO.GetComponent<UI_Menu>();
        _MenuUI.Binding();

        GameObject BuildingUIGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_MENU_BUILDING, this.transform);
        _BuildingUI = BuildingUIGO.GetComponent<UI_Building>();
        _BuildingUI.Binding();

        Camera MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        MainCamera.orthographicSize = 6;
    }

    public void SetSkillExplanation(st_SkillInfo SkillInfo)
    {
        _SkillExplanationUI.gameObject.transform.SetAsLastSibling();

        _SkillExplanationUI.SkillExplanationSet(SkillInfo);
        _SkillExplanationUI.ShowCloseUI(true);

        _SkillExplanationUI.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void EmptySkillExplanation()
    {
        _SkillExplanationUI.ShowCloseUI(false);
    }

    public void SetItemExplanation(st_ItemInfo ItemInfo)
    {
        _ItemExplanationUI.gameObject.transform.SetAsLastSibling();

        _ItemExplanationUI.ItemExplanationSet(ItemInfo);
        _ItemExplanationUI.ShowCloseUI(true);

        _ItemExplanationUI.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void EmptyItemExplanation()
    {
        _ItemExplanationUI.ShowCloseUI(false);
    }

    public void SetItemDivideUI(st_ItemInfo ItemInfo)
    {
        _InventoryItemDivideUI.gameObject.transform.SetAsLastSibling();

        _InventoryItemDivideUI.SetInventoryItemDivideIteminfo(ItemInfo);
        _InventoryItemDivideUI.ShowCloseUI(true);

        _InventoryItemDivideUI.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void AddGameSceneUIStack(UI_Base AddUI)
    {
        AddUI.ShowCloseUI(true);
        AddUI.gameObject.transform.SetAsLastSibling();

        for (int EmptyIndex = 0; EmptyIndex < Constants.GameSceneUIListMax; EmptyIndex++)
        {
            if (_GameSceneUIList[EmptyIndex] == null)
            {
                _GameSceneUIList[EmptyIndex] = AddUI;
                AddUI._SceneUIListIndex = EmptyIndex;
                break;
            }
        }
    }

    public void DeleteGameSceneUIStack(UI_Base DeleteUI)
    {
        _GameSceneUIList[DeleteUI._SceneUIListIndex] = null;

        DeleteUI.ShowCloseUI(false);
    }

    public bool IsGameSceneUIStackEmpty()
    {
        for (int i = 0; i < Constants.GameSceneUIListMax; ++i)
        {
            if (_GameSceneUIList[i] != null)
            {
                return false;
            }
        }

        return true;
    }

    public UI_Base FindGameSceneUIStack()
    {
        for (int i = 0; i < Constants.GameSceneUIListMax; ++i)
        {
            if (_GameSceneUIList[i] != null)
            {
                return _GameSceneUIList[i].GetComponent<UI_Base>();
            }
        }

        return null;
    }

    private void Update()
    {
        if (_SkillExplanationUI != null && _SkillExplanationUI.gameObject.activeSelf == true)
        {
            _SkillExplanationUI.GetComponent<RectTransform>().position = Input.mousePosition;
        }

        if (_ItemExplanationUI != null && _ItemExplanationUI.gameObject.activeSelf == true)
        {
            _ItemExplanationUI.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}
