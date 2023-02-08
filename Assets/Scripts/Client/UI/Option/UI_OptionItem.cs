using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_OptionItem : UI_Base
{
    en_OptionType _OptionType;

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

    public void SetOptionItemType(en_OptionType OptionType)
    {
        _OptionType = OptionType;
    }

    private void OnOptionItemClick(PointerEventData Event)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if (GameSceneUI != null)
        {
            switch (_OptionType)
            {
                case en_OptionType.OPTION_TYPE_NONE:                  
                    break;
            }           

            GameSceneUI.DeleteGameSceneUIStack(GameSceneUI._OptionUI);
        }
    }
}
