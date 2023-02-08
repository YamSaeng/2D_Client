using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemExplanation : UI_Base
{
    st_ItemInfo _ItemInfo;

    enum en_ItemMaterialExplainImage
    {
        ItemExplainImage
    }

    enum en_ItemMaterialExplainText
    {
        ItemName,
        ItemCategoryText,
        ItemExplainText
    }

    public override void Init()
    {
        
    }

    public override void Binding()
    {
        Bind<Image>(typeof(en_ItemMaterialExplainImage));
        Bind<TextMeshProUGUI>(typeof(en_ItemMaterialExplainText));

        gameObject.SetActive(false);
    }

    public void ItemExplanationSet(st_ItemInfo ItemInfo)
    {
        _ItemInfo = ItemInfo;

        Sprite ItemImage = Managers.Sprite._ItemSprite[_ItemInfo.ItemSmallCategory];

        GetImage((int)en_ItemMaterialExplainImage.ItemExplainImage).sprite = ItemImage;
        GetTextMeshPro((int)en_ItemMaterialExplainText.ItemName).text = _ItemInfo.ItemName;

        switch (_ItemInfo.ItemSmallCategory)
        {
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_SWORD_WOOD:
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "검";
                break;
            case en_SmallItemCategory.ITEM_SAMLL_CATEGORY_WEAPON_WOOD_SHIELD:
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "방패";
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_WEAR_LEATHER:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_HAT_LEATHER:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_BOOT_LEATHER:
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "방어구";
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_TOOL_FARMING_SHOVEL:
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "농사 도구";
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_POTION_HEALTH_RESTORATION_POTION_SMALL:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_POTION_MANA_RESTORATION_POTION_SMALL:
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "물약";
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_KNIGHT_FIERCE_ATTACK:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_KNIGHT_CONVERSION_ATTACK:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_KNIGHT_SHAEHONE_ATTACK:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_KNIGHT_CHOHONE_ATTACK:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_KNIGHT_SMASH_WAVE_ATTACK:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_KNIGHT_CHARGE_POSE:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_SHAMAN_FLAME_HARPOON:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_SHAMAN_HELL_FIRE:
            case en_SmallItemCategory.ITEM_SMALL_CATEOGRY_SKILLBOOK_TAIOIST_HEALING_LIGHT:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_TAIOIST_HEALING_WIND:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_SKILLBOOK_SHOCK_RELEASE:
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "기술 책";
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_LEATHER:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_SLIMEGEL:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_BRONZE_COIN:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_SLIVER_COIN:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_GOLD_COIN:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_STONE:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_WOOD_LOG:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_WOOD_FLANK:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_YARN:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_COPPER_NUGGET:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_COPPER_INGOT:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_IRON_NUGGET:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_IRON_INGOT:
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "재료";
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_SEED_POTATO:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_SEED_CORN:            
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "씨앗";
                break;
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_FRUIT_POTATO:
            case en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_FRUIT_CORN:
                GetTextMeshPro((int)en_ItemMaterialExplainText.ItemCategoryText).text = "농작물";
                break;
        }

        GetTextMeshPro((int)en_ItemMaterialExplainText.ItemExplainText).text = _ItemInfo.ItemExplain;
    }


    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }
}
