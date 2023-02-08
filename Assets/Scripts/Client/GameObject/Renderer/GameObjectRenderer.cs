using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class GameObjectRenderer : MonoBehaviour
{
    protected SpriteRenderer _SpriteRenderer;

    [field: SerializeField]
    public UnityEvent<int> OnBackwardMovement { get; set; }

    private void Awake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // �Ű� ������ ���� ��ġ �ٶ󺸴� ���� ���͸� ���ϰ�
    // Sprite�� �����ų� �״�� ��
    public void FaceDirection(Vector2 Pointerinput)
    {
        var Direction = (Vector3)Pointerinput - transform.position;
        var Result = Vector3.Cross(Vector2.up, Direction);
        if (Result.z > 0)
        {
            _SpriteRenderer.flipX = true;
        }
        else if (Result.z < 0)
        {
            _SpriteRenderer.flipX = false;
        }
    }

    public void CheckIfBackwardMovement(Vector2 MovementVector)
    {
        float Angle = 0;
        if (_SpriteRenderer.flipX == true)
        {
            Angle = Vector2.Angle(-transform.right, MovementVector);
        }
        else
        {
            Angle = Vector2.Angle(transform.right, MovementVector);
        }

        if (Angle > 90)
        {
            OnBackwardMovement?.Invoke(-1);
        }
        else
        {
            OnBackwardMovement?.Invoke(1);
        }
    }
}
