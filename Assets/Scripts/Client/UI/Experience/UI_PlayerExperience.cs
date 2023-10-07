using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerExperience : UI_Base
{
    enum en_PlayerExperienceSlider
    {
        ExperienceBar
    }

    enum en_PlayerExperienceText
    {
        CurrentExperienceText,
        RequireExperienceText,
        ExperienceRatioText
    }

    public override void Init()
    {        
    }

    public override void Binding()
    {
        Bind<Slider>(typeof(en_PlayerExperienceSlider));
        Bind<TextMeshProUGUI>(typeof(en_PlayerExperienceText));
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void PlayerGainExp(long GainExp, long CurrentExp, long RequireExp, long TotalExp)
    {
        float CurrentExpRatio = 0.00f;
        float GainExpRatio = 0.00f;

        // 얻은 경험치의 레벨업에 필요한 경험치에 대한 비율
        GainExpRatio = ((float)GainExp) / RequireExp;
        // 현재까지 얻은 경험치의 레벨업에 필요한 경험치에 대한 비율
        CurrentExpRatio = ((float)CurrentExp) / RequireExp;

        GetTextMeshPro((int)en_PlayerExperienceText.CurrentExperienceText).text = CurrentExp.ToString();
        GetTextMeshPro((int)en_PlayerExperienceText.RequireExperienceText).text = RequireExp.ToString();
        GetTextMeshPro((int)en_PlayerExperienceText.ExperienceRatioText).text = (CurrentExpRatio * 100.0f).ToString("F2") + "%";

        GetSlider((int)en_PlayerExperienceSlider.ExperienceBar).value = CurrentExpRatio;
    }    
}
