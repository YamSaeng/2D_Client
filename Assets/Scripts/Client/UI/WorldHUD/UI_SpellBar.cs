using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SpellBar : MonoBehaviour
{
    [HideInInspector]
    Transform _SpellBarScale = null;

    [HideInInspector]
    TextMeshPro _SpellName;

    [HideInInspector]
    TextMeshPro _SpellCastingTimeText;
        
    float _SpellTimeSpeed;
    float _SpellTime;

    private void Start()
    {
        _SpellBarScale = transform.Find("SpellBarScale").transform;
        _SpellName = transform.Find("SpellName").GetComponent<TextMeshPro>();
        _SpellCastingTimeText = transform.Find("SpellCastingTime").GetComponent<TextMeshPro>();

        gameObject.SetActive(false);
    }
        
    public void Init(float PositionX, float PositionY)
    {
        gameObject.transform.position = new Vector3(PositionX, PositionY, 0);
    }

    public void SpellStart(string SpellName, float SpellTime, float SpellSpeed)
    {
        // 마법 이름 저장
        _SpellName.text = SpellName;
        // 마법 캐스팅 시간
        _SpellTime = SpellTime;
        // 마법 캐스팅 바가 차오르는 속도
        _SpellTimeSpeed = 1.0f / SpellTime;        
        // 마법 캐스팅 남은 시간
        _SpellCastingTimeText.text = _SpellTime.ToString();
        // 스펠 바 활성화
        gameObject.SetActive(true);

        // 캐스팅 바 채우기 시작
        StartCoroutine(SpellBarFill());
    }

    IEnumerator SpellBarFill()
    {
        float TimePassed = Time.deltaTime;

        float Progress = 0.0f;

        while (Progress <= 1.0f)
        {
            // 스펠바 채울 비율 구하기 0 ~ 1 사이로
            float FillAmount = Mathf.Lerp(0, 1.38f, Progress);
            _SpellBarScale.localScale = new Vector3(FillAmount, 1, 1);
            Progress += _SpellTimeSpeed * Time.deltaTime;
            
            // 남은 캐스팅 시간 구하기 위해서 deltaTime을 누적하고 _SpellTime에서 빼준다.
            TimePassed += Time.deltaTime;

            // 소수점 1자리 까지 출력
            _SpellCastingTimeText.text = (_SpellTime - TimePassed).ToString("F1");
            yield return null;
        }
    }

    public void SpellEnd()
    {         
        // 스펠바 비활성화
        _SpellBarScale.localScale = new Vector3(0, 1, 1);
        gameObject.SetActive(false);
    }   
}
