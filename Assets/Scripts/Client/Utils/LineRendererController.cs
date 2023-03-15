using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererController : MonoBehaviour
{
    private CreatureObject _OwnCreature;
    private LineRenderer _LineRenderer;
    private List<Vector3> _MainRectCollision = new List<Vector3>();
    private List<st_RayCastingPosition> _RayCastingPositions = new List<st_RayCastingPosition>();

    private void Awake()
    {
        _LineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpOwnPlayer(CreatureObject OwnPlayer)
    {
        _OwnCreature = OwnPlayer;
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

        if(_RayCastingPositions.Count > 0)
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
            Vector2 Size = new Vector2();            

            switch (_OwnCreature._GameObjectInfo.ObjectType)
            {
                case en_GameObjectType.OBJECT_PLAYER:
                case en_GameObjectType.OBJECT_GOBLIN:
                case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                    Size.x = 1.0f;
                    Size.y = 1.0f;
                    break;
                case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                    Size.x = 2.0f;
                    Size.y = 2.0f;
                    break;
            }

            Vector3 LeftTop = new Vector3(OwnPlayerPosition.x, OwnPlayerPosition.y);
            Vector3 RightTop = new Vector3(OwnPlayerPosition.x + Size.x, OwnPlayerPosition.y);
            Vector3 RightDown = new Vector3(OwnPlayerPosition.x + Size.x, OwnPlayerPosition.y - Size.y);
            Vector3 LeftDown = new Vector3(OwnPlayerPosition.x, OwnPlayerPosition.y - Size.y);
            Vector3 LeftDownToTop = new Vector3(OwnPlayerPosition.x, OwnPlayerPosition.y);

            _MainRectCollision.Add(LeftTop);
            _MainRectCollision.Add(RightTop);
            _MainRectCollision.Add(RightDown);
            _MainRectCollision.Add(LeftDown);
            _MainRectCollision.Add(LeftDownToTop);

            _LineRenderer.positionCount = _MainRectCollision.Count;
            _LineRenderer.startWidth = 0.04f;
            _LineRenderer.endWidth = 0.04f;
        }
    }

    public void UpdateRayCasting()
    {
        if (_OwnCreature != null && _LineRenderer != null)
        {

        }
    }
}
