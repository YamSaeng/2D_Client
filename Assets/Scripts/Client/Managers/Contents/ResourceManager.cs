﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    Dictionary<en_ResourceName, string> _ResourcePath = new Dictionary<en_ResourceName, string>();

    public void Init()
    {
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SCENE_LOGIN, "UI/Scene/UI_LoginScene");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SCENE_GAME, "UI/Scene/UI_GameScene");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_EVENT_SYTEM, "ETC/EventSystem");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_CHATTING_BOX, "UI/ChattingBox/UI_ChattingBox");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SPEECH_BUBBLE, "UI/SpeechBubble/UI_SpeechBubble");

        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_PLAYER, "Creature/Player/Player");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_NON_PLAYER_GENERAL_MERCHANT, "Creature/NPC/GeneralMerchantNPC");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_GOBLIN, "Creature/Monster/Goblin");

        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_STONE, "Environment/Stone");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_TREE, "Environment/Tree");

        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_BUILDING_GOVERMENT_OFFICE, "Building/GovernmentOffice");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_BUILDING_WEAPON_STORE, "Building/WeaponStore");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_BUILDING_ARMOR_STORE, "Building/ArmorStore");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_BUILDING_WALL, "Building/Wall");

        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_SWORD_BLADE, "Skill/SwordBlade");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_FLAME_BOLT, "Skill/FlameBolt");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_DIVINE_BOLT, "Skill/DivineBolt");

        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE, "CraftingTable/Furnace");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_SAWMILL, "CraftingTable/Sawmill");

        _ResourcePath.Add(en_ResourceName.CLIENT_WEAPON_PARENT, "Weapon/WeaponParent");
        _ResourcePath.Add(en_ResourceName.CLIENT_WEAPON_DAGGER_WOOD, "Weapon/WeaponDaggerWood");
        _ResourcePath.Add(en_ResourceName.CLIENT_WEAPON_LONG_SWORD_WOOD, "Weapon/WeaponLongSwordWood");
        _ResourcePath.Add(en_ResourceName.CLIENT_WEAPON_BOW_WOOD, "Weapon/WeaponBowWood");

        _ResourcePath.Add(en_ResourceName.CLIENT_COLLISION_RECT, "Collision/RectCollision");        

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_CHATTING_TEXT, "UI/ChattingBox/UI_ChattingText");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_PLAYER_OPTION, "UI/Option/UI_PlayerOption");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_PARTY_PLAYER_OPTION, "UI/Option/UI_PartyPlayerOption");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_PARTY, "UI/PartyHUD/UI_PartyFrame");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_PARTY_PLAYER_INFO_FRAME, "UI/PartyHUD/UI_PartyPlayerInfoFrame");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_PARTY_REACTION, "UI/PartyHUD/UI_PartyReaction");
        
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_HP_BAR, "UI/WorldHUD/UI_HPBar");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SKILL_CASTING_BAR, "UI/WorldHUD/UI_SkillCastingBar");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_MY_CHARACTER_HUD, "UI/MyHUD/UI_MyCharacterHUD");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_TARGET_HUD, "UI/TargetHUD/UI_TargetHUD");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_DAMAGE, "UI/Damage/UI_Damage");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SKILL_BOX, "UI/Skill/UI_SkillBox");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SKILL_CHARACTERISTIC_SELECT, "UI/Skill/UI_SkilCharacteristic");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SKILL_ITEM, "UI/Skill/UI_SkillItem");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SKILL_ITEM_DRAG, "UI/Skill/UI_SkillItemDrag");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SKILL_EXPLANATION, "UI/Skill/UI_SkillExplanation");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SKILL_BUF_DEBUF, "UI/Skill/UI_BufDebuf");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_INVENTORY_BOX, "UI/Inventory/UI_Inventory");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_INVENTORY_ITEM, "UI/Inventory/UI_InventoryItem");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_INVENTORY_ITEM_DRAG, "UI/Inventory/UI_InventoryItemDrag");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_INVENTORY_ITEM_GAIN, "UI/Inventory/UI_ItemGain");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_QUICK_SLOT_BAR_BOX, "UI/QuickSlot/UI_QuickSlotBarBox.prefab");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_QUICK_SLOT_BAR, "UI/QuickSlot/UI_QuickSlotBar");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_QUICK_SLOT_BAR_BUTTON, "UI/QuickSlot/UI_QuickSlotBarButton");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_QUICK_SLOT_ITEM_DRAG, "UI/QuickSlot/UI_QuickSlotItemDrag");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_QUICK_SLOT_KEY, "UI/QuickSlotKey/UI_QuickSlotKey");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_QUICK_SLOT_KEY_ITEM, "UI/QuickSlotKey/UI_QuickSlotKeyItem");
                
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_LEATHER, "Item/Leather");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN, "Item/BronzeCoin");
        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG, "Item/WoodLog");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_NAME, "UI/WorldHUD/UI_Name");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_ITEM_EXPLANATION, "UI/Inventory/UI_ItemExplanation");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_ITEM_DIVIDE, "UI/Inventory/UI_InventoryItemDivide");
        
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_EQUIPMENT_BOX, "UI/Equipment/UI_EquipmentBox");        
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_EQUIPMENT_ITEM, "UI/Equipment/UI_EquipmentItem");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_GLOBAL_MESSAGE_BOX, "UI/GlobalMessage/UI_GlobalMessageBox");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_GLOBAL_MESSAGE, "UI/GlobalMessage/UI_GlobalMessage");        

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SERVER_SELECT_ITEM, "UI/ServerSelect/UI_SelectServerItem");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_CHARACTER_CHOICE_ITEM, "UI/CharacterChoice/UI_CharacterChoiceItem");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_FURNACE, "UI/CraftingTable/UI_Furnace");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_SAWMILL, "UI/CraftingTable/UI_Sawmill");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_OPTION, "UI/Option/UI_Option");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_OPTION_ITEM, "UI/Option/UI_OptionItem");
        
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_INTERACTION, "UI/Interaction/UI_Interaction");

        _ResourcePath.Add(en_ResourceName.CLIENT_UI_MENU, "UI/Menu/UI_Menu");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_MENU_BUILDING, "UI/Building/UI_Building");
        _ResourcePath.Add(en_ResourceName.CLIENT_UI_BUILDING_ITEM, "UI/Building/UI_BuildingItem");

        _ResourcePath.Add(en_ResourceName.CLIENT_GAMEOBJECT_DAY, "Day/DayManager");

        _ResourcePath.Add(en_ResourceName.CLIENT_MAP_MAIN_FIELD, "Map/MainField");
        _ResourcePath.Add(en_ResourceName.CLIENT_MAP_TILE, "Map/Tile");
    }

    public T Load<T>(string Path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            // /Knight
            // /의 위치를 찾아서 그다음부터의 값을 가져옴
            string Name = Path;
            int Index = Name.LastIndexOf('/');
            if(Index >= 0)
            {
                Name = Name.Substring(Index + 1);
            }            
        }

        return Resources.Load<T>(Path);
    }

    public T[] LoadAll<T>(string Path) where T : Object
    {
        return Resources.LoadAll<T>(Path);
    }

    //Prefabs 안에 있는 대상들을 
    public GameObject Instantiate(en_ResourceName ResourcePath, Transform Parent = null)
    {
        // 기존에 이미 만들었던 대상이면 바로 사용할수 있게 해준다.
        GameObject Prefab = Load<GameObject>($"Prefabs/{_ResourcePath[ResourcePath]}");
        if(Prefab == null)
        {
            Debug.Log($"프리팹 생성에 실패 ( 경로 : {_ResourcePath[ResourcePath]})");
            return null;
        }      

        // Object를 붙인이유는 안 붙이면 재귀적으로 Instantiate 함수가 한번더 호출 되기 때문
        GameObject Go = Object.Instantiate(Prefab, Parent);        
        Go.name = Prefab.name;
        
        return Go;
    }

    public void Destroy(GameObject Instance)
    {
        if(Instance == null)
        {
            Debug.Log($"삭제할 인스턴스가 존재하지 않습니다.");
        }      

        Object.Destroy(Instance);
    }
}
