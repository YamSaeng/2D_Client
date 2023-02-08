using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class PlayerObject : CreatureObject
{
    Camera MainCamera;
        
    private UI_GameScene _GameSceneUI;
    
    private bool _IsChattingFocus;
        
    [field: SerializeField]
    public UnityEvent OnHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    public override void Init()
    {
        base.Init();

        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        _IsChattingFocus = false;      
    }

    protected virtual void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UI_GameScene gameSceneUI = Managers.UI._SceneUI as UI_GameScene;

            // IsPointerOverGameObject == 마우스가 UI에 있으면 true 반환
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 MousePosition = Input.mousePosition;
                Vector3 ScreenToMousePosition = MainCamera.ScreenToWorldPoint(MousePosition);

                RaycastHit2D ObjectHit = Physics2D.Raycast(ScreenToMousePosition, Vector2.zero);

                if (ObjectHit)
                {
                    PlayerObject BC = ObjectHit.transform.GetComponent<PlayerObject>();
                    if (BC != null)
                    {
                        if (BC._GameObjectInfo.ObjectType != en_GameObjectType.OBJECT_TREE
                            && BC._GameObjectInfo.ObjectType != en_GameObjectType.OBJECT_STONE
                            && BC._GameObjectInfo.ObjectType != en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE
                            && BC._GameObjectInfo.ObjectType != en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL)
                        {
                            CMessage ReqLeftMousePositionObjectInfoPacket = Packet.MakePacket.ReqMakeLeftMouseWorldObjectInfoPacket(
                                Managers.NetworkManager._AccountId,
                                Managers.NetworkManager._PlayerDBId,
                                BC._GameObjectInfo.ObjectId,
                                BC._GameObjectInfo.ObjectType);
                            Managers.NetworkManager.GameServerSend(ReqLeftMousePositionObjectInfoPacket);
                        }                       
                    }                    
                }
                else
                {
                    gameSceneUI._PlayerOptionUI.ShowCloseUI(false);
                    gameSceneUI._PartyPlayerOptionUI.ShowCloseUI(false);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 MousePosition = Input.mousePosition;
                Vector3 ScreenToMousePosition = MainCamera.ScreenToWorldPoint(MousePosition);

                RaycastHit2D ObjectHit = Physics2D.Raycast(ScreenToMousePosition, Vector2.zero);

                if (ObjectHit)
                {
                    CreatureObject CC = ObjectHit.transform.GetComponent<CreatureObject>();
                    if (CC != null)
                    {
                        Vector3 FurnaceTargetVector = CC.gameObject.transform.position;
                        Vector3 MyVector = gameObject.transform.position;

                        UI_GameScene gameSceneUI = Managers.UI._SceneUI as UI_GameScene;

                        float Distance = Vector3.Distance(FurnaceTargetVector, MyVector);                        

                        switch (CC._GameObjectInfo.ObjectType)
                        {
                            case en_GameObjectType.OBJECT_PLAYER:                            
                                Vector2 localPos = Vector2.zero;
                                RectTransformUtility.ScreenPointToLocalPointInRectangle(gameSceneUI.GetComponent<RectTransform>(),
                                    Camera.main.WorldToScreenPoint(Input.mousePosition),
                                    Camera.main, out localPos);

                                gameSceneUI._PlayerOptionUI.UIPlayerOptionSetPlayerGameObjectInfo(CC._GameObjectInfo);
                                gameSceneUI._PlayerOptionUI.ShowCloseUI(true);
                                gameSceneUI._PlayerOptionUI.PlayerOptionSetPosition(localPos);
                                break;
                            case en_GameObjectType.OBJECT_STONE:
                            case en_GameObjectType.OBJECT_TREE:
                                EnvironmentController EnvironmentObject = CC.GetComponent<EnvironmentController>();
                                if (EnvironmentObject != null && (EnvironmentObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.READY_DEAD
                                        && EnvironmentObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.DEAD))
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
                                if (CropObject != null && (CropObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.READY_DEAD
                                        && CropObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.DEAD))
                                {
                                    CMessage ReqGathering = Packet.MakePacket.ReqMakeGatheringPacket(Managers.NetworkManager._AccountId,
                                    Managers.NetworkManager._PlayerDBId,
                                    CC._GameObjectInfo.ObjectId,
                                    CC._GameObjectInfo.ObjectType);
                                    Managers.NetworkManager.GameServerSend(ReqGathering);
                                }
                                break;
                            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                                if(Distance < 2.0f)
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
    }  

    protected virtual void GetUIKeyInput()
    {
        if (_IsChattingFocus == false)
        {
            _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;                      

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(_GameSceneUI.IsGameSceneUIStackEmpty() == true)
                { 
                    _GameSceneUI.AddGameSceneUIStack(_GameSceneUI._OptionUI);
                }
                else
                {
                    // GameSceneUIStack이 비어 있지 않을 경우
                    // 하나씩 뽑아서 UI를 닫아줌
                    UI_Base GameSceneUIStackUI = _GameSceneUI.FindGameSceneUIStack();
                    if(GameSceneUIStackUI != null)
                    {
                        UI_Furnace FurnaceUI = GameSceneUIStackUI as UI_Furnace;
                        if(FurnaceUI != null)
                        {
                            CMessage ReqCraftingTableNonSelectPacket = Packet.MakePacket.ReqMakeCraftingTableNonSelectPacket(Managers.NetworkManager._AccountId,
                                Managers.NetworkManager._PlayerDBId,
                                FurnaceUI._FurnaceController._GameObjectInfo.ObjectId,
                                FurnaceUI._FurnaceController._GameObjectInfo.ObjectType);
                            Managers.NetworkManager.GameServerSend(ReqCraftingTableNonSelectPacket);
                        }

                        _GameSceneUI.DeleteGameSceneUIStack(GameSceneUIStackUI);                        
                    }
                }               
            }
        }

        if (!_IsChattingFocus
            && !_GameSceneUI._InventoryItemDivideUI.gameObject.active
            && Input.GetKeyDown(KeyCode.Return))
        {
            State = en_CreatureState.STOP;

            CMessage ReqMoveStop = Packet.MakePacket.ReqMakeMoveStopPacket(
               gameObject.transform.position.x,
               gameObject.transform.position.y,
               _GameObjectInfo.ObjectPositionInfo.State);
            Managers.NetworkManager.GameServerSend(ReqMoveStop);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            UI_ChattingBoxGroup ChattingBoxGroupUI = GameSceneUI._ChattingBoxGroup;

            InputField TextBox = ChattingBoxGroupUI.GetChattingInputField();
            TextBox.text = "";
            _IsChattingFocus = true;
            TextBox.gameObject.SetActive(true);

            TextBox.ActivateInputField();
        }
        else if (_IsChattingFocus && Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log("채팅 박스 비활성화");
            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            UI_ChattingBoxGroup ChattingBoxGroupUI = GameSceneUI._ChattingBoxGroup;

            InputField TextBox = ChattingBoxGroupUI.GetChattingInputField();

            if (TextBox.text.Length > 0)
            {
                CMessage ReqChattingMessage = Packet.MakePacket.ReqMakeChattingPacket(Managers.NetworkManager._AccountId, Managers.NetworkManager._PlayerDBId, TextBox.text);
                Managers.NetworkManager.GameServerSend(ReqChattingMessage);
            }

            TextBox.text = "";
            _IsChattingFocus = false;
            TextBox.gameObject.SetActive(false);

            TextBox.DeactivateInputField();
        }
    }

    protected override void UpdateController()
    {
        base.UpdateController();
    }     

    public override void OnDamaged()
    {
        Debug.Log("공격받음");
    }
}