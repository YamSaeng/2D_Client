using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager
{
    public Dictionary<en_SmallItemCategory, Sprite> _ItemSprite = new Dictionary<en_SmallItemCategory, Sprite>();
    public Dictionary<en_SkillType,Sprite> _SkillSprite = new Dictionary<en_SkillType, Sprite>();
    public Dictionary<byte, Sprite> _PotatoGrowthSprite = new Dictionary<byte, Sprite>();
    public Dictionary<byte, Sprite> _CornGrowthSprite = new Dictionary<byte, Sprite>();    

    public void Init()
    {
        LoadItemSprite();
        LoadSkillSprite();
    }

    private void LoadItemSprite()
    {
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_WEAPON_SWORD_WOOD,
            Managers.Resource.Load<Sprite>("Sprites/Weapon & Tool/Wooden Sword"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SAMLL_CATEGORY_WEAPON_WOOD_SHIELD,
            Managers.Resource.Load<Sprite>("Sprites/Weapon & Tool/Wooden Shield"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_WEAR_LEATHER,
            Managers.Resource.Load<Sprite>("Sprites/Equipment/Leather Armor"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_HAT_LEATHER,
            Managers.Resource.Load<Sprite>("Sprites/Equipment/Leather Helmet"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_ARMOR_BOOT_LEATHER,
            Managers.Resource.Load<Sprite>("Sprites/Equipment/Leather Boot"));        
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_POTION_HEALTH_RESTORATION_POTION_SMALL,
            Managers.Resource.Load<Sprite>("Sprites/Potion/Red Potion"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_POTION_MANA_RESTORATION_POTION_SMALL,
            Managers.Resource.Load<Sprite>("Sprites/Potion/Blue Potion"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_LEATHER,
            Managers.Resource.Load<Sprite>("Sprites/Material/Leather"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_SLIMEGEL,
            Managers.Resource.Load<Sprite>("Sprites/MonsterPart/SlimeGel"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_BRONZE_COIN,
            Managers.Resource.Load<Sprite>("Sprites/Misc/CopperCoin"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_SLIVER_COIN,
            Managers.Resource.Load<Sprite>("Sprites/Misc/Silver Coin"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_GOLD_COIN,
            Managers.Resource.Load<Sprite>("Sprites/Misc/Golden Coin"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_STONE,
            Managers.Resource.Load<Sprite>("Sprites/Material/Stone"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_WOOD_LOG,
            Managers.Resource.Load<Sprite>("Sprites/Material/Wood Log"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_WOOD_FLANK,
            Managers.Resource.Load<Sprite>("Sprites/Material/Wooden Plank"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_YARN,
            Managers.Resource.Load<Sprite>("Sprites/Material/Yarn"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_CHAR_COAL,
            Managers.Resource.Load<Sprite>("Sprites/Ore & Gem/CharCoal"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_COPPER_NUGGET,
            Managers.Resource.Load<Sprite>("Sprites/Ore & Gem/Copper Nugget"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_COPPER_INGOT,
            Managers.Resource.Load<Sprite>("Sprites/Ore & Gem/Copper Ingot"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_IRON_NUGGET,
            Managers.Resource.Load<Sprite>("Sprites/Ore & Gem/Iron Nugget"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_MATERIAL_IRON_INGOT,
            Managers.Resource.Load<Sprite>("Sprites/Ore & Gem/Iron Ingot"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_SEED_POTATO,
            Managers.Resource.Load<Sprite>("Sprites/Plant/Seed/PotatoSeed"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_SEED_CORN,
            Managers.Resource.Load<Sprite>("Sprites/Plant/Seed/CornSeed"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_CRAFTING_TABLE_FURANCE,
            Managers.Resource.Load<Sprite>("Sprites/CraftingTable/Furnace"));
        _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_CRAFTING_TABLE_SAWMILL,
            Managers.Resource.Load<Sprite>("Sprites/CraftingTable/Sawmill"));

        Sprite[] FruitSprites = Managers.Resource.LoadAll<Sprite>("Sprites/Plant/Fruit");
        foreach (Sprite FruitSprite in FruitSprites)
        {
            switch (FruitSprite.name)
            {
                case "Potato_Fruit":
                    _ItemSprite.Add(en_SmallItemCategory.ITEM_SMALL_CATEGORY_CROP_FRUIT_POTATO, FruitSprite);
                    break;
            }
        }

        Sprite[] FruitGrownSprites = Managers.Resource.LoadAll<Sprite>("Sprites/Plant/FruitGrown");
        foreach(Sprite FruitGrownSprite in FruitGrownSprites)
        {
            switch(FruitGrownSprite.name)
            {
                case "FruitGrown_Potato_01":
                    _PotatoGrowthSprite.Add(0, FruitGrownSprite);
                    break;
                case "FruitGrown_Potato_02":
                    _PotatoGrowthSprite.Add(1, FruitGrownSprite);
                    break;
                case "FruitGrown_Potato_03":
                    _PotatoGrowthSprite.Add(2, FruitGrownSprite);
                    break;
                case "FruitGrown_Potato_04":
                    _PotatoGrowthSprite.Add(3, FruitGrownSprite);
                    break;
                case "FruitGrown_Potato_05":
                    _PotatoGrowthSprite.Add(4, FruitGrownSprite);
                    break;
                case "FruitGrown_Potato_06":
                    _PotatoGrowthSprite.Add(5, FruitGrownSprite);
                    break;
                case "FruitGrown_Potato_07":
                    _PotatoGrowthSprite.Add(6, FruitGrownSprite);
                    break;
                case "FruitGrown_Corn_01":
                    _CornGrowthSprite.Add(0, FruitGrownSprite);
                    break;
                case "FruitGrown_Corn_02":
                    _CornGrowthSprite.Add(1, FruitGrownSprite);
                    break;
                case "FruitGrown_Corn_03":
                    _CornGrowthSprite.Add(2, FruitGrownSprite);
                    break;
                case "FruitGrown_Corn_04":
                    _CornGrowthSprite.Add(3, FruitGrownSprite);
                    break;
                case "FruitGrown_Corn_05":
                    _CornGrowthSprite.Add(4, FruitGrownSprite);
                    break;
                case "FruitGrown_Corn_06":
                    _CornGrowthSprite.Add(5, FruitGrownSprite);
                    break;
                case "FruitGrown_Corn_07":
                    _CornGrowthSprite.Add(6, FruitGrownSprite);
                    break;
                case "FruitGrown_Corn_08":
                    _CornGrowthSprite.Add(7, FruitGrownSprite);
                    break;
            }
        }
    }

    private void LoadSkillSprite()
    {
        // ���� ��� Sprite
        _SkillSprite.Add(en_SkillType.SKILL_DEFAULT_ATTACK,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/�Ϲݰ���"));
        _SkillSprite.Add(en_SkillType.SKILL_PUBLIC_ACTIVE_BUF_SHOCK_RELEASE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/�������"));

        // ���� ��� Sprite
        _SkillSprite.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/�ͷ��� �ϰ�"));
        _SkillSprite.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/ȸ���� �ϰ�"));        
        _SkillSprite.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_JUMPING_ATTACK,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/���� ����"));
        _SkillSprite.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_PIERCING_WAVE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/��� �ĵ�"));
        _SkillSprite.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_FLY_KNIFE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/Į�� ������"));
        _SkillSprite.Add(en_SkillType.SKILL_FIGHT_ACTIVE_ATTACK_COMBO_FLY_KNIFE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/Į�� ���� ������"));        
        _SkillSprite.Add(en_SkillType.SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/���� �ڼ�"));

        // ��� ��� Sprite
        _SkillSprite.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/���/���� ��Ÿ"));
        _SkillSprite.Add(en_SkillType.SKILL_PROTECTION_ACTIVE_ATTACK_CAPTURE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/���/��ȹ"));

        // ���� ��� Sprite
        _SkillSprite.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_HARPOON,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/�Ҳ� �ۻ�"));
        _SkillSprite.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ROOT,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/�ӹ�"));
        _SkillSprite.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/���� �罽"));
        _SkillSprite.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/�ñ� �ĵ�"));
        _SkillSprite.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/����"));
        _SkillSprite.Add(en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/����"));
        _SkillSprite.Add(en_SkillType.SKILL_SPELL_ACTIVE_BUF_TELEPORT,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/�ð��� ��Ʋ��"));

        // ���� ��� Sprite
        _SkillSprite.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/�ż��� �ϰ�"));
        _SkillSprite.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/ġ���� ��"));
        _SkillSprite.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/ġ���� �ٶ�"));
        _SkillSprite.Add(en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_ROOT,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/����/�ӹ�"));
        
        //// �ϻ� ��� Sprite
        _SkillSprite.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_QUICK_CUT,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/�ϻ�/���� ����"));
        _SkillSprite.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_FAST_CUT,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/�ϻ�/�ż� ����"));
        _SkillSprite.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_ATTACK,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/�ϻ�/���"));
        _SkillSprite.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_STEP,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/�ϻ�/�Ͻ�"));
        _SkillSprite.Add(en_SkillType.SKILL_ASSASSINATION_ACTIVE_BUF_WEAPON_POISON,
            Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/�ϻ�/�� �ٸ���"));        

        //// �û� ��� Sprite
        //_SkillSprite.Add(en_SkillType.SKILL_ARCHER_SNIFING,
        //    Managers.Resource.Load<Sprite>("Sprites/Spell/Icon/�û�/�Ϲݰ���"));
    }
}