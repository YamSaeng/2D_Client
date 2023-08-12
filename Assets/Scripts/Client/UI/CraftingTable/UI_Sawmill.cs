using ServerCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Sawmill : UI_Base
{
    [HideInInspector]
    public st_CraftingTableRecipe _SawmillCraftingTable; // �뱤��UI�� �������� ������ ���
    [HideInInspector]
    public SawmillController _SawmillController;
    //[HideInInspector] // �뱤�� ���۹�
    //public Dictionary<en_SmallItemCategory, UI_CraftingCompleteItem> _UICraftingCompleteItems = new Dictionary<en_SmallItemCategory, UI_CraftingCompleteItem>();
    //[HideInInspector]
    //public List<UI_CraftingMaterialItem> _SawmillMaterialItemUIList; // �뱤�ΰ� �������� ��� ������ ���
    //[HideInInspector]
    //public List<UI_CraftingTableCompleteItem> _SawmillCompleteItemUIList; // �뱤�ΰ� �������� ���� �Ϸ� ������ ���

    enum en_SawmillGameObject
    {
        SawmillMaterialContent,
        SawmillCompleteContent,
        CraftingBoxCompleteItemScroll,
        CraftingBoxCompleteItemContent,
        UI_Sawmill
    }

    enum en_SawmillButton
    {
        SawmillCraftingStartButton,
        SawmillCraftingStopButton
    }

    public override void Init()
    {
        
    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_SawmillGameObject));
        Bind<Button>(typeof(en_SawmillButton));

        BindEvent(GetGameObject((int)en_SawmillGameObject.UI_Sawmill).gameObject, OnSawmillBoxDrag, Define.en_UIEvent.Drag);

        BindEvent(GetButton((int)en_SawmillButton.SawmillCraftingStartButton).gameObject, OnSawmillCraftingStartButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_SawmillButton.SawmillCraftingStopButton).gameObject, OnSawmillCraftingStopButtonClick, Define.en_UIEvent.MouseClick);

        gameObject.SetActive(false);
    }    

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);

        if(IsShowClose == true)
        {
            SawmillRefreshUI();
        }
    }

    public void SetSamillController(SawmillController SawmaillObject)
    {
        _SawmillController = SawmaillObject;
    }

    private void OnSawmillCraftingStartButtonClick(PointerEventData Event)
    {
        CMessage ReqFurnaceCraftingStartPacket = Packet.MakePacket.ReqMakeCraftingTableCraftingStartPacket(Managers.NetworkManager._AccountId,
            Managers.NetworkManager._PlayerDBId,
            _SawmillController._GameObjectInfo.ObjectId,
            _SawmillController._SelectCompleteItemCategory,
            1);
        Managers.NetworkManager.GameServerSend(ReqFurnaceCraftingStartPacket);        
    }

    private void OnSawmillCraftingStopButtonClick(PointerEventData Event)
    {
        CMessage ReqFurnaceCraftingStopPacket = Packet.MakePacket.ReqMakeCraftingTableCraftingStopPacket(
            Managers.NetworkManager._AccountId,
            Managers.NetworkManager._PlayerDBId,
            _SawmillController._GameObjectInfo.ObjectId);
        Managers.NetworkManager.GameServerSend(ReqFurnaceCraftingStopPacket);        
    }

    private void OnSawmillBoxDrag(PointerEventData Event)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Event.delta;
    }

    // ����� ���۴� ��� ������ �簻��
    public void SawmillMaterialItemRefreshUI(en_SmallItemCategory RefreshCompleteItem)
    {
        //if(_SawmillMaterialItemUIList.Count > 0)
        //{
        //    for(int i=0; i< _SawmillMaterialItemUIList.Count;i++)
        //    {
        //        Destroy(_SawmillMaterialItemUIList[i].gameObject);
        //        _SawmillMaterialItemUIList[i] = null;
        //    }

        //    _SawmillMaterialItemUIList.Clear();
        //}
        
        st_ItemInfo FindCompleteItem = null;
        for (int i = 0; i < _SawmillCraftingTable.CraftingTableCompleteItems.Count; i++)
        {
            if (_SawmillCraftingTable.CraftingTableCompleteItems[i].ItemSmallCategory == RefreshCompleteItem)
            {
                FindCompleteItem = _SawmillCraftingTable.CraftingTableCompleteItems[i];
            }
        }

        if(FindCompleteItem != null)
        {
            foreach (st_CraftingMaterialItemInfo MaterialItemInfo in FindCompleteItem.Materials)
            {
                //// ��� ������ UI ����
                //GameObject CraftingTableMaterialItemGO = Managers.Resource.Instantiate("UI/Crafting/UI_CraftingMaterialItem", GetGameObject((int)en_SawmillGameObject.SawmillMaterialContent).transform);
                //UI_CraftingMaterialItem CraftingTableMaterialUI = CraftingTableMaterialItemGO.GetComponent<UI_CraftingMaterialItem>();
                //CraftingTableMaterialUI.CraftingMaterialItemUpdate(MaterialItemInfo, _SawmillController);
                //// ����
                //_SawmillMaterialItemUIList.Add(CraftingTableMaterialUI);

                int MaterialCount = 0;
                // �뱤�ο��� �ش� �������� � ������ �ִ��� Ȯ���Ѵ�.                
                foreach (st_ItemInfo MaterialItem in _SawmillController._SawmillMaterials.Values.ToList())
                {
                    if (MaterialItem.ItemSmallCategory == MaterialItemInfo.MaterialItemType)
                    {
                        MaterialCount = MaterialItem.ItemCount;
                    }
                }

                //CraftingTableMaterialUI.RefreshMaterialItemUI(MaterialCount);
            }
        }
    }

    // ����� ���� �ϼ� ������ �簻��
    public void SawmillCompleteItemRefreshUI()
    {
        //if(_SawmillCompleteItemUIList.Count > 0)
        //{
        //    for(int i=0;i< _SawmillCompleteItemUIList.Count;i++)
        //    {
        //        Destroy(_SawmillCompleteItemUIList[i].gameObject);
        //        _SawmillCompleteItemUIList[i] = null;
        //    }

        //    _SawmillCompleteItemUIList.Clear();
        //}

        foreach (st_ItemInfo CompleteItem in _SawmillController._SawmillCompleteItems.Values.ToList())
        {
            switch (CompleteItem.ItemSmallCategory)
            {
                case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_WOOD_FLANK:
                    //GameObject CraftingCompleteItemGO = Managers.Resource.Instantiate("UI/Crafting/UI_CraftingTableCompleteItem", GetGameObject((int)en_SawmillGameObject.SawmillCompleteContent).transform);
                    //UI_CraftingTableCompleteItem CraftingCraftMaterialItemUI = CraftingCompleteItemGO.GetComponent<UI_CraftingTableCompleteItem>();
                    //CraftingCraftMaterialItemUI.CraftingCraftMaterialItemUpdate(CompleteItem._ItemInfo, _SawmillController);
                    //CraftingCraftMaterialItemUI.RefreshCarftingCraftMaterialItemUI();

                    //_SawmillCompleteItemUIList.Add(CraftingCraftMaterialItemUI);
                    break;
            }
        }
    }

    // ����� UI �簻��
    public void SawmillRefreshUI()
    {
        //if(_UICraftingCompleteItems.Count > 0)
        //{
        //    foreach(UI_CraftingCompleteItem CraftingCompleteItemUI in _UICraftingCompleteItems.Values)
        //    {
        //        Destroy(CraftingCompleteItemUI.gameObject);
        //    }

        //    _UICraftingCompleteItems.Clear();
        //}

        //for (int i = 0; i < _SawmillMaterialItemUIList.Count; i++)
        //{
        //    Destroy(_SawmillMaterialItemUIList[i].gameObject);
        //    _SawmillMaterialItemUIList[i] = null;
        //}

        //_SawmillMaterialItemUIList.Clear();

        // ����� ���� �Ϸ� ������ ��� ����
        SawmillCompleteItemRefreshUI();

        // ������ ��� Scroll Rect ViewPort �ʱ�ȭ
        GetGameObject((int)en_SawmillGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta = new Vector2(150.0f, 0);
        // ������ ��� Scroll Rect Content �ʱ�ȭ
        GetGameObject((int)en_SawmillGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        // �������� SawmillController�� ������ ����Ұ� �����Ҽ� �ִ� ������ ����� ��ȯ�ϸ鼭 UI�� ����
        foreach (st_ItemInfo CraftingCompleteItem in _SawmillCraftingTable.CraftingTableCompleteItems)
        {
            // ����ǰ ����
            //GameObject CraftingCompleteItemGO = Managers.Resource.Instantiate("UI/Crafting/UI_CraftingCompleteItem", GetGameObject((int)en_SawmillGameObject.CraftingBoxCompleteItemContent).transform);
            //UI_CraftingCompleteItem CraftingCompleteItemUI = CraftingCompleteItemGO.GetComponent<UI_CraftingCompleteItem>();
            //// ����ǰ ���� ���� ( ����ǰ����, ����ǰ�� �������� ������Ʈ )
            //CraftingCompleteItemUI.CraftingCompleteItemUpdate(CraftingCompleteItem, _SawmillController);
            //// ����ǰ ���õ� ������ UI ����
            //CraftingCompleteItemUI.RefreshCraftingCompleteItemUI();

            //// Scroll RectViewPort ũ�� ����
            //Vector2 CraftingBoxCompleteItemScrollSize = GetGameObject((int)en_SawmillGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta + new Vector2(0, 100.0f);
            //GetGameObject((int)en_SawmillGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta = CraftingBoxCompleteItemScrollSize;

            //// Scroll RectContent ũ�� ����
            //Vector2 CraftingBoxCompleteItemContentSize = GetGameObject((int)en_SawmillGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().sizeDelta + new Vector2(0, 100.0f);
            //GetGameObject((int)en_SawmillGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().sizeDelta = CraftingBoxCompleteItemContentSize;

            //_UICraftingCompleteItems.Add(CraftingCompleteItemUI._CraftingCompleteItem.ItemSmallCategory, CraftingCompleteItemUI);
        }
    }

    public void CraftingScrollBoxMove(Vector2 MoveData)
    {
        GetGameObject((int)en_SawmillGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().anchoredPosition += (MoveData * -1.0f * 10.0f);
    }
}