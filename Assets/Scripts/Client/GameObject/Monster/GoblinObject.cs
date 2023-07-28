using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoblinObject : CreatureObject
{
    public override void Init()
    {
        base.Init();

        if (_HPBarUI == null)
        {
            AddHPBar(0, 0.95f);
        }        

        if (_NameUI == null)
        {
            AddNameBar(0, 0.35f);
        }        
    }    

    protected override void UpdateController()
    {
        Transform RendererTransform = transform.Find("GameObjectRenderer");

        GameObject RightWeaponGO = _EquipmentBox?.GetEquipmentItem(en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND);        

        Vector2 LookAtPosition = gameObject.transform.position + (Vector3)_GameObjectInfo.ObjectPositionInfo.LookAtDireciton;

        //Debug.Log(_GameObjectInfo.ObjectName + " : " + _GameObjectInfo.ObjectPositionInfo.State);

        switch (_GameObjectInfo.ObjectPositionInfo.State)
        {
            case en_CreatureState.IDLE:
                break;
            case en_CreatureState.PATROL:                

                if(_EquipmentBox != null)
                {                    
                    if(RightWeaponGO != null)
                    {
                        PlayerWeapon RightWeapon = RightWeaponGO.GetComponent<PlayerWeapon>();
                        if(RightWeapon != null)
                        {
                            RightWeapon.AimWeapon(LookAtPosition);
                        }
                    }
                }
               
                break;
            case en_CreatureState.MOVING:
                if(_TargetObject != null)
                {
                    if (RendererTransform != null)
                    {
                        GameObjectRenderer Renderer = RendererTransform.GetComponent<GameObjectRenderer>();
                        if(Renderer != null)
                        {
                            Renderer.FaceDirection(new Vector2(_TargetObject.gameObject.transform.position.x,
                                _TargetObject.gameObject.transform.position.y));
                        }
                    }

                    if (RightWeaponGO != null)
                    {
                        PlayerWeapon RightWeapon = RightWeaponGO.GetComponent<PlayerWeapon>();
                        if (RightWeapon != null)
                        {
                            RightWeapon.AimWeapon(new Vector2(_TargetObject.gameObject.transform.position.x,
                                    _TargetObject.gameObject.transform.position.y));
                        }
                    }                   
                }
                break;
            case en_CreatureState.ATTACK:
                if(_TargetObject != null)
                {
                    if (RightWeaponGO != null)
                    {
                        PlayerWeapon RightWeapon = RightWeaponGO.GetComponent<PlayerWeapon>();
                        if (RightWeapon != null)
                        {
                            RightWeapon.AimWeapon(new Vector2(_TargetObject.gameObject.transform.position.x,
                                _TargetObject.gameObject.transform.position.y));
                        }
                    }                   
                    
                    if (RendererTransform != null)
                    {
                        GameObjectRenderer Renderer = RendererTransform.GetComponent<GameObjectRenderer>();
                        if (Renderer != null)
                        {
                            Renderer.FaceDirection(new Vector2(_TargetObject.gameObject.transform.position.x,
                                _TargetObject.gameObject.transform.position.y));
                        }
                    }
                }                
                break;                
        }
    }    
}
