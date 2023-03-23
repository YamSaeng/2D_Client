using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoblinObject : CreatureObject
{
    public override void Init()
    {
        base.Init();

        if (_HpBar == null)
        {
            AddHPBar(0.5f, 0.35f);
        }

        if(_NameUI == null)
        {
            AddNameBar(0.5f, -0.2f);
        }

        _NameUI.gameObject.SetActive(true);
    }

    protected override void UpdateController()
    {
        Transform RendererTransform = transform.Find("GameObjectRenderer");
        Transform RightWeaponParentTransform = transform.Find("RightWeaponParent");

        switch (_GameObjectInfo.ObjectPositionInfo.State)
        {
            case en_CreatureState.IDLE:
                break;
            case en_CreatureState.PATROL:                
                Vector2 LookAtPosition = gameObject.transform.position + (Vector3)_GameObjectInfo.ObjectPositionInfo.LookAtDireciton;

                if (RightWeaponParentTransform != null)
                {
                    GameObject RightWeaponParent = RightWeaponParentTransform.gameObject;
                    if (RightWeaponParent != null)
                    {
                        PlayerWeapon Weapon = RightWeaponParent.GetComponent<PlayerWeapon>();
                        if (Weapon != null)
                        {
                            Weapon.AimWeapon(LookAtPosition);
                        }
                    }
                }
                break;
            case en_CreatureState.MOVING:
                if(_TargetObject != null)
                {                    
                    if(RendererTransform != null)
                    {
                        GameObjectRenderer Renderer = RendererTransform.GetComponent<GameObjectRenderer>();
                        if(Renderer != null)
                        {
                            Renderer.FaceDirection(new Vector2(_TargetObject.gameObject.transform.position.x,
                                _TargetObject.gameObject.transform.position.y));
                        }
                    }
                    
                    if (RightWeaponParentTransform != null)
                    {
                        GameObject RightWeaponParent = RightWeaponParentTransform.gameObject;
                        if (RightWeaponParent != null)
                        {
                            PlayerWeapon Weapon = RightWeaponParent.GetComponent<PlayerWeapon>();
                            if (Weapon != null)
                            {
                                Weapon.AimWeapon(new Vector2(_TargetObject.gameObject.transform.position.x, _TargetObject.gameObject.transform.position.y));
                            }
                        }
                    }
                }
                break;
            case en_CreatureState.ATTACK:
                if(_TargetObject != null)
                {                    
                    if (RightWeaponParentTransform != null)
                    {
                        GameObject RightWeaponParent = RightWeaponParentTransform.gameObject;
                        if (RightWeaponParent != null)
                        {
                            PlayerWeapon Weapon = RightWeaponParent.GetComponent<PlayerWeapon>();
                            if (Weapon != null)
                            {
                                Weapon.AimWeapon(new Vector2(_TargetObject.gameObject.transform.position.x, _TargetObject.gameObject.transform.position.y));
                            }
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
