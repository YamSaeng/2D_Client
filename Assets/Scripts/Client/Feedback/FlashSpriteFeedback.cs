using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSpriteFeedback : Feedback
{
    [SerializeField]
    private SpriteRenderer _SpriteRenderer = null;

    // �����̴� �ð�
    [SerializeField]
    private float _FlashTime = 0.1f;

    // �����϶� ǥ������ ���͸���
    [SerializeField]
    private Material _FlashMaterial = null;

    // �����̰� ���� ������� ���ư��� ����� �⺻ ��
    private Shader _OriginalMaterialShader;

    private void Start()
    {
        _OriginalMaterialShader = _SpriteRenderer.material.shader;
    }

    public override void CompletePreviousFeedback()
    {
        // �ڷ�ƾ ��� �����ϰ� �⺻ ���̴��� �ǵ��ư�
        StopAllCoroutines();
        _SpriteRenderer.material.shader = _OriginalMaterialShader;
    }

    public override void StartFeedback()
    {
        // MakeSolidColor �� �ִ��� Ȯ��
        if (_SpriteRenderer.material.HasProperty("_MakeSolidColor") == false)
        {
            // ���� ��� ���̴��� �ٲ��� 
            _SpriteRenderer.material.shader = _FlashMaterial.shader;
        }

        // ������ �� �ֵ��� MakeSolidColor Ȱ��ȭ
        _SpriteRenderer.material.SetInt("_MakeSolidColor", 1);

        // ���� �ð� �Ŀ� ������� �ǵ��� �� �ֵ��� �ڷ�ƾ ����
        StartCoroutine(WaitBeforeChangingBackCoroutine());
    }

    IEnumerator WaitBeforeChangingBackCoroutine()
    {
        yield return new WaitForSeconds(_FlashTime);
        // ���� �ð� �Ŀ� MakeSolidColor�� 0���� ������ ������� ���ư�
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
