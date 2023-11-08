using System;
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
    public byte WallBuildingCount;
    [HideInInspector]
    public byte WeaponBuildingCount;
    [HideInInspector]
    public byte ArmorBuildingCount;

    private UI_BuildingItem _BuildingMainHall;
    private UI_BuildingItem _BuildingWeaponStore;
    private UI_BuildingItem _BuildingArmorStore;
    [HideInInspector]
    public UI_BuildingItem _DummyBuilding;

    enum en_BuildingButton
    {
        GovernmentOfficeListButton,
        StoreListButton,
        CloseButton
    }

    enum en_BuildingGameObject
    {
        BuildingGovermentOffice,
        BuildingWeaponStore,
        BuildingArmorStore,
        DummyBuilding
    }

    enum en_BuildingText
    {
        MainHallCountText,
        WeaponStoreCountText,
        ArmorStoreCountText
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
        Bind<GameObject>(typeof(en_BuildingGameObject));
        Bind<TextMeshProUGUI>(typeof(en_BuildingText));

        BindEvent(GetButton((int)en_BuildingButton.GovernmentOfficeListButton).gameObject, OnGovermentOfficeListButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_BuildingButton.StoreListButton).gameObject, OnStoreListButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_BuildingButton.CloseButton).gameObject, OnCloseButtonClick, Define.en_UIEvent.MouseClick);

        BindEvent(GetGameObject((int)en_BuildingGameObject.BuildingGovermentOffice).gameObject, OnMainHallBuildingButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetGameObject((int)en_BuildingGameObject.BuildingWeaponStore).gameObject, OnWeaponStoreBuildingButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetGameObject((int)en_BuildingGameObject.BuildingArmorStore).gameObject, OnArmorStoreBuildingButtonClick, Define.en_UIEvent.MouseClick);

        _BuildingMainHall = GetGameObject((int)en_BuildingGameObject.BuildingGovermentOffice)?.GetComponent<UI_BuildingItem>();
        if (_BuildingMainHall != null)
        {
            _BuildingMainHall.Binding();

            st_BuildingInfo MainHallInfo = new st_BuildingInfo();
            MainHallInfo.IsImageChange = false;
            MainHallInfo.BuildingHeight = 4;
            MainHallInfo.BuildingWidth = 4;
            MainHallInfo.BuildinSmallCategory = en_BuildingSmallCategory.BUILDING_SMALL_CATEGORY_MAIN_HALL;

            _BuildingMainHall.SetBuildingInfo(MainHallInfo);
            _BuildingMainHall.ShowCloseUI(false);
        }

        _BuildingWeaponStore = GetGameObject((int)en_BuildingGameObject.BuildingWeaponStore)?.GetComponent<UI_BuildingItem>();
        if (_BuildingWeaponStore != null)
        {
            _BuildingWeaponStore.Binding();

            st_BuildingInfo WeaponStoreInfo = new st_BuildingInfo();
            WeaponStoreInfo.IsImageChange = false;
            WeaponStoreInfo.BuildingHeight = 2;
            WeaponStoreInfo.BuildingWidth = 2;
            WeaponStoreInfo.BuildinSmallCategory = en_BuildingSmallCategory.BUILDING_SMALL_CATEGORY_WEAPON_STORE;

            _BuildingWeaponStore.SetBuildingInfo(WeaponStoreInfo);
            _BuildingWeaponStore.ShowCloseUI(false);
        }

        _BuildingArmorStore = GetGameObject((int)en_BuildingGameObject.BuildingArmorStore)?.GetComponent<UI_BuildingItem>();
        if (_BuildingArmorStore != null)
        {
            _BuildingArmorStore.Binding();

            st_BuildingInfo ArmorStoreInfo = new st_BuildingInfo();
            ArmorStoreInfo.IsImageChange = false;
            ArmorStoreInfo.BuildingHeight = 2;
            ArmorStoreInfo.BuildingWidth = 2;
            ArmorStoreInfo.BuildinSmallCategory = en_BuildingSmallCategory.BUILDING_SMALL_CATEGORY_ARMOR_STORE;

            _BuildingArmorStore.SetBuildingInfo(ArmorStoreInfo);
            _BuildingArmorStore.ShowCloseUI(false);
        }

        _DummyBuilding = GetGameObject((int)en_BuildingGameObject.DummyBuilding)?.GetComponent<UI_BuildingItem>();
        if (_DummyBuilding != null)
        {
            _DummyBuilding.Binding();
        }

        GetGameObject((int)en_BuildingGameObject.BuildingGovermentOffice)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(false);
        GetGameObject((int)en_BuildingGameObject.BuildingWeaponStore)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(false);
        GetGameObject((int)en_BuildingGameObject.BuildingArmorStore)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(false);

        ShowCloseUI(false);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    void OnGovermentOfficeListButtonClick(PointerEventData Event)
    {
        Get<TextMeshProUGUI>((int)en_BuildingText.MainHallCountText).text = MainHallBuildingCount.ToString();
        GetGameObject((int)en_BuildingGameObject.BuildingGovermentOffice)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(true);

        GetGameObject((int)en_BuildingGameObject.BuildingWeaponStore)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(false);
        GetGameObject((int)en_BuildingGameObject.BuildingArmorStore)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(false);
    }

    void OnMainHallBuildingButtonClick(PointerEventData Event)
    {
        if (MainHallBuildingCount == 0)
        {
            Managers.GameMessage._GlobalMessageBox.NewStatusAbnormalMessage(en_GlobalMessageType.PERSONAL_MESSAGE_BUILDING_FAIL, "해당 건물을 더 이상 지을 수 없습니다");
            return;
        }

        UI_BuildingItem MainHallBuilding = GetGameObject((int)en_BuildingGameObject.BuildingGovermentOffice)?.GetComponent<UI_BuildingItem>();
        if (MainHallBuilding != null)
        {
            UI_BuildingItem DummyBuilding = GetGameObject((int)en_BuildingGameObject.DummyBuilding)?.GetComponent<UI_BuildingItem>();
            if (DummyBuilding != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    st_BuildingInfo DummyBuildingInfo = MainHallBuilding.GetBuildingInfo();
                    DummyBuildingInfo.IsImageChange = true;

                    DummyBuilding.SetBuildingInfo(DummyBuildingInfo);

                    Managers.Mouse._ClickObject.ClickBuildingItem(DummyBuilding);

                    GameSceneUI.AddGameSceneUIStack(_DummyBuilding);
                }
            }
        }
    }

    void OnStoreListButtonClick(PointerEventData Event)
    {
        Get<TextMeshProUGUI>((int)en_BuildingText.WeaponStoreCountText).text = WeaponBuildingCount.ToString();
        Get<TextMeshProUGUI>((int)en_BuildingText.ArmorStoreCountText).text = ArmorBuildingCount.ToString();

        GetGameObject((int)en_BuildingGameObject.BuildingGovermentOffice)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(false);

        GetGameObject((int)en_BuildingGameObject.BuildingWeaponStore)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(true);
        GetGameObject((int)en_BuildingGameObject.BuildingArmorStore)?.GetComponent<UI_BuildingItem>()?.ShowCloseUI(true);
    }

    void OnWeaponStoreBuildingButtonClick(PointerEventData Event)
    {
        if (WeaponBuildingCount == 0)
        {
            Managers.GameMessage._GlobalMessageBox.NewStatusAbnormalMessage(en_GlobalMessageType.PERSONAL_MESSAGE_BUILDING_FAIL, "해당 건물을 더 이상 지을 수 없습니다");
            return;
        }

        UI_BuildingItem WeaponStoreBuilding = GetGameObject((int)en_BuildingGameObject.BuildingWeaponStore)?.GetComponent<UI_BuildingItem>();
        if (WeaponStoreBuilding != null)
        {
            UI_BuildingItem DummyBuilding = GetGameObject((int)en_BuildingGameObject.DummyBuilding)?.GetComponent<UI_BuildingItem>();
            if (DummyBuilding != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    st_BuildingInfo DummyBuildingInfo = WeaponStoreBuilding.GetBuildingInfo();
                    DummyBuildingInfo.IsImageChange = true;

                    DummyBuilding.SetBuildingInfo(DummyBuildingInfo);

                    Managers.Mouse._ClickObject.ClickBuildingItem(DummyBuilding);

                    GameSceneUI.AddGameSceneUIStack(_DummyBuilding);
                }
            }
        }      
    }

    void OnArmorStoreBuildingButtonClick(PointerEventData Event)
    {
        if (ArmorBuildingCount == 0)
        {
            Managers.GameMessage._GlobalMessageBox.NewStatusAbnormalMessage(en_GlobalMessageType.PERSONAL_MESSAGE_BUILDING_FAIL, "해당 건물을 더 이상 지을 수 없습니다");
            return;
        }

        UI_BuildingItem ArmorStoreBuilding = GetGameObject((int)en_BuildingGameObject.BuildingArmorStore)?.GetComponent<UI_BuildingItem>();
        if (ArmorStoreBuilding != null)
        {
            UI_BuildingItem DummyBuilding = GetGameObject((int)en_BuildingGameObject.DummyBuilding)?.GetComponent<UI_BuildingItem>();
            if (DummyBuilding != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    st_BuildingInfo DummyBuildingInfo = ArmorStoreBuilding.GetBuildingInfo();
                    DummyBuildingInfo.IsImageChange = true;

                    DummyBuilding.SetBuildingInfo(DummyBuildingInfo);

                    Managers.Mouse._ClickObject.ClickBuildingItem(DummyBuilding);

                    GameSceneUI.AddGameSceneUIStack(_DummyBuilding);
                }
            }
        }
    }

    void OnCloseButtonClick(PointerEventData Event)
    {
        ShowCloseUI(false);
    }

    public void BuildingCountTextRefresh()
    {
        Get<TextMeshProUGUI>((int)en_BuildingText.MainHallCountText).text = MainHallBuildingCount.ToString();

        Get<TextMeshProUGUI>((int)en_BuildingText.WeaponStoreCountText).text = WeaponBuildingCount.ToString();
        Get<TextMeshProUGUI>((int)en_BuildingText.ArmorStoreCountText).text = ArmorBuildingCount.ToString();
    }

    public void BuildingInstallation(st_BuildingInfo BuildingInfo, Vector2Int MousePosition)
    {
        if (_DummyBuilding != null)
        {
            _DummyBuilding.ShowCloseUI(false);

            GameObject BuildingGO = null;
            
            switch (BuildingInfo.BuildinSmallCategory)
            {
                case en_BuildingSmallCategory.BUILDING_SMALL_CATEGORY_MAIN_HALL:
                    MainHallBuildingCount--;

                    BuildingGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_BUILDING_GOVERMENT_OFFICE);                    
                    break;
                case en_BuildingSmallCategory.BUILDING_SMALL_CATEGORY_WEAPON_STORE:
                    WeaponBuildingCount--;

                    BuildingGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_BUILDING_WEAPON_STORE);
                    break;
                case en_BuildingSmallCategory.BUILDING_SMALL_CATEGORY_ARMOR_STORE:
                    ArmorBuildingCount--;

                    BuildingGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_BUILDING_ARMOR_STORE);
                    break;
                case en_BuildingSmallCategory.BUILDING_SMALL_CATEGORY_WALL:
                    WallBuildingCount--;

                    BuildingGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_BUILDING_WALL);
                    break;
            }

            if(BuildingGO != null)
            {
                BuildingGO.transform.position = new Vector3(MousePosition.x, MousePosition.y, 0);
            }
        }

        if (MainHallBuildingCount < 0)
        {
            MainHallBuildingCount = 0;
        }

        if (WeaponBuildingCount < 0)
        {
            WallBuildingCount = 0;
        }

        if (ArmorBuildingCount < 0)
        {
            ArmorBuildingCount = 0;
        }

        if (WallBuildingCount < 0)
        {
            WallBuildingCount= 0;
        }                       

        BuildingCountTextRefresh();
    }
}
