using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class BaseObject : MonoBehaviour
{
    protected UI_GameScene _GameSceneUI;
    // 상태이상 정보
    protected byte _StatusAbnormal;

    // 강화효과, 약화효과 정보
    public Dictionary<en_SkillType, st_SkillInfo> _Bufs = new Dictionary<en_SkillType, st_SkillInfo>();
    public Dictionary<en_SkillType, st_SkillInfo> _DeBufs = new Dictionary<en_SkillType, st_SkillInfo>();        
    
    // 게임 오브젝트 정보
    public st_GameObjectInfo _GameObjectInfo = new st_GameObjectInfo();         

    // 충돌체 정보
    public Transform[] _RectCollisionPosition;

    // StatInfo 가져오는걸 virtual로 선언해준다. 상속받는 애들이 수정
    public virtual st_StatInfo StatInfo
    {
        get { return _GameObjectInfo.ObjectStatInfo; }
        set
        {
            if (_GameObjectInfo.ObjectStatInfo.Equals(value))
            {                
                return;
            }

            _GameObjectInfo.ObjectStatInfo = value;            
        }
    }  

    //체력 프로퍼티
    //체력도 StatInfo와 마찬가지로 virtual로 선언해준다.
    public virtual int _HP
    {
        get { return StatInfo.HP; }
        set
        {
            StatInfo.HP = value;
            //체력바 업데이트
        }
    }
    
    public st_PositionInfo PositionInfo
    {
        get
        {
            return _GameObjectInfo.ObjectPositionInfo;
        }

        set
        {
            //if(_GameObjectInfo.ObjectPositionInfo.Equals(value))
            //{
            //    return;
            //}

            // 셀 좌표 셋팅
            _CellPosition = new Vector3Int(value.CollsitionPositionX, value.CollsitionPositionY, 0);
            
            _GameObjectInfo.ObjectPositionInfo.PositionX = value.PositionX;
            _GameObjectInfo.ObjectPositionInfo.PositionY = value.PositionY;

            // 상태값 변경
            State = value.State;            
        }
    }
        
    public void SetStatusAbnormal(byte StatusAbnormal)
    {
        // 상태이상 적용
        _StatusAbnormal |= StatusAbnormal;       
    }

    public void ReleaseStatusAbnormal(byte StatusAbnormal)
    {
        // 상태이상 해제
        _StatusAbnormal &= StatusAbnormal;
    }

    //위치 강제로 이동시켜주는 함수 주로 스폰 시킬때 사용
    public virtual void SyncPostion()
    {
        Vector3 DestPosition = Managers.Map.CurrentGrid.CellToWorld(_CellPosition) + new Vector3(0.5f, 0.5f);
        transform.position = DestPosition;
    }

    public Vector3Int _CellPosition
    {
        get
        {
            return new Vector3Int(PositionInfo.CollsitionPositionX, PositionInfo.CollsitionPositionY, 0);
        }

        set
        {
            //동일한 셀 좌표면 나감
            if (PositionInfo.CollsitionPositionX == value.x && PositionInfo.CollsitionPositionY == value.y)
            {                
                return;
            }

            PositionInfo.CollsitionPositionX = value.x;
            PositionInfo.CollsitionPositionY = value.y;            
        }
    }    

    public virtual en_CreatureState State
    {
        get { return PositionInfo.State; }
        set
        {            
            PositionInfo.State = value;                   
        }
    }  

    public void AnimationPlay(string AnimationName)
    {
        //_Animator.Play(AnimationName, -1, 0f);
    }
    
    void Update()
    {
        UpdateController();                   
    }

    public virtual void Init()
    {
        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

        transform.position = new Vector3(_GameObjectInfo.ObjectPositionInfo.PositionX, _GameObjectInfo.ObjectPositionInfo.PositionY, 0);
    }    

    protected virtual void UpdateController()
    {             
        if(_GameObjectInfo.ObjectId == Managers.NetworkManager._PlayerDBId)
        {
            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                UI_Minimap MiniMap = GameSceneUI._Minimap;
                if (MiniMap != null)
                {
                    MiniMap.MiniMapMyPositionUpdate((int)gameObject.transform.position.x, (int)gameObject.transform.position.y);
                }
            }
        }        

        switch (State)
        {
            case en_CreatureState.SPAWN_IDLE:
            case en_CreatureState.IDLE:                
                break;
            case en_CreatureState.PATROL:                
                break;
            case en_CreatureState.MOVING:
            case en_CreatureState.RETURN_SPAWN_POSITION:                
                break;             
            case en_CreatureState.ATTACK:                
                break;
            case en_CreatureState.SPELL:                
                break;
            case en_CreatureState.GATHERING:
                break;
            case en_CreatureState.DEAD:                
                break;            
        }       
    }  
}
