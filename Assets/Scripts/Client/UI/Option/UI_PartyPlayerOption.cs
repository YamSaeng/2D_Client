using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PartyPlayerOption : UI_Base
{
    private st_GameObjectInfo _PartyPlayerGameObjectInfo;

    enum en_PartyPlayerOptionText
    {
        PartyPlayerOptionNameText
    }

    enum en_PartyPlayerOptionButton
    {
        PlayerOptionPartyQuitButton,
        PlayerOptionPartyBanishButton
    }
    

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<TextMeshProUGUI>(typeof(en_PartyPlayerOptionText));
        Bind<Button>(typeof(en_PartyPlayerOptionButton));

        BindEvent(GetButton((int)en_PartyPlayerOptionButton.PlayerOptionPartyQuitButton).gameObject, OnPlayerQuitButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_PartyPlayerOptionButton.PlayerOptionPartyBanishButton).gameObject, OnPlayerBanishButtonClick, Define.en_UIEvent.MouseClick);

        ShowCloseUI(false);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }
    
    public void PartyPlayerOptionSetPosition(Vector2 NewPosition)
    {
        GetComponent<RectTransform>().localPosition = NewPosition;
    }

    public void SetPartyPlayerOptionGameObjectInfo(st_GameObjectInfo PartyPlayerOptionGameObjectInfo)
    {
        _PartyPlayerGameObjectInfo = PartyPlayerOptionGameObjectInfo;

        PartyPlayerOptionUpdate();

        // 그룹 프레임이 다른 유저의 정보를 저장하고 있을 경우
        if(PartyPlayerOptionGameObjectInfo.ObjectId != Managers.NetworkManager._PlayerDBId)
        {
            GetButton((int)en_PartyPlayerOptionButton.PlayerOptionPartyQuitButton).gameObject.SetActive(false);
            GetButton((int)en_PartyPlayerOptionButton.PlayerOptionPartyBanishButton).gameObject.SetActive(true);
        }
        else
        {
            // 그룹 프레임이 내 정보를 저장하고 있을 경우
            GetButton((int)en_PartyPlayerOptionButton.PlayerOptionPartyQuitButton).gameObject.SetActive(true);
            GetButton((int)en_PartyPlayerOptionButton.PlayerOptionPartyBanishButton).gameObject.SetActive(false);
        }
    }

    public void PartyPlayerOptionUpdate()
    {
        GetTextMeshPro((int)en_PartyPlayerOptionText.PartyPlayerOptionNameText).text = _PartyPlayerGameObjectInfo.ObjectName;
    }

    void OnPlayerQuitButtonClick(PointerEventData Event)
    {
        CMessage ReqPartyQuitPacket = Packet.MakePacket.ReqMakePartyQuitPacket();
        Managers.NetworkManager.GameServerSend(ReqPartyQuitPacket);
    }

    void OnPlayerBanishButtonClick(PointerEventData Event)
    {
        CMessage ReqPartyBanishPacket = Packet.MakePacket.ReqMakePartyBanishPacket(_PartyPlayerGameObjectInfo.ObjectId);
        Managers.NetworkManager.GameServerSend(ReqPartyBanishPacket);
    }
}
