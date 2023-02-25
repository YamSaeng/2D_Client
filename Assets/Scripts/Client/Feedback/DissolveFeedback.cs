using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DissolveFeedback : Feedback
{
    [SerializeField]
    private SpriteRenderer _SpriteRenderer = null;
    [SerializeField]
    private float _Duration = 0.05f;

    [field: SerializeField]
    public UnityEvent DeathEvent { get; set; }

    public override void CompletePreviousFeedback()
    {
        _SpriteRenderer.DOComplete();
        _SpriteRenderer.material.DOComplete();        
    }

    public override void StartFeedback()
    {
        // DOTween�� �����ϴ� Sequnce�� �����´�.
        var Sequence = DOTween.Sequence();
        // �������� �������� Ʈ����Ͽ��� �������� �߰��Ѵ�.
        // ���͸����� ������ �ִ� _Dissolve�� _Duration�ð� ���� 0 ���� �����ϴ� �۾�
        Sequence.Append(_SpriteRenderer.material.DOFloat(0, "_Dissolve", _Duration));
        // �Ҵ�� �̺�Ʈ�� ������ 
        if (DeathEvent != null)
        {
            // �������� Ʈ���� �Ϸ�ǰ� ���� �Լ�( DeathCallBack�� ����Ǿ� �ִ� �Լ� )�� ȣ���� �� �ֵ��� ��
            Sequence.AppendCallback(() => DeathEvent.Invoke());
        }
    }    
}
