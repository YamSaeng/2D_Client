using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipmentBox : UI_Base
{
    enum en_EquipmentBoxGameObjects
    {
        UI_EquipmentBox,
        UI_HeadEquipmentItem,
        UI_BodyEquipmentItem,
        UI_BootEquipmentItem,
        UI_LeftHandEquipmentItem,
        UI_RightHandEquipmentItem
    }

    enum en_EquipmentBoxTexts
    {
        UserNameText,
        UserLevelText,
        UserJobText,
        HealthPointText,
        ManaPointText,
        MeleeAttackPointText,
        MeleeAttackHitRatePointText,
        MeleeCriticalPointText,
        MagicDamagePointText,
        MagicHitRatePointText,
        MagicCriticalPointText,
        DefencePointText,
        EvasionRatePointText
    }

    public int _WeaponMinDamage = 0;
    public int _WeaponMaxDamage = 0;
    public int _HeadArmorDefence = 0;
    public int _WearArmorDefence = 0;
    public int _BootArmorDefence = 0;

    UI_EquipmentItem[] _EquipmentParts;

    public CreatureObject _OwnerObject;

    public override void Init()
    {

    }    

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_EquipmentBoxGameObjects));
        Bind<TextMeshProUGUI>(typeof(en_EquipmentBoxTexts));

        BindEvent(GetGameObject((int)en_EquipmentBoxGameObjects.UI_EquipmentBox).gameObject, OnEquipmentBoxDrag, Define.en_UIEvent.Drag);

        GetGameObject((int)en_EquipmentBoxGameObjects.UI_HeadEquipmentItem).GetComponent<UI_EquipmentItem>().Binding();
        GetGameObject((int)en_EquipmentBoxGameObjects.UI_BodyEquipmentItem).GetComponent<UI_EquipmentItem>().Binding();        
        GetGameObject((int)en_EquipmentBoxGameObjects.UI_LeftHandEquipmentItem).GetComponent<UI_EquipmentItem>().Binding();
        GetGameObject((int)en_EquipmentBoxGameObjects.UI_RightHandEquipmentItem).GetComponent<UI_EquipmentItem>().Binding();
        GetGameObject((int)en_EquipmentBoxGameObjects.UI_BootEquipmentItem).GetComponent<UI_EquipmentItem>().Binding();
    }

    public void EquipmentBoxUICreate(byte EquipmentPartCount)
    {
        _EquipmentParts = new UI_EquipmentItem[EquipmentPartCount];

        _EquipmentParts[(int)en_EquipmentParts.EQUIPMENT_PARTS_HEAD] = GetGameObject((int)en_EquipmentBoxGameObjects.UI_HeadEquipmentItem).GetComponent<UI_EquipmentItem>();
        _EquipmentParts[(int)en_EquipmentParts.EQUIPMENT_PARTS_BODY] = GetGameObject((int)en_EquipmentBoxGameObjects.UI_BodyEquipmentItem).GetComponent<UI_EquipmentItem>();
        _EquipmentParts[(int)en_EquipmentParts.EQUIPMENT_PARTS_LEFT_HAND] = GetGameObject((int)en_EquipmentBoxGameObjects.UI_LeftHandEquipmentItem).GetComponent<UI_EquipmentItem>();
        _EquipmentParts[(int)en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND] = GetGameObject((int)en_EquipmentBoxGameObjects.UI_RightHandEquipmentItem).GetComponent<UI_EquipmentItem>();
        _EquipmentParts[(int)en_EquipmentParts.EQUIPMENT_PARTS_BOOT] = GetGameObject((int)en_EquipmentBoxGameObjects.UI_BootEquipmentItem).GetComponent<UI_EquipmentItem>();
    }

    private void OnEquipmentBoxDrag(PointerEventData Event)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Event.delta;
    }

    public UI_EquipmentItem GetEquipment(en_EquipmentParts EquipmentPart)
    {
        return _EquipmentParts[(int)en_EquipmentParts.EQUIPMENT_PARTS_HEAD];
    }

    public void OnEquipmentItem(st_ItemInfo OnEquipmentItemInfo)
    {
        _EquipmentParts[(int)OnEquipmentItemInfo.ItemEquipmentPart].SetEquipment(OnEquipmentItemInfo);        

        switch(OnEquipmentItemInfo.ItemLargeCategory)
        {
            case en_LargeItemCategory.ITEM_LARGE_CATEGORY_WEAPON:
                PlayerWeapon RightWeaponParent = _OwnerObject.transform.Find("RightWeaponParent").GetComponent<PlayerWeapon>();
                if(RightWeaponParent != null)
                {                                     
                    switch (OnEquipmentItemInfo.ItemSmallCategory)
                    {
                        case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_DAGGER_WOOD:
                            Managers.Resource.Instantiate(en_ResourceName.CLIENT_WEAPON_DAGGER_WOOD, RightWeaponParent.transform);
                            break;
                        case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_LONG_SWORD_WOOD:
                            Managers.Resource.Instantiate(en_ResourceName.CLIENT_WEAPON_LONG_SWORD_WOOD, RightWeaponParent.transform);
                            break;
                    }

                    RightWeaponParent.OnWeapon();
                }             
                break;
        }        

        EquipmentBoxRefreshUI();
    }

    public void OffEquipmentItem(en_EquipmentParts OffEquipmentParts)
    {
        switch (_EquipmentParts[(int)OffEquipmentParts].GetEquipmentItemInfo().ItemLargeCategory)
        {
            case en_LargeItemCategory.ITEM_LARGE_CATEGORY_WEAPON:
                PlayerWeapon RightWeapon = _OwnerObject.transform.Find("RightWeaponParent").GetComponent<PlayerWeapon>();
                if (RightWeapon != null)
                {                    
                    switch(_EquipmentParts[(int)OffEquipmentParts].GetEquipmentItemInfo().ItemSmallCategory)
                    {
                        case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_LONG_SWORD_WOOD:                            
                            Destroy(RightWeapon.transform.Find("WeaponLongSwordWood").gameObject);
                            break;
                    }

                    RightWeapon.OffWeapon();                    
                }

                break;
        }

        _EquipmentParts[(int)OffEquipmentParts].InitEquipmentItemUI();        

        EquipmentBoxRefreshUI();
    }
   
    public void EquipmentBoxRefreshUI()
    {
        GameObject FindPlayerGO = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
        if (FindPlayerGO != null)
        {
            PlayerObject Player = FindPlayerGO.GetComponent<PlayerObject>();

            GetTextMeshPro((int)en_EquipmentBoxTexts.UserNameText).text = Player._GameObjectInfo.ObjectName;
            GetTextMeshPro((int)en_EquipmentBoxTexts.HealthPointText).text = $"레벨 {Player._GameObjectInfo.ObjectStatInfo.Level}";            

            GetTextMeshPro((int)en_EquipmentBoxTexts.HealthPointText).text = $"체력 {Player._GameObjectInfo.ObjectStatInfo.HP} / {Player._GameObjectInfo.ObjectStatInfo.MaxHP}";
            GetTextMeshPro((int)en_EquipmentBoxTexts.ManaPointText).text = $"마력 {Player._GameObjectInfo.ObjectStatInfo.MP} / {Player._GameObjectInfo.ObjectStatInfo.MaxMP}";

            GetTextMeshPro((int)en_EquipmentBoxTexts.MeleeAttackPointText).text = $"근접 공격력 {_WeaponMinDamage + Player._GameObjectInfo.ObjectStatInfo.MinMeleeAttackDamage} ~ {_WeaponMaxDamage + Player._GameObjectInfo.ObjectStatInfo.MaxMeleeAttackDamage}";
            GetTextMeshPro((int)en_EquipmentBoxTexts.MeleeAttackHitRatePointText).text = $"근접 명중률 {Player._GameObjectInfo.ObjectStatInfo.MeleeAttackHitRate}";
            GetTextMeshPro((int)en_EquipmentBoxTexts.MeleeCriticalPointText).text = $"근접 치명타 {Player._GameObjectInfo.ObjectStatInfo.MeleeCriticalPoint}";
            GetTextMeshPro((int)en_EquipmentBoxTexts.MagicDamagePointText).text = $"마법 공격력 {Player._GameObjectInfo.ObjectStatInfo.MagicDamage}";
            GetTextMeshPro((int)en_EquipmentBoxTexts.MagicHitRatePointText).text = $"마법 명중률 {Player._GameObjectInfo.ObjectStatInfo.MagicHitRate}";
            GetTextMeshPro((int)en_EquipmentBoxTexts.MagicCriticalPointText).text = $"마법 치명타 {Player._GameObjectInfo.ObjectStatInfo.MagicCriticalPoint}";
            GetTextMeshPro((int)en_EquipmentBoxTexts.DefencePointText).text = $"방어력 {_HeadArmorDefence + _WearArmorDefence + _BootArmorDefence + Player._GameObjectInfo.ObjectStatInfo.Defence}";
            GetTextMeshPro((int)en_EquipmentBoxTexts.EvasionRatePointText).text = $"회피율 {Player._GameObjectInfo.ObjectStatInfo.EvasionRate}";
        }
    }     
}
