using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//------------------------------------
// 채팅 박스 UI
//------------------------------------
public class UI_ChattingBox : UI_Scene
{
    public int _ChattingBoxIndex;
    // 표시할 채팅 메세지 최대 개수
    int MaxMessage = 30;

    // 채팅 메세지
    public class CChattingMessage
    {
        public TextMeshProUGUI ChattingTextObject;             
    }   

    // 내부적으로 보관할 채팅메세지 배열
    List<CChattingMessage> ChattingMessageList;    
        
    enum en_ChattingBoxGameObject
    {
        ChattingPannel, // 생성한 채팅메세지를 자식으로 붙일 패널
        ChattingBox,
        ChattingViewSubject
    }

    enum en_ChattingBoxButton
    {
        ChattingSubjectButton
    }

    enum en_ChattingBoxText
    {
        ChattingSubjectText
    }

    public List<en_MessageType> _ChattingBoxProperty = new List<en_MessageType>();

    public override void Init()
    {
        ChattingMessageList = new List<CChattingMessage>();
                
        Bind<GameObject>(typeof(en_ChattingBoxGameObject));
        Bind<Button>(typeof(en_ChattingBoxButton));
        Bind<TextMeshProUGUI>(typeof(en_ChattingBoxText));

        BindEvent(GetButton((int)en_ChattingBoxButton.ChattingSubjectButton).gameObject, OnChattingBoxSubjectButtonClick, Define.en_UIEvent.MouseClick);
        
    }

    public void SetChattingMessage(string SendUser, string ChattingText, en_MessageType MessageType, st_Color MessageColor)
    {
        // 채팅메세지 개수가 최대 갯수 이상이면
        if (ChattingMessageList.Count >= MaxMessage)
        {
            // 맨 앞에 있는 채팅 메세지를 삭제한다.
            Destroy(ChattingMessageList[0].ChattingTextObject.gameObject);
            ChattingMessageList.Remove(ChattingMessageList[0]);
        }

        // 채팅메세지를 새로 생성한다.
        CChattingMessage ChattingMessage = new CChattingMessage();
        ChattingMessage.ChattingTextObject = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_CHATTING_TEXT, Get<GameObject>((int)en_ChattingBoxGameObject.ChattingPannel).transform).GetComponent<TextMeshProUGUI>();

        Color32 Color = new Color32();
        Color.r = (byte)MessageColor._Red;
        Color.g = (byte)MessageColor._Green;
        Color.b = (byte)MessageColor._Blue;
        Color.a = 255;

        //-----------------------------------------------
        // 선택되지 않은 채팅창은 숨겨줘야하는 작업 필요 
        //-----------------------------------------------

        // 메세지 타입에 따라 색을 다르게 지정해서 채팅메세지를 셋팅한다.
        switch (MessageType)
        {
            case en_MessageType.MESSAGE_TYPE_CHATTING:
                string CommonText = SendUser + " : " + ChattingText;
                ChattingMessage.ChattingTextObject.color = Color;
                ChattingMessage.ChattingTextObject.text = CommonText;
                break;
            case en_MessageType.MESSAGE_TYPE_SYSTEM:
                string SystemText = ChattingText;
                ChattingMessage.ChattingTextObject.color = Color;
                ChattingMessage.ChattingTextObject.text = SystemText;
                break;
        }

        // 배열에 추가한다.
        ChattingMessageList.Add(ChattingMessage);
    }

    public void SetDamageChattingMessage(string AttackerName, string TargetName, en_SkillType SkillType, int Damage)
    {
        // 채팅메세지 개수가 최대 갯수 이상이면
        if (ChattingMessageList.Count >= MaxMessage)
        {
            // 맨 앞에 있는 채팅 메세지를 삭제한다.
            Destroy(ChattingMessageList[0].ChattingTextObject.gameObject);
            ChattingMessageList.Remove(ChattingMessageList[0]);
        }

        // 채팅메세지를 새로 생성한다.
        CChattingMessage DamageChattingMessage = new CChattingMessage();
        DamageChattingMessage.ChattingTextObject = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_CHATTING_TEXT, Get<GameObject>((int)en_ChattingBoxGameObject.ChattingPannel).transform).GetComponent<TextMeshProUGUI>();      

        string CommonText = AttackerName + "이 " + Managers.String._SkillString[SkillType] + "을 사용해" + TargetName + "에게 " + Damage.ToString() + "의 데미지를 줬습니다.";
        DamageChattingMessage.ChattingTextObject.text = CommonText;        

        // 배열에 추가한다.
        ChattingMessageList.Add(DamageChattingMessage);
    }

    void OnChattingBoxSubjectButtonClick(PointerEventData Event)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if(GameSceneUI == null)
        {
            return;
        }

        GameSceneUI._ChattingBoxGroup.SelectChattingBox(_ChattingBoxIndex);        
    }

    public void ChattingScrollBoxActive(bool Active)
    {
        GetGameObject((int)en_ChattingBoxGameObject.ChattingBox).gameObject.SetActive(Active);
    }

    public GameObject GetChattingViewSubject()
    {
        return GetGameObject((int)en_ChattingBoxGameObject.ChattingViewSubject).gameObject;
    }

    public void SetChattingBoxName(string BoxName)
    {
        GetTextMeshPro((int)en_ChattingBoxText.ChattingSubjectText).text = BoxName;
    }
}
