using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    private CreatureObject _OwnerCreature;
    private byte _InventoryCount;

    public List<Inventory> _Inventorys = new List<Inventory>();

    public UI_Inventory _InventoryUI;

    private byte _InventoryWidth;
    private byte _InventoryHeight;

    // 가방 생성
    public void InventoryCreate(CreatureObject OwnerCreature, byte InventoryCount, byte InventoryWidth, byte InventoryHeight)
    {
        _OwnerCreature = OwnerCreature;

        _InventoryCount = InventoryCount;

        _InventoryWidth = InventoryWidth;
        _InventoryHeight = InventoryHeight;

        for (int i = 0; i < InventoryCount; i++)
        {
            _Inventorys.Add(new Inventory());
            _Inventorys[i].InventoryCreate(_InventoryWidth, _InventoryHeight);
        }       
    }

    // 서버로부터 받은 아이템을 가방에 넣음
    public void S2C_InventoryInsertItem(st_ItemInfo[] InventoryItems,        
        long GoldCoinCount, short SliverCoinCount, short BronzeCoinCount)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if (GameSceneUI == null)
        {
            return;
        }        

        foreach (st_ItemInfo ItemInfo in InventoryItems)
        {
            _Inventorys[0].PlaceItem(ItemInfo, ItemInfo.ItemTileGridPositionX, ItemInfo.ItemTileGridPositionY);
        }

        GameObject InventoryBodyGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INVENTORY_BOX, GameSceneUI.transform);
        InventoryBodyGO.GetComponent<RectTransform>().localPosition = new Vector3(600.0f, 100.0f, 0.0f);

        GameObject InentoryEdgeGO = InventoryBodyGO.transform.Find("InventoryEdge").gameObject;
        GameObject InentoryGO = InentoryEdgeGO.transform.Find("Inventory").gameObject;
        UI_Inventory InventorUI = InentoryGO.GetComponent<UI_Inventory>();
        _InventoryUI = InventorUI;
        _InventoryUI.Binding();
        _InventoryUI._InventoryRectTransform = InentoryGO.GetComponent<RectTransform>();

        _InventoryUI.InventoryCreate(_InventoryWidth, _InventoryHeight);

        for (byte i = 0; i < InventoryItems.Length; i++)
        {
            GameObject InventoryItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INVENTORY_ITEM, GameSceneUI.transform);

            UI_InventoryItem InventoryItem = InventoryItemGO.GetComponent<UI_InventoryItem>();
            InventoryItem.GetComponent<RectTransform>().SetAsLastSibling();
            InventoryItem.Set(InventoryItems[i]);
            InventoryItem.Binding();
            InventoryItem.SetParentGridInventory(_InventoryUI);

            _InventoryUI.PlaceItem(InventoryItem, InventoryItem._ItemInfo.ItemTileGridPositionX, InventoryItem._ItemInfo.ItemTileGridPositionY);
        }

        _InventoryUI.MoneyUIUpdate(GoldCoinCount, SliverCoinCount, BronzeCoinCount);

        _InventoryUI.ShowCloseUI(false);

        Managers.MyInventory.SelectedInventory = InventorUI;
    }

    public void InsertItem(byte SelectInventoryIndex, st_ItemInfo InsertNewItem)
    {
        _Inventorys[SelectInventoryIndex].PlaceItem(InsertNewItem, InsertNewItem.ItemTileGridPositionX, InsertNewItem.ItemTileGridPositionY);
    }
}
