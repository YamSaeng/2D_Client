using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemObject : CBaseObject
{
    public PlayerObject _Target;
    public en_ItemState _ItemState;    
    public float _MovePower = 10.0f;    

    public override void Init()
    {
        base.Init();

        _ItemState = en_ItemState.ITEM_IDLE;
                
        if(_NameUI == null)
        {
            _NameUI = transform.Find("ItemName").GetComponent<UI_Name>();            
        }
    }

    IEnumerator CoTargetFly(float Time)
    {
        yield return new WaitForSeconds(Time);

        _ItemState = en_ItemState.ITEM_READY_MOVE;
    }    

    public void TargetFlyStart(float TargetSpeed)
    {
        _MovePower = TargetSpeed * 2.0f;

        StartCoroutine("CoTargetFly", 1.0f);    
    }

    void Update()
    {
        switch (_ItemState)
        {
            case en_ItemState.ITEM_IDLE:
                break;
            case en_ItemState.ITEM_READY_MOVE:
                _ItemState = en_ItemState.ITEM_MOVE;
                break;
            case en_ItemState.ITEM_MOVE:
                if (_Target != null)
                {
                    GameObjectRenderer TargetRenderer = _Target.transform.Find("GameObjectRenderer").GetComponent<GameObjectRenderer>();
                    if(TargetRenderer != null)
                    {
                        GameObject Target = TargetRenderer.gameObject;

                        // 타겟까지의 방향값을 구함
                        Vector3 TargetDir = (Target.transform.position - transform.transform.position).normalized;

                        // 타겟까지의 거리를 구함
                        float Distance = Vector3.Distance(Target.transform.position, transform.position);

                        // 거리가 0.5보다 크다면
                        if (Distance > 0.5f)
                        {
                            // 타겟한테 다가감
                            //transform.position = transform.position + TargetDir * Distance * MOVE_POWER * Time.deltaTime;                
                            Vector3 newPosition = Vector3.MoveTowards(transform.position, Target.transform.position, _MovePower * Time.deltaTime);
                            transform.position = newPosition;
                        }
                        else
                        {
                            gameObject.SetActive(false);

                            Managers.Object.Remove(_GameObjectInfo.ObjectId);
                        }
                    }                    
                }
                break;
        }        
    }  
}
