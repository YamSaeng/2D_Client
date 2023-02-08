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
        SkillExplanationCoolTime
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
        GetTextMeshPro((int)en_SkillExplanationText.SkillExplanationText).text = _SkillInfo.SkillExplanation;
        if(_SkillInfo.SkillCastingTime == 0)
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
