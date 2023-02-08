using ServerCore;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PlayerOption : UI_Base
{
    private st_GameObjectInfo _PlayerGameObjectInfo;

    enum en_PlayerOptionText
    {
        UI_PlayerOptionNameText
    }

    enum en_PlayerOptionButton
    {
        UI_PlayerOptionPartyInviteButton
    }

    public override void Init()
    {
        
    }

    public override void Binding()
    {
        Bind<TextMeshProUGUI>(typeof(en_PlayerOptionText));
        Bind<Button>(typeof(en_PlayerOptionButton));

        BindEvent(GetButton((int)en_PlayerOptionButton.UI_PlayerOptionPartyInviteButton).gameObject, OnPlayerInviteButtonClick, Define.en_UIEvent.MouseClick);

        ShowCloseUI(false);
    }    

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void UIPlayerOptionSetPlayerGameObjectInfo(st_GameObjectInfo PlayerGameObjectInfo)
    {
        _PlayerGameObjectInfo = PlayerGameObjectInfo;

        GetTextMeshPro((int)en_PlayerOptionText.UI_PlayerOptionNameText).text = _PlayerGameObjectInfo.ObjectName;
    }

    void OnPlayerInviteButtonClick(PointerEventData Event)
    {
        if(Managers.NetworkManager._PlayerDBId != _PlayerGameObjectInfo.ObjectId)
        {
            CMessage ReqPartyInvitePacket = Packet.MakePacket.ReqMakePartyInvitePacket(_PlayerGameObjectInfo.ObjectId);
            Managers.NetworkManager.GameServerSend(ReqPartyInvitePacket);

            ShowCloseUI(false);
        }        
    }

    public void PlayerOptionSetPosition(Vector2 NewPosition)
    {
        GetComponent<RectTransform>().localPosition = NewPosition;
    }
}
