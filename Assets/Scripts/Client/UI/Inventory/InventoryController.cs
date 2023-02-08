using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : UI_Base
{    
    // 선택한 아이템
    public UI_InventoryItem SelectedItem;
    // 아이템 넣을때 이미 존재하는 아이템
    UI_InventoryItem OverlapItem;

    // 마우스로 선택한 아이템의 RectTransform
    RectTransform SelectedItemRectTransform;    

    // 현재 선택한 아이템의 표시
    UI_HighlightInventorySlot HighlightInventorySlot;

    // 선택한 Grid 인벤토리
    private UI_Inventory _SelectedInventory;    

    long _GoldCoinCount;
    short _SliverCoinCount;
    short _BronzeCoinCount;

    public UI_Inventory SelectedInventory
    {
        get => _SelectedInventory;
        set
        {
            _SelectedInventory = value;
                       
        }
    }    

    public override void Init()
    { 
        //gameObject.AddComponent<UI_HighlightInventorySlot>();
        //HighlightInventorySlot = GetComponent<UI_HighlightInventorySlot>();        
    }

    public override void Binding()
    {
        
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        
    }   

    UI_InventoryItem ItemToHighlight;
    // 하이라이트 표시
    private void HandleHighlight()
    {
        Vector2Int PositionOnGrid = GetTileGridPosition();

        // 선택한 아이템이 없을 경우
        if(SelectedItem == null)
        {
            // 마우스 위치에 있는 아이템을 가져온다.
            ItemToHighlight = SelectedInventory.GetItem(PositionOnGrid.x, PositionOnGrid.y);

            // 선택한 아이템은 없고 마우스 오버랩 한 곳에 아이템이 있을 경우
            if(ItemToHighlight != null && HighlightInventorySlot != null)
            {               
                // 아이템 표시 기능을 켜준다.
                HighlightInventorySlot.Show(true);
                // 크기를 마우스 오버된 아이ㅏ템의 크기에 맞게 설정한다.
                HighlightInventorySlot.SetSize(ItemToHighlight);
                // 선택한 인벤토리 아이템에 맞게 위치를 새로 잡는다.
                HighlightInventorySlot.SetPosition(SelectedInventory, ItemToHighlight);
            }
            else
            {
                // 마우스 위치에 아이템이 없을 경우 표시 기능을 꺼준다.
                HighlightInventorySlot.Show(false);
            }
        }
        else // 현재 선택하고 있는 아이템이 있을 경우
        {
            // 아이템 표시기능을 범위체크를 해준 후 켜준다.
            HighlightInventorySlot.Show(SelectedInventory.BoundryCheck(
                PositionOnGrid.x,
                PositionOnGrid.y,
                SelectedItem.WIDTH,
                SelectedItem.HEIGHT));
            // 선택한 아이템의 크기만큼 하이라이트를 표시해준다.
            HighlightInventorySlot.SetSize(SelectedItem);
            // 하이라이트 표시 위치를 새로 잡는다.
            HighlightInventorySlot.SetPosition(_SelectedInventory, SelectedItem, PositionOnGrid.x, PositionOnGrid.y);
        }
    }   
    
    // 아이템 선택 요청 응답
    public void ResSelectItem(Vector2Int TileGridPosition)
    {
        SelectedItem = SelectedInventory.GetItem(TileGridPosition.x, TileGridPosition.y);
        SelectedItemRectTransform = SelectedItem.GetComponent<RectTransform>();
        SelectedItemRectTransform.SetAsLastSibling();
    }    

    public void ResPlaceItem(st_ItemInfo PlaceItemInfo, st_ItemInfo SelectItemInfo)
    {
        UI_InventoryItem PreviouseSelectItem = SelectedItem;
        // 선택해야할 아이템의 위치
        Vector2Int SelectedItemPosition = new Vector2Int();
        SelectedItemPosition.x = PlaceItemInfo.ItemTileGridPositionX;
        SelectedItemPosition.y = PlaceItemInfo.ItemTileGridPositionY;

        if(SelectItemInfo.ItemSmallCategory != en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE)
        {
            SelectedItem = SelectedInventory.GetItem(SelectItemInfo.ItemTileGridPositionX, SelectItemInfo.ItemTileGridPositionY);
            SelectedItemRectTransform = SelectedItem.GetComponent<RectTransform>();
            SelectedItemRectTransform.SetAsLastSibling();

            SelectedInventory.CleanGridReference(SelectedItem);
        }
        else
        {
            SelectedItem = null;
        }
        
        SelectedInventory.PlaceItem(PreviouseSelectItem, PlaceItemInfo.ItemTileGridPositionX, PlaceItemInfo.ItemTileGridPositionY);               
    }
    
    public void ResRotateItem()
    {
        SelectedItem.Rotate();
    }

    // 아이템 회전 시키기
    private void RotateItem()
    {
        // 선택된 아이템이 없을 경우 나간다.
        if(SelectedItem == null)
        {
            return;
        }

        // 아이템 회전
        SelectedItem.Rotate();
    }
    
    // 아이템 가방에 넣기
    public void InsertItem(UI_InventoryItem NewItem)
    {
        // 현재 선택한 Grid 인벤토리의 빈공간 찾기
        Vector2Int? PositionOnGrid = _SelectedInventory.FindSpaceForObject(NewItem);
        // 빈공간이 발견되지 않으면 나감
        if(PositionOnGrid == null)
        {
            return;
        }

        // 아이템을 가방에 넣는다.
        _SelectedInventory.PlaceItem(NewItem, PositionOnGrid.Value.x, PositionOnGrid.Value.y);
    }

    public void ServerInsertItem(UI_InventoryItem NewItem)
    {        
        _SelectedInventory.PlaceItem(NewItem, NewItem._ItemInfo.ItemTileGridPositionX, NewItem._ItemInfo.ItemTileGridPositionY);
    }
    
    public void InitItem(UI_InventoryItem DeleteItem)
    {
        _SelectedInventory.InitItem(DeleteItem._ItemInfo.ItemTileGridPositionX, DeleteItem._ItemInfo.ItemTileGridPositionY);
    }

    // 클릭한 곳의 타일 위치값을 얻음
    public Vector2Int GetTileGridPosition()
    {
        Vector2 Position = Input.mousePosition;

        // 선택한 아이템이 있을 경우
        if(SelectedItem != null)
        {
            // 선택한 아이템 기준 중앙값 위치를 얻는다.
            Position.x -= (SelectedItem.WIDTH - 1) * UI_Inventory.TileSizeWidth / 2;
            Position.y += (SelectedItem.HEIGHT - 1) * UI_Inventory.TileSizeHeight / 2;
        }
       
        // 그리드 인벤토리에서 마우스 위치에 해당 하는 타일 위치를 가져온다.
        return _SelectedInventory.GetTileGridPosition(Position);
    }   

    public void FindItem(short FindItemTileX, short FindItemTileY, short ItemCount)
    {
        UI_InventoryItem Item = SelectedInventory.GetItem(FindItemTileX, FindItemTileY);
        Item.ItemCountUpdate(ItemCount);        
    }

    public UI_InventoryItem FindItem(en_SmallItemCategory ItemCategory)
    {
        return SelectedInventory.FindItem(ItemCategory);
    }

    public void MoneyItemUpdate(Int64 GoldCoinCount, Int16 SliverCoinCount, Int16 BronzeCoinCount)
    {
        _GoldCoinCount = GoldCoinCount;
        _SliverCoinCount = SliverCoinCount;
        _BronzeCoinCount = BronzeCoinCount;

        _SelectedInventory.MoneyUIUpdate(_GoldCoinCount, _SliverCoinCount, _BronzeCoinCount);
    }

    public long GetGoldCount()
    {
        return _GoldCoinCount;
    }

    public short GetSilverCount()
    {
        return _SliverCoinCount;
    }

    public short GetBronzeCoinCount()
    {
        return _BronzeCoinCount;
    } 

    private void Update()
    {
        if(SelectedItem != null)
        {
            SelectedItem.GetComponent<RectTransform>().transform.position = Input.mousePosition;

            if(Input.GetKeyDown(KeyCode.R))
            {
                if(SelectedItem._ItemInfo.ItemWidth != SelectedItem._ItemInfo.ItemHeight)
                {
                    CMessage ReqRotateItemPacket = Packet.MakePacket.ReqMakeRotateItemPacket(Managers.NetworkManager._AccountId,
                    Managers.NetworkManager._PlayerDBId,
                    SelectedItem._ItemInfo.ItemSmallCategory);
                    Managers.NetworkManager.GameServerSend(ReqRotateItemPacket);
                }                
            }            
        }             
    }
}