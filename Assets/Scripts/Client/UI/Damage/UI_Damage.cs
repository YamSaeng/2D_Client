using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Damage : MonoBehaviour
{
    private Color DefaultColor = Color.yellow;
    private Color CriticalColor = Color.red;
    private Color HealColor = Color.green;

    private static int _SortingOrder = 14;
    private TextMeshPro _TextMesh;    

    private void Awake()
    {
        _TextMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(en_SkillType SkillType, int PointAmount, bool IsCriticalHit, float PositionX, float PositionY)
    {
        GameObject UIRoot = GameObject.Find("@UI_Root");
        if (UIRoot == null)
        {
            UIRoot = new GameObject { name = "@UI_Root" };
        }

        transform.SetParent(UIRoot.transform);

        switch (SkillType)
        {
            case en_SkillType.SKILL_DEFAULT_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_WRATH_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_SMASH_WAVE:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FLY_KNIFE:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_COMBO_FLY_KNIFE:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_JUMPING_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_PIERCING_WAVE:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_POWERFUL_ATTACK:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHARP_ATTACK:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_LAST_ATTACK:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_COUNTER:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SWORD_STORM:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_CAPTURE:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_QUICK_CUT:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_FAST_CUT:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_ATTACK:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_CUT:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_ADVANCE_CUT:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_POISON_INJECTION:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_POISON_STUN:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_ASSASSINATION:
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_STEP:
            case en_SkillType.SKILL_SHOOTING_ACTIVE_ATTACK_SNIFING:            
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_BOLT:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE:
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE:
            case en_SkillType.SKILL_GOBLIN_ACTIVE_MELEE_DEFAULT_ATTACK:            
                // 크리티컬 여부에 따라 크기와 색 조정
                if (IsCriticalHit == false)
                {
                    // 데미지 텍스트 적용
                    _TextMesh.SetText(PointAmount.ToString());
                    _TextMesh.fontSize = 5;                    

                    _TextMesh.color = DefaultColor;
                }
                else
                {
                    _TextMesh.SetText("치명타! " + PointAmount.ToString());
                    _TextMesh.fontSize = 8;
                    _TextMesh.color = new Color(200,0,0);
                }
                break;
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT:
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND:
                // 크리티컬 여부에 따라 크기와 색 조정
                if (IsCriticalHit == false)
                {
                    // 데미지 텍스트 적용
                    _TextMesh.SetText("+" + PointAmount.ToString());

                    _TextMesh.fontSize = 5;
                    _TextMesh.color = HealColor;
                }
                else
                {
                    _TextMesh.SetText("극대화! " + PointAmount.ToString());
                    _TextMesh.fontSize = 8;
                    _TextMesh.color = HealColor;
                }
                break;            
        }

        // 뒤에 나타난 데미지 팝업 ui가 앞쪽에 출력 될 수 있도록하기 위해 _SortingOrder 증가
        _SortingOrder++;
        _TextMesh.sortingOrder = _SortingOrder;        

        if (_SortingOrder >= 1000000)
        {
            _SortingOrder = 14;
        }        
    }
}