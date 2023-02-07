using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_GlobalMessageBox : UI_Scene
{
    public Dictionary<en_GlobalMessageType, UI_GlobalMessageText> _GlobalMessages = new Dictionary<en_GlobalMessageType, UI_GlobalMessageText>();

    enum en_GlobalMessageBoxGameObject
    {
        GlobalMessagePannel
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(en_GlobalMessageBoxGameObject));
    }

    public void NewStatusAbnormalMessage(en_GlobalMessageType PersonalMessageType, string StatusAbnormalMessage)
    {
        // 내부적으로 관리중인 사전에 데이터가 있는지 확인
        if (_GlobalMessages.Count > 0)
        {
            // 데이터가 있을 경우 같은 타입의 메세지가 있는지 확인
            UI_GlobalMessageText FindPersonalMessage = _GlobalMessages.Values
            .FirstOrDefault(FindStatusAbnormalMessageUI => FindStatusAbnormalMessageUI._GlobalMessage == PersonalMessageType);
            if (FindPersonalMessage != null)
            {
                // 같은 타입의 메세지가 있으면 이전 메세지를 삭제하고 추가적으로 생성해서 메세지를 갱신
                UI_GlobalMessageText PreviousMessage;
                _GlobalMessages.TryGetValue(PersonalMessageType, out PreviousMessage);
                Destroy(PreviousMessage.gameObject);
                _GlobalMessages.Remove(PersonalMessageType);
                                
                GameObject NewPersonalMessageGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_GLOBAL_MESSAGE,
                Get<GameObject>((int)en_GlobalMessageBoxGameObject.GlobalMessagePannel).transform);

                UI_GlobalMessageText StatusAbnormalMessageUI = NewPersonalMessageGo.GetComponent<UI_GlobalMessageText>();
                StatusAbnormalMessageUI.SetGlobalMessage(PersonalMessageType, StatusAbnormalMessage);
                _GlobalMessages.Add(PersonalMessageType, StatusAbnormalMessageUI);
            }
            else
            {
                // 같은 타임의 메세지가 없으면 메세지 생성해서 저장
                GameObject NewPersonalMessageGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_GLOBAL_MESSAGE,
                Get<GameObject>((int)en_GlobalMessageBoxGameObject.GlobalMessagePannel).transform);

                UI_GlobalMessageText StatusAbnormalMessageUI = NewPersonalMessageGo.GetComponent<UI_GlobalMessageText>();
                StatusAbnormalMessageUI.SetGlobalMessage(PersonalMessageType, StatusAbnormalMessage);
                _GlobalMessages.Add(PersonalMessageType, StatusAbnormalMessageUI);
            }
        }
        else
        {
            // 사전에 아무것도 없을 경우 메세지 생성해서 저장
            GameObject NewPersonalMessageGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_GLOBAL_MESSAGE,
                Get<GameObject>((int)en_GlobalMessageBoxGameObject.GlobalMessagePannel).transform);

            UI_GlobalMessageText StatusAbnormalMessageUI = NewPersonalMessageGo.GetComponent<UI_GlobalMessageText>();
            StatusAbnormalMessageUI.SetGlobalMessage(PersonalMessageType, StatusAbnormalMessage);
            _GlobalMessages.Add(PersonalMessageType, StatusAbnormalMessageUI);
        }
    }
}
