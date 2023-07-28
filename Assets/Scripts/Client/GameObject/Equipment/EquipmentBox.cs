using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBox
{
    private bool IsInit = false;

    private UI_GameScene _GameScene;

    public CreatureObject _OwnerObject;

    public Dictionary<en_EquipmentParts, st_ItemInfo> _EquipmentParts = new Dictionary<en_EquipmentParts, st_ItemInfo>();   
    
    public Dictionary<en_EquipmentParts, GameObject> _EquipemtPartsItem = new Dictionary<en_EquipmentParts, GameObject>();

    public void Init(CreatureObject OwnerObject)
    {
        _OwnerObject = OwnerObject;
        
        if (!IsInit)
        {
            IsInit = true;

            _GameScene = Managers.UI._SceneUI as UI_GameScene;

            _EquipmentParts.Add(en_EquipmentParts.EQUIPMENT_PARTS_HEAD, null);
            _EquipmentParts.Add(en_EquipmentParts.EQUIPMENT_PARTS_BODY, null);
            _EquipmentParts.Add(en_EquipmentParts.EQUIPMENT_PARTS_LEFT_HAND, null);
            _EquipmentParts.Add(en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND, null);
            _EquipmentParts.Add(en_EquipmentParts.EQUIPMENT_PARTS_BOOT, null);

            _EquipemtPartsItem.Add(en_EquipmentParts.EQUIPMENT_PARTS_HEAD, null);
            _EquipemtPartsItem.Add(en_EquipmentParts.EQUIPMENT_PARTS_BODY, null);
            _EquipemtPartsItem.Add(en_EquipmentParts.EQUIPMENT_PARTS_LEFT_HAND, Managers.Resource.Instantiate(en_ResourceName.CLIENT_WEAPON_PARENT, _OwnerObject.transform));
            _EquipemtPartsItem.Add(en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND, Managers.Resource.Instantiate(en_ResourceName.CLIENT_WEAPON_PARENT, _OwnerObject.transform));
            _EquipemtPartsItem.Add(en_EquipmentParts.EQUIPMENT_PARTS_BOOT, null);

            _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_LEFT_HAND].gameObject.name = "LeftHandWeapon";
            _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND].gameObject.name = "RightHandWeapon";

            PlayerWeapon RightWeapon = _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND].GetComponent<PlayerWeapon>();

            GameObjectInput OwnerObjectInput = _OwnerObject.GetComponent<GameObjectInput>();
            if (OwnerObjectInput != null)
            {
                OwnerObjectInput.OnPointerPositionChange.AddListener(RightWeapon.AimWeapon);
                OwnerObjectInput.OnS2CPointerPositionChange.AddListener(RightWeapon.S2C_AimWeapon);
                OwnerObjectInput.OnDefaultAttackPressed.AddListener(RightWeapon.Attack);
                OwnerObjectInput.OnDefaultAttackReleased.AddListener(RightWeapon.StopAttack);
            }
        }        
    }

    // 장비 착용
    public void OnEquipmentItem(st_ItemInfo EquipmentItemInfo)
    {
        _EquipmentParts[EquipmentItemInfo.ItemEquipmentPart] = EquipmentItemInfo;

        if(_OwnerObject != null)
        {
            switch(EquipmentItemInfo.ItemEquipmentPart)
            {
                case en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND:
                    PlayerWeapon RightWeapon = _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND].GetComponent<PlayerWeapon>();
                    if (RightWeapon != null)
                    {
                        // 착용하고 있는 장비가 있을 경우 기존 장비는 파괴
                        if (_EquipemtPartsItem[EquipmentItemInfo.ItemEquipmentPart] != null)
                        {                            
                            if(RightWeapon._Weapon != null)
                            {
                                OffEquipmentItem(EquipmentItemInfo.ItemEquipmentPart);
                            }                            
                        }

                        GameObject NewRightWeaponGO = null;

                        switch (EquipmentItemInfo.ItemSmallCategory)
                        {
                            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_DAGGER_WOOD:
                                NewRightWeaponGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_WEAPON_DAGGER_WOOD, RightWeapon.transform);
                                break;
                            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_LONG_SWORD_WOOD:
                                NewRightWeaponGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_WEAPON_LONG_SWORD_WOOD, RightWeapon.transform);
                                break;
                        }

                        if(NewRightWeaponGO != null)
                        {
                            Weapon NewRightWeapon = NewRightWeaponGO.GetComponent<Weapon>();
                            if(NewRightWeapon != null)
                            {
                                RightWeapon.OnWeapon(NewRightWeapon);
                            }
                        }                        
                    }
                    break;
            }           
        }

        // 장비창 업데이트
        if(_OwnerObject._GameObjectInfo.ObjectId == Managers.NetworkManager._PlayerDBId)
        {
            if (_GameScene != null)
            {
                _GameScene._EquipmentBoxUI.OnEquipmentItem(EquipmentItemInfo, _OwnerObject._GameObjectInfo.ObjectId);
            }
        }        
    }

    // 장비 벗기
    public void OffEquipmentItem(en_EquipmentParts OffEquipmentPart)
    {        
        if(_EquipemtPartsItem[OffEquipmentPart] != null)
        {
            switch (OffEquipmentPart)
            {
                case en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND:
                    GameObjectWeapon RightWeapon = _EquipemtPartsItem[OffEquipmentPart].GetComponent<GameObjectWeapon>();
                    if(RightWeapon != null)
                    {
                        RightWeapon.ChildWeaponDestory();
                    }
                    break;
            }

            _EquipmentParts[OffEquipmentPart] = null;
        }                

        // 장비창 업데이트
        if (_OwnerObject._GameObjectInfo.ObjectId == Managers.NetworkManager._PlayerDBId)
        {
            if(_GameScene != null)
            {
                _GameScene._EquipmentBoxUI.OffEquipmentItem(OffEquipmentPart);
            }
        }
    }

    public void WeaponItemInit()
    {
        GameObject RightWeaponGO = _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND];
        if(RightWeaponGO != null)
        {
            PlayerWeapon RightWeapon = RightWeaponGO.GetComponent<PlayerWeapon>();
            if(RightWeapon != null)
            {
                RightWeapon.Init();
            }
        }        

        GameObject LeftWeaponGO = _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_LEFT_HAND];
        if(LeftWeaponGO != null)
        {
            PlayerWeapon LeftWeapon = LeftWeaponGO.GetComponent<PlayerWeapon>();
            if(LeftWeapon != null)
            {
                LeftWeapon.Init();
            }
        }
    }

    public void WeaponItemShowClose(bool IsShowClose)
    {
        GameObject RightWeaponGO = _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND];
        if (RightWeaponGO != null)
        {
            PlayerWeapon RightWeapon = RightWeaponGO.GetComponent<PlayerWeapon>();
            if (RightWeapon != null)
            {
                RightWeapon.gameObject.SetActive(IsShowClose);
            }
        }

        GameObject LeftWeaponGO = _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_LEFT_HAND];
        if (LeftWeaponGO != null)
        {
            PlayerWeapon LeftWeapon = LeftWeaponGO.GetComponent<PlayerWeapon>();
            if (LeftWeapon != null)
            {
                LeftWeapon.gameObject.SetActive(IsShowClose);
            }
        }
    }

    public GameObject GetEquipmentItem(en_EquipmentParts EquipmentPart)
    {
        return _EquipemtPartsItem[EquipmentPart].gameObject;
    }

    public void WeaponDestroy()
    {
        GameObject RightWeaponGO = _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND];
        if(RightWeaponGO != null)
        {
            GameObject RightWeapon = null;

            switch (_EquipmentParts[en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND].ItemSmallCategory)
            {
                case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_DAGGER_WOOD:
                    RightWeapon = RightWeaponGO.transform.Find("WeaponDaggerWood")?.gameObject;                    
                    break;
                case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_LONG_SWORD_WOOD:
                    RightWeapon = RightWeaponGO.transform.Find("WeaponLongSwordWood")?.gameObject;
                    break;
            }

            if(RightWeapon != null)
            {
                Object.Destroy(RightWeapon.gameObject);
            }
        }

        GameObject LeftWeaponGO = _EquipemtPartsItem[en_EquipmentParts.EQUIPMENT_PARTS_LEFT_HAND];
        if (LeftWeaponGO != null)
        {
            
        }
    }
}
