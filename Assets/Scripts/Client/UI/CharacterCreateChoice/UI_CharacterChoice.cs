using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CharacterChoice : UI_Base
{
    public UI_CharacterChoiceItem[] _CharacterChoiceItems = new UI_CharacterChoiceItem[3];

    enum en_CharacterChoiceGameObject
    {
        CharacterChoicePanel,
        CharacterChoiceClose
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(en_CharacterChoiceGameObject));

        // 프리팹에 있던 자식들 삭제
        GameObject CharacterChoiceVertical = GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        foreach (Transform Child in CharacterChoiceVertical.transform)
        {
            Destroy(Child.gameObject);
        }        

        // 캐릭터 선택창 버튼 3개 생성
        for (byte i = 0; i < 3; i++)
        {
            GameObject CharacterChoicePopupItemGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_CHARACTER_CHOICE_ITEM, CharacterChoiceVertical.transform);                   
            UI_CharacterChoiceItem CharacterChoiceItem = Util.GetOrAddComponent<UI_CharacterChoiceItem>(CharacterChoicePopupItemGo);
            _CharacterChoiceItems[i] = CharacterChoiceItem;
            CharacterChoiceItem._Index = i;
            CharacterChoiceItem._GameObjectInfo = null;
        }

        BindEvent(GetGameObject((int)en_CharacterChoiceGameObject.CharacterChoicePanel).gameObject, OnCharacterChoiceDrag, Define.en_UIEvent.Drag);
        BindEvent(GetGameObject((int)en_CharacterChoiceGameObject.CharacterChoiceClose).gameObject, OnCharacterChoiceClose, Define.en_UIEvent.MouseClick);
    }

    public override void Binding()
    {
        
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    void OnCharacterChoiceDrag(PointerEventData Event)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Event.delta;
    }

    void OnCharacterChoiceClose(PointerEventData Event)
    {
        gameObject.SetActive(false);

        UI_LoginScene LoginScene = Managers.UI._SceneUI as UI_LoginScene;
        if(LoginScene != null)
        {
            LoginScene._SelectServerUI.UISelectServerVisivle(true);
        }        
    }

    // 캐릭터를 만들고 결과값을 받아서 버튼 셋팅할때 사용
    public void SetCharacterChoiceItem(byte Index, st_GameObjectInfo CharacterObjectInfo)
    {        
        _CharacterChoiceItems[Index]._GameObjectInfo = CharacterObjectInfo;
        _CharacterChoiceItems[Index].CharacterChoicePopupItemRefresh(Index);
    }

    // 받은 캐릭터 정보들로 버튼을 생성하고 초기화 해준다.
    // 로그인 성공시 이미 만들어둔 캐릭터 목록을 받을 때 사용
    public void SetCharacterChoiceItem(st_GameObjectInfo[] CharacterInfos)
    {
        gameObject.SetActive(true);

        for (int i = 0; i < CharacterInfos.Length; i++)
        {
            SetCharacterChoiceItem(CharacterInfos[i].PlayerSlotIndex, CharacterInfos[i]);
        }
    }  
}
