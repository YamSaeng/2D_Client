using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventoryItem : UI_Base
{
    // 아이템 정보
    public st_ItemInfo _ItemInfo;

    UI_GameScene _GameSceneUI;

    UI_Inventory _ParentInventory;
    // 아이템의 회전 여부
    public bool Rotated = false;

    // 높이 반환
    // 회전 중이라면 너비로 반환
    public int HEIGHT
    {
        get
        {
            if(Rotated == false)
            {
                return _ItemInfo.ItemHeight;
            }

            return _ItemInfo.ItemWidth;
        }
    }

    // 너비 반환
    // 회전 중이라면 높이로 반환
    public int WIDTH
    {
        get
        {
            if(Rotated == false)
            {
                return _ItemInfo.ItemWidth;
            }

            return _ItemInfo.ItemHeight;
        }
    }

    enum en_InventoryItemImage
    {
        InventoryItemImage
    }

    enum en_InventoryItemText
    {
        InventoryItemCountText
    }

    public override void Init()
    {
        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;        

        Bind<Image>(typeof(en_InventoryItemImage));
        Bind<TextMeshProUGUI>(typeof(en_InventoryItemText));

        BindEvent(GetImage((int)en_InventoryItemImage.InventoryItemImage).gameObject, OnInventoryItemClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetImage((int)en_InventoryItemImage.InventoryItemImage).gameObject, OnInventoryItemPointerEnter, Define.en_UIEvent.PointerEnter);
        BindEvent(GetImage((int)en_InventoryItemImage.InventoryItemImage).gameObject, OnInventoryItemPointerExit, Define.en_UIEvent.PointerExit);

        //BindEvent(GetImage((int)en_GridInventoryItemImage.GridInventoryItemImage).gameObject, OnGridInventoryItemDragBegin, Define.en_UIEvent.BeginDrag);
        //BindEvent(GetImage((int)en_GridInventoryItemImage.GridInventoryItemImage).gameObject, OnGridInventoryItemDrag, Define.en_UIEvent.Drag);
        //BindEvent(GetImage((int)en_GridInventoryItemImage.GridInventoryItemImage).gameObject, OnGridInventoryItemDragEnd, Define.en_UIEvent.EndDrag);                   
    }

    public override void Binding()
    {
        
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        
    }

    public void SetParentGridInventory(UI_Inventory ParentInventory)
    {
        _ParentInventory = ParentInventory;
    }

    private void OnInventoryItemClick(PointerEventData InventoryItemClickEvent)
    {           
        if(InventoryItemClickEvent.button == PointerEventData.InputButton.Left)
        {            
            if (InventoryItemClickEvent.clickCount == 1)
            {                
                if (Managers.MyInventory.SelectedInventory != null)
                {
                    Vector2Int NewTilePosition = Managers.MyInventory.GetTileGridPosition();

                    if (Managers.MyInventory.SelectedItem != null)
                    {
                        // 아이템을 선택한 상태에서 마우스 왼쪽 클릭 했을때
                        if (!Managers.MyInventory.SelectedInventory.IsCollision(Managers.MyInventory.SelectedItem))
                        {
                            // 가방와 부딪히지 않을 경우
                            switch (_ItemInfo.ItemSmallCategory)
                            {
                                case en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_SEED_POTATO:
                                case en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_SEED_CORN:
                                    CMessage ReqSeedFarmingPacket = Packet.MakePacket.ReqMakeSeedFarmingPacket(Managers.NetworkManager._AccountId,
                                        Managers.NetworkManager._PlayerDBId,
                                        _ItemInfo.ItemSmallCategory);
                                    Managers.NetworkManager.GameServerSend(ReqSeedFarmingPacket);
                                    break;
                                default:
                                    UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                                    GameSceneUI.AddGameSceneUIStack(GameSceneUI._InventoryItemDivideUI);
                                    GameSceneUI.SetItemDivideUI(Managers.MyInventory.SelectedItem._ItemInfo);

                                    CMessage ReqPlaceItemPacket = Packet.MakePacket.ReqMakePlaceItemPacket(Managers.NetworkManager._AccountId,
                                    Managers.NetworkManager._PlayerDBId, (short)NewTilePosition.x, (short)NewTilePosition.y);
                                    Managers.NetworkManager.GameServerSend(ReqPlaceItemPacket);
                                    break;
                            }                            
                        }
                        else
                        {
                            // 가방과 부딪힐 경우 ( = 가방에 아이템을 놓은 경우 )
                            CMessage ReqPlaceItemPacket = Packet.MakePacket.ReqMakePlaceItemPacket(Managers.NetworkManager._AccountId,
                            Managers.NetworkManager._PlayerDBId, (short)NewTilePosition.x, (short)NewTilePosition.y);
                            Managers.NetworkManager.GameServerSend(ReqPlaceItemPacket);
                        }                       
                    }
                    else
                    {
                        CMessage ReqSelectItemPacket = Packet.MakePacket.ReqMakeSelectItemPacket(Managers.NetworkManager._AccountId,
                            Managers.NetworkManager._PlayerDBId, (short)NewTilePosition.x, (short)NewTilePosition.y);
                        Managers.NetworkManager.GameServerSend(ReqSelectItemPacket);
                    }
                }
            }
        }
        else if(InventoryItemClickEvent.button == PointerEventData.InputButton.Right)
        {
            CMessage ReqMakeItemUsePacket = Packet.MakePacket.ReqMakeItemUsePacket(
                    Managers.NetworkManager._AccountId,
                    Managers.NetworkManager._PlayerDBId,
                    _ItemInfo.ItemSmallCategory,
                    _ItemInfo.ItemTileGridPositionX,
                    _ItemInfo.ItemTileGridPositionY);
            Managers.NetworkManager.GameServerSend(ReqMakeItemUsePacket);

            if (_GameSceneUI._FurnaceUI.gameObject.active == true)
            {
                if(_GameSceneUI._FurnaceUI._FurnaceController._SelectCompleteItemCategory != en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE)
                {
                    CMessage ReqFurnaceInputItemPacket = Packet.MakePacket.ReqMakeCraftingTableItemAddPacket(
                        Managers.NetworkManager._AccountId,
                        Managers.NetworkManager._PlayerDBId,
                        _GameSceneUI._FurnaceUI._FurnaceController._GameObjectInfo.ObjectId,                    
                        _ItemInfo.ItemSmallCategory, 1);
                    Managers.NetworkManager.GameServerSend(ReqFurnaceInputItemPacket);
                }                                
            }

            if (_GameSceneUI._SawmillUI.gameObject.active == true)
            {
                if (_GameSceneUI._SawmillUI._SawmillController._SelectCompleteItemCategory != en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE)
                {
                    CMessage ReqSawmillInputItemPacket = Packet.MakePacket.ReqMakeCraftingTableItemAddPacket(
                        Managers.NetworkManager._AccountId,
                        Managers.NetworkManager._PlayerDBId,
                        _GameSceneUI._SawmillUI._SawmillController._GameObjectInfo.ObjectId,
                        _ItemInfo.ItemSmallCategory, 1);
                    Managers.NetworkManager.GameServerSend(ReqSawmillInputItemPacket);
                }
            }            
        }
        
        _GameSceneUI.EmptyItemExplanation();
    }

    private void OnInventoryItemPointerEnter(PointerEventData Event)
    {
        if(Managers.MyInventory.SelectedItem == null && _ItemInfo != null)
        {
            _GameSceneUI.SetItemExplanation(_ItemInfo);
        }        
    }

    private void OnInventoryItemPointerExit(PointerEventData Event)
    {        
        _GameSceneUI.EmptyItemExplanation();
    }

    private void OnInventoryItemDragBegin(PointerEventData DragBeginEvent)
    {
        CMessage ReqSelectItemPacket = Packet.MakePacket.ReqMakeSelectItemPacket(Managers.NetworkManager._AccountId,
            Managers.NetworkManager._PlayerDBId,
            _ItemInfo.ItemTileGridPositionX,
            _ItemInfo.ItemTileGridPositionY);
        Managers.NetworkManager.GameServerSend(ReqSelectItemPacket);

        _GameSceneUI._DragItemUI.ShowCloseUI(true);
        _GameSceneUI._DragItemUI._DragItemInfo = _ItemInfo;
        _GameSceneUI._DragItemUI.SetSkillItemDragRaycast(false);
        _GameSceneUI._DragItemUI.DragItemSet(Managers.Sprite._ItemSprite[_ItemInfo.ItemSmallCategory]);
        _GameSceneUI._DragItemUI.GetComponent<RectTransform>().transform.position = Input.mousePosition;
    }
    private void OnInventoryItemDrag(PointerEventData DragEvent)
    {
        _GameSceneUI.EmptyItemExplanation();

        _GameSceneUI._DragItemUI.GetComponent<RectTransform>().anchoredPosition += DragEvent.delta;
    }
    private void OnInventoryItemDragEnd(PointerEventData DragEndEvent)
    {
        Vector2Int NewTilePosition = Managers.MyInventory.GetTileGridPosition();

        // 아이템을 퀵바에 올려 놓지 않는다면
        if(!_GameSceneUI._QuickSlotBarBoxUI.IsCollision(_GameSceneUI._DragItemUI))
        {
            if(!_ParentInventory.PositionCheck(NewTilePosition.x,NewTilePosition.y))
            {
                _GameSceneUI.AddGameSceneUIStack(_GameSceneUI._InventoryItemDivideUI);
                _GameSceneUI.SetItemDivideUI(_ItemInfo);
            }

            CMessage ReqPlaceItemPacket = Packet.MakePacket.ReqMakePlaceItemPacket(Managers.NetworkManager._AccountId,
            Managers.NetworkManager._PlayerDBId, (short)NewTilePosition.x, (short)NewTilePosition.y);
            Managers.NetworkManager.GameServerSend(ReqPlaceItemPacket);
        }               

        _GameSceneUI._DragItemUI.ShowCloseUI(false);
        _GameSceneUI._DragItemUI.SetSkillItemDragRaycast(false);

        _GameSceneUI._DragItemUI._DragItemInfo = null;
    }    

    // 아이템 정보 셋팅
    public void Set(st_ItemInfo ItemInfo)
    {
        // 아이템 정보 저장
        _ItemInfo = ItemInfo;

        RefreshInventoryItemUI();
    }
    
    // 아이템 회전
    public void Rotate()
    {
        Rotated = !Rotated;

        // z 축 기준으로 90도 돌림
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, Rotated == true ? 90.0f : 0f);
    }

    public void RefreshInventoryItemUI()
    {
        // 아이템 정보에 있는 Sprite 읽어서 저장
        GetImage((int)en_InventoryItemImage.InventoryItemImage).sprite = Managers.Sprite._ItemSprite[_ItemInfo.ItemSmallCategory];

        // 아이템의 넓이를 구해서 저장
        Vector2 ImageSize = new Vector2();
        ImageSize.x = _ItemInfo.ItemWidth * UI_Inventory.TileSizeWidth;
        ImageSize.y = _ItemInfo.ItemHeight * UI_Inventory.TileSizeHeight;

        // RectTransform도 넓이만큼 재 조정
        GetImage((int)en_InventoryItemImage.InventoryItemImage).GetComponent<RectTransform>().sizeDelta = ImageSize;

        switch(_ItemInfo.ItemSmallCategory)
        {
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_SWORD_WOOD:
            case en_SmallItemCategory.ITEM_SAMLL_CATEGORY_WEAPON_WOOD_SHIELD:            
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_WEAR_LEATHER:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_HAT_LEATHER:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_BOOT_LEATHER:
                GetTextMeshPro((int)en_InventoryItemText.InventoryItemCountText).text = "";
                break;
            default:
                GetTextMeshPro((int)en_InventoryItemText.InventoryItemCountText).text = this._ItemInfo.ItemCount.ToString();
                break;
        }        
    }

    public void ItemCountUpdate(short ItemCount)
    {
        _ItemInfo.ItemCount = ItemCount;

        GetTextMeshPro((int)en_InventoryItemText.InventoryItemCountText).text = _ItemInfo.ItemCount.ToString();
    }
}
