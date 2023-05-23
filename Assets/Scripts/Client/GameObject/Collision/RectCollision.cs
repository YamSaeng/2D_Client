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

    private en_CollisionPosition _CollisionPositionType;

    public Vector2 _Direction;

    public Vector2 _Position;

    public Vector2 _MiddlePosition;

    public Vector2 _LeftTop;
    public Vector2 _RightTop;
    public Vector2 _RightDown;
    public Vector2 _LeftDown;        
    private Vector2 _LeftDownToTop;

    public Vector2 _CreatePositionSize;
    public Vector2 _Size;

    private float _Angle;    

    private void Awake()
    {
        _LineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpOwnPlayer(CBaseObject OwnPlayer)
    {
        _OwnCreature = OwnPlayer;                          

        switch (OwnPlayer._GameObjectInfo.ObjectType)
        {
            case en_GameObjectType.OBJECT_PLAYER:
            case en_GameObjectType.OBJECT_PLAYER_DUMMY:
            case en_GameObjectType.OBJECT_NON_PLAYER_GENERAL_MERCHANT:
                _Size.x = 1.0f;
                _Size.y = 1.0f;

                _CollisionPositionType = en_CollisionPosition.COLLISION_POSITION_OBJECT;

                _Direction = Vector2.zero;
                break;
            case en_GameObjectType.OBJECT_WALL:
                _Size.x = 1.0f;
                _Size.y = 1.0f;

                _CollisionPositionType = en_CollisionPosition.COLLISION_POSITION_OBJECT;
                break;
            case en_GameObjectType.OBJECT_GOBLIN:
                _Size.x = 1.0f;
                _Size.y = 1.0f;

                _CollisionPositionType = en_CollisionPosition.COLLISION_POSITION_OBJECT;

                _Direction = Vector2.zero;
                break;
            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                _Size.x = 2.0f;
                _Size.y = 2.0f;

                _CollisionPositionType = en_CollisionPosition.COLLISION_POSITION_OBJECT;

                _Direction = Vector2.zero;
                break;
            case en_GameObjectType.OBJECT_SKILL_SWORD_BLADE:
                _Size.x = 1.0f;
                _Size.y = 0.5f;

                _CollisionPositionType = en_CollisionPosition.COLLISION_POSITION_SKILL_MIDDLE;

                _Direction = _OwnCreature._GameObjectInfo.ObjectPositionInfo.LookAtDireciton;
                break;
            default:
                _Direction = _OwnCreature._GameObjectInfo.ObjectPositionInfo.LookAtDireciton;
                break;
        }
    }

    public void SetPositionDirection(en_CollisionPosition CollisionPositionType, Vector2 Position, Vector2 Direction, Vector2 CreatePositionSize, Vector2 Size)
    {
        _CollisionPositionType = CollisionPositionType;
        _Position = Position;
        _LeftTop = _Position;
        _Direction = Direction;
        _CreatePositionSize = CreatePositionSize;
        _Size = Size;
        
        StartCoroutine(RectCollisionDestoryCoroutine());        
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
        if (_LineRenderer != null)
        {
            PositionUpdate();
            RotateUpdate();

            _Angle = Mathf.Rad2Deg * Mathf.Atan2(_Direction.x, _Direction.x);                        

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
        if(_OwnCreature != null)
        {
            _Position = _OwnCreature.transform.position;
        }

        switch (_CollisionPositionType)
        {
            case en_CollisionPosition.COLLISION_POSITION_OBJECT:            
                _MiddlePosition.x = _Position.x + _Size.x / 2.0f;
                _MiddlePosition.y = _Position.y - _Size.y / 2.0f;
                _LeftTop = _Position;
                break;
            case en_CollisionPosition.COLLISION_POSITION_SKILL_MIDDLE:
                _MiddlePosition.x = _Position.x + _CreatePositionSize.x / 2.0f;
                _MiddlePosition.y = _Position.y - _CreatePositionSize.y / 2.0f;

                _LeftTop.x = _MiddlePosition.x - _Size.x / 2.0f;
                _LeftTop.y = _MiddlePosition.y + _Size.y / 2.0f;
                break;
            case en_CollisionPosition.COLLISION_POSITION_SKILL_FRONT:
                _MiddlePosition.x = _Position.x + _CreatePositionSize.x / 2.0f;
                _MiddlePosition.y = _Position.y - _CreatePositionSize.y / 2.0f;

                _LeftTop = _Position;
                break;
            default:
                _LeftTop = _Position;
                break;
        }               

        _LeftDown.x = _LeftTop.x;
        _LeftDown.y = _LeftTop.y - _Size.y;

        _RightTop.x = _LeftTop.x + _Size.x;
        _RightTop.y = _LeftTop.y;

        _RightDown.x = _LeftTop.x + _Size.x;
        _RightDown.y = _LeftTop.y - _Size.y;

        _LeftDownToTop = _LeftTop;
    }

    private void RotateUpdate()
    {
        _Angle = Mathf.Rad2Deg * Mathf.Atan2(_Direction.x, _Direction.x);
        if(_Angle == 0)
        {
            return;
        }

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

    IEnumerator RectCollisionDestoryCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
