using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillItem : UI_Base
{
    UI_SkillBox _SkillBox;    
    private st_SkillInfo _SkillInfo;
    UI_GameScene _GameSceneUI;

    enum en_SkillItemImage
    {
        SkillImage
    }
        
    enum en_SkillItemGameObject
    {
        SkillItemSlot
    }

    public override void Init()
    {
        
    }

    public override void Binding()
    {
        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        _SkillBox = _GameSceneUI._SkillBoxUI;

        Bind<Image>(typeof(en_SkillItemImage));
        Bind<GameObject>(typeof(en_SkillItemGameObject));

        BindEvent(GetImage((int)en_SkillItemImage.SkillImage).gameObject, OnSkillItemClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetImage((int)en_SkillItemImage.SkillImage).gameObject, OnSkillItemPointerEnter, Define.en_UIEvent.PointerEnter);
        BindEvent(GetImage((int)en_SkillItemImage.SkillImage).gameObject, OnSkillItemPointerExit, Define.en_UIEvent.PointerExit);

        BindEvent(GetImage((int)en_SkillItemImage.SkillImage).gameObject, OnSkillItemDragBegin, Define.en_UIEvent.BeginDrag);
        BindEvent(GetImage((int)en_SkillItemImage.SkillImage).gameObject, OnSkillItemDrag, Define.en_UIEvent.Drag);
        BindEvent(GetImage((int)en_SkillItemImage.SkillImage).gameObject, OnSkillItemDragEnd, Define.en_UIEvent.EndDrag);
    }
    
    public override void ShowCloseUI(bool IsShowClose)
    {
        
    }


    public void SetSkillInfo(st_SkillInfo SkillInfo)
    {
        _SkillInfo = SkillInfo;

        Sprite SkillImage = Managers.Sprite._SkillSprite[_SkillInfo.SkillType];
        
        GetImage((int)en_SkillItemImage.SkillImage).sprite = SkillImage;

        if (_SkillInfo.IsSkillLearn == true)
        {
            GetImage((int)en_SkillItemImage.SkillImage).color = new Color32(255, 255, 255, 255);
        }
        else
        {
            GetImage((int)en_SkillItemImage.SkillImage).color = new Color32(128, 128, 128, 255);
        }
    }

    private void OnSkillItemClick(PointerEventData ClickEvent)
    {
        if(ClickEvent.button == PointerEventData.InputButton.Left)
        {
            CMessage ReqSkillLearnPacket = Packet.MakePacket.ReqSkillLearnPacket(Managers.NetworkManager._AccountId,
                Managers.NetworkManager._PlayerDBId,
                true,
                _SkillBox._SelectSkilCharacteristic._SkillChracteristicIndex,
                _SkillInfo.SkillCharacteristic,
                _SkillInfo.SkillType);
            Managers.NetworkManager.GameServerSend(ReqSkillLearnPacket);
        }

        if(ClickEvent.button == PointerEventData.InputButton.Right)
        {
            CMessage ReqSkillLearnPacket = Packet.MakePacket.ReqSkillLearnPacket(Managers.NetworkManager._AccountId,
                Managers.NetworkManager._PlayerDBId,
                false,
                _SkillBox._SelectSkilCharacteristic._SkillChracteristicIndex,
                _SkillInfo.SkillCharacteristic,
                _SkillInfo.SkillType);
            Managers.NetworkManager.GameServerSend(ReqSkillLearnPacket);
        }
    }

    private void OnSkillItemPointerEnter(PointerEventData PointerEnterEvent)
    {
        // 스킬 창에서 스킬을 드래그 중이라면 스킬 설명을 표시하지 않는다.
        if(_GameSceneUI._DragSkillItemUI.gameObject.active == false)
        {
            _GameSceneUI.SetSkillExplanation(_SkillInfo);
        }                
    }

    private void OnSkillItemPointerExit(PointerEventData PointerEnterEvent)
    {        
        _GameSceneUI.EmptySkillExplanation();        
    }
    
    private void OnSkillItemDragBegin(PointerEventData DragBeginEvent)
    {
        if(_SkillInfo.IsSkillLearn == false)
        {
            return;
        }

        //Debug.Log("스킬 창에서 스킬 드래그 시작");
        _GameSceneUI.EmptySkillExplanation();

        //드래그 스킬 더미 아이템 활성화
        _GameSceneUI._DragSkillItemUI.ShowCloseUI(true);
        // 스킬 정보 셋팅
        _GameSceneUI._DragSkillItemUI._DragSkillInfo = _SkillInfo;
        // Raycast 비활성화
        _GameSceneUI._DragSkillItemUI.SetSkillItemDragRaycast(false);
        // 이미지 로딩
        _GameSceneUI._DragSkillItemUI.DragSkillItemSet(Managers.Sprite._SkillSprite[_GameSceneUI._DragSkillItemUI._DragSkillInfo.SkillType]);
        // 시작 좌표 설정
        _GameSceneUI._DragSkillItemUI.GetComponent<RectTransform>().transform.position = Input.mousePosition;
    }

    private void OnSkillItemDrag(PointerEventData DragEvent)
    {
        //Debug.Log("스킬 창에서 스킬 드래그 중");
        if (_SkillInfo != null && _SkillInfo.SkillType == en_SkillType.SKILL_TYPE_NONE)
        {
            return;
        }

        _GameSceneUI._DragSkillItemUI.GetComponent<RectTransform>().anchoredPosition += DragEvent.delta;
    }

    private void OnSkillItemDragEnd(PointerEventData DragEndEvent)
    {
        //Debug.Log("스킬 창에서 스킬 드래그 끝");

        _GameSceneUI._DragSkillItemUI.ShowCloseUI(false);
        _GameSceneUI._DragSkillItemUI.SetSkillItemDragRaycast(true);

        _GameSceneUI._DragSkillItemUI._DragSkillInfo = null;
    }
}
