using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Interaction : UI_Base
{
    private en_GameObjectType _ObjectType;
    private long _ObjectID;
    private en_InteractionType _InteractionType;

    enum en_InteractionText
    {
        InteractionInfoText,
        InteractionKeyText
    }

    enum en_InteractionGameObject
    {
        UI_InteractionGameObject,
        InteractionKeySubjectBackGround,
        InteractionKeyBackGround
    }

    public override void Init()
    {
        
    }

    public override void Binding()
    {
        Bind<TextMeshProUGUI>(typeof(en_InteractionText));
        Bind<GameObject>(typeof(en_InteractionGameObject));

        GetComponent<RectTransform>().localPosition = new Vector3(0, -195.0f, 0);

        ShowCloseUI(false);
    }    

    public override void ShowCloseUI(bool IsShowClose)
    {
        GetGameObject((int)en_InteractionGameObject.UI_InteractionGameObject).gameObject.SetActive(IsShowClose);        
    }

    public void SetInteractionInfoText(en_GameObjectType ObjectType, long ObjectID)
    {
        _ObjectType = ObjectType;
        _ObjectID = ObjectID;

        string InteractionInfoString = "";

        switch(ObjectType)
        {
            case en_GameObjectType.OBJECT_GOBLIN:
                _InteractionType = en_InteractionType.INTERACTION_TYPE_ROOTING;

                InteractionInfoString = "고블린 시체";
                break;
        }

        GetTextMeshPro((int)en_InteractionText.InteractionInfoText).text = InteractionInfoString;

        ShowCloseUI(true);
    }

    public void SetInteractionKey(en_KeyCode KeyCode)
    {
        GetTextMeshPro((int)en_InteractionText.InteractionKeyText).text = Managers.String._KeyCodeString[KeyCode];
    }

    public void C2S_InteractionPacketSend()
    {
        CMessage ReqInteractionPacket = Packet.MakePacket.ReqMakeInteractionPacket(_InteractionType, _ObjectType, _ObjectID);
        Managers.NetworkManager.GameServerSend(ReqInteractionPacket);
    }

    private void Update()
    {
        GameObject TargetGO = Managers.Object.FindById(_ObjectID);
        if(TargetGO != null)
        {
            GameObject PlayerGO = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
            if(PlayerGO != null)
            {
                float Distance = Vector2.Distance(TargetGO.transform.position, PlayerGO.transform.position);
                if(Distance > 2)
                {
                    ShowCloseUI(false);
                }
                else
                {
                    UI_GameScene GameSCeneUI = Managers.UI._SceneUI as UI_GameScene;
                    if(GameSCeneUI != null)
                    {
                        if(GameSCeneUI._TargetHUDUI.gameObject.activeSelf == true)
                        {
                            ShowCloseUI(true);
                        }
                    }                    
                }
            }
        }
    }
}
