using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CharacterChoiceItem : UI_Base
{
    public byte _Index;
    public st_GameObjectInfo _GameObjectInfo { get; set; } = new st_GameObjectInfo();

    enum en_CharacterChoiceItemText
    {
        CharacterName,
        ClassName
    }

    enum en_CharacterChoiceItemButton
    {
        CharacterSelectButton,
        CharacterCreateButton
    }

    public override void Init()
    {
        Bind<Button>(typeof(en_CharacterChoiceItemButton));
        Bind<Text>(typeof(en_CharacterChoiceItemText));

        BindEvent(GetButton((int)en_CharacterChoiceItemButton.CharacterSelectButton).gameObject, OnCharacterSelectButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_CharacterChoiceItemButton.CharacterCreateButton).gameObject, OnCharacterCreateButtonClick, Define.en_UIEvent.MouseClick);

        GetText((int)en_CharacterChoiceItemText.CharacterName).text = "";
        GetText((int)en_CharacterChoiceItemText.ClassName).text = "";        
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        
    }


    // 캐릭터 버튼 정보 출력
    public void CharacterChoicePopupItemRefresh(byte Index)
    {
        if(_GameObjectInfo.ObjectName == null || _GameObjectInfo.ObjectName == "")
        {
            return;
        }

        // 캐릭터 이름 출력
        GetText((int)en_CharacterChoiceItemText.CharacterName).text = _GameObjectInfo.ObjectName;        

        GetButton((int)en_CharacterChoiceItemButton.CharacterCreateButton).gameObject.SetActive(false);
    }

    // 캐릭터 선택창 누르면 GameScene으로 이동
    void OnCharacterSelectButtonClick(PointerEventData Event)
    {
        if(_GameObjectInfo.ObjectId > 0)
        {
            // 게임씬 로딩
            Managers.Scene.LoadScene(Define.en_Scene.GameScene);

            Managers.NetworkManager._PlayerDBId = _GameObjectInfo.ObjectId;

            // 해당 캐릭터로 EnterGame
            CMessage ReqGameEnterPacket = Packet.MakePacket.ReqMakeEnterGamePacket(Managers.NetworkManager._AccountId, _GameObjectInfo.ObjectName);
            Managers.NetworkManager.GameServerSend(ReqGameEnterPacket);
        }      
    }

    // 캐릭터 생성창 버튼 누르면 
    public void OnCharacterCreateButtonClick(PointerEventData Event)
    {
        UI_LoginScene LoginSceneUI = Managers.UI._SceneUI as UI_LoginScene;
        if (LoginSceneUI == null)
        {
            Debug.Log("LoginSceneUI 가 null");
            return;
        }

        // 캐릭터 생성창 띄우기
        LoginSceneUI._CharacterCreateUI._CharacterCreateSlotIndex = _Index;
        LoginSceneUI._CharacterCreateUI.gameObject.SetActive(true);        

        // 캐릭터 선택창 감추기
        LoginSceneUI._CharacterChoiceUI.gameObject.SetActive(false);        
    }
}