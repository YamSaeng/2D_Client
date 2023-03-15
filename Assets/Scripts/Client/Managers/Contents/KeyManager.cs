using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager
{
    private Dictionary<KeyCode, Action<KeyCode>> _BindingKeys = new Dictionary<KeyCode, Action<KeyCode>>();

    public void BindingKey()
    {
        _BindingKeys.Add(KeyCode.Alpha1, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha2, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha3, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha4, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha5, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha6, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha7, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha8, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha9, QuickSlotBarAction);
        _BindingKeys.Add(KeyCode.Alpha0, QuickSlotBarAction);        
    }

    public void QuickSlotBarKeyUpdate()
    {        
        foreach (KeyValuePair<KeyCode, Action<KeyCode>> BindingKey in _BindingKeys)
        {
            if (Input.GetKeyDown(BindingKey.Key))
            {
                BindingKey.Value(BindingKey.Key);
            }            
        }
    }

    public void QuickSlotBarAction(KeyCode keyCode)
    {
        // 실행하고자 하는 퀵슬롯 정보를 가져옴
        st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo = Managers.QuickSlotBar.FindExekey(keyCode);       
        // 퀵슬롯에 스킬정보와 아이템 정보가 설정 되어 있지 않을 경우 나감
        if(QuickSlotBarSlotInfo.QuickBarSkillInfo == null && QuickSlotBarSlotInfo.QuickBarItemInfo == null)
        {
            return;
        }   

        CMessage ReqQuickSlotPacket = null;

        PlayerObject Player = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId).GetComponent<PlayerObject>();

        switch (QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType)
        {            
            case en_SkillType.SKILL_DEFAULT_ATTACK:         
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK:                        
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_JUMPING_ATTACK:                        
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_PIERCING_WAVE:                        
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FLY_KNIFE:                        
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_COMBO_FLY_KNIFE:                        
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_QUICK_CUT:
            case en_SkillType.SKILL_SHOOTING_ACTIVE_ATTACK_SNIFING:            
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_CAPTURE:
                ReqQuickSlotPacket = Packet.MakePacket.ReqMakeAttackPacket(
                               QuickSlotBarSlotInfo.QuickSlotBarIndex,
                           QuickSlotBarSlotInfo.QuickSlotBarSlotIndex,
                           QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillCharacteristic,
                           QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType);
                break;
            case en_SkillType.SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE:
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE:
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_ROOT:
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT:
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_HARPOON:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ROOT:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE:
            case en_SkillType.SKILL_SPELL_ACTIVE_BUF_TELEPORT:
            case en_SkillType.SKILL_PUBLIC_ACTIVE_BUF_SHOCK_RELEASE:
                ReqQuickSlotPacket = Packet.MakePacket.ReqMakeMagicPacket(Managers.NetworkManager._AccountId,
                           Managers.NetworkManager._PlayerDBId,                           
                           QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillCharacteristic,
                           QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType);
                break;
            case en_SkillType.SKILL_TYPE_NONE:
                break;
        }

        if (QuickSlotBarSlotInfo.QuickBarItemInfo != null && QuickSlotBarSlotInfo.QuickBarItemInfo.ItemSmallCategory != en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE)
        {
            CMessage ReqMakeItemUsePacket = Packet.MakePacket.ReqMakeItemUsePacket(Managers.NetworkManager._AccountId, Managers.NetworkManager._PlayerDBId,
                    QuickSlotBarSlotInfo.QuickBarItemInfo.ItemSmallCategory,
                    QuickSlotBarSlotInfo.QuickBarItemInfo.ItemTileGridPositionX,
                    QuickSlotBarSlotInfo.QuickBarItemInfo.ItemTileGridPositionY);
            Managers.NetworkManager.GameServerSend(ReqMakeItemUsePacket);
        }     

        if(ReqQuickSlotPacket != null)
        {
            Managers.NetworkManager.GameServerSend(ReqQuickSlotPacket);
        }
    }
}
