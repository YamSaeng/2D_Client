using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class CBaseObject : MonoBehaviour
{
    [HideInInspector]
    public CBaseObject _TargetObject;

    [HideInInspector]
    public RectCollision _RectCollision;
    
    [HideInInspector]
    public UI_Name _NameUI;

    protected UI_GameScene _GameSceneUI;
    // 상태이상 정보
    protected long _StatusAbnormal;

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
        
    public void SetStatusAbnormal(long StatusAbnormal)
    {
        // 상태이상 적용
        _StatusAbnormal |= StatusAbnormal;       
    }

    public void ReleaseStatusAbnormal(long StatusAbnormal)
    {
        // 상태이상 해제
        _StatusAbnormal &= StatusAbnormal;
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

        Transform CollisionTr = transform.Find("Collision");
        if(CollisionTr != null)
        {
            _RectCollision = CollisionTr.GetComponent<RectCollision>();
            if (_RectCollision != null)
            {
                _RectCollision.SetUpOwnPlayer(this);
            }
        }        

        transform.position = new Vector3(_GameObjectInfo.ObjectPositionInfo.Position.x, _GameObjectInfo.ObjectPositionInfo.Position.y, 0);
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
    }

    public void CreatureSpriteShowClose(bool IsShowClose)
    {
        GameObjectRenderer CreatureRenderer = transform.Find("GameObjectRenderer")?.GetComponent<GameObjectRenderer>();
        if (CreatureRenderer != null)
        {
            SpriteRenderer CreatureSprite = CreatureRenderer.GetComponent<SpriteRenderer>();
            if (CreatureSprite != null)
            {
                CreatureSprite.enabled = IsShowClose;
            }
        }
    }

    protected void AddNameBar(float NameBarPositionX, float NameBarPositionY)
    {
        GameObject NameUIGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_NAME, transform);
        NameUIGo.transform.localPosition = new Vector3(NameBarPositionX, NameBarPositionY, 0);
        NameUIGo.name = "UI_NameBar";

        _NameUI = NameUIGo.GetComponent<UI_Name>();
        _NameUI.Init(_GameObjectInfo.ObjectName);
    }

    // DissolveFeedback 완료시 반납
    public void ObjectReturn()
    {
        Managers.Object.Remove(_GameObjectInfo.ObjectId);
    }
}
