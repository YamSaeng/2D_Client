using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UI_ChattingBox;

public class UI_ChattingBoxGroup : UI_Scene
{
    enum en_ChattingBoxGroupGameObject
    {
        ChattingBoxNames,
        ChattingBoxes
    }

    enum en_ChattingBoxGroupInputField
    {
        ChattingInputField // 채팅메세지 입력창
    }

    enum en_ChattingBoxGroupText
    {
        ChattingRemainLengthText
    }

    List<UI_ChattingBox> _ChattingBoxUIs = new List<UI_ChattingBox>();
    
    public override void Init()
    {
        Bind<InputField>(typeof(en_ChattingBoxGroupInputField));
        Bind<GameObject>(typeof(en_ChattingBoxGroupGameObject));
        Bind<TextMeshProUGUI>(typeof(en_ChattingBoxGroupText));

        // 채팅 박스 2개 생성
        for (int i = 0; i < 2; i++)
        {
            GameObject ChattingBoxUIGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_CHATTING_BOX, GetGameObject((int)en_ChattingBoxGroupGameObject.ChattingBoxes).transform);
            if (ChattingBoxUIGo != null)
            {
                UI_ChattingBox ChattingBoxUI = ChattingBoxUIGo.GetComponent<UI_ChattingBox>();
                if (ChattingBoxUI != null)
                {
                    ChattingBoxUI._ChattingBoxProperty.Add((en_MessageType)i);
                    ChattingBoxUI._ChattingBoxIndex = i;
                    ChattingBoxUI.GetChattingViewSubject().transform.SetParent(GetGameObject((int)en_ChattingBoxGroupGameObject.ChattingBoxNames).transform);
                    _ChattingBoxUIs.Add(ChattingBoxUI);
                    
                    if (i == 1)
                    {
                        ChattingBoxUI.ChattingScrollBoxActive(false);
                        ChattingBoxUI.SetChattingBoxName("전투");
                    }
                }
            }
        }

        Get<InputField>((int)en_ChattingBoxGroupInputField.ChattingInputField).gameObject.SetActive(false);
    }


    public override void Binding()
    {
    }
                                                                                        
    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    // 채팅 메세지 생성
    public void NewChattingMessage(string SendUser, string ChattingText, en_MessageType MessageType, st_Color MessageColor)
    {
        foreach (UI_ChattingBox ChattingBoxUI in _ChattingBoxUIs)
        {
            foreach (en_MessageType ChattingBoxProperty in ChattingBoxUI._ChattingBoxProperty)
            {
                if (ChattingBoxProperty == MessageType)
                {
                    ChattingBoxUI.SetChattingMessage(SendUser, ChattingText, MessageType, MessageColor);
                }
            }
        }
    }

    public void NewDamageChattingMessage(en_MessageType MessageType, string AttackerName, string TargetName, en_SkillType SkillType, int Damage)
    {
        foreach(UI_ChattingBox ChattingBoxUI in _ChattingBoxUIs)
        {
            foreach (en_MessageType ChattingBoxProperty in ChattingBoxUI._ChattingBoxProperty)
            {
                if (ChattingBoxProperty == MessageType)
                {
                    ChattingBoxUI.SetDamageChattingMessage(AttackerName, TargetName, SkillType, Damage);
                }
            }
        }
    }

    public InputField GetChattingInputField()
    {
        return Get<InputField>((int)en_ChattingBoxGroupInputField.ChattingInputField);
    }

    public void SelectChattingBox(int Index)
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == Index)
            {
                _ChattingBoxUIs[i].ChattingScrollBoxActive(true);
            }
            else
            {
                _ChattingBoxUIs[i].ChattingScrollBoxActive(false);
            }
        }
    }

    private void Update()
    {
        if(GetChattingInputField() != null && GetChattingInputField().IsActive() == true)
        {
            GetTextMeshPro((int)en_ChattingBoxGroupText.ChattingRemainLengthText).text = (50 - GetChattingInputField().text.Length).ToString();            
        }
    }   
}
