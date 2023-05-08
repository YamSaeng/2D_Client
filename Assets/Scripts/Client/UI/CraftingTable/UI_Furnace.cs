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
    public st_CraftingTableRecipe _FurnaceCraftingTable; // 용광로UI가 보유중인 조합템 목록
    [HideInInspector]
    public FurnaceController _FurnaceController; // 용광로UI에 셋팅된 용광로     
    //[HideInInspector] // 용광로 제작법
    //public Dictionary<en_SmallItemCategory, UI_CraftingCompleteItem> _UICraftingCompleteItems = new Dictionary<en_SmallItemCategory, UI_CraftingCompleteItem>();
    //[HideInInspector]
    //public List<UI_CraftingMaterialItem> _FurnaceMaterialItemUIList; // 용광로가 보유중인 재료 아이템 목록
    //[HideInInspector]
    //public List<UI_CraftingTableCompleteItem> _FurnaceCompleteItemUIList; // 용광로가 보유중인 제작 완료 아이템 목록

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

    // 용광로 제작대 재료 아이템 재갱신
    public void FurnaceMaterialItemRefreshUI(en_SmallItemCategory RefreshCompleteItem)
    {
        //// 생성해둔 재료 UI 삭제
        //if (_FurnaceMaterialItemUIList.Count > 0)
        //{
        //    for (int i = 0; i < _FurnaceMaterialItemUIList.Count; i++)
        //    {
        //        Destroy(_FurnaceMaterialItemUIList[i].gameObject);
        //        _FurnaceMaterialItemUIList[i] = null;
        //    }

        //    _FurnaceMaterialItemUIList.Clear();
        //}

        // 용광로UI가 소유중인 용광로 조합템 정보에서 요청한 ItemCategory를 찾는다.
        st_ItemInfo FindCompleteItem = null;
        for (int i = 0; i < _FurnaceCraftingTable.CraftingTableCompleteItems.Count; i++)
        {
            if (_FurnaceCraftingTable.CraftingTableCompleteItems[i].ItemSmallCategory == RefreshCompleteItem)
            {
                FindCompleteItem = _FurnaceCraftingTable.CraftingTableCompleteItems[i];
            }
        }

        // 찾앗을 경우
        if (FindCompleteItem != null)
        {
            foreach (st_CraftingMaterialItemInfo MaterialItemInfo in FindCompleteItem.Materials)
            {
                //// 재료 아이템 UI 생성
                //GameObject CraftingTableMaterialItemGO = Managers.Resource.Instantiate("UI/Crafting/UI_CraftingMaterialItem", GetGameObject((int)en_FurnaceGameObject.FurnaceMaterialContent).transform);
                //UI_CraftingMaterialItem CraftingTableMaterialUI = CraftingTableMaterialItemGO.GetComponent<UI_CraftingMaterialItem>();
                //CraftingTableMaterialUI.CraftingMaterialItemUpdate(MaterialItemInfo, _FurnaceController);
                //// 저장
                //_FurnaceMaterialItemUIList.Add(CraftingTableMaterialUI);

                int MaterialCount = 0;
                // 용광로에서 해당 아이템을 몇개 가지고 있는지 확인한다.                
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

    // 용광로 제작 완성 아이템 재갱신
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

        //// 용광로 재료템 목록 초기화
        //for (int i = 0; i < _FurnaceMaterialItemUIList.Count; i++)
        //{
        //    Destroy(_FurnaceMaterialItemUIList[i].gameObject);
        //    _FurnaceMaterialItemUIList[i] = null;
        //}

        //_FurnaceMaterialItemUIList.Clear();

        // 용광로 제작 완료 아이템 목록 갱신
        FurnaceCompleteItemRefreshUI();

        // 제작템 목록 Scroll Rect ViewPort 초기화
        GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta = new Vector2(150.0f, 0);
        // 제작템 목록 Scroll Rect Content 초기화
        GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemContent).GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

        //// 설정되어 있는 용광로가 소유한 제작템 목록을 순환하면서 Scroll Rect에 담음
        //foreach (st_ItemInfo CraftingCompleteItem in _FurnaceCraftingTable.CraftingTableCompleteItems)
        //{
        //    // 제작품 생성
        //    GameObject CraftingCompleteItemGO = Managers.Resource.Instantiate("UI/Crafting/UI_CraftingCompleteItem", GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemContent).transform);
        //    UI_CraftingCompleteItem CraftingCompleteItemUI = CraftingCompleteItemGO.GetComponent<UI_CraftingCompleteItem>();
        //    // 제작품 정보 셋팅 ( 제작품정보, 제작품을 소유중인 오브젝트 )
        //    CraftingCompleteItemUI.CraftingCompleteItemUpdate(CraftingCompleteItem, _FurnaceController);
        //    // 제작품 셋팅된 정보로 UI 갱신
        //    CraftingCompleteItemUI.RefreshCraftingCompleteItemUI();

        //    // Scroll RectViewPort 크기 설정
        //    Vector2 CraftingBoxCompleteItemScrollSize = GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta + new Vector2(0, 100.0f);
        //    GetGameObject((int)en_FurnaceGameObject.CraftingBoxCompleteItemScroll).GetComponent<RectTransform>().sizeDelta = CraftingBoxCompleteItemScrollSize;

        //    // Scroll RectContent 크기 설정
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
