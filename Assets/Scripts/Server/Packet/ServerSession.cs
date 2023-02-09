using UnityEngine;
using ServerCore;
using System.Net;
using System;
using static Protocol;
using Packet;

public class CServerSession : CSession
{
    public void SendMessage(CMessage SendPacket)
    {
        SendPacket.Encode();
        Send(new ArraySegment<byte>(SendPacket._MessageBuf, 0, SendPacket.GetUseBufferSize()));
        SendPacket.Dispose();
    }  

    public override void OnConnected(EndPoint endPoint)
    {
        Debug.Log($"{endPoint} 연결");        
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Debug.Log($"{endPoint} 연결 종료");
    }

    public override void OnRecvPacket(CMessage CheckCompletePacket)
    {
        //--------------------------------------------------------------------------------------------
        // 패킷 확인후 처리
        //--------------------------------------------------------------------------------------------
        short PacketType;
                
        CheckCompletePacket.GetData(out PacketType, sizeof(short));            

        switch((en_GAME_SERVER_PACKET_TYPE)PacketType)
        {
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_GAME_CLIENT_CONNECTED:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ConnectHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_GAME_RES_LOGIN:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_LoginHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_GAME_CREATE_CHARACTER:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CreateCharacterHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_GAME_ENTER:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_EnterGameHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CHARACTER_INFO:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CharacterInfoHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_MOVE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_MoveHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_MOVE_STOP:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_MoveStopHandler);
                break;            
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_ITEM_MOVE_START:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ItemMoveStartHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_COMMON_DAMAGE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CommonDamageHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_GATHERING_DAMAGE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_GatheringDamageHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_ATTACK:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_AttackHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_SPELL:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_MagicHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_MAGIC_CANCEL:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_MagicCancelHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_GATHERING_CANCEL:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_GatheringCancelHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_ANIMATION_PLAY:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_AnimationPlayHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_SPAWN:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_SpawnHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_DESPAWN:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_DespawnHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_OBJECT_STAT_CHANGE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ChangeObjectStatHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_LEFT_MOUSE_OBJECT_INFO:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_LeftMousePositionObjectInfoHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_RIGHT_MOUSE_OBJECT_INFO:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_RightMouseObjectInfoHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_TABLE_CRAFT_REMAIN_TIME:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CraftingTableCraftRemainTimeHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_TABLE_NON_SELECT:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CraftingTableNonSelectHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_GATHERING:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_GatheringHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_OBJECT_STATE_CHANGE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ObjectStateChangeHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_MONSTER_OBJECT_STATE_CHANGE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_MonsterStateChangeHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_STATUS_ABNORMAL:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_StatusAbnormalHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_DIE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_DieHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_MESSAGE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ChattingMessageHandler);
                break;            
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_LOOTING:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_InventoryItemAddHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_TABLE_ITEM_ADD:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CratingTableItemInputHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_TABLE_COMPLETE_ITEM_SELECT:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CraftingTableCompleteItemSelectHandler);
                break;            
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_TABLE_CRAFTING_START:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CraftingTableCraftingStartHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_TABLE_CRAFTING_STOP:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CraftingTableCraftingStopHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_TABLE_MATERIAL_ITEM_LIST:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CraftingTableMaterialItemListHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_TABLE_COMPLETE_ITEM_LIST:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CraftingTableCompleteItemListHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_ITEM_SELECT:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ItemSelectHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_ITEM_PLACE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ItemPlaceHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_SEED_FARMING:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_SeedFarmingHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_PLANT_GROWTH_CHECK:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_PlantGrowthCheckHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_ITEM_ROTATE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ItemRotateHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_SYNC_OBJECT_POSITION:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_SyncPosition);                
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_SELECT_SKILL_CHARACTERISTIC:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_SelectSkillCharacteristic);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_LEARN_SKILL:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_SkillLearn);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_SKILL_TO_SKILLBOX:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_SkillToSkillBox);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_QUICKSLOT_CREATE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_QuickSlotCreate);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_QUICKSLOT_SAVE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_QuickSlotSaveHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_COOLTIME_START:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CoolTimeStartHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_QUICKSLOT_SWAP:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_QuickSlotSwapHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_QUICKSLOT_EMPTY:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_QuickSlotEmptyHandler);
                break;            
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_CRAFTING_LIST:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_CraftingListHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_INVENTORY_ITEM_UPDATE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_InventoryItemUpdateHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_INVENTORY_ITEM_USE:                
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_ON_EQUIPMENT:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_OnEquipmentHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_OFF_EQUIPMENT:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_OffEquipmentHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_EXPERIENCE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ExperienceHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_BUF_DEBUF:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_BufDeBufHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_BUF_DEBUF_OFF:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_BufDeBufOffHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_COMBO_SKILL_ON:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ComboSkillOnMessageHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_COMBO_SKILL_OFF:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_ComboSkillOffMessageHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_GLOBAL_MESSAGE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_GlobalMessage);
                break;            
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_PARTY_INVITE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_PartyInviteHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_PARTY_INVITE_ACCEPT:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_PartyAcceptHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_PARTY_QUIT:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_PartyQuitHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_PARTY_BANISH:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_PartyBanishHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_PARTY_LEADER_MANDATE:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_PartyBanishHandler);
                break;
            case en_GAME_SERVER_PACKET_TYPE.en_PACKET_S2C_PING:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_Ping);
                break;
        }

        switch ((en_LOGIN_SERVER_PACKET_TYPE)PacketType)
        {            
            case en_LOGIN_SERVER_PACKET_TYPE.en_LOGIN_SERVER_S2C_ACCOUNT_NEW:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_AccountNew);
                break;
            case en_LOGIN_SERVER_PACKET_TYPE.en_LOGIN_SERVER_S2C_ACCOUNT_LOGIN:
                CPacketQueue.GetInstance.Push(CheckCompletePacket, PacketHandler.S2C_AccountLogin);
                break;
        }
    }

    public override void OnSend(int NumOfBytes)
    {
        //Console.WriteLine($"{NumOfBytes} 만큼 보냄");
    }
}
