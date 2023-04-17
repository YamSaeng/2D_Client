using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectSkilCharacteristic : UI_Base
{
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
        BindEvent(GetButton((int)en_SelectSkillCharacteristicButton.SelectSkillCharacteristicCloseButton).gameObject,
            OnSelectSkillCharacteristicCloseButtonClick, Define.en_UIEvent.MouseClick);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void OnFightCharacteristicButtonClick(PointerEventData Event)
    {
        CMessage ReqSelectCharacteristicPacket = Packet.MakePacket.ReqSelectSkillChracteristicPacket(en_SkillCharacteristic.SKILL_CATEGORY_FIGHT);
        Managers.NetworkManager.GameServerSend(ReqSelectCharacteristicPacket);
    }

    public void OnProtectionCharacteristicButtonClick(PointerEventData Event)
    {
        CMessage ReqSelectCharacteristicPacket = Packet.MakePacket.ReqSelectSkillChracteristicPacket(en_SkillCharacteristic.SKILL_CATEGORY_PROTECTION);
        Managers.NetworkManager.GameServerSend(ReqSelectCharacteristicPacket);
    }

    public void OnSpellCharacteristicButtonClick(PointerEventData Event)
    {
        CMessage ReqSelectCharacteristicPacket = Packet.MakePacket.ReqSelectSkillChracteristicPacket(en_SkillCharacteristic.SKILL_CATEGORY_SPELL);
        Managers.NetworkManager.GameServerSend(ReqSelectCharacteristicPacket);
    }

    public void OnShootingCharacteristicButtonClick(PointerEventData Event)
    {
        CMessage ReqSelectCharacteristicPacket = Packet.MakePacket.ReqSelectSkillChracteristicPacket(en_SkillCharacteristic.SKILL_CATEGORY_SHOOTING);
        Managers.NetworkManager.GameServerSend(ReqSelectCharacteristicPacket);
    }

    public void OnDisciplineCharacteristicButtonClick(PointerEventData Event)
    {
        CMessage ReqSelectCharacteristicPacket = Packet.MakePacket.ReqSelectSkillChracteristicPacket(en_SkillCharacteristic.SKILL_CATEGORY_DISCIPLINE);
        Managers.NetworkManager.GameServerSend(ReqSelectCharacteristicPacket);
    }

    public void OnAssassinationCharacteristicButtonClick(PointerEventData Event)
    {
        CMessage ReqSelectCharacteristicPacket = Packet.MakePacket.ReqSelectSkillChracteristicPacket(en_SkillCharacteristic.SKILL_CATEGORY_ASSASSINATION);
        Managers.NetworkManager.GameServerSend(ReqSelectCharacteristicPacket);
    }

    public void OnSelectSkillCharacteristicCloseButtonClick(PointerEventData Event)
    {
        ShowCloseUI(false);

        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        GameSceneUI._SkillBoxUI.SkillBoxSkillCharacteristicShowClose(true);
        GameSceneUI._SkillBoxUI.SkillBoxButtonShowClose(true);
    }
}