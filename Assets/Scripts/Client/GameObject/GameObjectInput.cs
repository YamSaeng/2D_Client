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
    private Camera _MainCamera;    
    private bool _IsDefaultAttack = false;

    [field: SerializeField]
    public CreatureObject _OwnerObject { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

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

    private void Awake()
    {
        _MainCamera = Camera.main;
    }

    void Update()
    {
        GetKeyboardInput();
        GetPointerInput();
        GetMouseButtonInput();
        GetMovementInput();        

        Managers.Key.QuickSlotBarKeyUpdate();
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

    private void LeftMouseButtonClick()
    {
        Vector3 MousePosition = Input.mousePosition;
        Vector3 ScreenToMousePosition = _MainCamera.ScreenToWorldPoint(MousePosition);

        // IsPointerOverGameObject == 마우스가 UI에 있으면 true 반환
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D Hit = Physics2D.Raycast(ScreenToMousePosition, Vector2.zero);

            if(Hit)
            {                
                CBaseObject HitObject = Hit.transform.GetComponent<CBaseObject>();
                if(HitObject != null)
                {
                    switch(HitObject._GameObjectInfo.ObjectType)
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
                                CMessage ReqGathering = Packet.MakePacket.ReqMakeGatheringPacket(Managers.NetworkManager._AccountId,
                                Managers.NetworkManager._PlayerDBId,
                                CC._GameObjectInfo.ObjectId,
                                CC._GameObjectInfo.ObjectType);
                                Managers.NetworkManager.GameServerSend(ReqGathering);
                            }
                            break;
                        case en_GameObjectType.OBJECT_CROP_POTATO:
                            CropController CropObject = CC.GetComponent<CropController>();
                            if (CropObject != null && CropObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.DEAD)
                            {
                                CMessage ReqGathering = Packet.MakePacket.ReqMakeGatheringPacket(Managers.NetworkManager._AccountId,
                                Managers.NetworkManager._PlayerDBId,
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
                                    CMessage ReqMousePositionObjectInfo = Packet.MakePacket.ReqMakeRightMouseObjectInfoPacket(Managers.NetworkManager._AccountId,
                                    Managers.NetworkManager._PlayerDBId,
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
                                    CMessage ReqMousePositionObjectInfo = Packet.MakePacket.ReqMakeRightMouseObjectInfoPacket(Managers.NetworkManager._AccountId,
                                    Managers.NetworkManager._PlayerDBId,
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

    private void GetKeyboardInput()
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if(GameSceneUI != null)
        {
            PlayerObject Player = GetComponent<PlayerObject>();
            if(Player != null)
            {
                if (Player._IsChattingFocus == false)
                {
                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        OnInventroyUIOpen?.Invoke();
                    }

                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        OnEquipmentUIOpen?.Invoke();
                    }

                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        OnSkillUIOpen?.Invoke();
                    }
                }

                if(Player._IsChattingFocus == false)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        if (GameSceneUI.IsGameSceneUIStackEmpty() == true)
                        {
                            GameSceneUI.AddGameSceneUIStack(GameSceneUI._OptionUI);
                        }
                        else
                        {
                            // GameSceneUIStack이 비어 있지 않을 경우
                            // 하나씩 뽑아서 UI를 닫아줌
                            UI_Base GameSceneUIStackUI = GameSceneUI.FindGameSceneUIStack();
                            if (GameSceneUIStackUI != null)
                            {
                                UI_Furnace FurnaceUI = GameSceneUIStackUI as UI_Furnace;
                                if (FurnaceUI != null)
                                {
                                    CMessage ReqCraftingTableNonSelectPacket = Packet.MakePacket.ReqMakeCraftingTableNonSelectPacket(Managers.NetworkManager._AccountId,
                                        Managers.NetworkManager._PlayerDBId,
                                        FurnaceUI._FurnaceController._GameObjectInfo.ObjectId,
                                        FurnaceUI._FurnaceController._GameObjectInfo.ObjectType);
                                    Managers.NetworkManager.GameServerSend(ReqCraftingTableNonSelectPacket);
                                }

                                GameSceneUI.DeleteGameSceneUIStack(GameSceneUIStackUI);
                            }
                        }
                    }
                }

                if (Player._IsChattingFocus == false
                    && Input.GetKeyDown(KeyCode.Return))
                {
                    if (Player != null)
                    {
                        Player._GameObjectInfo.ObjectPositionInfo.State = en_CreatureState.STOP;

                        CMessage ReqMoveStop = Packet.MakePacket.ReqMakeMoveStopPacket(
                            gameObject.transform.position.x,
                            gameObject.transform.position.y,
                            Player._GameObjectInfo.ObjectPositionInfo.State);
                        Managers.NetworkManager.GameServerSend(ReqMoveStop);

                        GameObjectMovement gameObjectMovement = GetComponent<GameObjectMovement>();
                        if (gameObjectMovement != null)
                        {
                            Player._IsChattingFocus = true;

                            gameObjectMovement.OnMovementStop?.Invoke();                           

                            InputField ChattingInputField = GameSceneUI._ChattingBoxGroup.GetChattingInputField();
                            if(ChattingInputField != null)
                            {
                                ChattingInputField.text = "";
                                ChattingInputField.gameObject.SetActive(true);
                                ChattingInputField.ActivateInputField();
                            }                            
                        }
                    }
                }
                else if(Player._IsChattingFocus == true
                    && Input.GetKeyDown(KeyCode.Return))
                {
                    UI_ChattingBoxGroup ChattingBoxGroupUI = GameSceneUI._ChattingBoxGroup;

                    InputField ChattingInputField = GameSceneUI._ChattingBoxGroup.GetChattingInputField();
                    if(ChattingInputField != null)
                    {
                        Player._IsChattingFocus = false;

                        if (ChattingInputField.text.Length > 0)
                        {
                            CMessage ReqChattingMessage = Packet.MakePacket.ReqMakeChattingPacket(ChattingInputField.text);
                            Managers.NetworkManager.GameServerSend(ReqChattingMessage);
                        }

                        ChattingInputField.text = "";
                        ChattingInputField.gameObject.SetActive(false);
                        ChattingInputField.DeactivateInputField();
                    }
                }
            }
        }        
    }   

    private void GetPointerInput()
    {
        if (Input.GetMouseButton(1) == true)
        {
            Vector3 ScreenMousePosition = _MainCamera.ScreenToWorldPoint(Input.mousePosition);
            OnPointerPositionChange?.Invoke(ScreenMousePosition);

            //Vector2 Direction = ((Vector2)ScreenMousePosition - (Vector2)transform.position).normalized;

            Transform FindTransform = transform.Find("RightWeaponParent");
            if (FindTransform != null)
            {
                GameObject RightWeaponParent = FindTransform.gameObject;
                if (RightWeaponParent != null)
                {
                    PlayerWeapon Weapon = RightWeaponParent.GetComponent<PlayerWeapon>();
                    if (Weapon != null)
                    {
                        if (_OwnerObject != null)
                        {
                            _OwnerObject._GameObjectInfo.ObjectPositionInfo.LookAtDireciton = Weapon.transform.right;

                            _OwnerObject._WeaponPosition.x = Weapon.transform.position.x;
                            _OwnerObject._WeaponPosition.y = Weapon.transform.position.y;                                                      
                        }
                    }
                }
            }            

            //CMessage ReqFaceDirectionPacket = Packet.MakePacket.ReqMakeFaceDirectionPacket(Direction.x, Direction.y);
            //Managers.NetworkManager.GameServerSend(ReqFaceDirectionPacket);
        }        
    }

    private void GetMovementInput()
    {
        Vector2 Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMovementKeyPressed?.Invoke(Direction);
    }
}
