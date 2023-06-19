﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protocol
{
    public enum en_GAME_SERVER_PACKET_TYPE
    {
		////////////////////////////////////////////////////////
		//
		//	Client & GameServer Protocol
		//
		////////////////////////////////////////////////////////

		//------------------------------------------------------
		// Game Server
		//------------------------------------------------------
		en_PACKET_CS_GAME_SERVER = 0,

		//------------------------------------------------------------
		// 하트비트	
		//
		//	{
		//		WORD		Type
		//	}
		//
		//
		// 클라이언트는 이를 30초마다 보내줌.
		// 서버는 40초 이상동안 메시지 수신이 없는 클라이언트를 강제로 끊어줘야 함.
		//------------------------------------------------------------	
		en_PACKET_CS_GAME_REQ_HEARTBEAT,


		//-----------------------------------------------------------
		// 게임서버 클라이언트 접속 완료 응답
		// {
		// 	   short Type
		// }
		//-----------------------------------------------------------
		en_PACKET_S2C_GAME_CLIENT_CONNECTED,

		//------------------------------------------------------------
		// 게임서버 로그인 요청
		//	{
		//		WORD	Type
		//
		//		int 	AccountNo
		//		WCHAR	ClientID[20];		
		//		int     TokenKey
		//	}
		//
		//------------------------------------------------------------
		en_PACKET_C2S_GAME_REQ_LOGIN,

		//------------------------------------------------------------
		// 게임서버 로그인 응답
		//	{
		//		WORD	Type
		//
		//		bool	Status				// 0:실패	1:성공	
		//	}
		//------------------------------------------------------------
		en_PACKET_S2C_GAME_RES_LOGIN,

		//------------------------------------------------------------
		// 게임서버 캐릭터 정보 응답
		// int8 CharacterCount
		// st_GameObjectIno[] CharacterInfo
		//------------------------------------------------------------
		en_PACKET_S2C_GAME_CHARACTER_INFOS,

		//------------------------------------------------------------
		// 게임서버 캐릭터 생성 요청
		// int32 CharacterNameLen
		// WCHAR CharacterName
		//------------------------------------------------------------
		en_PACKET_C2S_GAME_CREATE_CHARACTER,

		//------------------------------------------------------------
		// 게임서버 캐릭터 생성 요청 응답
		// int32 PlayerDBId
		// bool IsSuccess
		// wstring PlayerName
		//------------------------------------------------------------
		en_PACKET_S2C_GAME_CREATE_CHARACTER,

		//------------------------------------------------------------
		// 게임서버 캐릭터 입장
		// int64 AccoountId
		// int8 EnterGameCharacterNameLen
		// wstring EnterGameCharacterName
		//------------------------------------------------------------
		en_PACKET_C2S_GAME_ENTER,

		//------------------------------------------------------------
		// 게임서버 캐릭터 입장 요청 응답
		// int64 AccountId
		// int32 PlaterDBId
		// wstring EnterCharaceterName
		// st_GameObjectInfo ObjectInfo
		//------------------------------------------------------------
		en_PACKET_S2C_GAME_ENTER,

		//------------------------------------------------------------
		// 게임서버 캐릭터 정보 요청
		// int64 AccountId
		// int64 PlayerDBId	
		//------------------------------------------------------------
		en_PACKET_C2S_CHARACTER_INFO,

		//------------------------------------------------------------
		// 게임서버 캐릭터 정보 요청 응답	
		//------------------------------------------------------------
		en_PACKET_S2C_CHARACTER_INFO,

		//------------------------------------------------------------
		// 오브젝트가 바라보는 방향 저장
		// int64 AccountID
		// int64 PlayerDBID
		// float DirectionX
		// float DirectionY
		//------------------------------------------------------------
		en_PACKET_C2S_FACE_DIRECTION,

		//------------------------------------------------------------
		// 게임서버 캐릭터가 바라보는 방향 전송	
		// int64 PlayerID
		// float DirectionX
		// float DirectionY
		//------------------------------------------------------------
		en_PACKET_S2C_FACE_DIRECTION,

		//------------------------------------------------------------
		// 게임서버 캐릭터 움직이기 요청
		// int64 AccountId
		// int64 PlayerDBId
		// float DirectionX
		// float DirectionY
		// float MoveStartPositionX
		// float MoveStartPositionY
		// int8 MoveStartState
		//------------------------------------------------------------
		en_PACKET_C2S_MOVE,

		//------------------------------------------------------------
		// 게임서버 캐릭터 움직이기 요청 응답	
		// int64 PlayerDBId
		// float MoveStopPosition
		// float MoveStopPosition
		// int8 MoveStopState	
		//------------------------------------------------------------
		en_PACKET_S2C_MOVE,

		//------------------------------------------------------------
		// 게임서버 캐릭터 멈춤 요청
		// int64 AccountId
		// int64 ObjectId
		// st_PositionInfo PositionInfo
		//------------------------------------------------------------
		en_PACKET_C2S_MOVE_STOP,

		//------------------------------------------------------------
		// 게임서버 캐릭터 멈춤 요청 응답
		// int64 AccountId
		// int64 ObjectId
		// st_PositionInfo PositionInfo
		//------------------------------------------------------------
		en_PACKET_S2C_MOVE_STOP,

		//------------------------------------------------------------
		// 게임서버 아이템 움직임 응답 
		// st_GameObjectInfo ItemObjectInfo	
		//------------------------------------------------------------
		en_PACKET_S2C_ITEM_MOVE_START,

		//------------------------------------------------------------
		// 게임서버 일반 데미지 출력
		// int64 ObjectID
		// int64 TargetObjectID
		// en_SkillType SkillType
		// int32 Damage
		// bool IsCritical
		//------------------------------------------------------------
		en_PACKET_S2C_COMMON_DAMAGE,

		//------------------------------------------------------------
		// 게임서버 채집 데미지 출력	
		// int64 TargetObjectID		
		//------------------------------------------------------------
		en_PACKET_S2C_GATHERING_DAMAGE,

		//------------------------------------------------------------
		// 게임서버 캐릭터 기술 처리 요청
		// int64 AccountId
		// int32 PlayerDBId
		// int8 SkillcharacteristicType
		// int16 SkillType
		// float SkillDirectionX
		// float SkillDirectionY
		//------------------------------------------------------------	
		en_PACKET_C2S_SKILL_PROCESS,

		//------------------------------------------------------------
		// 게임서버 캐릭터 공격 요청 응답
		// int64 AccountId
		// int32 PlayerDBId
		// int8 Dir
		//------------------------------------------------------------	
		en_PACKET_S2C_ATTACK,

		//------------------------------------------------------------
		// 게임서버 캐릭터 공격 상태로 전환
		// int64 ObjectID
		// float StopPositionX
		// float StopPositionY
		// byte State
		//------------------------------------------------------------
		en_PACKET_S2C_TO_ATTACK,

		//------------------------------------------------------------
		// 게임서버 캐릭터 기술 캐스팅 시작 응답	
		// int64 PlayerDBId
		// bool SpellStart
		// en_SkillType SpellSkillType
		// float SpellTime
		//------------------------------------------------------------
		en_PACKET_S2C_SKILL_CASTING_START,

		//------------------------------------------------------------
		// 게임서버 캐릭터 기술 시전 취소 요청
		// int64 AccountId
		// int64 PlayerId	
		//------------------------------------------------------------
		en_PACKET_C2S_SKILL_CASTING_CANCEL,

		//------------------------------------------------------------
		// 게임서버 캐릭터 기술 시전 취소 요청 응답
		// int64 AccountId
		// int64 PlayerId	
		//------------------------------------------------------------
		en_PACKET_S2C_SKILL_CASTING_CANCEL,

		//------------------------------------------------------------
		// 게임서버 채집 요청
		// int64 AccountId
		// int64 PlayerID
		// int64 ObjectID
		// st_GameObjectType ObjectType
		//------------------------------------------------------------
		en_PACKET_C2S_GATHERING,

		//------------------------------------------------------------
		// 게임서버 채집 요청 응답
		// int64 ObjectID
		//------------------------------------------------------------
		en_PACKET_S2C_GATHERING,

		//------------------------------------------------------------
		// 게임서버 채집 취소 요청
		// int64 AccountID
		// int64 ObjectID
		//------------------------------------------------------------
		en_PACKET_C2S_GATHERING_CANCEL,

		//------------------------------------------------------------
		// 게임서버 채집 취소 요청 응답
		// int64 AccountID
		// int64 ObjectID
		//------------------------------------------------------------
		en_PACKET_S2C_GATHERING_CANCEL,

		//------------------------------------------------------------
		// 게임서버 캐릭터 애니메이션 출력
		// int64 PlayerID	
		// string AnimationName
		//------------------------------------------------------------
		en_PACKET_S2C_ANIMATION_PLAY,

		//------------------------------------------------------------
		// 게임서버 캐릭터 스폰
		// int64 AccountId
		// int32 PlayerDBId
		// wstring SpawnObjectName
		// st_GameObjectInfo GameObjectInfo
		//------------------------------------------------------------	
		en_PACKET_S2C_SPAWN,

		//------------------------------------------------------------
		// 게임서버 캐릭터 디스폰
		// int64 AccountId
		// int32 PlayerDBId
		//------------------------------------------------------------
		en_PACKET_S2C_DESPAWN,

		//------------------------------------------------------------
		// 게임서버 오브젝트 스탯 변경
		// int64 AccountId
		// int32 PlayerDBId
		// st_StatInfo ChangeStatInfo
		//------------------------------------------------------------
		en_PACKET_S2C_OBJECT_STAT_CHANGE,

		//------------------------------------------------------------
		// 게임서버 왼쪽 월드 마우스 정보 요청	
		// int64 AccountId
		// int32 PlayerDBId
		// int64 ObjectID
		// int16 ObjectType
		//------------------------------------------------------------
		en_PACKET_C2S_LEFT_MOUSE_OBJECT_INFO,

		//------------------------------------------------------------
		// 게임서버 왼쪽 월드 마우스 정보 요청 응답
		// int64 AccountId
		// int64 PlayerDBId
		// st_GameObjectInfo ObjectInfo
		//------------------------------------------------------------
		en_PACKET_S2C_LEFT_MOUSE_OBJECT_INFO,

		//------------------------------------------------------------
		// 게임서버 왼쪽 UI 마우스 정보 요청 
		// int64 AccountID
		// int64 PlayerID
		// en_GameObjectType ObjectType
		//------------------------------------------------------------
		en_PACKET_C2S_LEFT_MOUSE_UI_OBJECT_INFO,

		//------------------------------------------------------------
		// 게임서버 왼쪽 UI 마우스 정보 요청 응답
		//------------------------------------------------------------
		en_PACKET_S2C_LEFT_MOUSE_UI_OBJECT_INFO,

		//------------------------------------------------------------
		// 게임서버 오른쪽 마우스 정보 요청	
		// int64 AccountId
		// int64 PlayerDBId
		// int64 ObjectID
		// int16 ObjectType
		//------------------------------------------------------------
		en_PACKET_C2S_RIGHT_MOUSE_OBJECT_INFO,

		//------------------------------------------------------------
		// 게임서버 오른쪽 마우스 정보 요청 응답
		// int64 ReqPlayerID
		// int64 FindObjectID
		// inf16 FindObjectType
		//------------------------------------------------------------
		en_PACKET_S2C_RIGHT_MOUSE_OBJECT_INFO,

		//------------------------------------------------------------
		// 게임서버 제작대 제작 남은 시간
		// int64 CraftingTableObjectID	
		// st_ItemInfo CraftingItemInfo
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_CRAFT_REMAIN_TIME,

		//------------------------------------------------------------
		// 게임서버 제작대 선택 풀림 요청
		// int64 AccountID
		// int64 PlayerID
		// int64 CraftingTableObjectID
		// int16 CraftingTableObjectType
		//------------------------------------------------------------
		en_PACKET_C2S_CRAFTING_TABLE_NON_SELECT,

		//------------------------------------------------------------
		// 게임서버 제작대 선택 풀림 요청 응답
		// int64 CraftingTableObjectID
		// int16 CraftingTableObjectType
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_NON_SELECT,

		//------------------------------------------------------------
		// 게임서버 오브젝트 상태 변경 요청 응답
		// int64 ObjectId	
		// en_GameObjectType ObjectType
		// en_CreatureState ObjectState	
		//------------------------------------------------------------
		en_PACKET_S2C_OBJECT_STATE_CHANGE,

		//------------------------------------------------------------
		// 게임서버 상태이상 적용 
		// int64 ObjectId
		// bool SetStatusAbnormal
		// int8 StatusAbnormal
		//------------------------------------------------------------
		en_PACKET_S2C_STATUS_ABNORMAL,

		//------------------------------------------------------------
		// 게임서버 오브젝트 죽음 응답
		// int64 ObjectId	
		//------------------------------------------------------------
		en_PACKET_S2C_DIE,

		//------------------------------------------------------------
		// 게임서버 채팅 메세지 요청
		// int32 ObjectId
		// string Message
		//------------------------------------------------------------
		en_PACKET_C2S_MESSAGE,

		//------------------------------------------------------------
		// 게임서버 채팅 메세지 요청
		// int32 ObjectId
		// string Message
		//------------------------------------------------------------
		en_PACKET_S2C_MESSAGE,

		//------------------------------------------------------------
		// 게임서버 줍기 요청
		// int64 AccountId
		// int64 PlayerId
		// st_PositionInfo LootingPositionInfo	
		//------------------------------------------------------------
		en_PACKET_C2S_LOOTING,

		//------------------------------------------------------------
		// 게임서버 줍기 요청 응답
		// int64 TargetObjectId
		// st_ItemInfo ItemInfo
		//------------------------------------------------------------
		en_PACKET_S2C_LOOTING,

		//------------------------------------------------------------
		// 게임서버 아이템 버리기 요청
		// int64 AccountID
		// int64 PlayerID
		// en_SmallItemType DropItemType
		// int32 DropItemCount
		//------------------------------------------------------------
		en_PACKET_C2S_ITEM_DROP,

		//------------------------------------------------------------
		// 게임서버 아이템 버리기 요청 응답
		// st_GameObjectInfo DropGameObjectInfo
		//------------------------------------------------------------
		en_PACKET_S2C_ITEM_DROP,

		//------------------------------------------------------------
		// 게임서버 아이템 제작대에 넣기 요청
		// int64 AccountID
		// int64 PlayerDBID
		// int64 CraftingTableObjectID
		// int16 CraftingTableGameObjectType
		// int16 InputItemSmallCategory 
		//------------------------------------------------------------
		en_PACKET_C2S_CRAFTING_TABLE_ITEM_ADD,

		//------------------------------------------------------------
		// 게임서버 아이템 제작대에 넣기 요청 응답
		// int64 CraftingTableObjectID
		// int16 MaterialItemsSize
		// map MaterialItems
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_ITEM_ADD,

		//------------------------------------------------------------
		// 게임서버 아이템 제작대에서 재료 아이템 빼기 요청
		// int64 AccountID
		// int64 PlayerID
		// int64 OwnerCraftingTableObjectID
		// en_GameObjectType OwnerCraftingTableObjectType
		// en_SmallCategory MaterialItemType
		//------------------------------------------------------------
		en_PACKET_C2S_CRAFTING_TABLE_MATERIAL_ITEM_SUBTRACT,

		//------------------------------------------------------------
		// 게임서버 아이템 제작대에서 완성 아이템 빼기 요청
		// int64 AccountID
		// int64 PlayerID
		// int64 OwnerCraftingTableObjectID
		// en_GameObjectType OwnerCraftingTableObjectType
		// en_SmallCategory CompleteItemType
		//------------------------------------------------------------
		en_PACKET_C2S_CRAFTING_TABLE_COMPLETE_ITEM_SUBTRACT,

		//------------------------------------------------------------
		// 게임서버 아이템 제작대에서 빼기 요청 응답
		// int64 PlayerID
		// int64 CraftingTableObjectID
		// st_ItemInfo MaterialItemInfo
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_ITEM_SUBTRACT,

		//------------------------------------------------------------
		// 게임서버 제작대 제작 아이템 선택 응답
		// int64 CraftingTableObjectID
		// en_SmallItemCategory SelectCompleteItemType
		// map MaterialItems
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_COMPLETE_ITEM_SELECT,

		//------------------------------------------------------------
		// 게임서버 제작대 제작 시작
		// int64 AccountID
		// int64 PlayerID
		// int64 CraftingTableObjectID
		// en_GameObject CraftingTableObjectType
		// en_SmallItemCateogry CraftingCompleteItem
		// int16 CraftingCount
		//------------------------------------------------------------
		en_PACKET_C2S_CRAFTING_TABLE_CRAFTING_START,

		//------------------------------------------------------------
		// 게임서버 제작대 제작 시작 응답
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_CRAFTING_START,

		//------------------------------------------------------------
		// 게임서버 제작대 제작 시작 멈춤 요청
		// int64 AccountID
		// int64 PlayerID
		// int64 CraftingTableObjectID
		//------------------------------------------------------------
		en_PACKET_C2S_CRAFTING_TABLE_CRAFTING_STOP,

		//------------------------------------------------------------
		// 게임서버 제작대 제작 시작 멈춤 요청 응답	
		// int64 CraftingTableObjectID
		// st_ItemInfo CraftingItemInfo
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_CRAFTING_STOP,

		//------------------------------------------------------------
		// 게임서버 제작대 재료 목록 
		// int64 CraftingTableObjectID
		// map MaterialItems
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_MATERIAL_ITEM_LIST,

		//------------------------------------------------------------
		// 게임서버 제작대 제작템 목록
		// int64 CraftingtableObjectID
		// map CompleteItems
		//------------------------------------------------------------
		en_PACKET_S2C_CRAFTING_TABLE_COMPLETE_ITEM_LIST,

		//------------------------------------------------------------
		// 게임서버 아이템 선택 요청
		// int64 AccountId
		// int64 ObjectId
		// int16 SelectItemTileGridPositionX
		// int16 SelectItemTileGridPositionY
		//------------------------------------------------------------
		en_PACKET_C2S_ITEM_SELECT,

		//------------------------------------------------------------
		// 게임서버 아이템 선택 요청 응답
		// int64 AccountId
		// int64 ObjectId
		// CItem* SelectItem	
		//------------------------------------------------------------
		en_PACKET_S2C_ITEM_SELECT,

		//------------------------------------------------------------
		// 게임서버 아이템 회전 요청
		// int64 AccountId
		// int64 ObjectId	
		// en_SmallItemCategory RotateItemSmallCategory
		//------------------------------------------------------------
		en_PACKET_C2S_ITEM_ROTATE,

		//------------------------------------------------------------
		// 게임서버 아이템 회전 요청 응답
		// int16 AccountID
		// int64 ObjectID	
		//------------------------------------------------------------
		en_PACKET_S2C_ITEM_ROTATE,

		//------------------------------------------------------------
		// 게임서버 아이템 놓기 요청
		// int64 AccountId
		// int64 ObjectId	
		// int16 PlaceTileGridPositionX
		// int16 PlaceTileGridPositionY
		//------------------------------------------------------------
		en_PACKET_C2S_ITEM_PLACE,

		//------------------------------------------------------------
		// 게임서버 아이템 놓기 요청 응답
		// int64 AccountId
		// int64 ObjectId
		// st_ItemInfo OverlapItemInfo
		//------------------------------------------------------------
		en_PACKET_S2C_ITEM_PLACE,

		//------------------------------------------------------------
		// 게임서버 오브젝트 위치 강제 조정
		// int64 TargetObjectId	
		// st_Position SyncPosition	
		//------------------------------------------------------------
		en_PACKET_S2C_SYNC_OBJECT_POSITION,

		//------------------------------------------------------------
		// 게임서버 스킬 특성 선택 요청
		// int64 AccountID
		// int64 PlayerID
		// en_SkillCharacteristic SkillCharacteristicType
		//------------------------------------------------------------
		en_PACKET_C2S_SELECT_SKILL_CHARACTERISTIC,

		//------------------------------------------------------------
		// 게임서버 스킬 특성 선택 요청 응답
		// int8 SkillCharacteristicIndex
		// en_SkillCharacteristic SkillCharacteristicType
		// CSkill[] PassiveSkills
		// CSkill[] ActiveSkills
		//------------------------------------------------------------
		en_PACKET_S2C_SELECT_SKILL_CHARACTERISTIC,

		//------------------------------------------------------------
		// 게임서버 스킬 배우기 요청
		// int64 AccountID
		// int64 PlayerID
		// en_SkillCharacteristic SkillCharacteristicType
		// en_SkillType SkillType
		//------------------------------------------------------------
		en_PACKET_C2S_LEARN_SKILL,

		//------------------------------------------------------------
		// 게임서버 스킬 배우기 요청 응답
		// int8 SkillCharacteristicIndex	
		// en_SkillCharacteristic SkillCharacteristicType
		// en_SkillType SkillType
		//------------------------------------------------------------
		en_PACKET_S2C_LEARN_SKILL,

		//------------------------------------------------------------
		// 게임서버 스킬 저장 요청
		// int64 TargetObjectId
		// st_SkillInfo SkillInfo
		//------------------------------------------------------------
		en_PACKET_C2S_SKILL_TO_SKILLBOX,

		//------------------------------------------------------------
		// 게임서버 스킬 저장 요청 응답
		// int64 TargetObjectId
		// st_SkillInfo SkillInfo
		//------------------------------------------------------------
		en_PACKET_S2C_SKILL_TO_SKILLBOX,

		//------------------------------------------------------------
		// 게임서버 퀵슬롯 생성 
		// int8 QuickSlotBarSize
		// int8 QuickSlotBarSlotSize
		// st_QuickSlotBarSlotInfo[] QuickSlotBarSlotInfos
		//------------------------------------------------------------
		en_PACKET_S2C_QUICKSLOT_CREATE,

		//------------------------------------------------------------
		// 게임서버 스킬 저장 요청 응답
		// int64 AccountId
		// int64 PlayerId 
		// st_SkillInfo SkillInfo
		//------------------------------------------------------------
		en_PACKET_C2S_QUICKSLOT_SAVE,

		//-----------------------------------------------------------
		// 게임서버 스킬 저장 요청 응답
		// int64 AccountId
		// int64 PlayerId 
		// st_SkillInfo SkillInfo
		//------------------------------------------------------------
		en_PACKET_S2C_QUICKSLOT_SAVE,

		//-----------------------------------------------------------
		// 게임서버 쿨타임 스타트
		// int64 PlayerId
		// int8 QuickSlotBarIndex;
		// int8 QuickSlotBarSlotIndex;
		// float SkillCoolTime;
		// float SkillCoolTimeSpeed;
		//-----------------------------------------------------------
		en_PACKET_S2C_COOLTIME_START,

		//------------------------------------------------------------
		// 게임서버 아이템 스왑 요청
		// int64 AccountId
		// int64 ObjectId
		// int8 SwapQuickSlotBarIndexA
		// int8 SwapQuickSlotBarSlotIndexA
		// int8 SwapQuickSlotBarIndexB
		// int8 SwapQuickSlotBarSlotIndexB
		//------------------------------------------------------------
		en_PACKET_C2S_QUICKSLOT_SWAP,

		//------------------------------------------------------------
		// 게임서버 아이템 스왑 요청 응답
		// int64 ObjectId
		// st_QuickSlotInfo SwapQuickSlotAItem
		// st_QuickSlotInfo SwapQuickSlotBItem
		//------------------------------------------------------------
		en_PACKET_S2C_QUICKSLOT_SWAP,

		//------------------------------------------------------------
		// 게임서버 퀵슬롯 초기화 요청
		// int64 ObjectId
		// int8 QuickSlotBarIndexA
		// int8 QuickSlotBarSlotIndexA	
		//------------------------------------------------------------
		en_PACKET_C2S_QUICKSLOT_EMPTY,

		//------------------------------------------------------------
		// 게임서버 퀵슬롯 초기화 요청 응답
		// int64 ObjectId
		// int8 QuickSlotBarIndexA
		// int8 QuickSlotBarSlotIndexA	
		//------------------------------------------------------------
		en_PACKET_S2C_QUICKSLOT_EMPTY,

		//------------------------------------------------------------
		// 게임서버 제작템 목록
		// int64 ObjectId
		// st_CraftingItemCategory CraftingItemCategory;
		//------------------------------------------------------------	
		en_PACKET_S2C_CRAFTING_LIST,

		//------------------------------------------------------------
		// 게임서버 제작 요청
		// int64 AccountId
		// int64 PlayerId
		// en_ItemType CompleteItemType
		// int8 MaterialCount
		// st_ItemInfo[] Materials
		//------------------------------------------------------------
		en_PACKET_C2S_CRAFTING_CONFIRM,

		//------------------------------------------------------------
		// 게임서버 인벤토리 아이템 업데이트
		// int64 AccountId
		// int64 PlayerId
		// st_ItemInfo CompleteItem		
		//------------------------------------------------------------
		en_PACKET_S2C_INVENTORY_ITEM_UPDATE,

		//------------------------------------------------------------
		// 게임서버 인벤토리 아이템 사용 요청
		// int64 AccountId
		// int64 PlayerId
		// st_ItemInfo UseItemInfo
		//------------------------------------------------------------
		en_PACKET_C2S_INVENTORY_ITEM_USE,

		//------------------------------------------------------------
		// 게임서버 인벤토리 아이템 사용 요청 응답
		// int64 PlayerId
		// st_ItemInfo UseItemInfo
		//------------------------------------------------------------
		en_PACKET_S2C_INVENTORY_ITEM_USE,

		//------------------------------------------------------------
		// 게임서버 장비착용 응답
		// int64 PlayerId	
		// st_ItemInfo Equipment
		//------------------------------------------------------------
		en_PACKET_S2C_ON_EQUIPMENT,

		//------------------------------------------------------------
		// 게임서버 장비 해제 요청
		// int64 AccountID	
		// int64 PlayerID
		// st_ItemInfo OffEquipmentItemInfo
		//------------------------------------------------------------
		en_PACKET_C2S_OFF_EQUIPMENT,

		//------------------------------------------------------------
		// 게임서버 장비 해제 요청 응답
		// int64 AccountID
		// int64 PlayerID
		// en_EquipmentPart EquipmentPart
		//------------------------------------------------------------
		en_PACKET_S2C_OFF_EQUIPMENT,

		//------------------------------------------------------------
		// 게임서버 경험치 응답
		// int64 AccountId
		// int64 PlayerId
		// int64 GainExp
		// int64 CurrentExp
		// int64 RequireExp
		// int64 TotalExp
		//------------------------------------------------------------
		en_PACKET_S2C_EXPERIENCE,

		//------------------------------------------------------------
		// 게임서버 버프 패킷
		// int64 PlayerId
		// st_SkillInfo SkillInfo
		//------------------------------------------------------------
		en_PACKET_S2C_BUF_DEBUF,

		//------------------------------------------------------------
		// 게임서버 강화효과 약화효과 끄기 패킷
		// int64 TargetObjectId
		// en_SkillType OffSkillType
		//------------------------------------------------------------
		en_PACKET_S2C_BUF_DEBUF_OFF,

		//------------------------------------------------------------
		// 게임서버 연속기 스킬 켜기 패킷
		// int8 QuickSlotBarIndex
		// int8 QuickSlotBarSlotIndex
		// st_SkillInfo ComboSkillInfo
		//------------------------------------------------------------
		en_PACKET_S2C_COMBO_SKILL_ON,

		//------------------------------------------------------------
		// 게임서버 연속기 스킬 끄기 패킷
		// int8 QuickSlotBarIndex
		// int8 QuickSlotBarSlotIndex
		// en_SkillType ComboSkilltype
		//------------------------------------------------------------
		en_PACKET_S2C_COMBO_SKILL_OFF,

		//-----------------------------------------------------------
		// 게임서버 개인 메세지 전송
		// int8 MessageCount
		// wstring Messages	
		//-----------------------------------------------------------
		en_PACKET_S2C_GLOBAL_MESSAGE,

		//-----------------------------------------------------------
		// 게임서버 씨앗 심기 요청
		// int64 AccountID
		// int64 PlayerID
		// int16 SeedItemSmallCategory
		//-----------------------------------------------------------
		en_PACKET_C2S_SEED_FARMING,

		//-----------------------------------------------------------
		// 게임서버 씨앗 심기 요청 응답
		//-----------------------------------------------------------
		en_PACKET_S2C_SEED_FARMING,

		//-----------------------------------------------------------
		// 게임서버 작물 성장 확인 요청
		// int64 AccountID
		// int64 PlayerID
		// int16 PlantObjectType
		//-----------------------------------------------------------
		en_PACKET_C2S_PLANT_GROWTH_CHECK,

		//-----------------------------------------------------------
		// 게임서버 작물 성장 확인 요청
		// int64 PlantObjectID
		// int8 PlantGrowthStep	
		//-----------------------------------------------------------
		en_PACKET_S2C_PLANT_GROWTH_CHECK,

		//-----------------------------------------------------------
		// 게임서버 그룹 요청 
		// int64 AccountID
		// int64 PlayerID
		// int64 PartyPlayerID
		//-----------------------------------------------------------
		en_PACKET_C2S_PARTY_INVITE,

		//-----------------------------------------------------------
		// 게임서버 그룹 요청 응답
		// int64 PartyPlayerID
		//-----------------------------------------------------------
		en_PACKET_S2C_PARTY_INVITE,

		//-----------------------------------------------------------
		// 게임서버 그룹 요청 수락 
		// int64 AccountID
		// int64 PlayerID
		// int64 ReqPartyPlayerID
		//-----------------------------------------------------------
		en_PACKET_C2S_PARTY_INVITE_ACCEPT,

		//-----------------------------------------------------------
		// 게임서버 그룹 요청 수락 응답	
		// int64 ReqPartyPlayerID	
		//-----------------------------------------------------------
		en_PACKET_S2C_PARTY_INVITE_ACCEPT,

		//-----------------------------------------------------------
		// 게임서버 그룹 요청 거절
		// int64 AccountID
		// int64 PlayerID		
		// int64 ReqPartyInvitePlayerObjectID
		//-----------------------------------------------------------
		en_PACKET_C2S_PARTY_INVITE_REJECT,

		//-----------------------------------------------------------
		// 게임서버 그룹 요청 거절 응답
		// string PartyInviteRejectPlayerName
		//-----------------------------------------------------------
		en_PACKET_S2C_PARTY_INVITE_REJECT,

		//-----------------------------------------------------------
		// 게임서버 그룹 탈퇴 요청
		// int64 PlayerID
		// int64 AccountID
		//-----------------------------------------------------------
		en_PACKET_C2S_PARTY_QUIT,

		//----------------------------------------------------------
		// 게임서버 그룹 탈퇴 요청 응답
		// int64 QuitPartyPlayer
		//----------------------------------------------------------
		en_PACKET_S2C_PARTY_QUIT,

		//-----------------------------------------------------------
		// 게임서버 그룹 추방 요청
		// int64 PlayerID
		// int64 AccountID
		// int64 BanishPlayerID
		//-----------------------------------------------------------
		en_PACKET_C2S_PARTY_BANISH,

		//-----------------------------------------------------------
		// 게임서버 그룹 추방 요청 응답	
		// int64 BanishPlayerID
		//-----------------------------------------------------------
		en_PACKET_S2C_PARTY_BANISH,

		//-----------------------------------------------------------
		// 게임서버 그룹장 위임 요청
		// int64 PlayerID
		// int64 AccountID
		// int64 PartyLeaderMandatePlayerID
		//-----------------------------------------------------------
		en_PACKET_C2S_PARTY_LEADER_MANDATE,

		//-----------------------------------------------------------
		// 게임서버 그룹장 위임 요청	
		// int64 PartyLeaderMandatePlayerID
		//-----------------------------------------------------------
		en_PACKET_S2C_PARTY_LEADER_MANDATE,

		//-----------------------------------------------------------
		// 게임서버 메뉴 요청
		// int64 PlayerID
		// int64 AccountID
		// en_MenuType MenuType
		//-----------------------------------------------------------
		en_PACKET_C2S_MENU,

		//-----------------------------------------------------------
		// 게임서버 메뉴 요청 응답
		// en_MenuType MenuType
		//-----------------------------------------------------------
		en_PACKET_S2C_MENU,

		//-----------------------------------------------------------
		// 게임서버 레이캐스팅 응답
		// int64 ObjectID
		// int64 RayCastingStartPosition
		// int64 RayCastingEndPosition
		//-----------------------------------------------------------
		en_PACKET_S2C_RAY_CASTING,

		//-----------------------------------------------------------
		// 게임서버 충돌체 정보
		// float PositionX
		// float PositionY
		// float DirectionX
		// float DirectionY
		// float SizeX
		// float SizeY
		//-----------------------------------------------------------
		en_PACKET_S2C_COLLISION,

		//-----------------------------------------------------------
		// 게임서버 시간 요청 
		// int64 AccountID
		// int64 PlayerID
		//-----------------------------------------------------------
		en_PACKET_C2S_SERVER_TIME,

		//-----------------------------------------------------------
		// 게임서버 시간 요청 응답
		// int64 ServerDayTime
		//-----------------------------------------------------------
		en_PACKET_S2C_SERVER_TIME,

		//-----------------------------------------------------------
		// 게임서버 클라 퐁 전송	
		//-----------------------------------------------------------
		en_PACKET_C2S_PONG,

		//-----------------------------------------------------------
		// 게임서버 서버 핑 전송
		// int64 AccountId
		// int64 PlayerId
		//-----------------------------------------------------------
		en_PACKET_S2C_PING
	};


    public enum en_LOGIN_SERVER_PACKET_TYPE
    {
		////////////////////////////////////////////////////////
		//
		//	Client & LoginServer Protocol
		//
		////////////////////////////////////////////////////////

		//------------------------------------------------------
		// Login Server
		//------------------------------------------------------
		en_PACKET_CS_LOGIN_SERVER = 1000,

		//----------------------------------
		// 로그인 서버 회원가입 요청
		// wstring AccountName
		// wstring Password
		//----------------------------------
		en_LOGIN_SERVER_C2S_ACCOUNT_NEW,

		//----------------------------------
		// 로그인 서버 회원가입 요청 응답
		// bool AccountNewSuccess
		//----------------------------------
		en_LOGIN_SERVER_S2C_ACCOUNT_NEW,

		//----------------------------------
		// 로그인 서버 로그인 요청
		// wstring AccountName
		// wstring Password
		//----------------------------------
		en_LOGIN_SERVER_C2S_ACCOUNT_LOGIN,

		//----------------------------------
		// 로그인 서버 로그인 요청 응답
		// en_LoginInfo LoginInfo
		// int64 AccountID
		// wstring AccountName
		// int8 TokenLen
		// byte Token[]
		// int8 ServerListSize
		// vector<st_ServerInfo> ServerLists
		//----------------------------------
		en_LOGIN_SERVER_S2C_ACCOUNT_LOGIN,

		//---------------------------------
		// 로그인 서버 로그아웃 요청 
		// int64 AccountID	
		//---------------------------------
		en_LOGIN_SERVER_C2S_ACCOUNT_LOGOUT,

		//---------------------------------
		// 로그인 서버 로그아웃 요청 응답
		//---------------------------------
		en_LOGIN_SERVER_S2C_ACCOUNT_LOGOUT,

		//--------------------------------
		// 로그인 서버 로그인 상태 바꾸기 요청
		// int64 AccountID
		// en_LoginState LoginState
		//--------------------------------
		en_LOGIN_SERVER_C2S_LOGIN_STATE_CHANGE
	};
}

