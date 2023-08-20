using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameObjectInput : MonoBehaviour
{
    private bool _IsMouseDrag = false;
    private Vector3 _LeftMouseCurrentClickPoint;
    private Vector3 _LeftMouseCurrentDragPoint;
    private Camera _MainCamera;        

    [field: SerializeField]
    public CreatureObject _OwnerObject { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnS2CPointerPositionChange { get; set; }

    [field: SerializeField]
    public UnityEvent OnDefaultAttackPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnDefaultAttackReleased { get; set; }

    [field: SerializeField]
    public UnityEvent OnInventroyUIOpen { get; set; }

    [field: SerializeField]
    public UnityEvent OnEquipmentUIOpen { get; set; }

    [field: SerializeField]
    public UnityEvent OnSkillUIOpen { get; set; }

    Vector2 _MoveInputDirection;

    private void Awake()
    {
        _MainCamera = Camera.main;
    }

    void Update()
    {
        GetPointerInput();
        GetMouseButtonInput();
        GetMouseDragInput();

        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if(GameSceneUI != null)
        {
            if(!GameSceneUI._QuickSlotKeyUI.gameObject.activeSelf)
            {
                Managers.Key.MoveQuickSlotKeyUpdate();
                Managers.Key.QuickSlotBarActions();
                Managers.Key.UIActions();
            }           
        }         
    }

    public void SetOwner(CreatureObject OwnerObject)
    {
        _OwnerObject = OwnerObject;
    }

    private void GetMouseButtonInput()
    {
        // 왼쪽 클릭
        if(Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonClick();            
        }

        // 오른쪽 클릭
        if(Input.GetMouseButtonDown(1))
        {
            RightMouseButtonClick();
        }
    }

    private void GetMouseDragInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 DragMousePosition = _MainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (DragMousePosition != _LeftMouseCurrentDragPoint)
            {
                _IsMouseDrag = true;
            }

            _LeftMouseCurrentDragPoint = _MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_IsMouseDrag == true)
            {
                _IsMouseDrag = false;
                _LeftMouseCurrentDragPoint = _MainCamera.ScreenToWorldPoint(Input.mousePosition);

                CMessage ReqLeftMouseDragObjectsSelectPacket = Packet.MakePacket.ReqMakeLeftMouseDragObjectsSelectPacket(_LeftMouseCurrentClickPoint.x, _LeftMouseCurrentClickPoint.y, _LeftMouseCurrentDragPoint.x, _LeftMouseCurrentDragPoint.y);
                Managers.NetworkManager.GameServerSend(ReqLeftMouseDragObjectsSelectPacket);                
            }
        }
    }

    private void LeftMouseButtonClick()
    {        
        Vector3 ScreenToMousePosition = _MainCamera.ScreenToWorldPoint(Input.mousePosition);

        _LeftMouseCurrentClickPoint = ScreenToMousePosition;
        _LeftMouseCurrentDragPoint = ScreenToMousePosition;

        // IsPointerOverGameObject == 마우스가 UI에 있으면 true 반환
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D Hit = Physics2D.Raycast(ScreenToMousePosition, Vector2.zero);

            if(Hit)
            {                
                CBaseObject HitObject = Hit.transform.GetComponent<CBaseObject>();
                if(HitObject != null)
                {
                    if(HitObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.ROOTING
                        && HitObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.DEAD)
                    {
                        switch (HitObject._GameObjectInfo.ObjectType)
                        {
                            case en_GameObjectType.OBJECT_PLAYER:
                            case en_GameObjectType.OBJECT_GOBLIN:
                                CMessage ReqLeftMousePositionObjectInfoPacket = Packet.MakePacket.ReqMakeLeftMouseWorldObjectInfoPacket(
                                    HitObject._GameObjectInfo.ObjectId,
                                    HitObject._GameObjectInfo.ObjectType);
                                Managers.NetworkManager.GameServerSend(ReqLeftMousePositionObjectInfoPacket);
                                break;
                        }
                    }                    
                }                
            }
            else
            {
               // gameSceneUI._PlayerOptionUI.ShowCloseUI(false);
               // gameSceneUI._PartyPlayerOptionUI.ShowCloseUI(false);
            }
        }
    }

    private void RightMouseButtonClick()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 MousePosition = Input.mousePosition;
            Vector3 ScreenToMousePosition = _MainCamera.ScreenToWorldPoint(MousePosition);

            RaycastHit2D ObjectHit = Physics2D.Raycast(ScreenToMousePosition, Vector2.zero);

            if (ObjectHit)
            {
                CreatureObject CC = ObjectHit.transform.GetComponent<CreatureObject>();
                if (CC != null ) 
                {
                    Vector3 FurnaceTargetVector = CC.gameObject.transform.position;
                    Vector3 MyVector = gameObject.transform.position;

                    UI_GameScene gameSceneUI = Managers.UI._SceneUI as UI_GameScene;

                    float Distance = Vector3.Distance(FurnaceTargetVector, MyVector);

                    switch (CC._GameObjectInfo.ObjectType)
                    {
                        case en_GameObjectType.OBJECT_PLAYER:
                            if(CC._GameObjectInfo.ObjectId != Managers.NetworkManager._PlayerDBId)
                            {
                                Vector2 localPos = Vector2.zero;
                                RectTransformUtility.ScreenPointToLocalPointInRectangle(gameSceneUI.GetComponent<RectTransform>(),
                                    Camera.main.WorldToScreenPoint(Input.mousePosition),
                                    Camera.main, out localPos);

                                gameSceneUI._PlayerOptionUI.UIPlayerOptionSetPlayerGameObjectInfo(CC._GameObjectInfo);
                                gameSceneUI._PlayerOptionUI.ShowCloseUI(true);
                                gameSceneUI._PlayerOptionUI.PlayerOptionSetPosition(localPos);
                            }
                            
                            break;
                        case en_GameObjectType.OBJECT_NON_PLAYER_GENERAL_MERCHANT:
                            break;
                        case en_GameObjectType.OBJECT_STONE:
                        case en_GameObjectType.OBJECT_TREE:
                            EnvironmentController EnvironmentObject = CC.GetComponent<EnvironmentController>();
                            if (EnvironmentObject != null && EnvironmentObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.DEAD)
                            {
                                CMessage ReqGathering = Packet.MakePacket.ReqMakeGatheringPacket(
                                CC._GameObjectInfo.ObjectId,
                                CC._GameObjectInfo.ObjectType);
                                Managers.NetworkManager.GameServerSend(ReqGathering);
                            }
                            break;
                        case en_GameObjectType.OBJECT_CROP_POTATO:
                            CropController CropObject = CC.GetComponent<CropController>();
                            if (CropObject != null && CropObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.DEAD)
                            {
                                CMessage ReqGathering = Packet.MakePacket.ReqMakeGatheringPacket(
                                CC._GameObjectInfo.ObjectId,
                                CC._GameObjectInfo.ObjectType);
                                Managers.NetworkManager.GameServerSend(ReqGathering);
                            }
                            break;
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                            if (Distance < 2.0f)
                            {
                                FurnaceController FurnaceObject = CC.GetComponent<FurnaceController>();
                                if (FurnaceObject != null)
                                {
                                    CMessage ReqMousePositionObjectInfo = Packet.MakePacket.ReqMakeRightMouseObjectInfoPacket(
                                    FurnaceObject._GameObjectInfo.ObjectId,
                                    FurnaceObject._GameObjectInfo.ObjectType);
                                    Managers.NetworkManager.GameServerSend(ReqMousePositionObjectInfo);
                                }
                            }
                            break;
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                            if (Distance < 2.0f)
                            {
                                SawmillController SawmillObject = CC.GetComponent<SawmillController>();
                                if (SawmillObject != null)
                                {
                                    CMessage ReqMousePositionObjectInfo = Packet.MakePacket.ReqMakeRightMouseObjectInfoPacket(
                                    SawmillObject._GameObjectInfo.ObjectId,
                                    SawmillObject._GameObjectInfo.ObjectType);
                                    Managers.NetworkManager.GameServerSend(ReqMousePositionObjectInfo);
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
   
    private void GetPointerInput()
    {
        if (_OwnerObject._GameObjectInfo.ObjectId == Managers.NetworkManager._PlayerDBId 
            && Input.GetMouseButton(1) == true)
        {
            Vector3 ScreenMousePosition = _MainCamera.ScreenToWorldPoint(Input.mousePosition);
            OnPointerPositionChange?.Invoke(ScreenMousePosition);

            //Vector2 Direction = ((Vector2)ScreenMousePosition - (Vector2)transform.position).normalized;

            GameObject RightWeaponGO = _OwnerObject._EquipmentBox.GetEquipmentItem(en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND);
            if(RightWeaponGO != null)
            {
                PlayerWeapon Weapon = RightWeaponGO.GetComponent<PlayerWeapon>();
                if (Weapon != null)
                {
                    if (_OwnerObject != null)
                    {
                        float PreviouseAngle = Mathf.Rad2Deg * Mathf.Atan2(_OwnerObject._GameObjectInfo.ObjectPositionInfo.LookAtDireciton.y,
                            _OwnerObject._GameObjectInfo.ObjectPositionInfo.LookAtDireciton.x);

                        _OwnerObject._GameObjectInfo.ObjectPositionInfo.LookAtDireciton = Weapon.transform.right;

                        float CurrentAngle = Mathf.Rad2Deg * Mathf.Atan2(_OwnerObject._GameObjectInfo.ObjectPositionInfo.LookAtDireciton.y,
                            _OwnerObject._GameObjectInfo.ObjectPositionInfo.LookAtDireciton.x);

                        if (Mathf.Abs(Mathf.Abs(CurrentAngle) - Mathf.Abs(PreviouseAngle)) > 1.2f)
                        {
                            CMessage LookAtDirectionPacket = Packet.MakePacket.ReqMakeLookAtDirectionPacket(_OwnerObject._GameObjectInfo.ObjectPositionInfo.LookAtDireciton);
                            Managers.NetworkManager.GameServerSend(LookAtDirectionPacket);
                        }

                        _OwnerObject._WeaponPosition.x = Weapon.transform.position.x;
                        _OwnerObject._WeaponPosition.y = Weapon.transform.position.y;
                    }
                }
            }                   

            //CMessage ReqFaceDirectionPacket = Packet.MakePacket.ReqMakeFaceDirectionPacket(Direction.x, Direction.y);
            //Managers.NetworkManager.GameServerSend(ReqFaceDirectionPacket);
        }        
    }   
}
