using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Damage : MonoBehaviour
{
    private const float INVISIBLE_TIMER_MAX = 0.8f;
    private const float GOAL_DISTANCE_X = 1.0f;
    private const float GOAL_DISTANCE_Y = 1.0f;
    private const float MOVING_SPEED = 3.0f;
    private const float STOP_SPEED = 2.0f;

    private static int _SortingOrder = 14;
    private TextMeshPro _Textmesh;
    private float _InvisibleTimer;
    private Color _TextColor;
    private Vector3 _MoveUpVector;
    private Vector3 _MoveLeftVector;
    private bool _IsCritical;

    private void Awake()
    {
        _Textmesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(en_SkillType SkillType, int PointAmount, bool IsCriticalHit)
    {
        _IsCritical = IsCriticalHit;

        switch (SkillType)
        {            
            case en_SkillType.SKILL_DEFAULT_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_SMASH_WAVE:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_SHAHONE:
            case en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH:
            case en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CHOHONE:            
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_HARPOON:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE:
            case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE:
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE:
            case en_SkillType.SKILL_SLIME_NORMAL:
            case en_SkillType.SKILL_BEAR_NORMAL:
                // 크리티컬 여부에 따라 크기와 색 조정
                if (IsCriticalHit == false)
                {
                    // 데미지 텍스트 적용
                    _Textmesh.SetText(PointAmount.ToString());

                    _Textmesh.fontSize = 45;
                    _TextColor = new Color(255, 212, 0);
                }
                else
                {
                    _Textmesh.SetText("치명타! " + PointAmount.ToString());
                    _Textmesh.fontSize = 50;
                    _TextColor = new Color(200, 0, 0);
                }
                break;
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT:                
            case en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND:
                // 크리티컬 여부에 따라 크기와 색 조정
                if (IsCriticalHit == false)
                {
                    // 데미지 텍스트 적용
                    _Textmesh.SetText("+" + PointAmount.ToString());

                    _Textmesh.fontSize = 45;
                    _TextColor = new Color(0, 200, 0);
                }
                else
                {
                    _Textmesh.SetText("극대화! " + PointAmount.ToString());
                    _Textmesh.fontSize = 50;
                    _TextColor = new Color(0, 200, 0);
                }
                break;
            case en_SkillType.SKILL_ASSASSINATION_ACTIVE_BUF_WEAPON_POISON:
            case en_SkillType.SKILL_SLIME_ACTIVE_POISION_ATTACK:
                _Textmesh.SetText(PointAmount.ToString());

                _Textmesh.fontSize = 45;
                _TextColor = new Color(0, 200, 0);
                break;
        }        

        // 뒤에 나타난 데미지 팝업 ui가 앞쪽에 출력 될 수 있도록하기 위해 _SortingOrder 증가
        _SortingOrder++;
        _Textmesh.sortingOrder = _SortingOrder;

        if (_SortingOrder >= 1000000)
        {
            _SortingOrder = 14;
        }

        // 데미지 색 지정
        _Textmesh.color = _TextColor;
        // 데미지 UI가 사라지는 시간을 지정
        _InvisibleTimer = INVISIBLE_TIMER_MAX;

        // 데미지 팝업 Y 목적지 벡터값 구하기
        _MoveUpVector = new Vector3(0.0f, GOAL_DISTANCE_Y) * MOVING_SPEED;
        // 데미지 팝업 X 목적지 벡터값 구하기
        _MoveLeftVector = new Vector3(GOAL_DISTANCE_X, 0.0f) * MOVING_SPEED;
    }

    private void Update()
    {
        float IncreaseScaleAmount;
                
        transform.position += _MoveUpVector * Time.deltaTime;
        _MoveUpVector -= _MoveUpVector * STOP_SPEED * Time.deltaTime;

        IncreaseScaleAmount = 0.1f;

        // 처음엔 사이즈를 키웠다가 줄여주는 방식
        if (_InvisibleTimer > INVISIBLE_TIMER_MAX * 0.5f)
        {            
            transform.localScale += Vector3.one * IncreaseScaleAmount * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * IncreaseScaleAmount * Time.deltaTime;
        }

        // InvisibleTimer에서 델타값을 빼줘서 투명하게 만들어줌
        _InvisibleTimer -= Time.deltaTime;

        if (_InvisibleTimer < 0)
        {
            float _InvisibleSpeed = 3.0f;
            _TextColor.a -= _InvisibleSpeed * Time.deltaTime;
            _Textmesh.color = _TextColor;

            if (_TextColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // 데미지 팝업 생성 
    public static UI_Damage Create(en_SkillType SkillType, int PointsAmount, bool IsCriticalHit, float TargetX, float TargetY)
    {
        GameObject UIRoot = GameObject.Find("@UI_Root");
        if (UIRoot == null)
        {
            UIRoot = new GameObject { name = "@UI_Root" };
        }        

        Transform PointsPopUpTransform = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_DAMAGE).transform;
        PointsPopUpTransform.SetParent(UIRoot.transform);

        UI_Damage pointPopUp = PointsPopUpTransform.GetComponent<UI_Damage>();
        float ScaleRand = Random.Range(-0.01f, 0.05f);
        pointPopUp.transform.localScale = new Vector3(0.05f + ScaleRand, 0.05f + ScaleRand, 1.0f);        
        pointPopUp.transform.position = new Vector3(TargetX + Random.Range(-0.35f, 0.35f), TargetY + 0.45f);
        pointPopUp.Setup(SkillType, PointsAmount, IsCriticalHit);

        return pointPopUp;
    }
}