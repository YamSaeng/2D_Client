using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillCharacteristic : UI_Base
{    
    private List<UI_SkillItem> _PassiveSkillItemUI = new List<UI_SkillItem>();
    private List<UI_SkillItem> _ActiveSkillItemUI = new List<UI_SkillItem>();

    public UI_SkillBox _SkillBox;

    enum en_SkillChracteristicButton
    {
        SelectCharcteristicButton
    }  

    public override void Init()
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;        
    }

    public override void Binding()
    {
        Bind<Button>(typeof(en_SkillChracteristicButton));        

        BindEvent(GetButton((int)en_SkillChracteristicButton.SelectCharcteristicButton).gameObject, OnSelectChracteristicButtonClick, Define.en_UIEvent.MouseClick);
    }    

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }    

    public void OnSelectChracteristicButtonClick(PointerEventData Event)
    {
        if(_SkillBox != null)
        {
            _SkillBox.SkillBoxCharacteristicShowClose(false);
            _SkillBox.SkillBoxSelectCharacteristicShowClose(true);
            _SkillBox.SkillBoxButtonShowClose(false);
        }
    }

    public void CharcteristicSelectButtonShowClose(bool IsShowClose)
    {
        GetButton((int)en_SkillChracteristicButton.SelectCharcteristicButton).gameObject.SetActive(IsShowClose);
    }

    public List<UI_SkillItem> GetPassiveSkills()
    {
        return _PassiveSkillItemUI;
    }

    public List<UI_SkillItem> GetActiveSkills()
    {
        return _ActiveSkillItemUI;
    }
}
