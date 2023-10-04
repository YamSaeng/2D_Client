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

        UI_GlobalMessageBox GlobalMessageBox = Managers.GameMessage._GlobalMessageBox;
        if(GlobalMessageBox != null)
        {
            GlobalMessageBox.GlobalMessageDestory(this);
        }
    }
}
