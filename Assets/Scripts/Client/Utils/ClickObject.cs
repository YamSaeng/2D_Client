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
        if(_InventorySelectedItem == null)
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
        if(_InventorySelectedItem != null)
        {
            _InventorySelectedItem.GetComponent<RectTransform>().transform.position = Input.mousePosition;

            if(Input.GetKeyDown(KeyCode.R))
            {
                if (_InventorySelectedItem._ItemInfo.ItemWidth != _InventorySelectedItem._ItemInfo.ItemHeight)
                {
                    CMessage ReqRotateItemPacket = Packet.MakePacket.ReqMakeRotateItemPacket(_InventorySelectedItem._ItemInfo.ItemSmallCategory);
                    Managers.NetworkManager.GameServerSend(ReqRotateItemPacket);
                }
            }
        }

        if(_BuildingSelectedItem != null)
        {
            _BuildingSelectedItem.GetComponent<RectTransform>().transform.position = Input.mousePosition;
        }
    }
}
