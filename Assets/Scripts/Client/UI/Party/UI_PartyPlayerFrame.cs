using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PartyPlayerFrame : UI_Base
{
    private st_GameObjectInfo _PartyPlayerGameObjectInfo;

    public Dictionary<en_SkillType, UI_BufDebufItem> _PartyPlayerBufItems = new Dictionary<en_SkillType, UI_BufDebufItem>();
    public Dictionary<en_SkillType, UI_BufDebufItem> _PartyPlayerDeBufItems = new Dictionary<en_SkillType, UI_BufDebufItem>();

    enum en_PartyPlayerFrameGameObject
    {
        UI_PartyPlayerInfoFrame
    }

    enum en_PartyPlayerFrameSlider
    {
        PartyPlayerHealthBar,
        PartyPlayerManaBar
    }

    enum en_PartyPlayerFrameText
    {
        PartyPlayerNameText,
        CurrentHPText,
        MaxHPText,
        CurrentMPText,
        MaxMPText
    }

    public override void Init()
    {
    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_PartyPlayerFrameGameObject));

        Bind<Slider>(typeof(en_PartyPlayerFrameSlider));
        Bind<TextMeshProUGUI>(typeof(en_PartyPlayerFrameText));

        BindEvent(GetGameObject((int)en_PartyPlayerFrameGameObject.UI_PartyPlayerInfoFrame).gameObject, OnPartyPlayerFrameClick, Define.en_UIEvent.MouseClick);
    }    

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }   

    public st_GameObjectInfo GetPartyPlayerGameObjectInfo()
    {
        return _PartyPlayerGameObjectInfo;
    }

    public void SetPartyPlayerGameObjectInfo(st_GameObjectInfo PartyPlayerGameObjectInfo)
    {
        _PartyPlayerGameObjectInfo = PartyPlayerGameObjectInfo;

        PartyPlayerFramUpdate();
    }

    public void PartyPlayerFramUpdate()
    {
        if(_PartyPlayerGameObjectInfo != null)
        {
            float CurrentPartyPlayerHPRatio = 0.0f;
            float CurrentPartyPlayerMPRatio = 0.0f;

            CurrentPartyPlayerHPRatio = ((float)_PartyPlayerGameObjectInfo.ObjectStatInfo.HP) / _PartyPlayerGameObjectInfo.ObjectStatInfo.MaxHP;
            CurrentPartyPlayerMPRatio = ((float)_PartyPlayerGameObjectInfo.ObjectStatInfo.MP) / _PartyPlayerGameObjectInfo.ObjectStatInfo.MaxMP;

            GetTextMeshPro((int)en_PartyPlayerFrameText.PartyPlayerNameText).text = _PartyPlayerGameObjectInfo.ObjectName;

            GetSlider((int)en_PartyPlayerFrameSlider.PartyPlayerHealthBar).value = CurrentPartyPlayerHPRatio;
            GetTextMeshPro((int)en_PartyPlayerFrameText.CurrentHPText).text = _PartyPlayerGameObjectInfo.ObjectStatInfo.HP.ToString();
            GetTextMeshPro((int)en_PartyPlayerFrameText.MaxHPText).text = _PartyPlayerGameObjectInfo.ObjectStatInfo.MaxHP.ToString();

            GetSlider((int)en_PartyPlayerFrameSlider.PartyPlayerManaBar).value = CurrentPartyPlayerMPRatio;
            GetTextMeshPro((int)en_PartyPlayerFrameText.CurrentMPText).text = _PartyPlayerGameObjectInfo.ObjectStatInfo.MP.ToString();
            GetTextMeshPro((int)en_PartyPlayerFrameText.MaxMPText).text = _PartyPlayerGameObjectInfo.ObjectStatInfo.MaxMP.ToString();
        }
    }   
    
    public void OnPartyPlayerFrameClick(PointerEventData Event)
    {
        if(Event.button == PointerEventData.InputButton.Left)
        {            
            CMessage ReqLeftMousePositionObjectInfoPacket = Packet.MakePacket.ReqMakeLeftMouseWorldObjectInfoPacket(                               
                               _PartyPlayerGameObjectInfo.ObjectId,
                               _PartyPlayerGameObjectInfo.ObjectType);
            Managers.NetworkManager.GameServerSend(ReqLeftMousePositionObjectInfoPacket);
        }
        else if(Event.button == PointerEventData.InputButton.Right)
        {
            UI_GameScene GameScene = Managers.UI._SceneUI as UI_GameScene;
            if(GameScene != null)
            {
                Vector2 localPos = Vector2.zero;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(GameScene.GetComponent<RectTransform>(),
                    Camera.main.WorldToScreenPoint(Input.mousePosition),
                    Camera.main, out localPos);

                GameScene._PartyPlayerOptionUI.PartyPlayerOptionSetPosition(localPos);

                GameScene._PartyPlayerOptionUI.ShowCloseUI(true);
                GameScene._PartyPlayerOptionUI.SetPartyPlayerOptionGameObjectInfo(_PartyPlayerGameObjectInfo);
            }            
        }        
    }
}
