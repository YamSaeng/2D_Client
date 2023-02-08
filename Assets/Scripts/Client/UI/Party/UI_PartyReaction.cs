using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PartyReaction : UI_Base
{
    private long _ReqPartyPlayerObjectID;

    enum en_PartyReactionButton
    {
        PartyAcceptButton,
        PartyRejectButton
    }

    enum en_PartyReactionText
    {
        PartyReactionBodyText
    }

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<Button>(typeof(en_PartyReactionButton));
        Bind<TextMeshProUGUI>(typeof(en_PartyReactionText));

        BindEvent(GetButton((int)en_PartyReactionButton.PartyAcceptButton).gameObject, OnPartyAcceptButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_PartyReactionButton.PartyRejectButton).gameObject, OnPartyRejectButtonClick, Define.en_UIEvent.MouseClick);

        ShowCloseUI(false);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    private void OnPartyAcceptButtonClick(PointerEventData Event)
    {
        CMessage ReqPartyInviteAcceptPacket = Packet.MakePacket.ReqMakePartyInviteAcceptPacket(_ReqPartyPlayerObjectID);
        Managers.NetworkManager.GameServerSend(ReqPartyInviteAcceptPacket);

        ShowCloseUI(false);
    }

    private void OnPartyRejectButtonClick(PointerEventData Event)
    {
        CMessage ReqPartyInviteAcceptPacket = Packet.MakePacket.ReqMakePartyInviteRejectPacket(_ReqPartyPlayerObjectID);
        Managers.NetworkManager.GameServerSend(ReqPartyInviteAcceptPacket);

        ShowCloseUI(false);
    }

    public void SetPartyReactionBodytext(string ReqPartyPlayerName, long ReqPartyPlayerObjectID)
    {
        _ReqPartyPlayerObjectID = ReqPartyPlayerObjectID;

        GetTextMeshPro((int)en_PartyReactionText.PartyReactionBodyText).text = ReqPartyPlayerName + "이\n 그룹에 초대 했습니다.\n 수락하시겠습니까 ?";
    }
}
