using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_HPBar : MonoBehaviour
{
    // HPBar 선택 여부 판단 
    bool _IsSelectTargetHPBar = false;

    [SerializeField]
    Transform _HpBar = null;  

    Coroutine _HPBarHide;

    public void Init(float PositionX, float PositionY)
    {
        gameObject.transform.position = new Vector3(PositionX, PositionY, 0);
    }

    public void SetHPBar(float Ratio)
    {
        //Ratio의 값을 0 ~ 1 사이의 값으로 유지해준다.
        float HP = Mathf.Clamp(Ratio, 0, 1);
        _HpBar.localScale = new Vector3(HP, 1, 1);          
    }
   
    // HPBar 선택 ( HPBar를 선택하면 HPBar 숨기기 코루틴 중지 )
    public void SelectTargetHPBar(bool IsSelectTargetHPBar)
    {        
        _IsSelectTargetHPBar = IsSelectTargetHPBar;

        gameObject.SetActive(IsSelectTargetHPBar);

        if(IsSelectTargetHPBar == true)
        {
            if (_HPBarHide != null)
            {
                StopCoroutine(_HPBarHide);
            }
        }             
    }


    public void ActiveChoiceUI(bool Active)
    {
        gameObject.SetActive(Active);

        if (_IsSelectTargetHPBar == true)
        {
            return;    
        }

        if (Active == true)
        {
            // 실행 중인 코루틴 있을 경우
            if (_HPBarHide != null)
            {
                // 멈추고
                StopCoroutine(_HPBarHide);
                // 새로 시작
                _HPBarHide = StartCoroutine("HPBarHide");
            }
            else
            {
                // 실행 중인 코루틴 없을 경우
                // HP Bar 숨기기 코루틴 시작
                _HPBarHide = StartCoroutine("HPBarHide");
            }
        }
        else
        {
            StopCoroutine(_HPBarHide);
            _HPBarHide = null;
        }
    }

    IEnumerator HPBarHide()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);

        _HPBarHide = null;
    }
}
