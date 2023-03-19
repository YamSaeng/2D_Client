using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum en_GameObjectType
{
    OBJECT_NON_TYPE,

    OBJECT_PLAYER,
    OBJECT_NON_PLAYER,

    OBJECT_MONSTER,
    OBJECT_GOBLIN,

    OBJECT_ENVIRONMENT,
    OBJECT_STONE,
    OBJECT_TREE,

    OBJECT_WALL,

    OBJECT_ARCHITECTURE,
    OBJECT_ARCHITECTURE_CRAFTING_TABLE,
    OBJECT_ARCHITECTURE_CRAFTING_DEFAULT_CRAFTING_TABLE,
    OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE,
    OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL,

    OBJECT_CROP,
    OBJECT_CROP_POTATO,
    OBJECT_CROP_CORN,

    OBJECT_STORAGE,
    OBJECT_STORAGE_BOX,

    OBJECT_ITEM,
    OBJECT_ITEM_WEAPON,
    OBJECT_ITEM_WEAPON_WOOD_SWORD,
    OBJECT_ITEM_WEAPON_WOOD_SHIELD,

    OBJECT_ITEM_TOOL,
    OBJECT_ITEM_TOOL_FARMING_SHOVEL,

    OBJECT_ITEM_ARMOR,
    OBJECT_ITEM_ARMOR_LEATHER_ARMOR,

    OBJECT_ITEM_ARMOR_LEATHER_HELMET,
    OBJECT_ITEM_ARMOR_LEATHER_BOOT,

    OBJECT_ITEM_CONSUMABLE,
    OBJECT_ITEM_CONSUMABLE_HEALTH_RESTORATION_POTION_SMALL,
    OBJECT_ITEM_CONSUMABLE_MANA_RESTORATION_POTION_SMALL,

    OBJECT_ITEM_MATERIAL,
    OBJECT_ITEM_MATERIAL_LEATHER,
    OBJECT_ITEM_MATERIAL_BRONZE_COIN,
    OBJECT_ITEM_MATERIAL_SLIVER_COIN,
    OBJECT_ITEM_MATERIAL_GOLD_COIN,
    OBJECT_ITEM_MATERIAL_WOOD_LOG,
    OBJECT_ITEM_MATERIAL_STONE,
    OBJECT_ITEM_MATERIAL_WOOD_FLANK,
    OBJECT_ITEM_MATERIAL_YARN,
    OBJECT_ITEM_MATERIAL_CHAR_COAL,
    OBJECT_ITEM_MATERIAL_COPPER_NUGGET,
    OBJECT_ITEM_MATERIAL_COPPER_INGOT,
    OBJECT_ITEM_MATERIAL_IRON_NUGGET,
    OBJECT_ITEM_MATERIAL_IRON_INGOT,

    OBJECT_ITEM_CROP_SEED,
    OBJECT_ITEM_CROP_SEED_POTATO,
    OBJECT_ITEM_CROP_SEED_CORN,

    OBJECT_ITEM_CROP_FRUIT,
    OBJECT_ITEM_CROP_FRUIT_POTATO,
    OBJECT_ITEM_CROP_FRUIT_CORN,

    OBJECT_ITEM_STORAGE,
    OBJECT_ITEM_STORAGE_BOX,

    OBJECT_PLAYER_DUMMY = 32000
}

public enum en_CreatureState
{
    SPAWN_READY,
    SPAWN_IDLE,
    IDLE,
    PATROL,
    MOVING,
    STOP,
    RETURN_SPAWN_POSITION,
    ATTACK,
    SPELL,
    GATHERING,    
    DEAD
};

public enum en_MonsterState
{
    MONSTER_IDLE,
    MONSTER_READY_PATROL,
    MONSTER_PATROL,
    MONSTER_READY_MOVE,
    MONSTER_MOVE,
    MONSTER_READY_ATTACK,
    MONSTER_ATTACK
};

public enum en_ItemState
{
    ITEM_IDLE,
    ITEM_READY_MOVE,
    ITEM_MOVE
}

public enum en_MessageType
{
    MESSAGE_TYPE_CHATTING,
    MESSAGE_TYPE_DAMAGE_CHATTING,
    MESSAGE_TYPE_SYSTEM
};

enum en_StateChange
{
    MOVE_TO_STOP,
    SPELL_TO_IDLE,
};

public enum en_SkillCharacteristic
{
    SKILL_CATEGORY_NONE,
    SKILL_CATEGORY_PUBLIC,
    SKILL_CATEGORY_FIGHT,
    SKILL_CATEGORY_PROTECTION,
    SKILL_CATEGORY_SPELL,
    SKILL_CATEGORY_DISCIPLINE,
    SKILL_CATEGORY_ASSASSINATION,
    SKILL_CATEGORY_SHOOTING
};

public enum en_SkillLargeCategory
{
    SKILL_LARGE_CATEGORY_NONE = 0,
    SKILL_LARGE_CATEGORY_PUBLIC,

    SKILL_LARGE_CATEGORY_WARRIOR,

    SKILL_LARGE_CATEGORY_SHMAN,

    SKILL_LARGE_CATEGORY_TAOIST,

    SKILL_LARGE_CATEGORY_THIEF,

    SKILL_LARGE_CATEGORY_ARCHER,

    SKILL_LARGE_CATEGORY_MONSTER_MELEE,
    SKILL_LARGE_CATEGORY_MONSTER_MAGIC
};

public enum en_SkillMediumCategory
{
    SKILL_MEDIUM_CATEGORY_NONE = 0,

    SKILL_MEDIUM_CATEGORY_PUBLIC_ACTIVE_ATTACK,
    SKILL_MEDIUM_CATEGORY_PUBLIC_ACTIVE_BUF,
    SKILL_MEDIUM_CATEGORY_PUBLIC_PASSIVE,

    SKILL_MEDIUM_CATEGORY_FIGHT_ACTIVE_ATTACK,
    SKILL_MEDIUM_CATEGORY_FIGHT_ACTIVE_BUF,
    SKILL_MEDIUM_CATEGORY_FIGHT_PASSIVE,

    SKILL_MEDIUM_CATEGORY_PROTECTION_ACTIVE_ATTACK,
    SKILL_MEDIUM_CATEGORY_PROTECTION_ACTIVE_BUF,
    SKILL_MEDIUM_CATEOGRY_PROTECTION_PASSIVE,

    SKILL_MEDIUM_CATEGORY_ASSASSINATION_ACTIVE_ATTACK,
    SKILL_MEDIUM_CATEGORY_ASSASSINATION_ACTIVE_BUF,
    SKILL_MEDIUM_CATEGORY_ASSASSINATION_PASSIVE,

    SKILL_MEDIUM_CATEGORY_SPELL_ACTIVE_ATTACK,
    SKILL_MEDIUM_CATEGORY_SPELL_ACTIVE_BUF,
    SKILL_MEDIUM_CATEGORY_SPELL_PASSIVE,

    SKILL_MEDIUM_CATEGORY_SHOOTING_ACTIVE_ATTACK,
    SKILL_MEDIUM_CATEGORY_SHOOTING_ACTIVE_BUF,
    SKILL_MEDIUM_CATEGORY_SHOOTING_PASSIVE,

    SKILL_MEDIUM_CATEGORY_DISCIPLINE_ACTIVE_ATTACK,
    SKILL_MEDIUM_CATEGORY_DISCIPLINE_ACTIVE_HEAL,
    SKILL_MEDIUM_CATEGORY_DISCIPLINE_ACTIVE_BUF,
    SKILL_MEDIUM_CATEGORY_DISCIPLINE_PASSIVE
};

public enum en_SkillType
{
    SKILL_TYPE_NONE = 0,
    SKILL_GLOBAL_SKILL = 1,

    SKILL_DEFAULT_ATTACK,
    SKILL_PUBLIC_ACTIVE_BUF_SHOCK_RELEASE,

    SKILL_FIGHT_TWO_HAND_SWORD_MASTER,

    SKILL_FIGHT_ACTIVE_ATTACK_FIERCE_ATTACK,
    SKILL_FIGHT_ACTIVE_ATTACK_CONVERSION_ATTACK,
    SKILL_FIGHT_ACTIVE_ATTACK_JUMPING_ATTACK,
    SKILL_FIGHT_ACTIVE_ATTACK_PIERCING_WAVE,
    SKILL_FIGHT_ACTIVE_ATTACK_FLY_KNIFE,
    SKILL_FIGHT_ACTIVE_ATTACK_COMBO_FLY_KNIFE,
    SKILL_FIGHT_ACTIVE_BUF_CHARGE_POSE,

    SKILL_PROTECTION_ACTIVE_ATTACK_SHIELD_SMASH,
    SKILL_PROTECTION_ACTIVE_ATTACK_CAPTURE,

    SKILL_SPELL_ACTIVE_ATTACK_FLAME_HARPOON,
    SKILL_SPELL_ACTIVE_ATTACK_ROOT,
    SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN,
    SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE,
    SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE,
    SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE,
    SKILL_SPELL_ACTIVE_BUF_TELEPORT,

    SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE,
    SKILL_DISCIPLINE_ACTIVE_ATTACK_ROOT,
    SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT,
    SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND,

    SKILL_ASSASSINATION_ACTIVE_ATTACK_QUICK_CUT,
    SKILL_ASSASSINATION_ACTIVE_ATTACK_FAST_CUT,
    SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_ATTACK,
    SKILL_ASSASSINATION_ACTIVE_ATTACK_BACK_STEP,
    SKILL_ASSASSINATION_ACTIVE_BUF_WEAPON_POISON,

    SKILL_SHOOTING_ACTIVE_ATTACK_SNIFING,

    SKILL_GOBLIN_ACTIVE_MELEE_DEFAULT_ATTACK
};

public enum en_EquipmentParts
{
    EQUIPMENT_PARTS_NONE,
    EQUIPMENT_PARTS_HEAD,
    EQUIPMENT_PARTS_BODY,
    EQUIPMENT_PARTS_LEFT_HAND,
    EQUIPMENT_PARTS_RIGHT_HAND,
    EQUIPMENT_PARTS_BOOT
};

public enum en_GlobalMessageType
{
    PERSONAL_MESSAGE_NONE,

    PERSONAL_MESSAGE_STATUS_ABNORMAL,
    PERSONAL_MESSAGE_STATUS_ABNORMAL_WARRIOR_CHOHONE,
    PERSONAL_MESSAGE_STATUS_ABNORMAL_WARRIOR_SHAEHONE,
    PERSONAL_MESSAGE_STATUS_ABNORMAL_SHAMAN_ROOT,
    PERSONAL_MESSAGE_STATUS_ABNORMAL_SHAMAN_ICE_CHAIN,
    PERSONAL_MESSAGE_STATUS_ABNORMAL_SHAMAN_ICE_WAVE,
    PERSONAL_MESSAGE_STATUS_ABNORMAL_SHAMAN_LIGHTNING_STRIKE,
    PERSONAL_MESSAGE_STATUS_ABNORMAL_TAIOIST_ROOT,
    PERSOANL_MESSAGE_STATUS_ABNORMAL_SPELL,

    PERSONAL_MESSAGE_NON_SKILL_CHARACTERISTIC,
    PERSONAL_MESSAGE_SKILL_COOLTIME,
    PERSONAL_MESSAGE_NON_SELECT_OBJECT,
    PERSONAL_MESSAGE_HEAL_NON_SELECT_OBJECT,
    PERSONAL_MESSAGE_PLACE_BLOCK,
    PERSONAL_MESSAGE_PLACE_DISTANCE,
    PERSONAL_MESSAGE_FAR_DISTANCE,
    PERSONAL_MESSAGE_MYSELF_TARGET,

    PERSONAL_MESSAGE_DIR_DIFFERENT,
    PERSONAL_MESSAGE_GATHERING_DISTANCE,

    PERSONAL_MEESAGE_CRAFTING_TABLE_OVERLAP_SELECT,
    PERSONAL_MESSAGE_CRAFTING_TABLE_OVERLAP_CRAFTING_START,
    PERSONAL_MESSAGE_CRAFTING_TABLE_MATERIAL_COUNT_NOT_ENOUGH,
    PERSOANL_MESSAGE_CRAFTING_TABLE_MATERIAL_WRONG_ITEM_ADD,

    PERSONAL_MESSAGE_LOGIN_ACCOUNT_NOT_EXIST,
    PERSONAL_MESSAGE_LOGIN_ACCOUNT_OVERLAP,
    PERSONAL_MESSAGE_LOGIN_ACCOUNT_DB_WORKING,
    PERSONAL_MESSAGE_LOGIN_ACCOUNT_DIFFERENT_PASSWORD,

    PERSONAL_MESSAGE_SYSTEM_SYSTEM_ALLOC_TILE,
    PERSONAL_MESSAGE_SYSTEM_ALLOC_TILE,

    PERSOANL_MESSAGE_SEED_FARMING_EXIST,
    PERSONAL_MESSAGE_PARTY_INVITE_REJECT,

    PERSONAL_FAULT_ITEM_USE,

    PERSONAL_UI_CLOSE
};

public enum en_Inventory
{
    INVENTORY_SIZE = 30
};

public enum en_QuickSlotBar
{
    QUICK_SLOT_BAR_SIZE = 3,
    QUICK_SLOT_BAR_SLOT_SIZE = 5
};

public enum en_UIObjectInfo
{
    UI_OBJECT_INFO_NONE = 0,

    UI_OBJECT_INFO_CRAFTING_TABLE_COMMON,
    UI_OBJECT_INFO_CRAFTING_TABLE_FURNACE,
    UI_OBJECT_INFO_CRAFTING_TABLE_SAWMILL
};

public enum en_TileMapEnvironment
{
    TILE_MAP_NONE = 0,
    TILE_MAP_WALL,
    TILE_MAP_TREE,
    TILE_MAP_STONE,
    TILE_MAP_SLIME,
    TILE_MAP_BEAR
};

public enum en_LargeItemCategory
{
    ITEM_LARGE_CATEGORY_NONE = 0,
    ITEM_LARGE_CATEGORY_ARCHITECTURE,
    ITEM_LARGE_CATEGORY_WEAPON,
    ITEM_LARGE_CATEGORY_ARMOR,
    ITEM_LARGE_CATEGORY_TOOL,
    ITEM_LARGE_CATEGORY_FOOD,
    ITEM_LARGE_CATEGORY_POTION,
    ITEM_LARGE_CATEGORY_MATERIAL,
    ITEM_LARGE_CATEGORY_CROP
};

public enum en_MediumItemCategory
{
    ITEM_MEDIUM_CATEGORY_NONE = 0,
    ITEM_MEDIUM_CATEGORY_CRAFTING_TABLE,
    ITEM_MEDIUM_CATEGORY_SWORD,
    ITEM_MEDIUM_CATEGORY_SHIELD,
    ITEM_MEDIUM_CATEGORY_HAT,
    ITEM_MEDIUM_CATEGORY_WEAR,
    ITEM_MEDIUM_CATEGORY_GLOVE,
    ITEM_MEDIUM_CATEGORY_BOOT,
    ITEM_MEDIUM_CATEOGRY_FARMING,
    ITEM_MEDIUM_CATEGORY_HEAL,
    ITEM_MEDIUM_CATEGORY_MANA,
    ITEM_MEDIUM_CATEGORY_CROP_SEED,
    ITEM_MEDIUM_CATEGORY_CROP_FRUIT
};

public enum en_SmallItemCategory
{
    ITEM_SMALL_CATEGORY_NONE = 0,

    ITEM_SMALL_CATEGORY_WEAPON_SWORD_WOOD = 1,
    ITEM_SAMLL_CATEGORY_WEAPON_WOOD_SHIELD,

    ITEM_SMALL_CATEGORY_TOOL_FARMING_SHOVEL,

    ITEM_SMALL_CATEGORY_ARMOR_WEAR_LEATHER = 100,
    ITEM_SMALL_CATEGORY_ARMOR_HAT_LEATHER,
    ITEM_SMALL_CATEGORY_ARMOR_BOOT_LEATHER,

    ITEM_SMALL_CATEGORY_POTION_HEALTH_RESTORATION_POTION_SMALL = 200,
    ITEM_SMALL_CATEGORY_POTION_MANA_RESTORATION_POTION_SMALL,

    ITEM_SMALL_CATEGORY_MATERIAL_LEATHER = 2000,
    ITEM_SMALL_CATEGORY_MATERIAL_BRONZE_COIN,
    ITEM_SMALL_CATEGORY_MATERIAL_SLIVER_COIN,
    ITEM_SMALL_CATEGORY_MATERIAL_GOLD_COIN,
    ITEM_SMALL_CATEGORY_MATERIAL_STONE,
    ITEM_SMALL_CATEGORY_MATERIAL_WOOD_LOG,
    ITEM_SMALL_CATEGORY_MATERIAL_WOOD_FLANK,
    ITEM_SMALL_CATEGORY_MATERIAL_YARN,
    ITEM_SMALL_CATEGORY_MATERIAL_CHAR_COAL,
    ITEM_SMALL_CATEGORY_MATERIAL_COPPER_NUGGET,
    ITEM_SMALL_CATEGORY_MATERIAL_COPPER_INGOT,
    ITEM_SMALL_CATEGORY_MATERIAL_IRON_NUGGET,
    ITEM_SMALL_CATEGORY_MATERIAL_IRON_INGOT,

    ITEM_SMALL_CATEGORY_CROP_SEED_POTATO,
    ITEM_SMALL_CATEGORY_CROP_FRUIT_POTATO,
    ITEM_SMALL_CATEGORY_CROP_SEED_CORN,
    ITEM_SMALL_CATEGORY_CROP_FRUIT_CORN,

    ITEM_SMALL_CATEGORY_CRAFTING_DEFAULT_CRAFTING_TABLE = 5000,
    ITEM_SMALL_CATEGORY_CRAFTING_TABLE_FURANCE,
    ITEM_SMALL_CATEGORY_CRAFTING_TABLE_SAWMILL
};

public enum en_LoginInfo
{
    LOGIN_ACCOUNT_NOT_EXIST,
    LOGIN_ACCOUNT_OVERLAP,
    LOGIN_ACCOUNT_DB_WORKING,
    LOGIN_ACCOUNT_DIFFERENT_PASSWORD,
    LOGIN_ACCOUNT_LOGIN_SUCCESS
}

public enum en_OptionType
{
    OPTION_TYPE_NONE = 0    
};

public enum en_DayType
{
    DAY_NONE = 0,
    DAY_DAWN,
    DAY_MORNING,
    DAY_AFTERNOON,
    DAY_EVENING,
    DAY_NIGHT
};

public enum en_QuickSlotBarType
{
    QUICK_SLOT_BAR_TYPE_NONE = 0,
    QUICK_SLOT_BAR_TYPE_SKILL,
    QUICK_SLOT_BAR_TYPE_ITEM
};

public enum en_ResourceName
{
    CLIENT_UI_NAME_NONE = 0,    

    CLIENT_GAMEOBJECT_PLAYER,
    
    CLIENT_GAMEOBJECT_MONSTER_GOBLIN,    
   
    CLIENT_GAMEOBJECT_LEFT_RIGHT_WALL,
    CLIENT_GAMEOBJECT_UP_DOWN_WALL,
    CLIENT_GAMEOBJECT_UP_TO_LEFT_WALL,
    CLIENT_GAMEOBJECT_UP_TO_RIGHT_WALL,
    CLIENT_GAMEOBJECT_DOWN_TO_LEFT_WALL,
    CLIENT_GAMEOBJECT_DOWN_TO_RIGHT_WALL,

    CLIENT_GAMEOBJECT_ENVIRONMENT_STONE,
    CLIENT_GAMEOBJECT_ENVIRONMENT_TREE,

    CLIENT_GAMEOBJECT_CROP_POTATO,
    CLIENT_GAMEOBJECT_CROP_CORN,

    CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE,
    CLIENT_GAMEOBJECT_CRAFTING_TABLE_SAWMILL,

    CLIENT_GAMEOBJECT_DAY,
    
    CLIENT_GAMEOBJECT_ITEM_LEATHER,
    CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN,
    CLIENT_GAMEOBJECT_ITEM_WOOD_LOG,
    CLIENT_GAMEOBJECT_ITEM_STONE,
    CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK,
    CLIENT_GAMEOBJECT_ITEM_CHARCOAL,
    CLIENT_GAMEOBJECT_ITEM_POTATO,

    CLIENT_EFFECT_ATTACK_TARGET_HIT,
    CLIENT_EFFECT_SMASH_WAVE,
    CLIENT_EFFECT_CHOHONE,
    CLIENT_EFFECT_SHAHONE,
    CLIENT_EFFECT_CHARGE_POSE,
    CLIENT_EFFECT_FLAME_HARPOON,
    CLIENT_EFFECT_LIGHTHING,
    CLIENT_EFFECT_BACK_TELEPORT,
    CLIENT_EFFECT_HEALING_LIGHT,
    CLIENT_EFFECT_HEALING_WIND,
    CLIENT_EFFECT_STUN,

    CLIENT_UI_SERVER_SELECT_ITEM,

    CLIENT_UI_CHARACTER_CHOICE_ITEM,

    CLIENT_UI_CHATTING_BOX,
    CLIENT_UI_CHATTING_TEXT,

    CLIENT_UI_CRAFTING_CATEGORY_ITEM,
    CLIENT_UI_CRAFTING_MATERIAL_ITEM,
    CLIENT_UI_CRAFTING_COMPLETE_ITEM,

    CLIENT_UI_QUICK_SLOT_BAR_BOX,
    CLIENT_UI_QUICK_SLOT_BAR,
    CLIENT_UI_QUICK_SLOT_BAR_BUTTON,
    CLIENT_UI_QUICK_SLOT_ITEM_DRAG,

    CLIENT_UI_INVENTORY_BOX,
    CLIENT_UI_INVENTORY_ITEM,
    CLIENT_UI_INVENTORY_ITEM_DRAG,
    CLIENT_UI_INVENTORY_ITEM_GAIN,

    CLIENT_UI_PARTY,
    CLIENT_UI_PARTY_PLAYER_INFO_FRAME,
    CLIENT_UI_PARTY_PLAYER_OPTION,
    CLIENT_UI_PARTY_REACTION,

    CLIENT_UI_GLOBAL_MESSAGE,

    CLIENT_UI_DAMAGE,

    CLIENT_UI_EQUIPMENT_BOX,

    CLIENT_UI_SKILL_BOX,
    CLIENT_UI_SKILL_EXPLANATION,
    CLIENT_UI_SKILL_ITEM,
    CLIENT_UI_SKILL_ITEM_DRAG,
    CLIENT_UI_SKILL_BUF_DEBUF,

    CLIENT_UI_ITEM_EXPLANATION,
    CLIENT_UI_ITEM_DIVIDE,

    CLIENT_UI_CRAFTING_BOX,
    CLIENT_UI_FURNACE,
    CLIENT_UI_SAWMILL,

    CLIENT_UI_OPTION,

    CLIENT_UI_MY_CHARACTER_HUD,
    CLIENT_UI_TARGET_HUD,
    CLIENT_UI_PLAYER_OPTION,

    CLIENT_UI_HP_BAR,
    CLIENT_UI_SPELL_BAR,
    CLIENT_UI_GATHERING_BAR,

    CLIENT_UI_NAME,

    CLIENT_UI_SPEECH_BUBBLE,

    CLIENT_UI_SCENE_LOGIN,
    CLIENT_UI_SCENE_GAME,

    CLIENT_UI_EVENT_SYTEM,

    CLIENT_MAP_MAIN_FIELD,
    CLIENT_MAP_TILE
};

public enum en_SoundClip
{
    SOUND_CLIP_NONE,
    SOUND_CLIP_LOGIN,
    SOUND_CLIP_FOREST
}

public enum en_WeaponType
{
    WEAPON_TYPE_NON,
    WEAPON_TYPE_MELEE,
    WEAPON_TYPE_RANGE
}

public enum en_AnimationType
{
    ANIMATION_TYPE_NONE,
    ANIMATION_TYPE_SWORD_MELEE_ATTACK
};

public class st_RayCastingPosition
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;    
}

public class st_ItemInfo
{
    public long ItemDBId; // 아이템 DB에 저장되어 있는 ID    
    public bool IsEquipped; // 아이템을 착용할 수 있는지
    public short ItemWidth;    // 아이템 너비
    public short ItemHeight;   // 아이템 높이    
    public short ItemTileGridPositionX; // 아이템 X 위치
    public short ItemTileGridPositionY; // 아이템 Y 위치
    public en_UIObjectInfo OwnerCraftingTable;
    public en_LargeItemCategory ItemLargeCategory; // 아이템 대 분류
    public en_MediumItemCategory ItemMediumCategory; // 아이템 중 분류
    public en_SmallItemCategory ItemSmallCategory; // 아이템 소 분류   3
    public en_EquipmentParts ItemEquipmentPart;
    public string ItemName; // 아이템 이름
    public string ItemExplain; // 아이템 설명문
    public long ItemCraftingTime;                   // 아이템 제작 시간
    public long ItemCraftingRemainTime;			  // 아이템 제작 남은 시간
    public int ItemMinDamage;            // 아이템 최소 공격력
    public int ItemMaxDamage;            // 아이템 최대 공격력
    public int ItemDefence;              // 아이템 방어력
    public int ItemMaxCount;				// 아이템을 소유 할 수 있는 최대 개수
    public short ItemCount; // 개수                
    public List<st_CraftingMaterialItemInfo> Materials = new List<st_CraftingMaterialItemInfo>();

    public st_ItemInfo()
    {
        ItemDBId = 0;
        IsEquipped = false;
        ItemWidth = 0;
        ItemHeight = 0;
        ItemTileGridPositionX = 0;
        ItemTileGridPositionY = 0;
        OwnerCraftingTable = en_UIObjectInfo.UI_OBJECT_INFO_NONE;
        ItemLargeCategory = en_LargeItemCategory.ITEM_LARGE_CATEGORY_NONE;
        ItemMediumCategory = en_MediumItemCategory.ITEM_MEDIUM_CATEGORY_NONE;
        ItemSmallCategory = en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE;
        ItemEquipmentPart = en_EquipmentParts.EQUIPMENT_PARTS_NONE;
        ItemName = "";
        ItemExplain = "";
        ItemCraftingTime = 0;
        ItemCraftingRemainTime = 0;
        ItemMinDamage = 0;
        ItemMaxDamage = 0;
        ItemDefence = 0;
        ItemMaxCount = 0;
        ItemCount = 0;
    }
};

public class st_CraftingMaterialItemInfo
{
    public en_SmallItemCategory MaterialItemType; // 재료템 종류
    public string MaterialItemName; // 재료템 이름
    public short ItemCount; // 재료템 필요 개수
};

public class st_CraftingCompleteItem
{
    public en_UIObjectInfo OwnerCraftingTable;
    public en_SmallItemCategory CompleteItemType; // 완성 제작템 종류
    public string CompleteItemName; // 완성 제작템 이름
    public List<st_CraftingMaterialItemInfo> Materials = new List<st_CraftingMaterialItemInfo>(); // 제작템 만들때 필요한 재료들
};

public class st_CraftingItemCategory
{
    public en_LargeItemCategory CategoryType; // 제작템 범주
    public string CategoryName; // 제작템 범주 이름
    public List<st_ItemInfo> CommonCraftingCompleteItems = new List<st_ItemInfo>(); // 범주에 속한 완성 제작템들
};

public class st_CraftingTableRecipe
{
    public en_GameObjectType CraftingTableType;
    public string CraftingTableName;
    public List<st_ItemInfo> CraftingTableCompleteItems;
};

public class st_PositionInfo : IEquatable<st_PositionInfo>
{
    public en_CreatureState State;
    public Vector2Int CollisitionPosition;    
    public Vector2 Position = new Vector2();
    public Vector2 LookAtDireciton = new Vector2();
    public Vector2 MoveDireciton = new Vector2();        

    public bool Equals(st_PositionInfo other)
    {
        if (State != other.State)
        {
            return false;
        }

        if (CollisitionPosition.x != other.CollisitionPosition.x)
        {
            return false;
        }

        if (CollisitionPosition.y != other.CollisitionPosition.y)
        {
            return false;
        }        

        return true;
    }
}

public class st_StatInfo
{
    public int Level;
    public int HP;
    public int MaxHP;
    public int MP;
    public int MaxMP;
    public int DP;
    public int MaxDP;
    public short AutoRecoveyHPPercent;
    public short AutoRecoveyMPPercent;
    public int MinMeleeAttackDamage;
    public int MaxMeleeAttackDamage;
    public short MeleeAttackHitRate;
    public short MagicDamage;
    public float MagicHitRate;
    public int Defence;
    public short EvasionRate;
    public short MeleeCriticalPoint;
    public short MagicCriticalPoint;
    public short StatusAbnormalResistance;
    public float Speed;
    public float MaxSpeed;
}

public class st_GameObjectInfo
{
    public long ObjectId;
    public string ObjectName;
    public byte ObjectCropStep;
    public byte ObjectCropMaxStep;
    public byte ObjectSkillPoint;
    public st_PositionInfo ObjectPositionInfo;
    public st_StatInfo ObjectStatInfo;
    public en_GameObjectType ObjectType;
    public long OwnerObjectId;
    public en_GameObjectType OwnerObjectType;
    public short ObjectWidth;
    public short ObjectHeight;
    public byte PlayerSlotIndex;

    public st_GameObjectInfo()
    {
        ObjectPositionInfo = new st_PositionInfo();
        ObjectStatInfo = new st_StatInfo();
    }
}

public class st_Experience
{
    public long CurrentExperience;
    public long RequireExperience;
    public long TotalExperience;

    st_Experience()
    {
        CurrentExperience = 0;
        RequireExperience = 0;
        TotalExperience = 0;
    }
};

public class st_Color
{
    public short _Red;
    public short _Green;
    public short _Blue;

    public st_Color() { }
    st_Color(short Red, short Green, short Blue)
    {
        _Red = Red;
        _Green = Green;
        _Blue = Blue;
    }

    public static st_Color Red() { return new st_Color(255, 0, 0); }
    public static st_Color Green() { return new st_Color(0, 255, 0); }
    public static st_Color Blue() { return new st_Color(0, 0, 255); }
    public static st_Color White() { return new st_Color(255, 255, 255); }
};

public class st_SkillInfo
{
    public bool IsSkillLearn;
    public bool CanSkillUse;
    public en_SkillCharacteristic SkillCharacteristic;
    public en_SkillLargeCategory SkillLargeCategory;
    public en_SkillMediumCategory SkillMediumCategory;
    public en_SkillType SkillType;
    public byte SkillLevel;
    public byte SkillOverlapStep;
    public string SkillName;
    public int SkillCoolTime;
    public int SkillCastingTime;
    public long SkillDurationTime;
    public long SkillDotTime;
    public long SkillRemainTime;    

    public st_SkillInfo()
    {
        IsSkillLearn = false;
        SkillLargeCategory = en_SkillLargeCategory.SKILL_LARGE_CATEGORY_NONE;
        SkillMediumCategory = en_SkillMediumCategory.SKILL_MEDIUM_CATEGORY_NONE;
        SkillType = en_SkillType.SKILL_TYPE_NONE;
        SkillLevel = 0;
        SkillOverlapStep = 0;
        SkillName = "";
        SkillCoolTime = 0;
        SkillCastingTime = 0;
        SkillDurationTime = 0;
        SkillDotTime = 0;
        SkillRemainTime = 0;        
    }
}

public class st_QuickSlotBarSlotInfo
{
    public en_QuickSlotBarType QuickSlotBarType; // 퀵슬롯 타입
    public long AccountDBId; // 퀵슬롯 슬롯 소유한 Account
    public long PlayerDBId;  // 퀵슬롯 슬롯 소유한 Player	
    public byte QuickSlotBarIndex; // 퀵슬롯 Index
    public byte QuickSlotBarSlotIndex; // 퀵슬롯 슬롯 Index
    public KeyCode QuickSlotKey;
    public st_SkillInfo QuickBarSkillInfo = null;	// 퀵슬롯에 등록할 스킬 정보    
    public st_ItemInfo QuickBarItemInfo = null;
};

public class st_ServerInfo
{
    public string ServerName;
    public string ServerIP;
    public int ServerPort;
    public float ServerBusy;
}

public class st_PersonalMessage
{
    public en_GlobalMessageType PersonalMessageType;
    public string PersonalMessage;
}

public class st_OptionItemInfo
{
    public en_OptionType OptionType;
    public string OptionName;
}

public class st_Day
{
    public float DayTimeCycle;
    public float DayTimeCheck;
    public float DayRatio;

    public en_DayType DayType;
}