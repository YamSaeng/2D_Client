using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_QuickSlotKey : UI_Base
{
    private List<UI_QuickSlotKeyItem> _QuickSlotBindingKeys = new List<UI_QuickSlotKeyItem>();

    private UI_QuickSlotKeyItem _MoveUpKeyBindingItem;
    private UI_QuickSlotKeyItem _MoveDownKeyBindingItem;
    private UI_QuickSlotKeyItem _MoveLeftKeyBindingItem;
    private UI_QuickSlotKeyItem _MoveRightKeyBindingItem;

    enum en_QuickSlotKeyGameObject
    {
        QuickSlotKeySubject,
        QuickSlotKeyItemList
    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_QuickSlotKeyGameObject));

        BindEvent(GetGameObject((int)en_QuickSlotKeyGameObject.QuickSlotKeySubject).gameObject, OnQuickSlotKeyDrag, Define.en_UIEvent.Drag);
    }

    public override void Init()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        QuickSlotKeyUIUpdate();

        gameObject.SetActive(IsShowClose);
    }

    private void OnQuickSlotKeyDrag(PointerEventData Event)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Event.delta;        
    }

    public void QuickSlotKeyCreate()
    {
        foreach(st_BindingKey BindingKey in Managers.Key._BindingKeys)
        {
            GameObject QuickSlotKeyItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_QUICK_SLOT_KEY_ITEM, GetGameObject((int)en_QuickSlotKeyGameObject.QuickSlotKeyItemList).gameObject.transform);
            UI_QuickSlotKeyItem QuickSlotKeyItemUI = QuickSlotKeyItemGO.GetComponent<UI_QuickSlotKeyItem>();
            if(QuickSlotKeyItemUI != null)
            {
                QuickSlotKeyItemUI.Binding();
                QuickSlotKeyItemUI.SetQuickSlotKeyItemBindingKey(BindingKey);

                _QuickSlotBindingKeys.Add(QuickSlotKeyItemUI);
            }
        }

        ShowCloseUI(false);
    }

    public void QuickSlotKeyFindFix(en_KeyCode KeyCode)
    {
        foreach(UI_QuickSlotKeyItem QuickSlotKeyItemUI in _QuickSlotBindingKeys)
        {
            if(QuickSlotKeyItemUI._BindingKey.KeyCode == KeyCode)
            {
                QuickSlotKeyItemUI._BindingKey.KeyCode = en_KeyCode.KEY_CODE_NONE;
                QuickSlotKeyItemUI.QuickSlotKeyItemUIUpdate();
                break;
            }
        }
    }

    public void QuickSlotKeyUIUpdate()
    {
        foreach (UI_QuickSlotKeyItem QuickSlotKeyItemUI in _QuickSlotBindingKeys)
        {
            QuickSlotKeyItemUI.QuickSlotKeyItemUIUpdate();
        }
    }
}
