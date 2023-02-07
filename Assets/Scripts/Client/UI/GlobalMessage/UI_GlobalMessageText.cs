using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GlobalMessageText : UI_Scene
{
    private float Timer = 0;
    private float DestroyTime = 1.0f;

    public en_GlobalMessageType _GlobalMessage = en_GlobalMessageType.PERSONAL_MESSAGE_NONE;

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
        _GlobalMessage = PersonalMessageType;

        GetTextMeshPro((int)en_GlobalMessageText.GlobalMessageText).text = PersonalMessage;
    }

    public void GlobalMessageDestory()
    {
        GetTextMeshPro((int)en_GlobalMessageText.GlobalMessageText).text = null;
        Destroy(gameObject);
    }

    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= DestroyTime)
        {
            UI_GlobalMessageBox GlobalMessageBoxUI;

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                GlobalMessageBoxUI = GameSceneUI._GlobalMessageBoxUI;
                GlobalMessageBoxUI._GlobalMessages.Remove(_GlobalMessage);
                Destroy(gameObject);
            }

            UI_LoginScene LoginSceneUI = Managers.UI._SceneUI as UI_LoginScene;
            if (LoginSceneUI != null)
            {
                GlobalMessageBoxUI = LoginSceneUI._PersonalMessageBoxUI;
                GlobalMessageBoxUI._GlobalMessages.Remove(_GlobalMessage);
                Destroy(gameObject);
            }
        }
    }
}
