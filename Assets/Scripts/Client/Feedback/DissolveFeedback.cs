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
        // DOTween을 관리하는 Sequnce를 가져온다.
        var Sequence = DOTween.Sequence();
        // 시퀀스가 실행중인 트윈목록에서 마지막에 추가한다.
        // 머터리얼이 가지고 있는 _Dissolve을 _Duration시간 동안 0 으로 수렴하는 작업
        Sequence.Append(_SpriteRenderer.material.DOFloat(0, "_Dissolve", _Duration));
        // 할당된 이벤트가 있으면 
        if (DeathEvent != null)
        {
            // 설정해준 트윈이 완료되고 나서 함수( DeathCallBack에 연결되어 있는 함수 )를 호출할 수 있도록 함
            Sequence.AppendCallback(() => DeathEvent.Invoke());
        }
    }    
}
