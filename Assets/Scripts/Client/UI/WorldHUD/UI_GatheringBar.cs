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
            // ����� ä�� ���� ���ϱ� 0 ~ 1 ���̷�
            float FillAmount = Mathf.Lerp(0, 1.38f, Progress);
            _GatheringBarScale.localScale = new Vector3(FillAmount, 1, 1);
            Progress += _GatheringTimeSpeed * Time.deltaTime;
            
            // ���� ĳ���� �ð� ���ϱ� ���ؼ� deltaTime�� �����ϰ� _SpellTime���� ���ش�.
            TimePassed += Time.deltaTime;

            // �Ҽ��� 1�ڸ� ���� ���
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
