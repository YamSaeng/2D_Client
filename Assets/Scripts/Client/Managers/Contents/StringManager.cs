using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager
{
    public Dictionary<en_SkillType, string> _SkillString = new Dictionary<en_SkillType, string>();

    public void Init()
    {
        LoadSkillString();
    }

    private void LoadSkillString()
    {
        // 공용 기술 String
        _SkillString.Add(en_SkillType.SKILL_DEFAULT_ATTACK,
            "일반 공격");
        _SkillString.Add(en_SkillType.SKILL_PUBLIC_ACTIVE_BUF_SHOCK_RELEASE,
            "충격 해제");

        // 격투 기술 String
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK,
            "맹렬한 일격");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK,
            "회심의 일격");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_SMASH_WAVE,
            "분쇄 파동");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_SHAHONE,
            "쇄혼 비무");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CHOHONE,
            "초혼 비무");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE,
            "돌격 자세");

        // 방어 기술 String
        _SkillString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH,
            "방패 반격");

        // 마법 기술 String
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_HARPOON,
            "불꽃 작살");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ROOT,
            "속박");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN,
            "얼음 사슬");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE,
            "냉기 파동");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE,
            "낙뢰");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE,
            "지옥의 화염");
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_TELEPORT,
            "");

        // 수양 기술 String
        _SkillString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE,
            "신성한 일격");
        _SkillString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT,
            "치유의 빛");
        _SkillString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND,
            "치유의 바람");
        _SkillString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_ROOT,
            "속박");

        // 암살 기술 String
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_QUICK_CUT,
            "빠른 베기");
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_FAST_CUT,
            "신속 베기");
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_ATTACK,
            "기습");
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_STEP,
            "암습");
        _SkillString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_BUF_WEAPON_POISON,
            "독 바르기");

        // 슬라임 기술 String
        _SkillString.Add(en_SkillType.SKILL_SLIME_NORMAL,
            "일반 공격");
        _SkillString.Add(en_SkillType.SKILL_SLIME_ACTIVE_POISION_ATTACK,
            "슬라임 독");
    }
}
