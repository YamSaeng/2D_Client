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

                _CollisionPositionType = en_CollisionPosition.COLLISION_POSITION_OBJECT;

                _Direction = _OwnCreature._GameObjectInfo.ObjectPositionInfo.LookAtDireciton;
                break;
            case en_GameObjectType.OBJECT_SKILL_FLAME_BOLT:
                _Size.x = 1.0f;
                _Size.y = 0.7f;

                _CollisionPositionType = en_CollisionPosition.COLLISION_POSITION_OBJECT;

                _Direction = _OwnCreature._GameObjectInfo.ObjectPositionInfo.LookAtDireciton;
                break;
            case en_GameObjectType.OBJECT_SKILL_DIVINE_BOLT:
                _Size.x = 1.0f;
                _Size.y = 0.7f;

                _CollisionPositionType = en_CollisionPosition.COLLISION_POSITION_OBJECT;

                _Direction = _OwnCreature._GameObjectInfo.ObjectPositionInfo.LookAtDireciton;
                break;
            default:
                _Direction = _OwnCreature._GameObjectInfo.ObjectPositionInfo.LookAtDireciton;
                break;
        }
    }

    public void SetPositionDirection(en_CollisionPosition CollisionPositionType, Vector2 Position, Vector2 Direction, Vector2 Size)
    {
        _CollisionPositionType = CollisionPositionType;
        
        _Position = Position;        
        _Direction = Direction;        
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
        if (_OwnCreature != null)
        {
            _Position = _OwnCreature.transform.position;
        }        

        _LeftTop.x = _Position.x - _Size.x / 2.0f;
        _LeftTop.y = _Position.y + _Size.y / 2.0f;

        _RightTop.x = _Position.x + _Size.x / 2.0f;
        _RightTop.y = _Position.y + _Size.y / 2.0f;

        _LeftDown.x = _Position.x - _Size.x / 2.0f;
        _LeftDown.y = _Position.y - _Size.y / 2.0f;

        _RightDown.x = _Position.x + _Size.x / 2.0f;
        _RightDown.y = _Position.y - _Size.y / 2.0f;

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

        Vector2 LeftTopRot = RotationMatrix * (_LeftTop - _Position);
        Vector2 LeftDownRot = RotationMatrix * (_LeftDown - _Position);
        Vector2 RightTopRot = RotationMatrix * (_RightTop - _Position);
        Vector2 RightDownRot = RotationMatrix * (_RightDown - _Position);
        Vector2 LeftDownToTopRotate = RotationMatrix * (_LeftTop - _Position);

        _LeftTop = LeftTopRot + _Position;
        _LeftDown = LeftDownRot + _Position;
        _RightTop = RightTopRot + _Position;
        _RightDown = RightDownRot + _Position;
        _LeftDownToTop = LeftDownToTopRotate + _Position;
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
