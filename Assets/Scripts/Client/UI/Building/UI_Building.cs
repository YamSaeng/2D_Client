using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Building : UI_Base
{
    [HideInInspector]
    public byte MainHallBuildingCount;
    [HideInInspector]
    public short WallBuildingCount;
    [HideInInspector]
    public byte WeaponBuildingCount;
    [HideInInspector]
    public byte ArmorBuildingCount;

    enum en_BuildingButton
    {
        GovernmentOfficeListButton,
        MainHallButton,
        WallButton,
        StoreListButton,
        WeaponStoreButton,
        ArmorStoreButton,
        CloseButton
    }

    enum en_BuildingText
    {
        MainHallCountText,
        WallCountText
    }


    public override void Init()
    {
        MainHallBuildingCount = 1;
        WallBuildingCount = 20;
        WeaponBuildingCount = 1;
        ArmorBuildingCount = 1;
    }

    public override void Binding()
    {
        Bind<Button>(typeof(en_BuildingButton));
        Bind<TextMeshProUGUI>(typeof(en_BuildingText));

        BindEvent(GetButton((int)en_BuildingButton.GovernmentOfficeListButton).gameObject, OnGovermentOfficeListButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_BuildingButton.MainHallButton).gameObject, OnMainHallBuildingButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_BuildingButton.WallButton).gameObject, OnWallBuildingButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_BuildingButton.StoreListButton).gameObject, OnStoreListButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_BuildingButton.WeaponStoreButton).gameObject, OnWeaponStoreBuildingButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_BuildingButton.ArmorStoreButton).gameObject, OnArmorStoreBuildingButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_BuildingButton.CloseButton).gameObject, OnCloseButtonClick, Define.en_UIEvent.MouseClick);

        ShowCloseUI(true);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    void OnGovermentOfficeListButtonClick(PointerEventData Event)
    {
        GetButton((int)en_BuildingButton.MainHallButton).gameObject.SetActive(true);
        GetButton((int)en_BuildingButton.WallButton).gameObject.SetActive(true);

        GetButton((int)en_BuildingButton.WeaponStoreButton).gameObject.SetActive(false);
        GetButton((int)en_BuildingButton.ArmorStoreButton).gameObject.SetActive(false);
    }

    void OnMainHallBuildingButtonClick(PointerEventData Event)
    {
        if (MainHallBuildingCount == 0)
        {
            return;
        }

        GetButton((int)en_BuildingButton.GovernmentOfficeListButton);

        MainHallBuildingCount--;

        if (MainHallBuildingCount < 0)
        {
            MainHallBuildingCount = 0;
        }
    }

    void OnWallBuildingButtonClick(PointerEventData Event)
    {
        if(WallBuildingCount == 0)
        {
            return;
        }
    }

    void OnStoreListButtonClick(PointerEventData Event)
    {
        GetButton((int)en_BuildingButton.WeaponStoreButton).gameObject.SetActive(true);
        GetButton((int)en_BuildingButton.ArmorStoreButton).gameObject.SetActive(true);

        GetButton((int)en_BuildingButton.MainHallButton).gameObject.SetActive(false);
        GetButton((int)en_BuildingButton.WallButton).gameObject.SetActive(false);        
    }

    void OnWeaponStoreBuildingButtonClick(PointerEventData Event)
    {
        if(WeaponBuildingCount == 0)
        {
            return;
        }
    }

    void OnArmorStoreBuildingButtonClick(PointerEventData Event)
    {
        if(ArmorBuildingCount == 0)
        {
            return;
        }
    }

    void OnCloseButtonClick(PointerEventData Event)
    {
        ShowCloseUI(false);
    }
}
