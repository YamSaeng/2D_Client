using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SpeechBubble : MonoBehaviour
{
    private CreatureObject _Owner;

    public TextMeshPro _SpeechBubbleText = new TextMeshPro();
    public GameObject _SpeechBubbleBackGroundGO;
    public GameObject _SpeechBubbleSpriteUpGo;

    private RectTransform SpeechBubbleTextRectTransform;
    private RectTransform SpeechBubbleSpriteUpRectTransform;
    
    private Coroutine _SpeechBubbleInvisibleCO = null;

    private Vector3 _InitSpritePosition;
    private Vector3 _InitSpriteScale;
    private Vector3 _InitTextPosition;

    public void SetOwner(CreatureObject OwnerObject)
    {
        _Owner = OwnerObject;

        _SpeechBubbleText = gameObject.transform.Find("SpeechBubbleText").GetComponent<TextMeshPro>();
        _SpeechBubbleSpriteUpGo = gameObject.transform.Find("SpeechBubbleSpriteUp").gameObject;

        _InitSpritePosition = _SpeechBubbleSpriteUpGo.GetComponent<RectTransform>().localPosition;
        _InitSpriteScale = _SpeechBubbleSpriteUpGo.GetComponent<RectTransform>().localScale;

        _InitTextPosition = _SpeechBubbleText.GetComponent<RectTransform>().localPosition;
    }

    public void SetSpeech(string SpeechString)
    {
        ShowCloseUI(true);

        SpeechBubbleTextRectTransform = _SpeechBubbleText.GetComponent<RectTransform>();
        SpeechBubbleSpriteUpRectTransform = _SpeechBubbleSpriteUpGo.GetComponent<RectTransform>();

        float SpeechBubbleBackGroundX = _SpeechBubbleText.preferredWidth;

        int SpeechStringLength = SpeechString.Length;

        char[] SpeechStringArray = SpeechString.ToCharArray();

        int InsertIndex = 1;

        SpeechBubbleTextRectTransform.transform.localPosition = _InitTextPosition;
        SpeechBubbleSpriteUpRectTransform.transform.localPosition = _InitSpritePosition;
        SpeechBubbleSpriteUpRectTransform.transform.localScale = _InitSpriteScale;

        for (int i = 1; i < SpeechStringLength; i++)
        {
            if (i % 10 == 0)
            {
                SpeechString = SpeechString.Insert((10 * InsertIndex) + (System.Environment.NewLine.Length * (InsertIndex - 1)), System.Environment.NewLine);
                InsertIndex++;

                // 채팅 텍스트 위치 조정
                SpeechBubbleTextRectTransform.transform.localPosition =
                    new Vector3(SpeechBubbleTextRectTransform.transform.localPosition.x,
                    SpeechBubbleTextRectTransform.transform.localPosition.y + 0.25f,
                    SpeechBubbleTextRectTransform.transform.localPosition.z);
                // 말풍선 그림 위치 조정
                SpeechBubbleSpriteUpRectTransform.transform.localPosition =
                    new Vector3(SpeechBubbleSpriteUpRectTransform.transform.localPosition.x,
                    SpeechBubbleSpriteUpRectTransform.transform.localPosition.y + 0.12f,
                    SpeechBubbleSpriteUpRectTransform.transform.localPosition.z);
                // 말풍성 그림 크기 조정
                SpeechBubbleSpriteUpRectTransform.transform.localScale =
                    new Vector3(SpeechBubbleSpriteUpRectTransform.transform.localScale.x,
                    SpeechBubbleSpriteUpRectTransform.transform.localScale.y + 0.45f,
                    SpeechBubbleSpriteUpRectTransform.transform.localScale.z);
            }
        }

        _SpeechBubbleText.text = SpeechString;
        
        if(_SpeechBubbleInvisibleCO != null)
        {
            StopCoroutine(_SpeechBubbleInvisibleCO);

            _SpeechBubbleInvisibleCO = StartCoroutine("SpeechBubbleInvisibleCO");
        }
        else
        {
            _SpeechBubbleInvisibleCO = StartCoroutine("SpeechBubbleInvisibleCO");
        }        
    }

    IEnumerator SpeechBubbleInvisibleCO()
    {
        yield return new WaitForSeconds(3.0f);

        if (_Owner != null)
        {
            gameObject.SetActive(false);
        }        
    }    

    public void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);        
    }
}
