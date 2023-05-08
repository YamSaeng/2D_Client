using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillExplanation : UI_Base
{
    st_SkillInfo _SkillInfo;

    enum en_SkillExplanationImage
    {
        SkillExplanationImage
    }

    enum en_SkillExplanationText
    {
        SkillExplanationName,
        SkillExplanationText,
        SkillExplanationCastingTime,
        SkillExplanationCoolTime,
        SkilDamage,
        SkilMinDamageText,
        SkilMaxDamageText
    }

    public override void Init()
    {
        Bind<Image>(typeof(en_SkillExplanationImage));
        Bind<TextMeshProUGUI>(typeof(en_SkillExplanationText));

        gameObject.SetActive(false);
    }

    public override void Binding()
    {

    }

    public void SkillExplanationSet(st_SkillInfo SkillInfo)
    {
        _SkillInfo = SkillInfo;

        Sprite SkillImage = Managers.Sprite._SkillSprite[_SkillInfo.SkillType];

        GetImage((int)en_SkillExplanationImage.SkillExplanationImage).sprite = SkillImage;
        GetTextMeshPro((int)en_SkillExplanationText.SkillExplanationName).text = _SkillInfo.SkillName;
        if(Managers.String._SkillExplanationString[_SkillInfo.SkillType] != null)
        {
            GetTextMeshPro((int)en_SkillExplanationText.SkillExplanationText).text = Managers.String._SkillExplanationString[_SkillInfo.SkillType];
        }        

        switch(SkillInfo.SkillType)
        {
            case en_SkillType.SKILL_DEFAULT_ATTACK:
            case en_SkillType.SKILL_PUBLIC_ACTIVE_BUF_SHOCK_RELEASE:
            case en_SkillType.SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE:
                GetTextMeshPro((int)en_SkillExplanationText.SkilDamage).text = "";
                GetTextMeshPro((int)en_SkillExplanationText.SkilMinDamageText).text = "";
                GetTextMeshPro((int)en_SkillExplanationText.SkilMaxDamageText).text = "";
                break;
            default:
                GetTextMeshPro((int)en_SkillExplanationText.SkilDamage).text = "피해량";
                GetTextMeshPro((int)en_SkillExplanationText.SkilMinDamageText).text = SkillInfo.SkillMinDamage.ToString() + " ~ ";
                GetTextMeshPro((int)en_SkillExplanationText.SkilMaxDamageText).text = SkillInfo.SkillMaxDamage.ToString();
                break;
        }

        if (_SkillInfo.SkillCastingTime == 0)
        {
            GetTextMeshPro((int)en_SkillExplanationText.SkillExplanationCastingTime).text = "즉시 시전";
        }
        else
        {
            GetTextMeshPro((int)en_SkillExplanationText.SkillExplanationCastingTime).text = "시전 시간 : " + (_SkillInfo.SkillCastingTime / 1000.0f).ToString() + " 초";
        }

        GetTextMeshPro((int)en_SkillExplanationText.SkillExplanationCoolTime).text = " 재사용대기 시간 : " + (_SkillInfo.SkillCoolTime / 1000.0f).ToString() + " 초";
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }
}
