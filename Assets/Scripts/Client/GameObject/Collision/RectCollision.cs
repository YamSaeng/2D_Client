using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RectCollision : MonoBehaviour
{
    private CBaseObject _OwnCreature;
    private LineRenderer _LineRenderer;
    private List<Vector3> _MainRectCollision = new List<Vector3>();
    private List<st_RayCastingPosition> _RayCastingPositions = new List<st_RayCastingPosition>();

    private Vector2 _Direction;

    private Vector2 _Position;

    private Vector2 _MiddlePosition;

    private Vector2 _LeftTop;
    private Vector2 _LeftDown;
    private Vector2 _RightTop;
    private Vector2 _RightDown;
    private Vector2 _LeftDownToTop;

    private Vector2 _Size;

    private void Awake()
    {
        _LineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpOwnPlayer(CBaseObject OwnPlayer)
    {
        _OwnCreature = OwnPlayer;
        _Direction = _OwnCreature._GameObjectInfo.ObjectPositionInfo.LookAtDireciton;
    }

    public void SetDirection(Vector2 Direction)
    {
        _Direction = Direction;
    }

    public void SetRayCastingPositions(st_RayCastingPosition[] RayCastingPositions)
    {
        _RayCastingPositions.Clear();

        for (int i = 0; i < RayCastingPositions.Length; i++)
        {
            _RayCastingPositions.Add(RayCastingPositions[i]);
        }
    }

    private void Update()
    {
        UpdateMainRectCollision();

        if (_MainRectCollision.Count > 0)
        {
            for (int i = 0; i < _MainRectCollision.Count; i++)
            {
                _LineRenderer.SetPosition(i, _MainRectCollision[i]);
            }
        }

        if (_RayCastingPositions.Count > 0)
        {
            int RayCastingPosition = _MainRectCollision.Count;

            for (int i = 0; i < _RayCastingPositions.Count; i++)
            {
                Vector2 StartPosition = _RayCastingPositions[i].StartPosition;
                Vector2 EndPosition = _RayCastingPositions[i].EndPosition;

                Vector2 Dir = (EndPosition - StartPosition).normalized;

                Debug.DrawRay(StartPosition, Dir * (EndPosition - StartPosition).magnitude, new Color(0, 1, 0));
            }
        }

        _MainRectCollision.Clear();
    }

    public void UpdateMainRectCollision()
    {
        if (_OwnCreature != null && _LineRenderer != null)
        {
            Vector3 OwnPlayerPosition = _OwnCreature.transform.position;

            switch (_OwnCreature._GameObjectInfo.ObjectType)
            {
                case en_GameObjectType.OBJECT_PLAYER:
                case en_GameObjectType.OBJECT_GOBLIN:
                case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                    _Size.x = 1.0f;
                    _Size.y = 1.0f;
                    break;
                case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                    _Size.x = 2.0f;
                    _Size.y = 2.0f;
                    break;
                case en_GameObjectType.OBJECT_SKILL_SWORD_BLADE:
                    _Size.x = 1.0f;
                    _Size.y = 0.5f;
                    break;
            }

            PositionUpdate();
            RotateUpdate();

            _MainRectCollision.Add(_LeftTop);
            _MainRectCollision.Add(_RightTop);
            _MainRectCollision.Add(_RightDown);
            _MainRectCollision.Add(_LeftDown);
            _MainRectCollision.Add(_LeftDownToTop);

            _LineRenderer.positionCount = _MainRectCollision.Count;
            _LineRenderer.startWidth = 0.04f;
            _LineRenderer.endWidth = 0.04f;
        }
    }

    private void PositionUpdate()
    {
        _Position = _OwnCreature.transform.position;

        _MiddlePosition.x = _Position.x + _Size.x / 2.0f;
        _MiddlePosition.y = _Position.y - _Size.y / 2.0f;

        _LeftTop = _Position;

        _LeftDown.x = _Position.x;
        _LeftDown.y = _Position.y - _Size.y;

        _RightTop.x = _Position.x + _Size.x;
        _RightTop.y = _Position.y;

        _RightDown.x = _Position.x + _Size.x;
        _RightDown.y = _Position.y - _Size.y;

        _LeftDownToTop = _Position;
    }

    private void RotateUpdate()
    {
        float Sin = Mathf.Sin(Mathf.Atan2(_Direction.y, _Direction.x));
        float Cos = Mathf.Cos(Mathf.Atan2(_Direction.y, _Direction.x));

        Vector2 Basis1 = new Vector2(Cos, Sin);
        Vector2 Basis2 = new Vector2(-Sin, Cos);
        Matrix2x2 RotationMatrix = new Matrix2x2(Basis1, Basis2);

        Vector2 LeftTopRot = RotationMatrix * (_LeftTop - _MiddlePosition);
        Vector2 LeftDownRot = RotationMatrix * (_LeftDown - _MiddlePosition);
        Vector2 RightTopRot = RotationMatrix * (_RightTop - _MiddlePosition);
        Vector2 RightDownRot = RotationMatrix * (_RightDown - _MiddlePosition);
        Vector2 LeftDownToTopRotate = RotationMatrix * (_LeftTop - _MiddlePosition);

        _LeftTop = LeftTopRot + _MiddlePosition;
        _LeftDown = LeftDownRot + _MiddlePosition;
        _RightTop = RightTopRot + _MiddlePosition;
        _RightDown = RightDownRot + _MiddlePosition;
        _LeftDownToTop = LeftDownToTopRotate + _MiddlePosition;
    }

    public void UpdateRayCasting()
    {
        if (_OwnCreature != null && _LineRenderer != null)
        {

        }
    }
}
