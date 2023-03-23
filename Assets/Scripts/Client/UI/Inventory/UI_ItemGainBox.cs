using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_ItemGainBox : UI_Base
{
    private long _ItemGainID = 0;
    Dictionary<long, UI_ItemGain> _ItemGains = new Dictionary<long, UI_ItemGain>();
    const float ITEM_GAIN_GAP = 110.0f;

    enum en_ItemGainBoxGameObject
    {
        ItemGainScrollBox,
        ItemGainPannel
    }

    public class CItemGainMessage
    {
        public Sprite ItemImage;
        public Text Count;
    }

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_ItemGainBoxGameObject));
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void Update()
    {
        int ItemGainCount = _ItemGains.Count;
        RectTransform ItemGainBoxRect = GetGameObject((int)en_ItemGainBoxGameObject.ItemGainScrollBox).GetComponent<RectTransform>();
        ItemGainBoxRect.sizeDelta = new Vector2(ItemGainBoxRect.rect.width, ItemGainCount * ITEM_GAIN_GAP);
    }

    public void NewItemGainMessage(st_ItemInfo GainItemInfo, int Count)
    {
        RectTransform ItemGainBoxRect = GetGameObject((int)en_ItemGainBoxGameObject.ItemGainScrollBox).GetComponent<RectTransform>();
        ItemGainBoxRect.sizeDelta = new Vector2(ItemGainBoxRect.rect.width, ItemGainBoxRect.rect.height + ITEM_GAIN_GAP);

        // 새로운 Gain 메세지 생성
        GameObject ItemGainGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INVENTORY_ITEM_GAIN);
        UI_ItemGain ItemGainUI = ItemGainGo.GetComponent<UI_ItemGain>();
        ItemGainUI._ItemGainID = _ItemGainID;
        _ItemGains.Add(_ItemGainID, ItemGainUI);

        _ItemGainID++;

        ItemGainGo.transform.SetParent(GetGameObject((int)en_ItemGainBoxGameObject.ItemGainPannel).transform);

        UI_ItemGain ItemGain = ItemGainGo.GetComponent<UI_ItemGain>();
        ItemGain.SetItemGain(GainItemInfo, Count);
    }

    public void ItemGainDestory(UI_ItemGain ItemGainUI)
    {
        GameObject DestoryItemGainUI = _ItemGains[ItemGainUI._ItemGainID].gameObject;
        _ItemGains.Remove(ItemGainUI._ItemGainID);
        Destroy(DestoryItemGainUI.gameObject);
    }
}