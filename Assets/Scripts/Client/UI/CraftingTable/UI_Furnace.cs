using ServerCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Furnace : UI_Base
{
    [HideInInspector]
    public st_CraftingTableRecipe _FurnaceCraftingTable; // �뱤��UI�� �������� ������ ���
    [HideInInspector]
    public FurnaceController _FurnaceController; // �뱤��UI�� ���õ� �뱤��     
    //[HideInInspector] // �뱤�� ���۹�
    //public Dictionary<en_SmallItemCategory, UI_CraftingCompleteItem> _UICraftingCompleteItems = new Dictionary<en_SmallItemCategory, UI_CraftingCompleteItem>();
    //[HideInInspector]
    //public List<UI_CraftingMaterialItem> _FurnaceMaterialItemUIList; // �뱤�ΰ� �������� ��� ������ ���
    //[HideInInspector]
    //public List<UI_CraftingTableCompleteItem> _FurnaceCompleteItemUIList; // �뱤�ΰ� �������� ���� �Ϸ� ������ ���

    enum en_FurnaceGameObject
    {
        FurnaceMaterialContent,
        FurnaceCompleteContent,
        CraftingBoxCompleteItemScroll,
        CraftingBoxCompleteItemContent,
        UI_Furnace
    }

    enum en_FurnaceButton
    {
        FurnaceCraftingStartButton,
        FurnaceCraftingStopButton
    }

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_FurnaceGameObject));
        Bind<Button>(typeof(en_FurnaceButton));

        BindEvent(GetGameObject((int)en_FurnaceGameObject.UI_Furnace).gameObject, OnFurnaceBoxDrag, Define.en_UIEvent.Drag);        

        BindEvent(GetButton((int)en_FurnaceButton.FurnaceCraftingStartButton).gameObject, OnFurnaceCraftingStartButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_FurnaceButton.FurnaceCraftingStopButton).gameObject, OnFurnaceCraftingStopButtonClick, Define.en_UIEvent.MouseClick);

        gameObject.SetActive(false);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);

        if (IsShowClose == true)
        {
            FurnaceRefreshUI();
        }
    }

    public void SetFurnaceController(FurnaceController FurnaceObject)
    {
        _FurnaceController = FurnaceObject;
    }    

    private void OnFurnaceCraftingStartButtonClick(PointerEventData Event)
    {
        CMessage ReqFurnaceCraftingStartPacket = Packet.MakePacket.ReqMakeCraftingTableCraftingStartPacket(Managers.NetworkManager._AccountId,
            Managers.NetworkManager._PlayerDBId,
            _FurnaceController._GameObjectInfo.ObjectId,            
            _FurnaceController._SelectCompleteItemCategory,
            1);
        Managers.NetworkManager.GameServerSend(ReqFurnaceCraftingStartPacket);        
    }

    private void OnFurnaceCraftingStopButtonClick(PointerEventData Event)
    {
        CMessage ReqFurnaceCraftingStopPacket = Packet.MakePacket.ReqMakeCraftingTableCraftingStopPacket(
            Managers.NetworkManager._AccountId,
            Managers.NetworkManager._PlayerDBId,
            _FurnaceController._GameObjectInfo.ObjectId);
        Managers.NetworkManager.GameServerSend(ReqFurnaceCraftingStopPacket);        
    }

    private void OnFurnaceBoxDrag(PointerEventData Event)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Event.delta;
    }    

    // �뱤�� ���۴� ��� ������ �簻��
    public void FurnaceMaterialItemRefreshUI(en_SmallItemCategory RefreshCompleteItem)
    {
        //// �����ص� ��� UI ����
        //if (_FurnaceMaterialItemUIList.Count > 0)
        //{
        //    for (int i = 0; i < _FurnaceMaterialItemUIList.Count; i++)
        //    {
        //        Destroy(_FurnaceMaterialItemUIList[i].gameObject);
        //        _FurnaceMaterialItemUIList[i] = null;
        //    }

        //    _FurnaceMaterialItemUIList.Clear();
        //}

        // �뱤��UI�� �������� �뱤�� ������ �������� ��û�� ItemCategory�� ã�´�.
        st_ItemInfo FindCompleteItem = null;
        for (int i = 0; i < _FurnaceCraftingTable.CraftingTableCompleteItems.Count; i++)
        {
            if (_FurnaceCraftingTable.CraftingTableCompleteItems[i].ItemSmallCategory == RefreshCompleteItem)
            {
                FindCompleteItem = _FurnaceCraftingTable.CraftingTableCompleteItems[i];
            }
        }

        // ã���� ���
        if (FindCompleteItem != null)
        {
            foreach (st_CraftingMaterialItemInfo MaterialItemInfo in FindCompleteItem.Materials)
            {
                //// ��� ������ UI ����
                //GameObject CraftingTableMaterialItemGO = Managers.Resource.Instantiate("UI/Crafting/UI_CraftingMaterialItem", GetGameObject((int)en_FurnaceGameObject.FurnaceMaterialContent).transform);
                //UI_CraftingMaterialItem CraftingTableMaterialUI = CraftingTableMaterialItemGO.GetComponent<UI_CraftingMaterialItem>();
                //CraftingTableMaterialUI.CraftingMaterialItemUpdate(MaterialItemInfo, _FurnaceController);
                //// ����
                //_FurnaceMaterialItemUIList.Add(CraftingTableMaterialUI);

                int MaterialCount = 0;
                // �뱤�ο��� �ش� �������� � ������ �ִ��� Ȯ���Ѵ�.                
                foreach (CItem MaterialItem in _FurnaceController._FurnaceMaterials.Values.ToList())
                {
                    if (MaterialItem._ItemInfo.ItemSmallCategory == MaterialItemInfo.MaterialItemType)
                    {
                        MaterialCount = MaterialItem._ItemInfo.ItemCount;
                    }
                }

                //CraftingTableMaterialUI.RefreshMaterialItemUI(MaterialCount);
            }
        }
    }

    // �뱤�� ���� �ϼ� ������ �簻��
    public void FurnaceCompleteItemRefreshUI()
    {
        //if (_FurnaceCompleteItemUIList.Count > 0)
        //{
        //    for (int i = 0; i < _FurnaceCompleteItemUIList.Count; i++)
        //    {
        //        Destroy(_FurnaceCompleteItemUIList[i].gameObject);
        //        _FurnaceCompleteItemUIList[i] = null;
        //    }

        //    _FurnaceCompleteItemUIList.Clear();
        //}

        //foreach (CItem CompleteItem in _FurnaceController._FurnaceCompleteItems.Values.ToList())
        //{
        //    switch (CompleteItem._ItemInfo.ItemSmallCategory)
        //    {
        //        case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_CHAR_COAL:
        //            GameObject CraftingCompleteItemGO = Managers.Resource.Instantiate("UI/Crafting/UI_CraftingTableCompleteItem", GetGameObject((int)en_FurnaceGameObject.FurnaceCompleteContent).transform);
        //            UI_CraftingTableCompleteItem CraftingCraftMaterialItemUI = CraftingCompleteItemGO.GetComponent<UI_CraftingTableCompleteItem>();
        //            CraftingCraftMaterialItemUI.CraftingCraftMaterialItemUpdate(CompleteItem._ItemInfo, _FurnaceController);
        //            CraftingCraftMaterialItemUI.RefreshCarftingCraftMaterialItemUI();

        //            _FurnaceCompleteItemUIList.Add(CraftingCraftMaterialItemUI);
        //            break;
        //    }
        //}
    }

    public void FurnaceRefreshUI()
    {
        //if (_UICraftingCompleteItems.Count > 0)
        //{
        //    foreach (UI_CraftingCompleteItem CraftingCompleteItemUI in _UICraftingCompleteItems.Values)
        //    {
        //        Destroy(CraftingCompleteItemUI.gameObject);
        //    }

        //    _UICraftingCompleteItems.Clear();
        //}        

        //// �뱤�� ����� ��� �ʱ�ȭ
        //for (int i = 0; i < _FurnaceMaterialItemUIList.Count; i++)
        //{
        //    Destroy(_FurnaceMaterialItemUIList[i].gameObject);
        //    _FurnaceMaterialItemUIList[i] = null;
        //}

        //_FurnaceMaterialItemUIList.Clear();

        // �뱤�� ���� �Ϸ� ������ ��� ����
        FurnaceCompleteItemRefreshUI();

        // ������ ��� Scroll Rect ViewPort �ʱ�ȭ
        GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta = new Vector2(150.0f, 0);
        // ������ ��� Scroll Rect Content �ʱ�ȭ
        GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        //// �����Ǿ� �ִ� �뱤�ΰ� ������ ������ ����� ��ȯ�ϸ鼭 Scroll Rect�� ����
        //foreach (st_ItemInfo CraftingCompleteItem in _FurnaceCraftingTable.CraftingTableCompleteItems)
        //{
        //    // ����ǰ ����
        //    GameObject CraftingCompleteItemGO = Managers.Resource.Instantiate("UI/Crafting/UI_CraftingCompleteItem", GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemContent).transform);
        //    UI_CraftingCompleteItem CraftingCompleteItemUI = CraftingCompleteItemGO.GetComponent<UI_CraftingCompleteItem>();
        //    // ����ǰ ���� ���� ( ����ǰ����, ����ǰ�� �������� ������Ʈ )
        //    CraftingCompleteItemUI.CraftingCompleteItemUpdate(CraftingCompleteItem, _FurnaceController);
        //    // ����ǰ ���õ� ������ UI ����
        //    CraftingCompleteItemUI.RefreshCraftingCompleteItemUI();

        //    // Scroll RectViewPort ũ�� ����
        //    Vector2 CraftingBoxCompleteItemScrollSize = GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta + new Vector2(0, 100.0f);
        //    GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta = CraftingBoxCompleteItemScrollSize;

        //    // Scroll RectContent ũ�� ����
        //    Vector2 CraftingBoxCompleteItemContentSize = GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().sizeDelta + new Vector2(0, 100.0f);
        //    GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().sizeDelta = CraftingBoxCompleteItemContentSize;

        //    _UICraftingCompleteItems.Add(CraftingCompleteItemUI._CraftingCompleteItem.ItemSmallCategory, CraftingCompleteItemUI);
        //}
    }

    public void CraftingScrollBoxMove(Vector2 MoveData)
    {
        GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().anchoredPosition += ( MoveData * -1.0f * 10.0f);
    }
}
