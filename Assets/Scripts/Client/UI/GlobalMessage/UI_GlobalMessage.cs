using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GlobalMessage : UI_Scene
{   
    public en_GlobalMessageType _GlobalMessageType = en_GlobalMessageType.PERSONAL_MESSAGE_NONE;

    enum en_GlobalMessageText
    {
        GlobalMessageText
    }

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(en_GlobalMessageText));
    }

    public void SetGlobalMessage(en_GlobalMessageType PersonalMessageType, string PersonalMessage)
    {
        _GlobalMessageType = PersonalMessageType;

        GetTextMeshPro((int)en_GlobalMessageText.GlobalMessageText).text = PersonalMessage;

        StartCoroutine(GlobalMessageUIDestory());
    }
   
    IEnumerator GlobalMessageUIDestory()
    {
        yield return new WaitForSeconds(0.5f);

        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if (GameSceneUI != null)
        {
            UI_GlobalMessageBox GlobalMessageBoxUI = GameSceneUI._GlobalMessageBoxUI;
            if(GlobalMessageBoxUI != null)
            {
                GlobalMessageBoxUI.GlobalMessageDestory(this);
            }
        }

        UI_LoginScene LoginSceneUI = Managers.UI._SceneUI as UI_LoginScene;
        if(LoginSceneUI != null)
        {
            UI_GlobalMessageBox GlobalMessageBoxUI = LoginSceneUI._GlobalMessageBoxUI;
            if (GlobalMessageBoxUI != null)
            {
                GlobalMessageBoxUI.GlobalMessageDestory(this);
            }
        }
    }
}
