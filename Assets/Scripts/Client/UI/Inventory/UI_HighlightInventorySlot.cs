using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤토리에 있는 아이템 크기만큼 표시 기능을 해줄 오브젝트
public class UI_HighlightInventorySlot : UI_Base
{
    [SerializeField] RectTransform HighligherRectTransform;
    
    public override void Init()
    {
        //GameObject HighligherGO = Managers.Resource.Instantiate("UI/Inventory/UI_HighlighteItem");
        //HighligherRectTransform = HighligherGO.GetComponent<RectTransform>();
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        
    }

    // 표시 기능을 bool 값에 따라 켜주거나 꺼준다.
    internal void Show(bool v)
    {
        if(HighligherRectTransform != null)
        {
            HighligherRectTransform.gameObject.SetActive(v);
        }        
    }

    // 하이라이팅 표시 기능의 크기를 셋팅
    // 표시 해줄 아이템의 정보를 받는다.
    internal void SetSize(UI_InventoryItem TargetItem)
    {
        // 표시할 아이템의 크기만큼 크기를 재설정한다.
        Vector2 Size = new Vector2();
        Size.x = TargetItem.WIDTH * UI_Inventory.TileSizeWidth;
        Size.y = TargetItem.HEIGHT * UI_Inventory.TileSizeHeight;

        HighligherRectTransform.sizeDelta = Size;
    }

    public void SetParent(UI_Inventory TargetGridInventory)
    {
        if(TargetGridInventory == null)
        {
            return;
        }

        HighligherRectTransform.SetParent(TargetGridInventory.GetComponent<RectTransform>());
    }

    // 위치를 설정
    internal void SetPosition(UI_Inventory TargetGridInventory, UI_InventoryItem TargetItem)
    {
        // 부모를 매개변수로 받은 Grid 인벤토리로 설정
        SetParent(TargetGridInventory);
                
        Vector2 Position = TargetGridInventory.CalculatePositionOnGrid(
            TargetItem,
            TargetItem._ItemInfo.ItemTileGridPositionX,
            TargetItem._ItemInfo.ItemTileGridPositionY);

        HighligherRectTransform.localPosition = Position;
    }    

    public void SetPosition(UI_Inventory TargetGrid, UI_InventoryItem TargetItem, int PositionX, int PositionY)
    {
        Vector2 Position = TargetGrid.CalculatePositionOnGrid(
          TargetItem,
          PositionX,
          PositionY);

        HighligherRectTransform.localPosition = Position;
    }
}
