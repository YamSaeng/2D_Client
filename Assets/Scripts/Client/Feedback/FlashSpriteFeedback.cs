using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSpriteFeedback : Feedback
{
    [SerializeField]
    private SpriteRenderer _SpriteRenderer = null;

    // 깜빡이는 시간
    [SerializeField]
    private float _FlashTime = 0.1f;

    // 깜빡일때 표시해줄 머터리얼
    [SerializeField]
    private Material _FlashMaterial = null;

    // 깜빡이고 나서 원래대로 돌아갈때 사용할 기본 값
    private Shader _OriginalMaterialShader;

    private void Start()
    {
        _OriginalMaterialShader = _SpriteRenderer.material.shader;
    }

    public override void CompletePreviousFeedback()
    {
        // 코루틴 모두 중지하고 기본 쉐이더로 되돌아감
        StopAllCoroutines();
        _SpriteRenderer.material.shader = _OriginalMaterialShader;
    }

    public override void StartFeedback()
    {
        // MakeSolidColor 이 있는지 확인
        if (_SpriteRenderer.material.HasProperty("_MakeSolidColor") == false)
        {
            // 있을 경우 쉐이더를 바꿔줌 
            _SpriteRenderer.material.shader = _FlashMaterial.shader;
        }

        // 깜빡일 수 있도록 MakeSolidColor 활성화
        _SpriteRenderer.material.SetInt("_MakeSolidColor", 1);

        // 일정 시간 후에 원래대로 되돌릴 수 있도록 코루틴 시작
        StartCoroutine(WaitBeforeChangingBackCoroutine());
    }

    IEnumerator WaitBeforeChangingBackCoroutine()
    {
        yield return new WaitForSeconds(_FlashTime);
        // 일정 시간 후에 MakeSolidColor을 0으로 돌려서 원래대로 돌아감
        if (_SpriteRenderer.material.HasProperty("_MakeSolidColor"))
        {
            _SpriteRenderer.material.SetInt("_MakeSolidColor", 0);
        }
        else
        {
            _SpriteRenderer.material.shader = _OriginalMaterialShader;
        }
    }
}
