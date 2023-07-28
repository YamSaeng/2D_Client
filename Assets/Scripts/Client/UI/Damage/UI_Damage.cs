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

    public void Setup(en_SkillKinds SkillKind, int PointAmount, bool IsCriticalHit, float PositionX, float PositionY)
    {
        GameObject UIRoot = GameObject.Find("@UI_Root");
        if (UIRoot == null)
        {
            UIRoot = new GameObject { name = "@UI_Root" };
        }

        transform.SetParent(UIRoot.transform);

        switch (SkillKind)
        {
            case en_SkillKinds.SKILL_KIND_MELEE_SKILL:
            case en_SkillKinds.SKILL_KIND_SPELL_SKILL:
            case en_SkillKinds.SKILL_KIND_RANGE_SKILL:
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
                    _TextMesh.color = new Color(200, 0, 0);
                }
                break;
            case en_SkillKinds.SKILL_KIND_HEAL_SKILL:
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