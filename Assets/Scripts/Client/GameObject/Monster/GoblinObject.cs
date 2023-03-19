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
        switch(_GameObjectInfo.ObjectPositionInfo.State)
        {
            case en_CreatureState.IDLE:
                break;
            case en_CreatureState.MOVING:
                if(_TargetObject != null)
                {
                    Transform RendererTransform = transform.Find("GameObjectRenderer");
                    if(RendererTransform != null)
                    {
                        GameObjectRenderer Renderer = RendererTransform.GetComponent<GameObjectRenderer>();
                        if(Renderer != null)
                        {
                            Renderer.FaceDirection(new Vector2(_TargetObject.gameObject.transform.position.x,
                                _TargetObject.gameObject.transform.position.y));
                        }
                    }

                    Transform RightWeaponParentTransform = transform.Find("RightWeaponParent");
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
                    Transform RightWeaponParentTransform = transform.Find("RightWeaponParent");
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

                    Transform RendererTransform = transform.Find("GameObjectRenderer");
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
