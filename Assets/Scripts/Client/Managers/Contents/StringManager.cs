using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager
{
    public Dictionary<en_SkillType, string> _SkillString = new Dictionary<en_SkillType, string>();
    public Dictionary<en_SkillCharacteristic, string> _SkillCharacteristicString = new Dictionary<en_SkillCharacteristic, string>();
    public Dictionary<en_SkillType, string> _SkillDamageString = new Dictionary<en_SkillType, string>();

    public void Init()
    {
        LoadSkillString();
        LoadSkillCharacteristicString();
    }

    private void LoadSkillCharacteristicString()
    {
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_FIGHT,
            "����");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_PROTECTION,
            "���");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_SPELL,
            "����");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_DISCIPLINE,
            "����");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_ASSASSINATION,
            "�ϻ�");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_SHOOTING,
            "���");
    }

    private void LoadSkillString()
    {
        // ���� ��� String
        _SkillString.Add(en_SkillType.SKILL_DEFAULT_ATTACK,
            "�Ϲ� ����");
        _SkillString.Add(en_SkillType.SKILL_PUBLIC_ACTIVE_BUF_SHOCK_RELEASE,
            "��� ����");

        // ���� ��� String
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK,
            "�ͷ��� �ϰ�");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK,
            "ȸ���� �ϰ�");        
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_JUMPING_ATTACK,
            "���� ����");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_PIERCING_WAVE,
            "��� �ĵ�");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FLY_KNIFE,
            "Į�� ������");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_COMBO_FLY_KNIFE,
            "Į�� ���� ������");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE,
            "���� �ڼ�");

        // ��� ��� String
        _SkillString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH,
            "���� ��Ÿ");
        _SkillString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_CAPTURE,
            "��ȹ");

        // ���� ��� String
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_HARPOON,
            "�Ҳ� �ۻ�");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ROOT,
            "�ӹ�");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN,
            "���� �罽");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE,
            "�ñ� �ĵ�");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE,
            "����");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE,
            "������ ȭ��");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_TELEPORT,
            "");

        // ���� ��� String
        _SkillString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE,
            "�ż��� �ϰ�");
        _SkillString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT,
            "ġ���� ��");
        _SkillString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND,
            "ġ���� �ٶ�");
        _SkillString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_ROOT,
            "�ӹ�");

        // �ϻ� ��� String
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_QUICK_CUT,
            "���� ����");
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_FAST_CUT,
            "�ż� ����");
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_ATTACK,
            "���");
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_STEP,
            "�Ͻ�");
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_BUF_WEAPON_POISON,
            "�� �ٸ���");       
    }
}
