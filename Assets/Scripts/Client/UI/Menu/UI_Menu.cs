using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Menu : UI_Base
{

    enum en_MenuButtonType
    {
        BulidingButton
    }


    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<Button>(typeof(en_MenuButtonType));

        BindEvent(GetButton((int)en_MenuButtonType.BulidingButton).gameObject, OnBuildingButtonClick, Define.en_UIEvent.MouseClick);

        ShowCloseUI(true);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    void OnBuildingButtonClick(PointerEventData Event)
    {

    }
}
