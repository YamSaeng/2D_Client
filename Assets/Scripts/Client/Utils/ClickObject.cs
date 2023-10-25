using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObject : UI_Base
{
    [HideInInspector]
    public GameObject _MouseSelectObject = null;

    public UI_InventoryItem _InventorySelectedItem;
    private RectTransform _SelectedItemRectTransform;

    public UI_BuildingItem _BuildingSelectedItem;
    private RectTransform _SelectedBuildingRectTransform;

    public short _ClickObjectWidthSize;
    public short _ClickObjectHeightSize;

    List<st_TileInfo> _TileInfos = new List<st_TileInfo>();

    public override void Init()
    {
        _ClickObjectHeightSize = 1;
        _ClickObjectWidthSize = 1;
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void ClickInventorySelectItem(UI_InventoryItem InventoryItem)
    {
        _InventorySelectedItem = InventoryItem;
        _SelectedItemRectTransform = _InventorySelectedItem.GetComponent<RectTransform>();
        _SelectedItemRectTransform.SetAsLastSibling();
    }

    public void ClickInitSelectItem()
    {
        _InventorySelectedItem = null;
    }

    public void SelectItemRotateIem()
    {
        if (_InventorySelectedItem == null)
        {
            return;
        }

        _InventorySelectedItem.Rotate();
    }

    public void ClickBuildingItem(UI_BuildingItem BuildingItem)
    {
        _BuildingSelectedItem = BuildingItem;
        _SelectedBuildingRectTransform = _BuildingSelectedItem.GetComponent<RectTransform>();
        _SelectedBuildingRectTransform.SetAsLastSibling();
    }

    public void ClickInitBuildingItem()
    {
        _BuildingSelectedItem = null;
    }

    void Update()
    {
        if (_InventorySelectedItem != null)
        {
            _InventorySelectedItem.GetComponent<RectTransform>().transform.position = Input.mousePosition;

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_InventorySelectedItem._ItemInfo.ItemWidth != _InventorySelectedItem._ItemInfo.ItemHeight)
                {
                    CMessage ReqRotateItemPacket = Packet.MakePacket.ReqMakeRotateItemPacket(_InventorySelectedItem._ItemInfo.ItemSmallCategory);
                    Managers.NetworkManager.GameServerSend(ReqRotateItemPacket);
                }
            }
        }

        if (_BuildingSelectedItem != null)
        {
            _BuildingSelectedItem.GetComponent<RectTransform>().transform.position = Input.mousePosition;

            Vector2 ScreenMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            st_BuildingInfo BuildingInfo = _BuildingSelectedItem.GetBuildingInfo();

            Vector2Int MousePosition = new Vector2Int();
            MousePosition.x = (int)ScreenMousePosition.x;
            MousePosition.y = (int)ScreenMousePosition.y;

            List<st_TileInfo> TileInfos = Managers.MapTile.FindTiles(en_WorldMapInfo.WORLD_MAP_INFO_MAIN_FIELD, MousePosition, BuildingInfo);
            if (TileInfos.Count > 0)
            {
                List<st_TileInfo> EmptyTileInfos = new List<st_TileInfo>();
                if(_TileInfos.Count > 0)
                {
                    EmptyTileInfos = _TileInfos;                    

                    for (int i = 0; i < EmptyTileInfos.Count; i++)
                    {
                        for (int j = 0; j < TileInfos.Count; j++)
                        {
                            if(EmptyTileInfos[i].Position == TileInfos[j].Position)
                            {
                                EmptyTileInfos.RemoveAt(i);
                                break;
                            }
                        }
                    }                

                    if(EmptyTileInfos.Count > 0)
                    {
                        foreach(st_TileInfo EmptyTile in EmptyTileInfos)
                        {
                            CTile Tile = EmptyTile.TileGO.GetComponent<CTile>();
                            if(Tile != null)
                            {
                                Tile.TileOff();
                            }
                        }
                    }
                }                

                foreach (st_TileInfo TileInfo in TileInfos)
                {
                    CTile Tile = TileInfo.TileGO.GetComponent<CTile>();
                    if (Tile != null)
                    {
                        Tile.TileOn(TileInfo.IsOccupation);
                    }
                }

                _TileInfos = TileInfos;
            }
        }
    }
}
