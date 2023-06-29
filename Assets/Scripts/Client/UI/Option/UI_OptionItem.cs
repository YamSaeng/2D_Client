using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_OptionItem : UI_Base
{
    en_MenuType _OptionType;

    enum en_OptionItemGameObject
    {
        UI_OptionItemButton
    }

    enum en_OptionItemText
    {
        UI_OptionItemText
    }

    public override void Init()
    {
    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_OptionItemGameObject));
        Bind<TextMeshProUGUI>(typeof(en_OptionItemText));

        BindEvent(GetGameObject((int)en_OptionItemGameObject.UI_OptionItemButton).gameObject, OnOptionItemClick, Define.en_UIEvent.MouseClick);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void SetOptionItemType(en_MenuType OptionType)
    {
        _OptionType = OptionType;

        switch(OptionType)
        {
            case en_MenuType.MENU_TYPE_CHARACTER_CHOICE:
                GetTextMeshPro((int)en_OptionItemText.UI_OptionItemText).text = "캐릭터 선택";
                break;
            case en_MenuType.MENU_TYPE_QUICK_SLOT_KEY_SETTING:
                GetTextMeshPro((int)en_OptionItemText.UI_OptionItemText).text = "단축키 설정";
                break;
        }
    }

    private void OnOptionItemClick(PointerEventData Event)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if (GameSceneUI != null)
        {            
            switch (_OptionType)
            {                
                case en_MenuType.MENU_TYPE_CHARACTER_CHOICE:   
                    CMessage ReqMenuOptionPacket = Packet.MakePacket.ReqMakeMenuOptionPacket(_OptionType);
                    Managers.NetworkManager.GameServerSend(ReqMenuOptionPacket);

                    Managers.NetworkManager._ServerState = en_ServerState.SERVER_STATE_LOGIN_CHARACTER_CHOICE;
                    
                    Managers.Clear();

                    // LoginScene 로딩
                    Managers.Scene.LoadScene(Define.en_Scene.LoginScene);           
                    break;
                case en_MenuType.MENU_TYPE_QUICK_SLOT_KEY_SETTING:
                    GameSceneUI.AddGameSceneUIStack(GameSceneUI._QuickSlotKeyUI);                    
                    GameSceneUI._QuickSlotKeyUI.ShowCloseUI(true);
                    break;
            }            

            GameSceneUI.DeleteGameSceneUIStack(GameSceneUI._OptionUI);
        }
    }    
}
