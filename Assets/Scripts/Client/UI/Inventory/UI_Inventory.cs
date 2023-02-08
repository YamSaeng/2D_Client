using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inventory : UI_Base
{    
    InventoryController _InventoryController;

    public byte _InventoryIndex;
    // 가방을 구성하는 타일의 너비와 높이
    public const float TileSizeWidth = 64;
    public const float TileSizeHeight = 64;

    // 가방이 소유중인 슬롯
    // ( 타일의 개수 만큼 대응 되며 각 슬롯은 아이템의 정보를 보관한다. )    
    UI_InventoryItem[,] InventorySlots;

    // 가방의 위치 넓이 값을 보관중인 RectTransform
    public RectTransform _InventoryRectTransform;

    // 가방의 너비와 높이
    [SerializeField] int InventorySizeWidth;
    [SerializeField] int InventorySizeHeight;
        
    [SerializeField] GameObject InventoryEdge;    

    enum en_InventoryGameObject
    {
        Inventory        
    }

    enum en_InventoryMoneyText
    {
        GoldCoinCountText,
        SliverCoinCountText,
        BronzeCoinCountText
    }

    public override void Init()
    {
        _InventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;

        _InventoryRectTransform = gameObject.GetComponent<RectTransform>();
    }    

    public override void Binding()
    {
        // 골드 초기화 되는지 확인해야함
        Bind<GameObject>(typeof(en_InventoryGameObject));
        Bind<TextMeshProUGUI>(typeof(en_InventoryMoneyText));

        BindEvent(GetGameObject((int)en_InventoryGameObject.Inventory).gameObject, OnInventoryEnter, Define.en_UIEvent.PointerEnter);
        BindEvent(GetGameObject((int)en_InventoryGameObject.Inventory).gameObject, OnInventoryExit, Define.en_UIEvent.PointerExit);
        BindEvent(InventoryEdge.gameObject, OnInventoryEdgeDrag, Define.en_UIEvent.Drag);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        InventoryEdge.gameObject.SetActive(IsShowClose);        
        gameObject.SetActive(IsShowClose);

        InventoryEdge.gameObject.transform.parent.SetAsLastSibling();
    }

    public void InventoryCreate(int GridInventoryWidth, int GridInventoryHeight)
    {
        // 그리드 가방 슬롯 개수 설정
        InventorySlots = new UI_InventoryItem[GridInventoryWidth, GridInventoryHeight];
        // 그리드 가방 크기 설정
        _InventoryRectTransform.sizeDelta = new Vector2(GridInventoryWidth * TileSizeWidth, GridInventoryHeight * TileSizeHeight);        
    }

    private void OnInventoryEnter(PointerEventData Event)
    {        
        //inventoryController.SelectedGridInventory = this;
    }

    private void OnInventoryExit(PointerEventData Event)
    {        
        //inventoryController.SelectedGridInventory = null;
    }

    private void OnInventoryEdgeDrag(PointerEventData Event)
    {
        InventoryEdge.gameObject.GetComponent<RectTransform>().anchoredPosition += Event.delta;        
    }

    // 아이템을 가방에서 선택할 경우
    public UI_InventoryItem PickUpItem(int TilePositionX, int TilePositionY)
    {
        if(PositionCheck(TilePositionX,TilePositionY)==false)
        {
            return null;
        }        

        // 집은 위치에 있는 아이템을 가져온다.
        UI_InventoryItem ToReturn = InventorySlots[TilePositionX, TilePositionY];
        if (ToReturn == null)
        {
            return null;
        }

        // 집은 아이템이 있을 경우 집은 아이템 넓이만큼의 타일을 정리해준다.
        CleanGridReference(ToReturn);

        return ToReturn;
    }

    // 아이템이 위치한 가방 정리
    public void CleanGridReference(UI_InventoryItem CleanItem)
    {
        // 아이템의 넓이 만큼 가방 위치 타일을 정리한다.
        for (int X = 0; X < CleanItem.WIDTH; X++)
        {
            for (int Y = 0; Y < CleanItem.HEIGHT; Y++)
            {
                InventorySlots[CleanItem._ItemInfo.ItemTileGridPositionX + X, CleanItem._ItemInfo.ItemTileGridPositionY + Y] = null;
            }
        }
    }

    // 가방에 있는 아이템을 가져온다.
    internal UI_InventoryItem GetItem(int X, int Y)
    {
        return InventorySlots[X, Y];
    }

    // 가방에서 아이템 찾기
    public UI_InventoryItem FindItem(en_SmallItemCategory FindSmallCategory)
    {
        for (int X = 0; X < InventorySizeWidth; X++)
        {
            for (int Y = 0; Y < InventorySizeHeight;Y++)
            {
                if(InventorySlots[X,Y] != null && InventorySlots[X, Y]._ItemInfo.ItemSmallCategory == FindSmallCategory)
                {
                    return InventorySlots[X, Y];
                }
            }
        }

        return null;
    }

    // 가방 슬롯 중에서 비어 있는 슬롯의 위치를 찾는다.
    public Vector2Int? FindSpaceForObject(UI_InventoryItem ItemToInsert)
    {
        // 탐색할 높이와 너비를 구한다.
        int Height = InventorySizeHeight - ItemToInsert.HEIGHT + 1;
        int Width = InventorySizeWidth - ItemToInsert.WIDTH + 1;

        // 가방 슬롯 공간 중에서 아이템의 넓이 만큼 비어 있는곳을 찾는다.
        for (int Y = 0; Y < Height; Y++)
        {
            for (int X = 0; X < Width; X++)
            {
                // 찾으면 탐색 시작 위치를 반환한다.
                if(CheckAvailableSpace(X,Y,ItemToInsert.WIDTH,ItemToInsert.HEIGHT) == true)
                {
                    return new Vector2Int(X, Y);
                }
            }
        }

        return null;
    }

    Vector2 PositionOnTheGrid = new Vector2Int();    

    // 마우스 위치의 타일 위치값을 얻는다.
    public Vector2Int GetTileGridPosition(Vector2 MousePosition)
    {
        Vector2Int TileGridPosition = new Vector2Int();
        TileGridPosition.x = -1;
        TileGridPosition.y = -1;

        // 클릭한 마우스 위치의 좌표를 구한다.
        // 마우스 X 좌표에서 Grid 가방 x 좌표를 빼줌
        // 마우스 Y 좌표에서 Grid 가방 y 좌표를 빼줌
        PositionOnTheGrid.x = MousePosition.x - _InventoryRectTransform.position.x;
        PositionOnTheGrid.y = _InventoryRectTransform.position.y - MousePosition.y;

        // 클릭한 마우스 위치를 토대로 클릭한 곳이 몇번째 타일인지 구한다.
        // 타일 x 좌표
        if (PositionOnTheGrid.x >= 0)
        {
            TileGridPosition.x = (int)(PositionOnTheGrid.x / TileSizeWidth);
        }

        // 타일 y 좌표
        if (PositionOnTheGrid.y >=0)
        {
            TileGridPosition.y = (int)(PositionOnTheGrid.y / TileSizeHeight);
        }            

        return TileGridPosition;
    }

    // 아이템을 가방에 넣기 ( 아이템 범위 체크, 중복 아이템 체크 )
    public bool PlaceItem(UI_InventoryItem NewItem, int PositionX, int PositionY, ref UI_InventoryItem OverlapItem)
    {
        // 아이템 범위 체크
        if (BoundryCheck(PositionX, PositionY, NewItem.WIDTH, NewItem.HEIGHT) == false)
        {
            return false;
        }

        // 아이템을 넣을 위치에 아이템이 이미 있는지 확인한다.
        if (OverlapCheck(PositionX, PositionY, NewItem.WIDTH, NewItem.HEIGHT, ref OverlapItem) == false)
        {
            // 아이템을 놓을 위치에 아이템이 2개 이상 있을 경우 
            // 첫번째로 발견한 OverlapItem을 null로 초기화하고 false를 반환한다.
            OverlapItem = null;
            return false;
        }

        // 중복한 아이템을 발견한 경우 ( == 놓을 위치에 아이템이 하나만 있을 경우 )
        if (OverlapItem != null)
        {
            // 중복 아이템의 공간을 비워준다.
            CleanGridReference(OverlapItem);
        }

        // 아이템을 넣는다.
        PlaceItem(NewItem, PositionX, PositionY);

        return true;
    }

    // 범위 체크, 중복 검사 체크 완료한 아이템을 가방에 넣는 함수
    public void PlaceItem(UI_InventoryItem NewItem, int PositionX, int PositionY)
    {
        // 가방에 넣을 아이템의 RectTransform을 가져온다.
        RectTransform NewItemRectTransform = NewItem.GetComponent<RectTransform>();
        // 아이템의 부모를 가방로 설정한다.
        NewItemRectTransform.SetParent(this._InventoryRectTransform);

        // 아이템의 넓이만큼 시작 위치에서 슬롯에 채워넣는다.
        for (int X = 0; X < NewItem.WIDTH; X++)
        {
            for (int Y = 0; Y < NewItem.HEIGHT; Y++)
            {
                InventorySlots[PositionX + X, PositionY + Y] = NewItem;
            }
        }

        // 넣을 아이템의 위치를 설정한다.
        NewItem._ItemInfo.ItemTileGridPositionX = (short)PositionX;
        NewItem._ItemInfo.ItemTileGridPositionY = (short)PositionY;

        // 넣을 아이템의 RectTransform (UI) 위치를 재설정 해준다.
        Vector2 Position = CalculatePositionOnGrid(NewItem, PositionX, PositionY);
        NewItemRectTransform.anchoredPosition = Position;    
    }

    // 위치 아이템 초기화
    public void InitItem(int PositionX, int PositionY)
    { 
        if(InventorySlots[PositionX, PositionY] != null)
        {
            UI_InventoryItem Item = InventorySlots[PositionX, PositionY];

            int TileWidth = Item.WIDTH;
            int TileHeight = Item.HEIGHT;

            for (int X = 0; X < TileWidth; X++)
            {
                for (int Y = 0; Y < TileHeight; Y++)
                {
                    InventorySlots[PositionX + X, PositionY + Y] = null;
                }
            }

            Destroy(Item.gameObject);
        }
    }

    // 아이템의 그리드 가방 위치를 계산한다.
    public Vector2 CalculatePositionOnGrid(UI_InventoryItem inventoryItem, int PositionX, int PositionY)
    {
        Vector2 Position = new Vector2();
        Position.x = PositionX * TileSizeWidth + TileSizeWidth * inventoryItem.WIDTH / 2;
        Position.y = -(PositionY * TileSizeHeight + TileSizeHeight * inventoryItem.HEIGHT / 2);
        return Position;
    }

    // 아이템을 놓을 위치에 이미 아이템이 있는지 확인한다.
    bool OverlapCheck(int PositionX, int PositionY, int Width, int Height, ref UI_InventoryItem OverlapItem)
    {
        // 매개변수로 받은 시작 위치부터 넓이만큼 검사한다.
        for (int X = 0; X < Width; X++)
        {
            for (int Y = 0; Y < Height; Y++)
            {
                // 만약 검사 위치에 아이템이 있을 경우
                if(InventorySlots[PositionX + X, PositionY + Y] != null)
                {
                    // OverlapItem이 null일 경우 즉, 첫번째로 중복된 아이템을 발견했을 경우
                    if(OverlapItem == null)
                    {
                        // OverlapItem에 검사 위치에서 발견한 아이템을 넣어준다.
                        OverlapItem = InventorySlots[PositionX + X, PositionY + Y];
                    }
                    else
                    {
                        // 중복 아이템을 발견 하고,
                        // 다른 위치를 검사하는데, 놓을 위치에 발견한 중복 아이템과 다른 아이템을 발견했을 경우
                        // 놓을 위치에 2개 이상의 아이템이 존재 한다는 것을 의미하므로 false를 반환한다.
                        if(OverlapItem != InventorySlots[PositionX + X , PositionY + Y])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    // 해당 공간이 비어 있는지 확인한다.
    bool CheckAvailableSpace(int PositionX, int PositionY, int Width, int Height)
    {
        // 매개변수로 받은 시작 위치부터 넓이만큼 비어 있는지 확인한다.
        for (int X = 0; X < Width; X++)
        {
            for (int Y = 0; Y < Height; Y++)
            {
                // 하나라도 공간을 차지할 경우 false를 반환한다.
                if (InventorySlots[PositionX + X, PositionY + Y] != null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    // 범위 체크
    public bool BoundryCheck(int PositionX, int PositionY, int Width, int Height)
    {
        if (PositionCheck(PositionX, PositionY) == false)
        {
            return false;
        }

        PositionX += Width - 1;
        PositionY += Height - 1;

        if (PositionCheck(PositionX, PositionY) == false)
        {
            return false;
        }

        return true;
    }

    public bool PositionCheck(int PositionX, int PositionY)
    {
        // 위치 값이 음수일 경우 false
        if(PositionX < 0 || PositionY < 0)
        {
            return false;
        }

        // x 위치값이 가방 너비보다 커지거나 Y 위치값이 가방 높이보다 커질 경우 false
        if(PositionX >= InventorySizeWidth || PositionY >= InventorySizeHeight)
        {
            return false;
        }

        return true;
    }

    public void MoneyUIUpdate(Int64 GoldCoinCount, Int16 SliverCoinCount, Int16 BronzeCoinCount)
    {
        GetTextMeshPro((int)en_InventoryMoneyText.GoldCoinCountText).text = GoldCoinCount.ToString();
        GetTextMeshPro((int)en_InventoryMoneyText.SliverCoinCountText).text = SliverCoinCount.ToString();
        GetTextMeshPro((int)en_InventoryMoneyText.BronzeCoinCountText).text = BronzeCoinCount.ToString();
    }

    public bool IsCollision(UI_Base CollisionUI)
    {
        RectTransform InventoryEdgeRect = InventoryEdge.GetComponent<RectTransform>();
        RectTransform CollisionUIRect = CollisionUI.GetComponent<RectTransform>();

        if (InventoryEdgeRect.transform.position.x - InventoryEdgeRect.rect.width / 2 < CollisionUIRect.transform.position.x + CollisionUIRect.rect.width / 2
              && InventoryEdgeRect.transform.position.x + InventoryEdgeRect.rect.width / 2 > CollisionUIRect.transform.position.x + CollisionUIRect.rect.width / 2
              && InventoryEdgeRect.transform.position.y - InventoryEdgeRect.rect.height / 2 < CollisionUIRect.transform.position.y
              && InventoryEdgeRect.transform.position.y + InventoryEdgeRect.rect.height / 2 > CollisionUIRect.transform.position.y)
        {            
            return true;
        }

        return false;
    }
}
