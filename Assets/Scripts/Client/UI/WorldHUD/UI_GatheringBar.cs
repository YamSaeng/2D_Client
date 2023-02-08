using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_GatheringBar : MonoBehaviour
{
    [HideInInspector]
    Transform _GatheringBarScale = null;

    [HideInInspector]
    TextMeshPro _GatheringName;

    [HideInInspector]
    TextMeshPro _GatheringTimeText;

    float _GatheringTimeSpeed;
    float _GatheringTime;
    
    private void Start()
    {
        _GatheringBarScale = transform.Find("GatheringBarScale").transform;
        _GatheringName = transform.Find("GatheringName").GetComponent<TextMeshPro>();
        _GatheringTimeText = transform.Find("GatheringTime").GetComponent<TextMeshPro>();

        gameObject.SetActive(false);
    }

    public void Init(float PositionX, float PositionY)
    {
        gameObject.transform.position = new Vector3(PositionX, PositionY, 0);
    }

    public void GatheringStart(string GatheringName, float GatheringTime, float GatheringSpeed)
    {
        _GatheringName.text = GatheringName;
        _GatheringTime = GatheringTime;
        _GatheringTimeSpeed = 1.0f / GatheringSpeed;
        _GatheringTimeText.text = _GatheringTime.ToString();

        gameObject.SetActive(true);

        StartCoroutine(GatheringBarFill());
    }

    IEnumerator GatheringBarFill()
    {
        float TimePassed = Time.deltaTime;

        float Progress = 0.0f;

        while (Progress <= 1.0f)
        {
            // 스펠바 채울 비율 구하기 0 ~ 1 사이로
            float FillAmount = Mathf.Lerp(0, 1.38f, Progress);
            _GatheringBarScale.localScale = new Vector3(FillAmount, 1, 1);
            Progress += _GatheringTimeSpeed * Time.deltaTime;
            
            // 남은 캐스팅 시간 구하기 위해서 deltaTime을 누적하고 _SpellTime에서 빼준다.
            TimePassed += Time.deltaTime;

            // 소수점 1자리 까지 출력
            _GatheringTimeText.text = (_GatheringTime - TimePassed).ToString("F1");
            yield return null;
        }
    }

    public void GatheringEnd()
    {
        _GatheringBarScale.localScale = new Vector3(0, 1, 1);
        gameObject.SetActive(false);
    }
}
