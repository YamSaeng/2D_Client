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
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_UP, "���� �ȱ�");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_DOWN, "�Ʒ��� �ȱ�");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_LEFT, "�������� �ȱ�");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_RIGHT, "���������� �ȱ�");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_INVENTORY, "���� ����/�ݱ�");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_SKILL_BOX, "Ư�� â ����/�ݱ�");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_EQUIPMENT_BOX, "��� â ����/�ݱ�");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_ONE, "ù��° ����Ű 1");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_TWO, "ù��° ����Ű 2");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_THREE, "ù��° ����Ű 3");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_FOUR, "ù��° ����Ű 4");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_FIVE, "ù��° ����Ű 5");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_ONE, "�ι�° ����Ű 1");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_TWO, "�ι�° ����Ű 2");        
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_THREE, "�ι�° ����Ű 3");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_FOUR, "�ι�° ����Ű 4");
        _UserQuickSlotString.Add(en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_FIVE, "�ι�° ����Ű 5");        
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
        // ���� ���
        _SkillExplanationString.Add(en_SkillType.SKILL_DEFAULT_ATTACK,
            "���� ��󿡰� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PUBLIC_ACTIVE_BUF_SHOCK_RELEASE,
            "����, �з��� �����̻��� �����ϰ� 8�� ���� �����̻� ���װ��� 1000��ŭ ������Ų��.");

        // ���� ���
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_TWO_HAND_SWORD_MASTER,
            "");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK,
            "���� ��󿡰� ���ظ� �ְ� ȸ���� �ϰ��� Ȱ��ȭ �Ѵ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK,
            "���� ��󿡰� ���ظ� �ְ� �г��� �ϰ��� Ȱ��ȭ �Ѵ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_WRATH_ATTACK,
            "������ ��󿡰� ���ظ� �ְ� �Ѿ� �߸���.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_SMASH_WAVE,
            "���� 3���� ���� ���鿡�� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_JUMPING_ATTACK,
            "7���� ���� ��󿡰� �̵��ϸ鼭 1�ʸ�ŭ �̵��Ұ� ���·� ����� ���ظ� ������.");        
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_PIERCING_WAVE,
            "���� 5���� ���� ���鿡�� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FLY_KNIFE,
            "8���� ���� ��󿡰� Į���� ���� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_COMBO_FLY_KNIFE,
            "8���� ���� ��󿡰� �������� Į���� ���� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE,
            "���ݷ��� 80% ��ŭ ������Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_FIGHT_ACTIVE_BUF_COUNTER_ARMOR,
            "5���� ���� ��󿡰� �������� �������� �ݻ��Ų��.");

        // ��� ���
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_POWERFUL_ATTACK,
            "������ ��󿡰� ���ظ� �ְ�, ������ �ϰ��� Ȱ��ȭ �Ѵ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHARP_ATTACK,
            "������ ��󿡰� ���ظ� �ְ�, �ʻ��� �ϰ��� Ȱ��ȭ �Ѵ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_LAST_ATTACK,
            "������ ��󿡰� ���ظ� �ְ�, 2�ʰ� ���� ���·� �����.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH,
            "������ ��󿡰� ���ظ� �ְ�, 2�ʰ� ���� ���·� ����ϴ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_COUNTER,
            "������ ��󿡰� ���ظ� �ְ�, 2�ʰ� ���� ���·� ����ϴ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SWORD_STORM,
            "������ ��󿡰� ���ظ� �ְ�, �Ѿ� �߸���.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_CAPTURE,
            "8m���� ��󿡰� ���ظ� �ְ� �������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_BUF_FURY,
            "30�� ���� ���� ���ݷ��� 50% �����մϴ�");
        _SkillExplanationString.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_DOUBLE_ARMOR,
            "10�� ���� �޴� ��� ���ط��� 50% ���ҽ�ŵ�ϴ�");

        // ���� ���
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_BOLT,
            "�Ҳ� ȭ���� ������ �������� �߻��ϰ�, �۷��� Ȱ��ȭ �Ѵ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_BLAZE,
            "10���� �Ÿ� �ȿ� �ִ� ��󿡰� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN,
            "10���� �Ÿ� �ȿ� �ִ� ��󿡰� ���ظ� �ְ� ������ �ӵ��� ���� ���δ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE,
            "10���� �Ÿ� �ȿ� �ִ� ��󿡰� ���ظ� �ְ� �о��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ROOT,
            "10���� �Ÿ� �ȿ� �ִ� ����� 10�� ���� �̵��Ұ� ���·� �����.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_SLEEP,
            "10���� �Ÿ� �ȿ� �ִ� ����� 20�� ���� ���� ���·� �����.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_WINTER_BINDING,
            "�ڽ� ���� 8���� �Ÿ� �ȿ� �ִ� ���鿡�� ���ظ� �ְ� 10�� ���� �̵��Ұ� ���·� �����.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE,
            "10���� �Ÿ� �ȿ� �ִ� ��󿡰� ���ظ� �ְ� 2�� ���� ���� ��Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE,
            "10���� �Ÿ� �ȿ� �ִ� ��󿡰� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_BACK_TELEPORT,
            "�̵� �Ұ��� �̵� �ӵ� ���Ҹ� �����ϰ� 4ĭ �ڷ� �̵��Ѵ�.");        
        _SkillExplanationString.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_ILLUSION,
            "10�� ���� ���� ���� 2ȸ�� ȸ���Ѵ�.");

        // ���� ���
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE,
            "10���� �Ÿ� �ȿ� �ִ� ��󿡰� ���ظ� �ְ�, ������ Ȱ��ȭ �Ѵ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_THUNDER_BOLT,
            "10���� �Ÿ� �ȿ� �ִ� ��󿡰� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_ROOT,
            "10���� �Ÿ� �ȿ� �ִ� ����� 2�� ���� �ӹ� ��Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_JUDGMENT,
            "10���� �Ÿ� �ȿ� �ִ� ��󿡰� ���ظ� �ְ�, 2�� ���� ���� ��Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT,
            "10���� �Ÿ� �ȿ� �ִ� ����� ü���� ȸ����Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_RECOVERY_LIGHT,
            "10���� �Ÿ� �ȿ� �ִ� ����� ü���� ȸ����Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_VITALITY_LIGHT,
            "10���� �Ÿ� �ȿ� �ִ� 30�� ���� 2�� �������� ȸ����Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_GRACE,
            "10���� �Ÿ� �ȿ� �ִ� ����� ȸ����Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND,
            "10���� �Ÿ� �ȿ� �ִ� �׷������ ü���� ȸ����Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_RECOVERY_WIND,
            "10���� �Ÿ� �ȿ� �ִ� �׷������ ü���� ȸ����Ų��.");

        // �ϻ� ���
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_QUICK_CUT,
            "���� ��󿡰� ���ظ� �ְ� �żӺ��⸦ Ȱ��ȭ�Ѵ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_FAST_CUT,
            "���� ��󿡰� ���ظ� �ش�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_ATTACK,
            "���� ��󿡰� ���ظ� �ְ�, �ĸ� ���⸦ Ȱ��ȭ �Ѵ�. �ڿ��� �����ϸ� ���ظ� 100% ��ŭ ������Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_CUT,
            "���� ��󿡰� ���ظ� ������. �ڿ��� �����ϸ� ���ظ� 100% ��ŭ ������Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_ADVANCE_CUT,
            "10���� ���� ��󿡰� �����ϰ� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_POISON_INJECTION,
            "������ ��󿡰� ���� ���Խ�Ų��. ( �ִ� 3�ܰ� )");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_POISON_STUN,
            "������ ����� ������ ���� 3�ܰ���� �����ϰ�, 3�� ���� ���� ��Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_ASSASSINATION,
            "���� ��󿡰� ���ظ� ������. ����� �ߵ� ���¶�� ���ظ� 100% ��ŭ ������Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_STEP,
            "������ ����� �ڷ� �̵��ϰ� ���ظ� �����鼭 2�� ���� ������Ų��.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_BUF_STEALTH,
            "40�� ���� ���� ���°� �ȴ�.");
        _SkillExplanationString.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_BUF_SIXTH_SENSE_MAXIMIZE,
            "10�� ���� ��� ���� ������ ȸ���Ѵ�.");

        _SkillExplanationString.Add(en_SkillType.SKILL_SHOOTING_ACTIVE_ATTACK_SNIFING,
            "6m �Ÿ� ���� ��󿡰� ���ظ� ������.");
        _SkillExplanationString.Add(en_SkillType.SKILL_GOBLIN_ACTIVE_MELEE_DEFAULT_ATTACK,
            "���� ��󿡰� ���ظ� ������.");
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
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_BOLT,
            "�Ҳ� ȭ��");
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
        _SkillString.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_BACK_TELEPORT,
            "�ð��� ��Ʋ��");

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
    }
}
