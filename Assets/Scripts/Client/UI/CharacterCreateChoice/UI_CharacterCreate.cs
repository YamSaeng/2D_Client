using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//-----------------------------------------
// 캐릭터 생성창 UI
//-----------------------------------------
public class UI_CharacterCreate : UI_Scene
{
    // 캐릭터 선택창으로 부터 받은 캐릭터 슬롯 Index;
    public byte _CharacterCreateSlotIndex { get; set; }    

    enum en_CharacterCreatePopButton
    {        
        CharacterCreateButton
    }

    enum en_CharacterCreateInputField
    {
        CharacterNameInputField
    }

    enum en_SampleCharacterImage
    {
        SampleCharacterImage
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(en_CharacterCreatePopButton));
        Bind<InputField>(typeof(en_CharacterCreateInputField));
        Bind<Image>(typeof(en_SampleCharacterImage));
        
        BindEvent(GetButton((int)en_CharacterCreatePopButton.CharacterCreateButton).gameObject, OnCharacterCreateButtonClick, Define.en_UIEvent.MouseClick);
    }  

    void OnCharacterCreateButtonClick(PointerEventData Event)
    {        
        if(GetInputField((int)en_CharacterCreateInputField.CharacterNameInputField).text.Length == 0 
            || GetInputField((int)en_CharacterCreateInputField.CharacterNameInputField).text.Length == 1)
        {
            return;    
        }               

        string CharacterName = GetInputField((int)en_CharacterCreateInputField.CharacterNameInputField).text;

        // 게임 캐릭터 생성 요청
        CMessage ReqGameServerCreateCharacterPacket = Packet.MakePacket.ReqMakeCreateCharacterPacket(CharacterName, _CharacterCreateSlotIndex);
        Managers.NetworkManager.GameServerSend(ReqGameServerCreateCharacterPacket);
    }
}
