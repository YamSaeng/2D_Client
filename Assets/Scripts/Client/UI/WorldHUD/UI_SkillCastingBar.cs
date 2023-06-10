using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillCastingBar : MonoBehaviour
{
    [HideInInspector]
    Transform _SkillCastingBarScale = null;

    [HideInInspector]
    TextMeshPro _SkillCastingName;

    [HideInInspector]
    TextMeshPro _SkillCastingTimeText;
        
    float _SkillCastingTimeSpeed;
    float _SkillCastingTime;

    private void Start()
    {
        _SkillCastingBarScale = transform.Find("SkillCastingBarScale").transform;
        _SkillCastingName = transform.Find("SkillCastingName").GetComponent<TextMeshPro>();
        _SkillCastingTimeText = transform.Find("SkillCastingTime").GetComponent<TextMeshPro>();

        gameObject.SetActive(false);
    }
    
    public void SpellStart(string SkillCastingName, float SkillCastingTime, float SkillCastingSpeed)
    {
        // 시전 기술 이름 저장
        _SkillCastingName.text = SkillCastingName;
        // 시전 캐스팅 시간
        _SkillCastingTime = SkillCastingTime;
        // 시전 캐스팅 바가 차오르는 속도
        _SkillCastingTimeSpeed = 1.0f / SkillCastingTime;        
        // 시전 캐스팅 남은 시간
        _SkillCastingTimeText.text = _SkillCastingTime.ToString();
        // 시전 기술 바 활성화
        gameObject.SetActive(true);

        // 시전 기술 바 채우기 시작
        StartCoroutine(SkillCastingBarFill());
    }

    IEnumerator SkillCastingBarFill()
    {
        float TimePassed = Time.deltaTime;

        float Progress = 0.0f;

        while (Progress <= 1.0f)
        {
            // 스펠바 채울 비율 구하기 0 ~ 1 사이로
            float FillAmount = Mathf.Lerp(0, 1.38f, Progress);
            _SkillCastingBarScale.localScale = new Vector3(FillAmount, 1, 1);
            Progress += _SkillCastingTimeSpeed * Time.deltaTime;
            
            // 남은 캐스팅 시간 구하기 위해서 deltaTime을 누적하고 _SpellTime에서 빼준다.
            TimePassed += Time.deltaTime;

            // 소수점 1자리 까지 출력
            _SkillCastingTimeText.text = (_SkillCastingTime - TimePassed).ToString("F1");

            if(Progress >= 1.0f)
            {
                SkillCastingEnd();
            }

            yield return null;
        }
    }

    public void SkillCastingEnd()
    {         
        // 스펠바 비활성화
        _SkillCastingBarScale.localScale = new Vector3(0, 1, 1);
        gameObject.SetActive(false);
    }   
}
