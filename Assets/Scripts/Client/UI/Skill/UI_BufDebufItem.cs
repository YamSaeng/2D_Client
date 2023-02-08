using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BufDebufItem : UI_Base
{
    public st_SkillInfo _SkillInfo;

    private float _SkillCoolTime;
    private float _SkillRemainTime;
    private float _SkillCoolTimeSpeed;

    private Coroutine _SkillBufDeBufCoolTimeCoroutine;

    private UI_GameScene _GameSceneUI;
    private bool _MousePointerEnterExit;

    private Coroutine _SkillBufDeBufCoolTimeCO;

    enum en_BufDebufImage
    {
        BufDebufSkillIconImage,
        BufDebufSkillCoolTimeImage
    }

    enum en_BufDebufText
    {
        BufDebufCoolTimeText,
        BufDebufSkillOverlapStepText
    }

    public override void Init()
    {
        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

        Bind<Image>(typeof(en_BufDebufImage));
        Bind<TextMeshProUGUI>(typeof(en_BufDebufText));

        BindEvent(GetImage((int)en_BufDebufImage.BufDebufSkillIconImage).gameObject, OnBufDeBufItemPointerEnter, Define.en_UIEvent.PointerEnter);
        BindEvent(GetImage((int)en_BufDebufImage.BufDebufSkillIconImage).gameObject, OnBufDeBufItemPointerExit, Define.en_UIEvent.PointerExit);
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        
    }


    private void OnBufDeBufItemPointerEnter(PointerEventData PointerEnterEvent)
    {
        _MousePointerEnterExit = true;
        
        _GameSceneUI.SetSkillExplanation(_SkillInfo);
    }

    private void OnBufDeBufItemPointerExit(PointerEventData PointerEnterEvent)
    {
        _MousePointerEnterExit = false;

        _GameSceneUI.EmptySkillExplanation();
    }

    public void SetSkillBufItem(float SkillCoolTimeSpeed, st_SkillInfo SkillInfo)
    {
        _SkillRemainTime = SkillInfo.SkillRemainTime / 1000.0f;
        _SkillCoolTime = SkillInfo.SkillDurationTime / 1000.0f;
        _SkillCoolTimeSpeed = 1.0f / _SkillCoolTime;
        _SkillInfo = SkillInfo;

        if (SkillInfo.SkillDurationTime > 0)
        {
            if(_SkillInfo.SkillOverlapStep > 0)
            {
                GetTextMeshPro((int)en_BufDebufText.BufDebufSkillOverlapStepText).text = _SkillInfo.SkillOverlapStep.ToString();
            }

            GetTextMeshPro((int)en_BufDebufText.BufDebufCoolTimeText).text = _SkillRemainTime.ToString("F1");            
            GetImage((int)en_BufDebufImage.BufDebufSkillIconImage).sprite = Managers.Sprite._SkillSprite[_SkillInfo.SkillType];

            if(_SkillBufDeBufCoolTimeCO != null)
            {
                StopCoroutine(_SkillBufDeBufCoolTimeCO);                
            }

            _SkillBufDeBufCoolTimeCO = StartCoroutine("SkillBufDeBufCoolTimeStart");
        }
    }         

    IEnumerator SkillBufDeBufCoolTimeStart()
    {
        float TimePassed = Time.deltaTime;

        float Rate = _SkillCoolTimeSpeed;

        float RemainProgress = _SkillRemainTime / _SkillCoolTime;

        float Progress = Mathf.Abs(1.0f - RemainProgress);

        while (Progress <= 1.0f)
        {
            float FillAmount = Mathf.Lerp(0, 1, Progress);
            GetImage((int)en_BufDebufImage.BufDebufSkillCoolTimeImage).fillAmount = FillAmount;
            Progress += Rate * Time.deltaTime;
            
            TimePassed += Time.deltaTime;

            GetTextMeshPro((int)en_BufDebufText.BufDebufCoolTimeText).text = (_SkillRemainTime - TimePassed).ToString("F1");
            
            yield return null;
        }

        if(_MousePointerEnterExit == true)
        {
            _GameSceneUI.EmptySkillExplanation();
        }
    }

    public void DestroyMe()
    {
        //UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        //GameSceneUI._CenterTopPlayerInfo._DeBufItems.Remove(this);

        //GetImage((int)en_BufDebufImage.BufDebufSkillIconImage).sprite = null;

        //Destroy(gameObject);
    }
}
