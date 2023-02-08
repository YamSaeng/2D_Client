using ServerCore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventoryItemDivide : UI_Base
{
    st_ItemInfo _DivideItemInfo;

    // ºÐ¹è °¹¼ö
    int _ItemDivideCount;

    enum en_InventoryItemDivideGameObject
    {
        UI_InventoryItemDivide
    }

    enum en_InvneotryItemDivideImage
    {
        ItemDivideImage
    }

    enum en_InventoryItemDivideButton
    {
        ItemDivideItemCountMinusButton,
        ItemDivideItemCountPlusButton,
        ItemDivideItemCountCancelButton,
        ItemDivideItemCountConfirmButton
    }

    enum en_InventoryItemDivideInputField
    {
        ItemDivideItemCountInputField
    }

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<InputField>(typeof(en_InventoryItemDivideInputField));
        Bind<GameObject>(typeof(en_InventoryItemDivideGameObject));
        Bind<Button>(typeof(en_InventoryItemDivideButton));
        Bind<Image>(typeof(en_InvneotryItemDivideImage));        

        BindEvent(GetGameObject((int)en_InventoryItemDivideGameObject.UI_InventoryItemDivide).gameObject, OnItemDivideDrag, Define.en_UIEvent.Drag);
        BindEvent(GetButton((int)en_InventoryItemDivideButton.ItemDivideItemCountMinusButton).gameObject, OnItemDivideMinusButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_InventoryItemDivideButton.ItemDivideItemCountPlusButton).gameObject, OnItemDividePlusButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_InventoryItemDivideButton.ItemDivideItemCountCancelButton).gameObject, OnItemCancelButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_InventoryItemDivideButton.ItemDivideItemCountConfirmButton).gameObject, OnItemConfirmButtonClick, Define.en_UIEvent.MouseClick);
        
        GetInputField((int)en_InventoryItemDivideInputField.ItemDivideItemCountInputField).onEndEdit.AddListener(ItemDivideInputFieldEnd);

        gameObject.SetActive(false);
    }

    public void ItemDivideInputFieldEnd(string Count)
    {
        int DivideCount = 1;

        if (Count.Length > 0)
        {
            DivideCount = int.Parse(Count);
            if (DivideCount > _DivideItemInfo.ItemCount)
            {
                DivideCount = _DivideItemInfo.ItemCount;
            }
            else if (DivideCount == 0)
            {
                DivideCount = 1;
            }            
        }           

        _ItemDivideCount = DivideCount;

        InputItemDivideCount(_ItemDivideCount);
    }

    public void InputItemDivideCount(int ItemDivideCount)
    {
        GetInputField((int)en_InventoryItemDivideInputField.ItemDivideItemCountInputField).text = ItemDivideCount.ToString();
    }

    public void OnItemDivideDrag(PointerEventData Event)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition += Event.delta;
    }

    public void OnItemDivideMinusButtonClick(PointerEventData Event)
    {
        if(_ItemDivideCount == 1)
        {
            return;
        }

        _ItemDivideCount--;
        InputItemDivideCount(_ItemDivideCount);
    }

    public void OnItemDividePlusButtonClick(PointerEventData Event)
    {
        if (_ItemDivideCount == _DivideItemInfo.ItemCount)
        {
            return;
        }

        _ItemDivideCount++;
        InputItemDivideCount(_ItemDivideCount);
    }

    public void OnItemCancelButtonClick(PointerEventData Event)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        GameSceneUI.DeleteGameSceneUIStack(this);         

        ShowCloseUI(false);
    }

    public void OnItemConfirmButtonClick(PointerEventData Event)
    {
        OnItemCancelButtonClick(Event);

        CMessage ReqItemDropPacket = Packet.MakePacket.ReqMakeItemDropPacket(
            Managers.NetworkManager._AccountId,
            Managers.NetworkManager._PlayerDBId,
            _DivideItemInfo.ItemSmallCategory,
            _ItemDivideCount);  
        Managers.NetworkManager.GameServerSend(ReqItemDropPacket);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void SetInventoryItemDivideIteminfo(st_ItemInfo ItemInfo)
    {
        _DivideItemInfo = ItemInfo;

        _ItemDivideCount = _DivideItemInfo.ItemCount;

        GetImage((int)en_InvneotryItemDivideImage.ItemDivideImage).sprite = Managers.Sprite._ItemSprite[_DivideItemInfo.ItemSmallCategory];
        GetInputField((int)en_InventoryItemDivideInputField.ItemDivideItemCountInputField).text = _ItemDivideCount.ToString();
    }
}
