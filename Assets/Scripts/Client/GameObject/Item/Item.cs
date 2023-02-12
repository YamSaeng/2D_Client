using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItem
{
    // ������ �⺻ ����
    public st_ItemInfo _ItemInfo { get; set; } = new st_ItemInfo();

    public long _ItemDbId
    {
        get { return _ItemInfo.ItemDBId; }
        set { _ItemInfo.ItemDBId = value; }
    }

    // ������ ����
    public short _Count
    {
        get { return _ItemInfo.ItemCount; }
        set { _ItemInfo.ItemCount = value; }
    }

    // ������ ���� ����
    public bool _IsEquipped
    {
        get { return _ItemInfo.IsEquipped; }
        set { _ItemInfo.IsEquipped = value; }
    }

    // �������� ������ �������� ����
    public bool _Stackable { get; protected set; }

    public CItem()
    {

    }

    public CItem(st_ItemInfo ItemInfo)
    {
        _ItemInfo = ItemInfo;
    }

    // �ܺο��� ������ �����ϰ��� �Ҷ� ȣ�� �� �Լ�
    public static CItem MakeItem(st_ItemInfo ItemInfo)
    {
        CItem Item = null;

        // ������ ����
        switch (ItemInfo.ItemSmallCategory)
        {
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_SWORD_WOOD:
                CWeapon Weapon = new CWeapon(ItemInfo);
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_HAT_LEATHER:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_WEAR_LEATHER:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_BOOT_LEATHER:
                Item = new CArmor(ItemInfo);
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_SLIMEGEL:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_BRONZE_COIN:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_LEATHER:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_WOOD_LOG:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_STONE:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_WOOD_FLANK:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_YARN:
                Item = new CMaterial(ItemInfo);
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_KNIGHT_CHOHONE_ATTACK:
                Item = new CConsumable(ItemInfo);
                break;
        }

        return Item;
    }
}

public class CWeapon : CItem
{
    public CWeapon()
    {

    }

    public CWeapon(st_ItemInfo ItemInfo) : base(ItemInfo)
    {

    }
}

public class CArmor : CItem
{
    public CArmor()
    {

    }

    public CArmor(st_ItemInfo ItemInfo) : base(ItemInfo)
    {

    }
}

public class CMaterial : CItem
{
    public CMaterial()
    {

    }

    public CMaterial(st_ItemInfo ItemInfo) : base(ItemInfo)
    {

    }
}

public class CConsumable : CItem
{
    public CConsumable()
    {

    }

    public CConsumable(st_ItemInfo ItemInfo) : base(ItemInfo)
    {

    }
}

public class CArchitectureItem : CItem
{
    public CArchitectureItem()
    {

    }

    public CArchitectureItem(st_ItemInfo ItemInfo) : base(ItemInfo)
    {

    }
}

public class CCropItem : CItem
{
    public CCropItem()
    {

    }

    public CCropItem(st_ItemInfo ItemInfo) : base(ItemInfo)
    {

    }
}
