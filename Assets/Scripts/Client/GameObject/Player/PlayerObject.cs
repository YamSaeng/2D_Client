using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class PlayerObject : CreatureObject
{    
    Dictionary<long, CreatureObject> _AroundObjects = new Dictionary<long, CreatureObject>();
    List<st_AroundObject> _SelectedObjects = new List<st_AroundObject>();

    public struct st_AroundObject : IComparable<st_AroundObject>
    {
        public long ObjectID;        
        public float Distance;      

        public int CompareTo(st_AroundObject Other)
        {
            return Distance < Other.Distance ? 1 : 0;
        }      
    }
        
    PriorityQueue<st_AroundObject> _DistanceAroundObjects = new PriorityQueue<st_AroundObject>();

    int _SelectdObjectCount = 0;

    public override void Init()
    {
        base.Init();        

        _IsChattingFocus = false;

        if (_HPBarUI == null)
        {
            AddHPBar(0, 0.95f);
        }

        if(_SkillCastingBarUI == null)
        {
            AddSkillCastingBar(0, -0.7f);
        }

        if (_NameUI == null)
        {
            AddNameBar(0, 0.35f);
        }

        GetComponent<GameObjectMovement>().SetOwner(this);
        GetComponent<GameObjectInput>().SetOwner(this);
    }   

    protected override void UpdateController()
    {
        base.UpdateController();

        SearchAroundObject();
    }

    public void SearchAroundObject()
    {
        if (_GameObjectInfo.ObjectId == Managers.NetworkManager._PlayerDBId)
        {
            List<GameObject> AroundObjects = Managers.Object.GetObjects();
            if (AroundObjects.Count > 0)
            {
                _AroundObjects.Clear();

                foreach (GameObject AroundObject in AroundObjects)
                {
                    CreatureObject Object = AroundObject?.GetComponent<CreatureObject>();
                    if (Object != null)
                    {
                        if(Object._GameObjectInfo.ObjectId == _GameObjectInfo.ObjectId)
                        {
                            continue;
                        }    

                        float Distance = Vector2.Distance(AroundObject.transform.position, transform.position);
                        if (Distance < 6.0f)
                        {
                            _AroundObjects.Add(Object._GameObjectInfo.ObjectId, Object);
                        }
                    }   
                }
            }
        }
    }

    public void FindAroundObject()
    {
        if(_AroundObjects.Count == 0)
        {
            return;
        }

        _DistanceAroundObjects.Clear();
                
        foreach (CreatureObject AroundObject in _AroundObjects.Values.ToList())
        {
            bool IsSelectObject = false;

            float Distance = Vector2.Distance(AroundObject.transform.position, transform.position);

            st_AroundObject STAroundObject;
            STAroundObject.ObjectID = AroundObject._GameObjectInfo.ObjectId;            
            STAroundObject.Distance = Distance;

            // 선택했던 대상들에 있는지 확인
            foreach(st_AroundObject SelectObject in _SelectedObjects)
            {
                if(SelectObject.ObjectID == STAroundObject.ObjectID)
                {
                    IsSelectObject = true;
                }
            }

            if(!IsSelectObject)
            {
                _DistanceAroundObjects.Push(STAroundObject);
            }            
        }

        if(_DistanceAroundObjects.Count() > 0)
        {
            st_AroundObject AroundObjectData= _DistanceAroundObjects.Pop();         

            GameObject AroundObjectGO = Managers.Object.FindById(AroundObjectData.ObjectID);
            if(AroundObjectGO != null)
            {
                CreatureObject AroundCreatureObject = AroundObjectGO.GetComponent<CreatureObject>();
                if(AroundCreatureObject != null)
                {
                    _SelectdObjectCount++;

                    _SelectedObjects.Add(AroundObjectData);

                    if(_AroundObjects.Count() == _SelectdObjectCount)
                    {
                        _SelectedObjects.Clear();

                        _SelectdObjectCount = 0;
                    }

                    CMessage ReqLeftMousePositionObjectInfoPacket = Packet.MakePacket.ReqMakeLeftMouseWorldObjectInfoPacket(
                                   AroundCreatureObject._GameObjectInfo.ObjectId,
                                   AroundCreatureObject._GameObjectInfo.ObjectType);
                    Managers.NetworkManager.GameServerSend(ReqLeftMousePositionObjectInfoPacket);
                }
            }
        }                
    }
}