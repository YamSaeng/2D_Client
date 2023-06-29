using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Option : UI_Base
{
    Dictionary<en_MenuType, UI_OptionItem> _OptionItems;

    enum en_OptionGameObject
    {
        UI_Option,
        OptionItemList
    }

    public override void Init()
    {
            
    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_OptionGameObject));

        BindEvent(GetGameObject((int)en_OptionGameObject.UI_Option).gameObject, OnOptionBoxDrag , Define.en_UIEvent.Drag);

        gameObject.SetActive(false);

        RectTransform OptionUIRectTransform = GetComponent<RectTransform>();
        OptionUIRectTransform.localPosition = new Vector3(0, 200.0f, 0);

        GameObject OptionItemCharacterChoiceGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_OPTION_ITEM, GetGameObject((int)en_OptionGameObject.OptionItemList).gameObject.transform);
        UI_OptionItem OptionItemCharacterChoiceUI = OptionItemCharacterChoiceGO.GetComponent<UI_OptionItem>();
        OptionItemCharacterChoiceUI.Binding();
        OptionItemCharacterChoiceUI.SetOptionItemType(en_MenuType.MENU_TYPE_CHARACTER_CHOICE);

        GameObject OptionItemQuickKeySetGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_OPTION_ITEM, GetGameObject((int)en_OptionGameObject.OptionItemList).gameObject.transform);
        UI_OptionItem OptionItemQuickKeyUI = OptionItemQuickKeySetGO.GetComponent<UI_OptionItem>();
        OptionItemQuickKeyUI.Binding();
        OptionItemQuickKeyUI.SetOptionItemType(en_MenuType.MENU_TYPE_QUICK_SLOT_KEY_SETTING);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    private void OnOptionBoxDrag(PointerEventData Event)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Event.delta;
    }
}
