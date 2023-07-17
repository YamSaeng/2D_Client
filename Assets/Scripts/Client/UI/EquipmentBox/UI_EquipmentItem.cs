using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipmentItem : UI_Base
{
    private st_ItemInfo _EquipmentItemInfo;

    enum en_EquipmentItemImage
    {
        EquipmentItemImage
    }

    public Sprite _InitImage;

    UI_GameScene _GameSceneUI;

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<Image>(typeof(en_EquipmentItemImage));

        BindEvent(GetImage((int)en_EquipmentItemImage.EquipmentItemImage).gameObject, OnEquipmentItemClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetImage((int)en_EquipmentItemImage.EquipmentItemImage).gameObject, OnEquipmentItemPointerEnter, Define.en_UIEvent.PointerEnter);
        BindEvent(GetImage((int)en_EquipmentItemImage.EquipmentItemImage).gameObject, OnEquipmentItemPointerExit, Define.en_UIEvent.PointerExit);

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    private void OnEquipmentItemClick(PointerEventData EquipmentItemClickEvent)
    {
        if(_EquipmentItemInfo != null && EquipmentItemClickEvent.button == PointerEventData.InputButton.Right)
        {
            if(EquipmentItemClickEvent.clickCount == 1)
            {
                CMessage ReqOffEquipmentPacket = Packet.MakePacket.ReqMakeOffEquipmentPacket(Managers.NetworkManager._AccountId,
                    Managers.NetworkManager._PlayerDBId,
                    _EquipmentItemInfo.ItemEquipmentPart);
                Managers.NetworkManager.GameServerSend(ReqOffEquipmentPacket);                
            }
        }
    }

    private void OnEquipmentItemPointerEnter(PointerEventData EquipmentItemClickEvent)
    {
        //if (Managers.Inventory.SelectedItem == null && _EquipmentItemInfo != null)
        //{
        //    _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

        //    _GameSceneUI.SetItemExplanation(_EquipmentItemInfo);
        //}
    }

    private void OnEquipmentItemPointerExit(PointerEventData EquipmentItemClickEvent)
    {
        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

        _GameSceneUI.EmptyItemExplanation();
    }

    public void SetEquipment(st_ItemInfo EquipmentItemInfo)
    {
        _EquipmentItemInfo = EquipmentItemInfo;

        GetImage((int)en_EquipmentItemImage.EquipmentItemImage).sprite = Managers.Sprite._ItemSprite[EquipmentItemInfo.ItemSmallCategory];      
    }

    public void InitEquipmentItemUI()
    {
        _EquipmentItemInfo = null;

        GetImage((int)en_EquipmentItemImage.EquipmentItemImage).sprite = _InitImage;
    }

    public st_ItemInfo GetEquipmentItemInfo()
    {
        return _EquipmentItemInfo;
    }
}

