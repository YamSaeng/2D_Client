using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectSkilCharacteristic : UI_Base
{
    public en_SkillCharacteristic _SkillCharacteristic;

    enum en_SelectSkillCharacteristicButton
    {
        FightCharacteristicButton,
        ProtectionCharacteristicButton,
        SpellCharacteristicButton,
        ShootingCharacteristicButton,
        DisciplineCharacteristicButton,
        AssassinationCharacteristicButton,
        SelectSkillCharacteristicCloseButton
    }

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<Button>(typeof(en_SelectSkillCharacteristicButton));

        BindEvent(GetButton((int)en_SelectSkillCharacteristicButton.FightCharacteristicButton).gameObject,
            OnFightCharacteristicButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_SelectSkillCharacteristicButton.ProtectionCharacteristicButton).gameObject,
            OnProtectionCharacteristicButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_SelectSkillCharacteristicButton.SpellCharacteristicButton).gameObject,
            OnSpellCharacteristicButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_SelectSkillCharacteristicButton.ShootingCharacteristicButton).gameObject,
            OnShootingCharacteristicButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_SelectSkillCharacteristicButton.DisciplineCharacteristicButton).gameObject,
            OnDisciplineCharacteristicButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_SelectSkillCharacteristicButton.AssassinationCharacteristicButton).gameObject,
            OnAssassinationCharacteristicButtonClick, Define.en_UIEvent.MouseClick);

        if(GetButton((int)en_SelectSkillCharacteristicButton.SelectSkillCharacteristicCloseButton) != null)
        {
            BindEvent(GetButton((int)en_SelectSkillCharacteristicButton.SelectSkillCharacteristicCloseButton).gameObject,
            OnSelectSkillCharacteristicCloseButtonClick, Define.en_UIEvent.MouseClick);
        }                
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void OnFightCharacteristicButtonClick(PointerEventData Event)
    {
        _SkillCharacteristic = en_SkillCharacteristic.SKILL_CATEGORY_FIGHT;        
    }

    public void OnProtectionCharacteristicButtonClick(PointerEventData Event)
    {
        _SkillCharacteristic = en_SkillCharacteristic.SKILL_CATEGORY_PROTECTION;        
    }

    public void OnSpellCharacteristicButtonClick(PointerEventData Event)
    {
        _SkillCharacteristic = en_SkillCharacteristic.SKILL_CATEGORY_SPELL;        
    }

    public void OnShootingCharacteristicButtonClick(PointerEventData Event)
    {
        _SkillCharacteristic = en_SkillCharacteristic.SKILL_CATEGORY_SHOOTING;        
    }

    public void OnDisciplineCharacteristicButtonClick(PointerEventData Event)
    {
        _SkillCharacteristic = en_SkillCharacteristic.SKILL_CATEGORY_DISCIPLINE;        
    }

    public void OnAssassinationCharacteristicButtonClick(PointerEventData Event)
    {
        _SkillCharacteristic = en_SkillCharacteristic.SKILL_CATEGORY_ASSASSINATION;        
    }

    public void OnSelectSkillCharacteristicCloseButtonClick(PointerEventData Event)
    {
        ShowCloseUI(false);

        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        GameSceneUI._SkillBoxUI.SkillBoxSkillCharacteristicShowClose(true);
        GameSceneUI._SkillBoxUI.SkillBoxButtonShowClose(true);
    }
}