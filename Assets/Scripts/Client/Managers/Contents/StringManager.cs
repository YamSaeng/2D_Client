using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager
{
    public Dictionary<en_SkillType, string> _SkillString = new Dictionary<en_SkillType, string>();
    public Dictionary<en_SkillCharacteristic, string> _SkillCharacteristicString = new Dictionary<en_SkillCharacteristic, string>();
    public Dictionary<en_SkillType, string> _SkillDamageString = new Dictionary<en_SkillType, string>();
    public Dictionary<en_SkillType, string> _SkillExplanationString = new Dictionary<en_SkillType, string>();
    public Dictionary<en_UserQuickSlot, string> _UserQuickSlotString = new Dictionary<en_UserQuickSlot, string>();
    public Dictionary<en_KeyCode, string> _KeyCodeString = new Dictionary<en_KeyCode, string>();

    public void Init()
    {
        LoadSkillString();
        LoadSkillCharacteristicString();
        LoadSkillExplanationString();
        LoadUserQuickSlotString();
        LoadKeyCodeString();
    }

    private void LoadUserQuickSlotString()
    {
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_UP, "위로 걷기");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_DOWN, "아래로 걷기");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_LEFT, "왼쪽으로 걷기");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_RIGHT, "오른쪽으로 걷기");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_INVENTORY, "가방 열기/닫기");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_SKILL_BOX, "특성 창 열기/닫기");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_EQUIPMENT_BOX, "장비 창 열기/닫기");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_ONE, "첫번째 단축키 1");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_TWO, "첫번째 단축키 2");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_THREE, "첫번째 단축키 3");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_FOUR, "첫번째 단축키 4");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_FIVE, "첫번째 단축키 5");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_ONE, "두번째 단축키 1");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_TWO, "두번째 단축키 2");        
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_THREE, "두번째 단축키 3");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_FOUR, "두번째 단축키 4");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_FIVE, "두번째 단축키 5");        
    }

    private void LoadKeyCodeString()
    {
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_NONE, "");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_W, "W");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_W, "Ctrl + W");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_W, "Alt + W");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_S, "S");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_S, "Ctrl + S");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_S, "Alt + S");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_A, "A");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_A, "A");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_A, "A");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_D, "D");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_D, "D");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_D, "D");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_I, "I");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_I, "I");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_I, "I");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_E, "E");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_E, "E");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_E, "E");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_K, "K");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_K, "K");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_K, "K");
        
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ONE, "1");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_ONE, "1");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_ONE, "1");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_TWO, "2");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_TWO, "2");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_TWO, "2");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_THREE, "3");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_THREE, "3");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_THREE, "3");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_FOUR, "4");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_FOUR, "4");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_FOUR, "4");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_FIVE, "5");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_FIVE, "5");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_FIVE, "5");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_SIX, "6");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_SIX, "6");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_SIX, "6");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_SEVEN, "7");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_SEVEN, "7");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_SEVEN, "7");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_EIGHT, "8");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_EIGHT, "8");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_EIGHT, "8");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_NINE, "9");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_NINE, "9");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_NINE, "9");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ZERO, "0");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_ZERO, "0");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_ZERO, "0");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_Q, "Q");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_Q, "Q");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_Q, "Q");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_R, "R");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_R, "R");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_R, "R");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_T, "T");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_T, "T");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_T, "T");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_Y, "Y");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_Y, "Y");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_Y, "Y");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_U, "U");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_U, "U");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_U, "U");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_O, "O");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_O, "O");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_O, "O");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_P, "P");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_P, "P");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_P, "P");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_F, "F");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_F, "F");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_F, "F");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_G, "G");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_G, "G");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_G, "G");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_H, "H");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_H, "H");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_H, "H");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_J, "J");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_J, "J");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_J, "J");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_L, "L");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_L, "L");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_L, "L");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_Z, "Z");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_Z, "Z");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_Z, "Z");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_X, "X");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_X, "X");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_X, "X");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_C, "C");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_C, "C");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_C, "C");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_V, "V");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_V, "V");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_V, "V");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_B, "B");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_B, "B");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_B, "B");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_N, "N");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_N, "N");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_N, "N");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_M, "M");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_M, "M");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_M, "M");

        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CAPSLOCK, "CapsLock");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_CTRL_CAPSLOCK, "Ctrl + CapsLock");
        _KeyCodeString.Add(en_KeyCode.KEY_CODE_ALT_CAPSLOCK, "Alt + CapsLock");
    }

    private void LoadSkillExplanationString()
    {
        // 공용 기술
        _SkillExplanationString.Add(en_SkillType.SKILL_DEFAULT_ATTACK,
            "전방 대상에게 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PUBLIC_ACTIVE_BUF_SHOCK_RELEASE,
            "기절, 밀려남 상태이상을 해제하고 8초 동안 상태이상 저항값을 1000만큼 증가시킨다.");

        // 격투 기술
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_TWO_HAND_SWORD_MASTER,
            "");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK,
            "전방 대상에게 피해를 주고 회심의 일격을 활성화 한다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK,
            "전방 대상에게 피해를 주고 분노의 일격을 활성화 한다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_WRATH_ATTACK,
            "선택한 대상에게 피해를 주고 넘어 뜨린다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_SMASH_WAVE,
            "주위 3미터 내의 대상들에게 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_JUMPING_ATTACK,
            "7미터 내의 대상에게 이동하면서 1초만큼 이동불가 상태로 만들고 피해를 입힌다.");        
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_PIERCING_WAVE,
            "주위 5미터 내의 대상들에게 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FLY_KNIFE,
            "8미터 내의 대상에게 칼날을 날려 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_COMBO_FLY_KNIFE,
            "8미터 내의 대상에게 연속으로 칼날을 날려 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE,
            "공격력을 80% 만큼 증가시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_BUF_COUNTER_ARMOR,
            "5미터 내의 대상에게 일정량의 데미지를 반사시킨다.");

        // 방어 기술
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_POWERFUL_ATTACK,
            "전방의 대상에게 피해를 주고, 예리한 일격을 활성화 한다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHARP_ATTACK,
            "전방의 대상에게 피해를 주고, 필사의 일격을 활성화 한다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_LAST_ATTACK,
            "선택한 대상에게 피해를 주고, 2초간 기절 상태로 만든다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH,
            "전방의 대상에게 피해를 주고, 2초간 기절 상태로 만듭니다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_COUNTER,
            "전방의 대상에게 피해를 주고, 2초간 기절 상태로 만듭니다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SWORD_STORM,
            "전방의 대상에게 피해를 주고, 넘어 뜨린다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_CAPTURE,
            "8m내의 대상에게 피해를 주고 끌어당긴다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_BUF_FURY,
            "30초 동안 물리 공격력이 50% 증가합니다");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_DOUBLE_ARMOR,
            "10초 동안 받는 모든 피해량을 50% 감소시킵니다");

        // 마법 기술
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_BOLT,
            "불꽃 화살을 생성해 전방으로 발사하고, 작렬을 활성화 한다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_BLAZE,
            "10미터 거리 안에 있는 대상에게 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN,
            "10미터 거리 안에 있는 대상에게 피해를 주고 상대방의 속도를 절반 줄인다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE,
            "10미터 거리 안에 있는 대상에게 피해를 주고 밀어낸다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ROOT,
            "10미터 거리 안에 있는 대상을 10초 동안 이동불가 상태로 만든다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_SLEEP,
            "10미터 거리 안에 있는 대상을 20초 동안 수면 상태로 만든다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_WINTER_BINDING,
            "자신 주위 8미터 거리 안에 있는 대상들에게 피해를 주고 10초 동안 이동불가 상태로 만든다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE,
            "10미터 거리 안에 있는 대상에게 피해를 주고 2초 동안 기절 시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE,
            "10미터 거리 안에 있는 대상에게 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_BACK_TELEPORT,
            "이동 불가와 이동 속도 감소를 삭제하고 4칸 뒤로 이동한다.");        
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_ILLUSION,
            "10초 동안 물리 공격 2회를 회피한다.");

        // 수양 기술
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE,
            "10미터 거리 안에 있는 대상에게 피해를 주고, 벼락을 활성화 한다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_THUNDER_BOLT,
            "10미터 거리 안에 있는 대상에게 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_ROOT,
            "10미터 거리 안에 있는 대상을 2초 동안 속박 시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_JUDGMENT,
            "10미터 거리 안에 있는 대상에게 피해를 주고, 2초 동안 기절 시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT,
            "10미터 거리 안에 있는 대상의 체력을 회복시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_RECOVERY_LIGHT,
            "10미터 거리 안에 있는 대상의 체력을 회복시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_VITALITY_LIGHT,
            "10미터 거리 안에 있는 30초 동안 2초 간격으로 회복시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_GRACE,
            "10미터 거리 안에 있는 대상을 회복시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND,
            "10미터 거리 안에 있는 그룹원들의 체력을 회복시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_RECOVERY_WIND,
            "10미터 거리 안에 있는 그룹원들의 체력을 회복시킨다.");

        // 암살 기술
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_QUICK_CUT,
            "전방 대상에게 피해를 주고 신속베기를 활성화한다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_FAST_CUT,
            "전방 대상에게 피해를 준다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_ATTACK,
            "전방 대상에게 피해를 주고, 후면 베기를 활성화 한다. 뒤에서 공격하면 피해를 100% 만큼 증가시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_CUT,
            "전방 대상에게 피해를 입힌다. 뒤에서 공격하면 피해를 100% 만큼 증가시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_ADVANCE_CUT,
            "10미터 내의 대상에게 돌진하고 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_POISON_INJECTION,
            "선택한 대상에게 독을 주입시킨다. ( 최대 3단계 )");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_POISON_STUN,
            "선택한 대상의 누적된 독을 3단계까지 삭제하고, 3초 동안 기절 시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_ASSASSINATION,
            "전방 대상에게 피해를 입힌다. 대상이 중독 상태라면 피해를 100% 만큼 증가시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_STEP,
            "선택한 대상의 뒤로 이동하고 피해를 입히면서 2초 동안 기절시킨다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_BUF_STEALTH,
            "40초 동안 은신 상태가 된다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_BUF_SIXTH_SENSE_MAXIMIZE,
            "10초 동안 모든 물리 공격을 회피한다.");

        _SkillExplanationString.Add(en_SkillType.SKILL_SHOOTING_ACTIVE_ATTACK_SNIFING,
            "6m 거리 안의 대상에게 피해를 입힌다.");
        _SkillExplanationString.Add(en_SkillType.SKILL_GOBLIN_ACTIVE_MELEE_DEFAULT_ATTACK,
            "전방 대상에게 피해를 입힌다.");
    }

    private void LoadSkillCharacteristicString()
    {
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_FIGHT,
            "격투");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_PROTECTION,
            "방어");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_SPELL,
            "마법");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_DISCIPLINE,
            "수양");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_ASSASSINATION,
            "암살");
        _SkillCharacteristicString.Add(en_SkillCharacteristic.SKILL_CATEGORY_SHOOTING,
            "사격");
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
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_JUMPING_ATTACK,
            "도약 공격");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_PIERCING_WAVE,
            "살기 파동");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FLY_KNIFE,
            "칼날 날리기");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_COMBO_FLY_KNIFE,
            "칼날 연속 날리기");
        _SkillString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE,
            "돌격 자세");

        // 방어 기술 String
        _SkillString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH,
            "방패 강타");
        _SkillString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_CAPTURE,
            "포획");

        // 마법 기술 String
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_BOLT,
            "불꽃 화살");
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
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_BACK_TELEPORT,
            "시공의 뒤틀림");

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
    }
}
