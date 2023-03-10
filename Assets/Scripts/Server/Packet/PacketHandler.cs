using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Packet
{
    class PacketHandler
    {
        public static void S2C_ConnectHandler(CMessage S2CConnectPacket)
        {
            //Debug.Log("게임 서버 연결 성공");

            S2CConnectPacket.Dispose();

            bool IsDummy = false;
            // Account 서버로부터 받은 AccountId와 Token을 들고 게임서버 로그인
            CMessage ReqGameServerLoginPacket = MakePacket.ReqGameServerLoginPacket(Managers.NetworkManager._AccountId,
                Managers.NetworkManager._LoginID,
                50,
                Managers.NetworkManager._Token,
                IsDummy);
            Managers.NetworkManager.GameServerSend(ReqGameServerLoginPacket);
        }

        public static void S2C_LoginHandler(CMessage S2CLoginPacket)
        {
            // 로그인에 성공했는지 여부 읽어오기
            bool GameServerLoginStatus;
            S2CLoginPacket.GetData(out GameServerLoginStatus, sizeof(bool));

            if (GameServerLoginStatus == true)
            {
                // 게임서버 로그인 성공
                // 로그인 성공한 대상이 소유하고 있는 캐릭터의 개수 읽어오기
                byte PlayerCount;
                S2CLoginPacket.GetData(out PlayerCount, sizeof(byte));

                // 캐릭터의 개수 만큼 st_GameObjectInfo 생성 후 읽어오기
                st_GameObjectInfo[] ObjectInfos = new st_GameObjectInfo[PlayerCount];
                S2CLoginPacket.GetData(ObjectInfos, PlayerCount);

                // 캐릭터 선택창 정보 셋팅 후 UI 출력
                UI_LoginScene LoginSceneUI = Managers.UI._SceneUI as UI_LoginScene;
                if (LoginSceneUI != null)
                {
                    LoginSceneUI._CharacterChoiceUI.SetCharacterChoiceItem(ObjectInfos);
                }
                else
                {
                    Debug.Log("S2C_LoginHandler LoginSceneUI을 찾을 수 없음");
                }
            }
            else
            {
                Debug.Log("S2C_LoginHandler 게임 서버 로그인 실패");
            }

            S2CLoginPacket.Dispose();
        }

        // 캐릭터 생성요청 응답 처리
        public static void S2C_CreateCharacterHandler(CMessage S2CCreateCharacerPacket)
        {
            bool Success;

            S2CCreateCharacerPacket.GetData(out Success, sizeof(bool));

            // 캐릭터 생성 성공
            if (Success == true)
            {
                UI_LoginScene LoginSceneUI = Managers.UI._SceneUI as UI_LoginScene;
                if (LoginSceneUI != null)
                {
                    // 캐릭터 생성에 성공하면 캐릭터 생성창 UI를 비활성화
                    LoginSceneUI._CharacterCreateUI.gameObject.SetActive(false);

                    // 생성한 플레이어정보 얻어오고
                    st_GameObjectInfo PlayerInfo;
                    S2CCreateCharacerPacket.GetData(out PlayerInfo);
                    // 정보 셋팅
                    Managers.NetworkManager._PlayerDBId = PlayerInfo.ObjectId;
                    Managers.NetworkManager._PlayerName = PlayerInfo.ObjectName;

                    // 캐릭터 선택창 활성화
                    LoginSceneUI._CharacterChoiceUI.gameObject.SetActive(true);
                    // 캐릭터 선택창 정보 셋팅
                    LoginSceneUI._CharacterChoiceUI.SetCharacterChoiceItem(PlayerInfo.PlayerSlotIndex, PlayerInfo);
                }
                else
                {
                    Debug.Log("S2C_CreateChracterHandler LoginSceneUI을 찾을 수 없음");
                }
            }
            else
            {
                Debug.Log("S2C_CreateCharacterHandler 캐릭터 생성에 실패함");
            }

            S2CCreateCharacerPacket.Dispose();
        }

        public static void S2C_EnterGameHandler(CMessage S2CEnterGamePacket)
        {
            bool EnterGameSuccess;
            st_GameObjectInfo PlayerInfo;
            int SpawnPositionX;
            int SpawnPositionY;

            S2CEnterGamePacket.GetData(out EnterGameSuccess, sizeof(bool));

            if (EnterGameSuccess == true)
            {
                S2CEnterGamePacket.GetData(out PlayerInfo);

                S2CEnterGamePacket.GetData(out SpawnPositionX, sizeof(int));
                S2CEnterGamePacket.GetData(out SpawnPositionY, sizeof(int));

                Vector2Int SpawnPosition = new Vector2Int(SpawnPositionX, SpawnPositionY);

                //Debug.Log("게임 입장 성공 " + " ObjectId : " + PlayerInfo.ObjectId.ToString() + " AccountID : " + Managers.NetworkManager._AccountId.ToString());

                PlayerObject EnterGamePlayerObject = Managers.Object.Add(PlayerInfo).GetComponent<PlayerObject>();
                if (EnterGamePlayerObject != null)
                {
                    UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

                    if (GameSceneUI != null)
                    {
                        GameSceneUI.Binding();

                        GameSceneUI._MyCharacterHUDUI._MyCharacterObject = EnterGamePlayerObject.GetComponent<BaseObject>();
                        GameSceneUI._MyCharacterHUDUI.MyCharacterHUDUpdate();

                        EnterGamePlayerObject.GetComponent<GameObjectMovement>().SetOwner(EnterGamePlayerObject);
                        EnterGamePlayerObject.GetComponent<GameObjectUI>().SetOwner(EnterGamePlayerObject);

                        Managers.Camera.CameraSetTarget(EnterGamePlayerObject.gameObject);

                        if(GameSceneUI._Minimap != null)
                        {
                            GameSceneUI._Minimap.MiniMapMyPositionUpdate(SpawnPositionX, SpawnPositionY);
                        }                
                    }
                    else
                    {
                        Debug.Log("S2C_EnterGameHandler GameSceneUI를 찾을 수 없음");
                    }
                }       
            }
            else
            {
                Debug.Log("S2C_EnterGameHandler 게임 입장 할 수 없음");
            }

            S2CEnterGamePacket.Dispose();
        }

        public static void S2C_CharacterInfoHandler(CMessage S2CCharacterInfoPacket)
        {
            long AccountId;
            long ObjectId;

            S2CCharacterInfoPacket.GetData(out AccountId, sizeof(long));
            S2CCharacterInfoPacket.GetData(out ObjectId, sizeof(long));

            //Debug.Log("캐릭터 정보 전달 받음 " + "AccountID : " + AccountId.ToString() + " ObjectId : " + ObjectId.ToString() + " NetworkManagerID : " +Managers.NetworkManager._PlayerDBId.ToString());

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                CreatureObject FindGameObject = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId).GetComponent<CreatureObject>();
                if (FindGameObject != null)
                {
                    long CurrentExperience;
                    long RequireExperience;
                    long TotalExperience;

                    S2CCharacterInfoPacket.GetData(out CurrentExperience, sizeof(long));
                    S2CCharacterInfoPacket.GetData(out RequireExperience, sizeof(long));
                    S2CCharacterInfoPacket.GetData(out TotalExperience, sizeof(long));

                    UI_PlayerExperience PlayerExperienceUI = GameSceneUI._PlayerExperienceUI;
                    PlayerExperienceUI.PlayerGainExp(0, CurrentExperience, RequireExperience, TotalExperience);

                    byte SkillPoint;
                    S2CCharacterInfoPacket.GetData(out SkillPoint, sizeof(byte));

                    FindGameObject._GameObjectInfo.ObjectSkillPoint = SkillPoint;

                    FindGameObject.SkillBoxUICreate();

                    // 스킬 데이터 가져오기
                    // 공용 스킬 패시브 개수
                    byte PublicPassiveSkillCount;
                    S2CCharacterInfoPacket.GetData(out PublicPassiveSkillCount, sizeof(byte));

                    if (PublicPassiveSkillCount > 0)
                    {
                        // 공용 패시브 스킬
                        st_SkillInfo[] PublicPassiveSkills = new st_SkillInfo[PublicPassiveSkillCount];
                        S2CCharacterInfoPacket.GetData(PublicPassiveSkills, PublicPassiveSkillCount);

                        Managers.SkillBox.CreatePublicChracteristicPassive(en_SkillCharacteristic.SKILL_CATEGORY_PUBLIC, PublicPassiveSkillCount, PublicPassiveSkills);
                    }

                    // 공용 스킬 액티브 개수
                    byte PublicActiveSkillCount;
                    S2CCharacterInfoPacket.GetData(out PublicActiveSkillCount, sizeof(byte));

                    if (PublicActiveSkillCount > 0)
                    {
                        // 공용 액티브 스킬
                        st_SkillInfo[] PublicActiveSkills = new st_SkillInfo[PublicActiveSkillCount];
                        S2CCharacterInfoPacket.GetData(PublicActiveSkills, PublicActiveSkillCount);

                        Managers.SkillBox.CreatePublicChracteristicActive(en_SkillCharacteristic.SKILL_CATEGORY_PUBLIC, PublicActiveSkillCount, PublicActiveSkills);
                    }

                    for (byte i = 0; i < 3; i++)
                    {
                        byte Characteristic;
                        S2CCharacterInfoPacket.GetData(out Characteristic, sizeof(byte));

                        if ((en_SkillCharacteristic)Characteristic != en_SkillCharacteristic.SKILL_CATEGORY_NONE)
                        {
                            byte CharacteristicPassiveSkillCount;
                            S2CCharacterInfoPacket.GetData(out CharacteristicPassiveSkillCount, sizeof(byte));

                            if (CharacteristicPassiveSkillCount > 0)
                            {
                                st_SkillInfo[] CharacteristicPassiveSkills = new st_SkillInfo[CharacteristicPassiveSkillCount];
                                S2CCharacterInfoPacket.GetData(CharacteristicPassiveSkills, CharacteristicPassiveSkillCount);

                                Managers.SkillBox.CreateChracteristicPassive(i, (en_SkillCharacteristic)Characteristic,
                                    CharacteristicPassiveSkillCount, CharacteristicPassiveSkills);
                            }

                            byte CharacteristicActiveSkillCount;
                            S2CCharacterInfoPacket.GetData(out CharacteristicActiveSkillCount, sizeof(byte));

                            if (CharacteristicActiveSkillCount > 0)
                            {
                                st_SkillInfo[] CharacteristicActiveSkills = new st_SkillInfo[CharacteristicActiveSkillCount];
                                S2CCharacterInfoPacket.GetData(CharacteristicActiveSkills, CharacteristicActiveSkillCount);

                                Managers.SkillBox.CreateChracteristicActive(i, (en_SkillCharacteristic)Characteristic,
                                   CharacteristicActiveSkillCount, CharacteristicActiveSkills);
                            }
                        }
                    }                    

                    // 가방 셋팅
                    byte InventoryWidth;  // 가방 너비
                    byte InventoryHeight; // 가방 높이

                    S2CCharacterInfoPacket.GetData(out InventoryWidth, sizeof(byte));
                    S2CCharacterInfoPacket.GetData(out InventoryHeight, sizeof(byte));

                    byte InventoryItemCount;
                    S2CCharacterInfoPacket.GetData(out InventoryItemCount, sizeof(byte));

                    CItem[] InventoryItems = new CItem[InventoryItemCount];
                    S2CCharacterInfoPacket.GetData(InventoryItems, InventoryItemCount);

                    //돈 정보 셋팅
                    long GoldCount;
                    short SliverCount;
                    short BronzeCount;

                    S2CCharacterInfoPacket.GetData(out GoldCount, sizeof(long));
                    S2CCharacterInfoPacket.GetData(out SliverCount, sizeof(short));
                    S2CCharacterInfoPacket.GetData(out BronzeCount, sizeof(short));

                    FindGameObject.InventoryCreate(InventoryWidth, InventoryHeight, InventoryItems, GoldCount, SliverCount, BronzeCount);

                    // 퀵슬롯 셋팅
                    byte QuickSlotBarSize;
                    byte QuickSlotBarSlotSize;
                    byte QuickSlotBarCount;

                    S2CCharacterInfoPacket.GetData(out QuickSlotBarSize, sizeof(byte));
                    S2CCharacterInfoPacket.GetData(out QuickSlotBarSlotSize, sizeof(byte));
                    S2CCharacterInfoPacket.GetData(out QuickSlotBarCount, sizeof(byte));

                    st_QuickSlotBarSlotInfo[] QuickSlotBarSlotInfos = new st_QuickSlotBarSlotInfo[QuickSlotBarCount];
                    S2CCharacterInfoPacket.GetData(QuickSlotBarSlotInfos, QuickSlotBarCount);

                    // 퀵슬롯 생성
                    Managers.QuickSlotBar.Init(QuickSlotBarSize, QuickSlotBarSlotSize);

                    // 생성한 퀵슬롯 업데이트
                    for (int i = 0; i < QuickSlotBarCount; i++)
                    {
                        Managers.QuickSlotBar.UpdateQuickSlotBarSlot(QuickSlotBarSlotInfos[i]);
                    }

                    // 퀵슬롯 UI 생성
                    UI_QuickSlotBarBox QuickSlotBarBoxUI = GameSceneUI._QuickSlotBarBoxUI;
                    QuickSlotBarBoxUI.UIQuickSlotBarBoxCreate(QuickSlotBarSize, QuickSlotBarSlotSize);

                    // 퀵슬롯 정보 업데이트
                    QuickSlotBarBoxUI.RefreshQuickSlotBarBoxUI();                    

                    // 착용 장비 개수
                    byte EquipmentCount;
                    S2CCharacterInfoPacket.GetData(out EquipmentCount, sizeof(byte));

                    if (EquipmentCount > 0)
                    {     
                        // 착용 장비 아이템 정보
                        CItem[] EquipmentItems = new CItem[EquipmentCount];
                        S2CCharacterInfoPacket.GetData(EquipmentItems, EquipmentCount);

                        FindGameObject.EquipmentBoxUICreate(EquipmentItems);                        
                    }                                        

                    ////조합템 정보 셋팅
                    //byte CraftItemCount;
                    //S2CCharacterInfoPacket.GetData(out CraftItemCount, sizeof(byte));

                    //st_CraftingItemCategory[] CraftingItemCategory = new st_CraftingItemCategory[CraftItemCount];
                    //S2CCharacterInfoPacket.GetData(CraftingItemCategory, CraftItemCount);

                    //GameObject CraftingBoxGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_NAME_CRAFTING_BOX, GameSceneUI.transform);
                    //UI_CraftingBox CraftingBoxUI = CraftingBoxGO.GetComponent<UI_CraftingBox>();
                    //CraftingBoxUI.GetComponent<RectTransform>().localPosition = new Vector3(600.0f, 200.0f, 0.0f);
                    //CraftingBoxUI.Binding();

                    //GameSceneUI._CraftingBoxUI = CraftingBoxUI;

                    //Managers.CraftingBox.Init(CraftingItemCategory);

                    //CraftingBoxUI.CraftingBoxCategoryUpdate(CraftingItemCategory);
                    //CraftingBoxUI.gameObject.SetActive(false);

                    //byte CraftingTableCount;
                    //S2CCharacterInfoPacket.GetData(out CraftingTableCount, sizeof(byte));

                    //// 제작대 조합템 정보 가져오기
                    //st_CraftingTableRecipe[] CraftingTables = new st_CraftingTableRecipe[CraftingTableCount];
                    //S2CCharacterInfoPacket.GetData(CraftingTables, CraftingTableCount);

                    //foreach (st_CraftingTableRecipe CraftingTable in CraftingTables)
                    //{
                    //    switch (CraftingTable.CraftingTableType)
                    //    {
                    //        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                    //            GameSceneUI._FurnaceUI._FurnaceCraftingTable = CraftingTable;
                    //            break;
                    //        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                    //            GameSceneUI._SawmillUI._SawmillCraftingTable = CraftingTable;
                    //            break;
                    //    }
                    //}

                    st_Day DayInfo = new st_Day();
                    S2CCharacterInfoPacket.GetData(out DayInfo);

                    //GameObject GameSceneGO = GameObject.Find("GameScene");
                    //GameScene GameScene = GameSceneGO.GetComponent<GameScene>();
                    //GameScene.GetDay().Init(DayInfo);
                }
                else
                {
                    Debug.Log("캐릭터 정보 전달 받기 캐릭터를 찾을 수 없음" + Managers.NetworkManager._PlayerDBId);
                }
            }

            S2CCharacterInfoPacket.Dispose();
        }

        public static void S2C_MoveHandler(CMessage S2CMovePacket)
        {
            long ObjectId;
            float DirectionX;
            float DirectionY;
            float PositionX;
            float PositionY;

            S2CMovePacket.GetData(out ObjectId, sizeof(long));
            S2CMovePacket.GetData(out DirectionX, sizeof(float));
            S2CMovePacket.GetData(out DirectionY, sizeof(float));
            S2CMovePacket.GetData(out PositionX, sizeof(float));
            S2CMovePacket.GetData(out PositionY, sizeof(float));

            BaseObject FindGameObject = Managers.Object.FindById(ObjectId).GetComponent<BaseObject>();
            if (FindGameObject != null)
            {
                Vector2 NewDirection = new Vector2(DirectionX, DirectionY);

                Vector2 NewFaceDirection = new Vector2(NewDirection.x + FindGameObject.transform.position.x, NewDirection.y + FindGameObject.transform.position.y);

                FindGameObject.transform.position = new Vector3(PositionX, PositionY, 0);
                FindGameObject.GetComponent<GameObjectMovement>().MoveOtherGameObject(NewDirection);
                FindGameObject.GetComponentInChildren<GameObjectRenderer>().FaceDirection(NewFaceDirection);
            }
            
            S2CMovePacket.Dispose();
        }

        public static void S2C_MoveStopHandler(CMessage S2CMoveStopPacket)
        {
            long ObjectId;
            float StopPositionX;
            float StopPositionY;

            S2CMoveStopPacket.GetData(out ObjectId, sizeof(long));
            S2CMoveStopPacket.GetData(out StopPositionX, sizeof(float));
            S2CMoveStopPacket.GetData(out StopPositionY, sizeof(float));
            
            BaseObject FindGameObject = Managers.Object.FindById(ObjectId).GetComponent<BaseObject>();
            if (FindGameObject != null) 
            {
                FindGameObject.GetComponent<GameObjectMovement>().MoveOtherGameObject(new Vector2(0, 0));

                FindGameObject.transform.position = new Vector3(StopPositionX, StopPositionY, 0);
            }

            S2CMoveStopPacket.Dispose();            
        }      

        public static void S2C_ItemMoveStartHandler(CMessage S2C_ItemMoveStartPacket)
        {
            st_GameObjectInfo ItemMoveStartObjectInfo;

            S2C_ItemMoveStartPacket.GetData(out ItemMoveStartObjectInfo);

            // 아이템을 찾음
            GameObject ItemGO = Managers.Object.FindById(ItemMoveStartObjectInfo.ObjectId);
            if (ItemGO != null)
            {
                // 아이템이 추적할 대상을 찾음
                GameObject ItemOwnerGO = Managers.Object.FindById(ItemMoveStartObjectInfo.OwnerObjectId);
                if (ItemOwnerGO != null)
                {
                    // 아이템 스크립트 가져오기
                    ItemController Item = ItemGO.GetComponent<ItemController>();
                    // 플레이어 스크립트 가져오기
                    PlayerObject ItemTarget = ItemOwnerGO.GetComponent<PlayerObject>();
                    // 아이템이 추적할 대상 설정
                    Item._Target = ItemTarget;
                    // 추적할 대상에게 다가감
                    Item.TargetFlyStart(ItemTarget._GameObjectInfo.ObjectStatInfo.Speed);
                }
            }
        }

        public static void S2C_CommonDamageHandler(CMessage S2C_CommonDamagePacket)
        {
            long AttackID;
            long TargetId;            
            short SkillType;
            short ResourceType;
            int DamagePoint;
            int HP;
            bool IsCritical;

            S2C_CommonDamagePacket.GetData(out AttackID, sizeof(long));
            S2C_CommonDamagePacket.GetData(out TargetId, sizeof(long));
            S2C_CommonDamagePacket.GetData(out SkillType, sizeof(short)); 
            S2C_CommonDamagePacket.GetData(out ResourceType, sizeof(short));
            S2C_CommonDamagePacket.GetData(out DamagePoint, sizeof(int));
            S2C_CommonDamagePacket.GetData(out HP, sizeof(int));            
            S2C_CommonDamagePacket.GetData(out IsCritical, sizeof(bool));

            // 크리티컬 공격 성공시 카메라 흔들기
            if (IsCritical && AttackID == Managers.NetworkManager._PlayerDBId)
            {
                CameraManager.CameraShake(0.3f);
            }

            BaseObject FindAttackerObject = Managers.Object.FindById(AttackID).GetComponent<BaseObject>();
            if(FindAttackerObject != null)
            {
                GameObjectAnimation Animator = FindAttackerObject.GetComponentInChildren<GameObjectAnimation>();
                if(Animator != null)
                {
                    Animator.PlayMeleeAnimation();
                }
            }

            GameObject FindTargetGameObject = Managers.Object.FindById(TargetId);
            if (FindTargetGameObject != null)
            {
                CreatureObject FindTargetCreature = FindTargetGameObject.GetComponent<CreatureObject>();

                if (FindTargetCreature != null)
                {
                    UI_Damage DamageUI = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_DAMAGE).GetComponent<UI_Damage>();
                    DamageUI.Setup((en_SkillType)SkillType, DamagePoint,
                        IsCritical,
                        FindTargetCreature.transform.position.x,
                        FindTargetCreature.transform.position.y);

                    Vector2 RandomDirection = Random.insideUnitCircle;
                    RandomDirection = RandomDirection.y < 0 ? new Vector2(RandomDirection.x, -RandomDirection.y) : RandomDirection;

                    DamageUI.GetComponent<RectTransform>().localPosition = new Vector3(
                        FindTargetCreature.transform.position.x + RandomDirection.x,
                        FindTargetCreature.transform.position.y + RandomDirection.y);

                    DamageUI.GetComponent<GameObjectAnimation>().PlayCommonDamage();

                    //FindTargetCreature._HpBar.ActiveChoiceUI(true);
                }
                else
                {
                    Debug.Log("S2C_Attack Player를 찾을 수 없습니다.");
                }


            }

            S2C_CommonDamagePacket.Dispose();
        }

        public static void S2C_GatheringDamageHandler(CMessage S2C_GatheringDamagePacket)
        {
            long TargetId;
            int DamagePoint;

            S2C_GatheringDamagePacket.GetData(out TargetId, sizeof(long));
            S2C_GatheringDamagePacket.GetData(out DamagePoint, sizeof(int));

            GameObject FindTargetGameObject = Managers.Object.FindById(TargetId);
            if (FindTargetGameObject != null)
            {
                CreatureObject FindTargetCreature = FindTargetGameObject.GetComponent<CreatureObject>();

                if (FindTargetCreature != null)
                {
                    FindTargetCreature._HpBar.ActiveChoiceUI(true);
                }
                else
                {
                    Debug.Log("S2C_Attack Player를 찾을 수 없습니다.");
                }


            }

            S2C_GatheringDamagePacket.Dispose();
        }

        public static void S2C_AttackHandler(CMessage S2CAttackPacket)
        {
            long ObjectId;
            long TargetId;
            short SkillType;
            int DamagePoint;
            bool IsCritical;

            S2CAttackPacket.GetData(out ObjectId, sizeof(long));
            S2CAttackPacket.GetData(out TargetId, sizeof(long));
            S2CAttackPacket.GetData(out SkillType, sizeof(short));
            S2CAttackPacket.GetData(out DamagePoint, sizeof(int));
            S2CAttackPacket.GetData(out IsCritical, sizeof(bool));

            // 크리티컬 공격 성공시 카메라 흔들기
            if (IsCritical && ObjectId == Managers.NetworkManager._PlayerDBId)
            {
                CameraManager.CameraShake(0.3f);
            }

            GameObject FindTargetGameObject = Managers.Object.FindById(TargetId);
            if (FindTargetGameObject != null)
            {
                CreatureObject FindTargetCreature = FindTargetGameObject.GetComponent<CreatureObject>();

                if (FindTargetCreature != null)
                {
                    

                    FindTargetCreature._HpBar.ActiveChoiceUI(true);
                }
                else
                {
                    Debug.Log("S2C_Attack Player를 찾을 수 없습니다.");
                }


            }

            S2CAttackPacket.Dispose();
        }

        public static void S2C_MagicHandler(CMessage S2CMagicPacket)
        {
            long ObjectId;
            bool SpellStart;
            short SkillType;
            float SpellTime;

            S2CMagicPacket.GetData(out ObjectId, sizeof(long));
            S2CMagicPacket.GetData(out SpellStart, sizeof(bool));
            S2CMagicPacket.GetData(out SkillType, sizeof(short));
            S2CMagicPacket.GetData(out SpellTime, sizeof(float));

            CreatureObject CC = Managers.Object.FindById(ObjectId).GetComponent<CreatureObject>();
            if (CC != null)
            {
                if (SpellStart == true)
                {
                    switch ((en_SkillType)SkillType)
                    {
                        case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_FLAME_HARPOON:
                            CC.SpellStart("불꽃작살", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ROOT:
                            CC.SpellStart("속박", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_CHAIN:
                            CC.SpellStart("얼음사슬", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_ICE_WAVE:
                            CC.SpellStart("냉기파동", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_LIGHTNING_STRIKE:
                            CC.SpellStart("낙뢰", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_SPELL_ACTIVE_ATTACK_HEL_FIRE:
                            CC.SpellStart("지옥의화염", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_DIVINE_STRIKE:
                            CC.SpellStart("신성한일격", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_DISCIPLINE_ACTIVE_ATTACK_ROOT:
                            CC.SpellStart("속박", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_LIGHT:
                            CC.SpellStart("치유의빛", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_DISCIPLINE_ACTIVE_HEAL_HEALING_WIND:
                            CC.SpellStart("치유의바람", SpellTime, 1.0f);
                            break;
                        case en_SkillType.SKILL_SLIME_ACTIVE_POISION_ATTACK:
                            CC.SpellStart("슬라임 독", SpellTime, 1.0f);
                            break;
                    }
                }
                else
                {
                    CC._SpellBar.SpellEnd();
                }
            }

            S2CMagicPacket.Dispose();
        }

        public static void S2C_MagicCancelHandler(CMessage S2CMagicCancelPacket)
        {
            long PlayerId;

            S2CMagicCancelPacket.GetData(out PlayerId, sizeof(long));

            PlayerObject FindPlayerObject = Managers.Object.FindById(PlayerId).GetComponent<PlayerObject>();
            if (FindPlayerObject != null)
            {
                FindPlayerObject._SpellBar.SpellEnd();
            }
            else
            {
                Debug.Log("마법 취소 요청한 유저를 찾을 수 없음");
            }

            S2CMagicCancelPacket.Dispose();
        }

        public static void S2C_GatheringCancelHandler(CMessage S2C_GatheringCancelPacket)
        {
            long ObjectID;

            S2C_GatheringCancelPacket.GetData(out ObjectID, sizeof(long));

            PlayerObject FindPlayerObject = Managers.Object.FindById(ObjectID).GetComponent<PlayerObject>();
            if (FindPlayerObject != null)
            {
                FindPlayerObject._GatheringBar.GatheringEnd();
            }
            else
            {
                Debug.Log("마법 취소 요청한 유저를 찾을 수 없음");
            }

            S2C_GatheringCancelPacket.Dispose();
        }

        public static void S2C_AnimationPlayHandler(CMessage S2C_AnimationPlayPacket)
        {
            long ObjectId;            
            string PlayAnimationName;

            S2C_AnimationPlayPacket.GetData(out ObjectId, sizeof(long));            
            S2C_AnimationPlayPacket.GetData(out PlayAnimationName);

            CreatureObject FindPlayerObject = Managers.Object.FindById(ObjectId).GetComponent<CreatureObject>();
            if (FindPlayerObject != null)
            {
                FindPlayerObject.AnimationPlay(PlayAnimationName);                
            }
        }

        public static void S2C_SpawnHandler(CMessage S2CSpawnPacket)
        {
            int SpawnCount;

            S2CSpawnPacket.GetData(out SpawnCount, sizeof(int));

            st_GameObjectInfo[] ObjectInfos = new st_GameObjectInfo[SpawnCount];
            S2CSpawnPacket.GetData(ObjectInfos, SpawnCount);

            //Debug.Log($"S2C_Spawn 호출 {SpawnCount} {ObjectInfos[0].ObjectName}");

            for (int i = 0; i < SpawnCount; i++)
            {
                Managers.Object.Add(ObjectInfos[i]);
            }

            S2CSpawnPacket.Dispose();
        }

        public static void S2C_DespawnHandler(CMessage S2CDeSpawnPacket)
        {
            int DeSpawnObjectCount;

            S2CDeSpawnPacket.GetData(out DeSpawnObjectCount, sizeof(int));

            long[] DeSpawnObjectIds = new long[DeSpawnObjectCount];
            S2CDeSpawnPacket.GetData(DeSpawnObjectIds, DeSpawnObjectCount);

            //Debug.Log($"S2C_DeSpawn 호출 {DeSpawnObjectCount}");

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            UI_TargetHUD TargetHUDUI = GameSceneUI._TargetHUDUI;

            for (int i = 0; i < DeSpawnObjectCount; i++)
            {
                if (TargetHUDUI._TargetObject != null
                    && TargetHUDUI._TargetObject._GameObjectInfo.ObjectId == DeSpawnObjectIds[i])
                {
                    TargetHUDUI.TargetHUDOff();
                }

                Managers.Object.Remove(DeSpawnObjectIds[i]);
            }

            S2CDeSpawnPacket.Dispose();
        }

        public static void S2C_ChangeObjectStatHandler(CMessage S2CChangeObjectStatPacket)
        {
            long ObjectId;
            st_StatInfo ChangeObjectStatInfo;

            S2CChangeObjectStatPacket.GetData(out ObjectId, sizeof(long));
            S2CChangeObjectStatPacket.GetData(out ChangeObjectStatInfo);

            GameObject FindGameObject = Managers.Object.FindById(ObjectId);
            if (FindGameObject == null)
            {
                return;
            }

            CreatureObject CC = FindGameObject.GetComponent<CreatureObject>();
            if (CC != null)
            {
                CC._GameObjectInfo.ObjectStatInfo = ChangeObjectStatInfo;
                CC.UpdateHPBar();
            }
            else
            {
                Debug.Log("CC가 없음");
            }

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            UI_TargetHUD TargetHUDUI = GameSceneUI._TargetHUDUI;

            if (TargetHUDUI._TargetObject != null
                && TargetHUDUI._TargetObject._GameObjectInfo.ObjectId == ObjectId)
            {
                TargetHUDUI.TargetHUDUpdate();
            }

            if (ObjectId == Managers.NetworkManager._PlayerDBId)
            {
                if (GameSceneUI._MyCharacterHUDUI != null)
                {
                    GameSceneUI._MyCharacterHUDUI.MyCharacterHUDUpdate();
                }
            }

            S2CChangeObjectStatPacket.Dispose();
        }

        public static void S2C_LeftMousePositionObjectInfoHandler(CMessage S2C_LeftMousePositionObjectInfoPacket)
        {
            long AccountId;
            long PreviousChoiceObjectId;
            long FindObjectId;
            short BufSize;
            short DeBufSize;

            S2C_LeftMousePositionObjectInfoPacket.GetData(out AccountId, sizeof(long));
            S2C_LeftMousePositionObjectInfoPacket.GetData(out PreviousChoiceObjectId, sizeof(long));
            S2C_LeftMousePositionObjectInfoPacket.GetData(out FindObjectId, sizeof(long));

            S2C_LeftMousePositionObjectInfoPacket.GetData(out BufSize, sizeof(short));
            st_SkillInfo[] BufSkillInfos = new st_SkillInfo[BufSize];
            S2C_LeftMousePositionObjectInfoPacket.GetData(BufSkillInfos, BufSize);

            S2C_LeftMousePositionObjectInfoPacket.GetData(out DeBufSize, sizeof(short));
            st_SkillInfo[] DeBufSkillInfos = new st_SkillInfo[DeBufSize];
            S2C_LeftMousePositionObjectInfoPacket.GetData(DeBufSkillInfos, DeBufSize);

            CreatureObject FindObject = Managers.Object.FindById(FindObjectId).GetComponent<CreatureObject>();
            if (FindObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                UI_TargetHUD TargetHUDUI = GameSceneUI._TargetHUDUI;
                if (TargetHUDUI != null)
                {
                    TargetHUDUI.TargetHUDOn(FindObject);

                    GameSceneUI.AddGameSceneUIStack(TargetHUDUI);

                    TargetHUDUI.gameObject.transform.SetAsLastSibling();

                    CreatureObject CC = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId).GetComponent<CreatureObject>();
                    if (CC != null)
                    {
                        CC._SelectTargetObjectInfo = FindObject._GameObjectInfo;
                    }

                    TargetHUDUI.TargetHUDUpdate();
                    TargetHUDUI.TargetHUDBufDebufEmpty();

                    for (int i = 0; i < BufSkillInfos.Length; i++)
                    {
                        if (FindObject._Bufs.Count > 0)
                        {
                            st_SkillInfo FindBufSkillInfo = FindObject._Bufs.Values
                            .FirstOrDefault(BufItem => BufItem.SkillType == BufSkillInfos[i].SkillType);
                            if (FindBufSkillInfo != null)
                            {
                                st_SkillInfo FindBufValue;
                                FindObject._Bufs.TryGetValue(BufSkillInfos[i].SkillType, out FindBufValue);
                                FindObject._Bufs[BufSkillInfos[i].SkillType] = BufSkillInfos[i];
                            }
                            else
                            {
                                FindObject._Bufs.Add(BufSkillInfos[i].SkillType, BufSkillInfos[i]);
                            }
                        }
                        else
                        {
                            FindObject._Bufs.Add(BufSkillInfos[i].SkillType, BufSkillInfos[i]);
                        }
                    }

                    for (int i = 0; i < DeBufSkillInfos.Length; i++)
                    {

                        if (FindObject._DeBufs.Count > 0)
                        {
                            st_SkillInfo FindDeBufSkillInfo = FindObject._DeBufs.Values
                            .FirstOrDefault(DeBufItem => DeBufItem.SkillType == DeBufSkillInfos[i].SkillType);
                            if (FindDeBufSkillInfo != null)
                            {
                                st_SkillInfo FindDefValue;
                                FindObject._DeBufs.TryGetValue(DeBufSkillInfos[i].SkillType, out FindDefValue);
                                FindObject._DeBufs[DeBufSkillInfos[i].SkillType] = DeBufSkillInfos[i];
                            }
                            else
                            {
                                FindObject._DeBufs.Add(DeBufSkillInfos[i].SkillType, DeBufSkillInfos[i]);
                            }
                        }
                        else
                        {
                            FindObject._DeBufs.Add(DeBufSkillInfos[i].SkillType, DeBufSkillInfos[i]);
                        }
                    }

                    // 위에서 설정한 정보를 토대로 Buf, DebufUI 업데이트

                    for (int i = 0; i < BufSkillInfos.Length; i++)
                    {
                        TargetHUDUI.TargetHUDBufUpdate(BufSkillInfos[i].SkillType);
                    }

                    for (int i = 0; i < DeBufSkillInfos.Length; i++)
                    {
                        TargetHUDUI.TargetHUDDeBufUpdate(DeBufSkillInfos[i].SkillType);
                    }
                }
            }

            if (PreviousChoiceObjectId != 0)
            {
                GameObject PreviousGameObject = Managers.Object.FindById(PreviousChoiceObjectId);
                if (PreviousGameObject != null)
                {
                    CreatureObject PreviousChoiceObject = PreviousGameObject.GetComponent<CreatureObject>();
                    if (PreviousChoiceObject != null)
                    {
                        PreviousChoiceObject._NameUI.ActiveChoiceUI(false);

                        PreviousChoiceObject._HpBar.SelectTargetHPBar(false);
                    }
                    else
                    {
                        Debug.Log("MousePositionObjectInfo CC를 찾을 수 없습니다.");
                    }
                }
            }

            FindObject._NameUI.ActiveChoiceUI(true);
            FindObject._HpBar.SelectTargetHPBar(true);

            S2C_LeftMousePositionObjectInfoPacket.Dispose();
        }

        public static void S2C_RightMouseObjectInfoHandler(CMessage S2C_RightMousePositionObjectInfoPacket)
        {
            long PlayerID;
            long FindObjectID;
            short FindObjectType;

            S2C_RightMousePositionObjectInfoPacket.GetData(out PlayerID, sizeof(long));
            S2C_RightMousePositionObjectInfoPacket.GetData(out FindObjectID, sizeof(long));
            S2C_RightMousePositionObjectInfoPacket.GetData(out FindObjectType, sizeof(short));

            GameObject FindObject = Managers.Object.FindById(FindObjectID);
            if (FindObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    switch ((en_GameObjectType)FindObjectType)
                    {
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                            FurnaceController FurnaceObject = FindObject.GetComponent<FurnaceController>();
                            if (FurnaceObject != null)
                            {
                                GameSceneUI._FurnaceUI.SetFurnaceController(FurnaceObject);

                                GameSceneUI.AddGameSceneUIStack(GameSceneUI._FurnaceUI);
                            }
                            break;
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                            SawmillController SawmillObject = FindObject.GetComponent<SawmillController>();
                            if (SawmillObject != null)
                            {
                                GameSceneUI._SawmillUI.SetSamillController(SawmillObject);

                                GameSceneUI.AddGameSceneUIStack(GameSceneUI._SawmillUI);
                            }
                            break;
                    }
                }
            }
            else
            {
                Debug.Log("RightMousePosition 게임오브젝트를 찾을 수 없습니다.");
            }
        }

        public static void S2C_CraftingTableCraftRemainTimeHandler(CMessage S2C_CraftingTableCraftRemainTimePacket)
        {
            long CraftingTableObjectID;
            st_ItemInfo CraftingItemInfo;

            S2C_CraftingTableCraftRemainTimePacket.GetData(out CraftingTableObjectID, sizeof(long));
            S2C_CraftingTableCraftRemainTimePacket.GetData(out CraftingItemInfo);

            GameObject FindCraftingTableObject = Managers.Object.FindById(CraftingTableObjectID);
            if (FindCraftingTableObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    UI_Furnace FurnaceUI = GameSceneUI._FurnaceUI;
                    if (FurnaceUI.gameObject.active == true)
                    {
                        //UI_CraftingCompleteItem CraftingCompleteItemUI = FurnaceUI._UICraftingCompleteItems.Values.FirstOrDefault(
                        //    CraftingCompleteItem => CraftingCompleteItem._CraftingCompleteItem.ItemSmallCategory == (en_SmallItemCategory)CraftingItemInfo.ItemSmallCategory);
                        //if (CraftingCompleteItemUI != null)
                        //{
                        //    CraftingCompleteItemUI.CraftingCompleteStart(CraftingItemInfo);
                        //}
                    }
                }
            }
        }

        public static void S2C_CraftingTableNonSelectHandler(CMessage S2C_CraftingTableNonSelectPacket)
        {
            long CraftingTableObjectID;
            short CraftingTableObjectType;

            S2C_CraftingTableNonSelectPacket.GetData(out CraftingTableObjectID, sizeof(long));
            S2C_CraftingTableNonSelectPacket.GetData(out CraftingTableObjectType, sizeof(short));

            GameObject FindCraftingTableObject = Managers.Object.FindById(CraftingTableObjectID);
            if (FindCraftingTableObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    switch ((en_GameObjectType)CraftingTableObjectType)
                    {
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                            if (GameSceneUI._FurnaceUI._FurnaceController != null && GameSceneUI._FurnaceUI._FurnaceController._GameObjectInfo.ObjectId == CraftingTableObjectID)
                            {
                                GameSceneUI._FurnaceUI._FurnaceController._SelectCompleteItemCategory = en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE;
                                GameSceneUI._FurnaceUI.ShowCloseUI(false);
                            }
                            break;
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                            if (GameSceneUI._SawmillUI._SawmillController != null && GameSceneUI._SawmillUI._SawmillController._GameObjectInfo.ObjectId == CraftingTableObjectID)
                            {
                                GameSceneUI._SawmillUI._SawmillController._SelectCompleteItemCategory = en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE;
                                GameSceneUI._SawmillUI.ShowCloseUI(false);
                            }
                            break;
                    }
                }
            }
        }

        public static void S2C_GatheringHandler(CMessage S2C_GatheringPacket)
        {
            long ObjectID;
            bool GatheringBarStart;
            string GatheringName;

            S2C_GatheringPacket.GetData(out ObjectID, sizeof(long));
            S2C_GatheringPacket.GetData(out GatheringBarStart, sizeof(bool));
            S2C_GatheringPacket.GetData(out GatheringName);

            PlayerObject FindPlayerObject = Managers.Object.FindById(ObjectID).GetComponent<PlayerObject>();
            if (FindPlayerObject == null)
            {
                Debug.Log("S2CStateChange 게임오브젝트를 찾을 수 없습니다");
                return;
            }

            if (GatheringBarStart == true)
            {
                FindPlayerObject._GatheringBar.GatheringStart(GatheringName, 1.0f, 1.0f);
            }
            else
            {
                FindPlayerObject._GatheringBar.GatheringEnd();
            }

            S2C_GatheringPacket.Dispose();
        }

        public static void S2C_ObjectStateChangeHandler(CMessage S2CMousePositionObjectInfoPacket)
        {
            long ObjectId;            
            short ObjectType;
            byte ObjectState;

            S2CMousePositionObjectInfoPacket.GetData(out ObjectId, sizeof(long));            
            S2CMousePositionObjectInfoPacket.GetData(out ObjectType, sizeof(short));
            S2CMousePositionObjectInfoPacket.GetData(out ObjectState, sizeof(byte));

            GameObject FindGameObject = Managers.Object.FindById(ObjectId);
            if (FindGameObject == null)
            {
                Debug.Log("S2CStateChange 게임오브젝트를 찾을 수 없습니다");
                return;
            }

            switch ((en_GameObjectType)ObjectType)
            {
                case en_GameObjectType.OBJECT_PLAYER:                
                    {
                        PlayerObject Player = FindGameObject.GetComponent<PlayerObject>();
                        if (Player != null)
                        {                            
                            Player.State = (en_CreatureState)ObjectState;
                        }
                    }
                    break;
                case en_GameObjectType.OBJECT_SLIME:
                    {
                        SlimeObject Slime = FindGameObject.GetComponent<SlimeObject>();
                        if (Slime != null)
                        {                            
                            Slime.State = (en_CreatureState)ObjectState;
                        }
                    }
                    break;
                case en_GameObjectType.OBJECT_BEAR:                    
                    break;
                case en_GameObjectType.OBJECT_TREE:
                    {
                        EnvironmentController Tree = FindGameObject.GetComponent<EnvironmentController>();
                        if (Tree != null)
                        {                            
                            Tree.State = (en_CreatureState)ObjectState;
                        }
                    }
                    break;
            }

            S2CMousePositionObjectInfoPacket.Dispose();
        }        

        public static void S2C_StatusAbnormalHandler(CMessage S2C_StatusAbnormalPacket)
        {
            long TargetId;
            short ObjectType;            
            short SkillType;
            bool SetStatusAbnormal;
            byte StatusAbnormal;

            S2C_StatusAbnormalPacket.GetData(out TargetId, sizeof(long));
            S2C_StatusAbnormalPacket.GetData(out ObjectType, sizeof(short));            
            S2C_StatusAbnormalPacket.GetData(out SkillType, sizeof(short));
            S2C_StatusAbnormalPacket.GetData(out SetStatusAbnormal, sizeof(bool));
            S2C_StatusAbnormalPacket.GetData(out StatusAbnormal, sizeof(byte));

            GameObject FindGameObject = Managers.Object.FindById(TargetId);

            if (FindGameObject != null)
            {
                PlayerObject StatusApplyObject = FindGameObject.GetComponent<PlayerObject>();

                if (SetStatusAbnormal == true)
                {
                    StatusApplyObject.SetStatusAbnormal(StatusAbnormal);
                    
                    StatusApplyObject.AnimationPlay("STATUS_ABNORMAL");
                }
                else
                {
                    StatusApplyObject.ReleaseStatusAbnormal(StatusAbnormal);
                    
                    // 상태이상이 끝낫으므로 기본 상태로 되돌아가야함
                }
            }
        }

        public static void S2C_DieHandler(CMessage S2CDiePacket)
        {
            long DieObjectId;

            S2CDiePacket.GetData(out DieObjectId, sizeof(long));

            GameObject FindGameObject = Managers.Object.FindById(DieObjectId);
            if (FindGameObject == null)
            {
                return;
            }

            CreatureObject DieCreature = FindGameObject.GetComponent<CreatureObject>();
            if (DieCreature != null)
            {
                DieCreature._HP = 0;
                DieCreature.OnDead();
            }

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

            UI_TargetHUD TargetHUDUI = GameSceneUI._TargetHUDUI;
            if (TargetHUDUI._TargetObject != null
                && TargetHUDUI._TargetObject._GameObjectInfo.ObjectId == DieObjectId)
            {
                CreatureObject UserCreature = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId).GetComponent<CreatureObject>();
                if (UserCreature != null)
                {
                    UserCreature._SelectTargetObjectInfo = null;
                }

                TargetHUDUI.TargetHUDOff();
            }

            S2CDiePacket.Dispose();
        }

        public static void S2C_ChattingMessageHandler(CMessage S2CChattingMessage)
        {
            st_Color MessageColor;
            string ChattingMessage;
            byte MessageType;

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                GameObject FindGameObject = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
                if (FindGameObject != null)
                {
                    CreatureObject CC = FindGameObject.GetComponent<CreatureObject>();
                    if (CC != null)
                    {
                        S2CChattingMessage.GetData(out MessageType, sizeof(byte));

                        switch ((en_MessageType)MessageType)
                        {
                            case en_MessageType.MESSAGE_TYPE_CHATTING:
                                S2CChattingMessage.GetData(out MessageColor);

                                S2CChattingMessage.GetData(out ChattingMessage);

                                CC._SpeechBubbleUI.SetSpeech(ChattingMessage);

                                GameSceneUI._ChattingBoxGroup.NewChattingMessage(CC._GameObjectInfo.ObjectName, ChattingMessage, (en_MessageType)MessageType, MessageColor);
                                break;
                            case en_MessageType.MESSAGE_TYPE_DAMAGE_CHATTING:
                                string AttackerName;
                                S2CChattingMessage.GetData(out AttackerName);

                                string TargetName;
                                S2CChattingMessage.GetData(out TargetName);

                                short SkillType;
                                S2CChattingMessage.GetData(out SkillType, sizeof(short));

                                int Damage;
                                S2CChattingMessage.GetData(out Damage, sizeof(int));

                                GameSceneUI._ChattingBoxGroup.NewDamageChattingMessage((en_MessageType)MessageType, AttackerName, TargetName, (en_SkillType)SkillType, Damage);
                                break;
                            case en_MessageType.MESSAGE_TYPE_SYSTEM:
                                break;
                        }
                    }
                }
            }

            S2CChattingMessage.Dispose();
        }

        public static void S2C_InventoryItemAddHandler(CMessage S2CItemToInventoryMessage)
        {
            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            st_ItemInfo AddItemInfo;

            long TargetId;
            S2CItemToInventoryMessage.GetData(out TargetId, sizeof(long));

            GameObject FindGameObject = Managers.Object.FindById(TargetId);
            if (FindGameObject == null)
            {
                Debug.Log($"ItemAdd 게임 오브젝트를 찾을 수 없습니다. {TargetId}");
                return;
            }

            PlayerObject PC = FindGameObject.GetComponent<PlayerObject>();
            if (PC != null)
            {
                en_SmallItemCategory ItemGainPrintSmallItemCategory = en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE;

                bool IsMoney;
                S2CItemToInventoryMessage.GetData(out IsMoney, sizeof(bool));

                if (IsMoney == true)
                {
                    long GoldCoinCount;
                    S2CItemToInventoryMessage.GetData(out GoldCoinCount, sizeof(long));

                    short SliverCoinCount;
                    S2CItemToInventoryMessage.GetData(out SliverCoinCount, sizeof(short));

                    short BronzeCoinCount;
                    S2CItemToInventoryMessage.GetData(out BronzeCoinCount, sizeof(short));

                    S2CItemToInventoryMessage.GetData(out AddItemInfo);

                    Managers.MyInventory.MoneyItemUpdate(GoldCoinCount, SliverCoinCount, BronzeCoinCount);
                }
                else
                {
                    bool ItemExist;

                    S2CItemToInventoryMessage.GetData(out AddItemInfo);
                    S2CItemToInventoryMessage.GetData(out ItemExist, sizeof(bool));

                    // 아이템이 중복되지 않은경우
                    if (!ItemExist)
                    {
                        // 새로운 아이템 UI 생성 후 값 세팅
                        GameObject GridInventoryItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INVENTORY_ITEM, GameSceneUI.transform);

                        UI_InventoryItem GridInventoryItem = GridInventoryItemGO.GetComponent<UI_InventoryItem>();
                        GridInventoryItem.GetComponent<RectTransform>().SetAsLastSibling();
                        GridInventoryItem.Set(AddItemInfo);
                        GridInventoryItem.SetParentGridInventory(Managers.MyInventory.SelectedInventory);

                        Managers.MyInventory.ServerInsertItem(GridInventoryItem);
                    }
                    else
                    {
                        UI_InventoryItem FindItem = Managers.MyInventory.FindItem(AddItemInfo.ItemSmallCategory);
                        FindItem._ItemInfo = AddItemInfo;

                        FindItem.RefreshInventoryItemUI();
                    }

                    ItemGainPrintSmallItemCategory = AddItemInfo.ItemSmallCategory;
                }

                short ItemEach;
                S2CItemToInventoryMessage.GetData(out ItemEach, sizeof(short));
                bool ItemGainPrint;
                S2CItemToInventoryMessage.GetData(out ItemGainPrint, sizeof(bool));

                // ItemGainPrint가 true면 Item 출력 UI 생성
                if (ItemGainPrint)
                {
                    GameSceneUI._ItemGainBox.NewItemGainMessage(AddItemInfo, ItemEach);
                    //GameSceneUI._CraftingBoxUI.CraftingCategoryBoxRefreshUI();
                    //GameSceneUI._CraftingBoxUI.CompleteItemConfirmCountCalculation();
                }
            }
            else
            {
                Debug.Log("ItemAdd PC를 찾을 수 없습니다.");
            }

            S2CItemToInventoryMessage.Dispose();
        }

        public static void S2C_CratingTableItemInputHandler(CMessage S2C_CraftingTableItemInputMessage)
        {
            long CraftingTableObjectID;
            short MaterialItemCount;

            // 선택한 제작대 아이디
            S2C_CraftingTableItemInputMessage.GetData(out CraftingTableObjectID, sizeof(long));
            // 선택한 제작대 재료 아이템 개수
            S2C_CraftingTableItemInputMessage.GetData(out MaterialItemCount, sizeof(short));

            GameObject FindGameObject = Managers.Object.FindById(CraftingTableObjectID);
            if (FindGameObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    CItem[] MaterialItems = new CItem[MaterialItemCount];
                    S2C_CraftingTableItemInputMessage.GetData(MaterialItems, MaterialItemCount);

                    PlayerObject ObjectTypeCheck = FindGameObject.GetComponent<PlayerObject>();
                    switch (ObjectTypeCheck._GameObjectInfo.ObjectType)
                    {
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                            FurnaceController Furnace = FindGameObject.GetComponent<FurnaceController>();
                            if (Furnace != null)
                            {
                                Furnace._FurnaceMaterials.Clear();

                                foreach (CItem MaterialItem in MaterialItems)
                                {
                                    Furnace._FurnaceMaterials.Add(MaterialItem._ItemInfo.ItemSmallCategory, MaterialItem);

                                    if (GameSceneUI._FurnaceUI.gameObject.active == true)
                                    {
                                        GameSceneUI._FurnaceUI.FurnaceMaterialItemRefreshUI(Furnace._SelectCompleteItemCategory);
                                    }
                                }
                            }
                            break;
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                            SawmillController Sawmill = FindGameObject.GetComponent<SawmillController>();
                            if (Sawmill != null)
                            {
                                Sawmill._SawmillMaterials.Clear();

                                foreach (CItem MaterialItem in MaterialItems)
                                {
                                    Sawmill._SawmillMaterials.Add(MaterialItem._ItemInfo.ItemSmallCategory, MaterialItem);

                                    if (GameSceneUI._SawmillUI.gameObject.active == true)
                                    {
                                        GameSceneUI._SawmillUI.SawmillMaterialItemRefreshUI(Sawmill._SelectCompleteItemCategory);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            else
            {
                Debug.Log("CraftingTableInputItem 게임 오브젝트를 찾을 수 없습니다.");
            }
        }

        public static void S2C_CraftingTableCompleteItemSelectHandler(CMessage S2C_CraftingTableCompleteItemSelectMessage)
        {
            long CraftingTableObjectID;
            short SelectCompleteItemType;
            short MaterialItemCount;

            S2C_CraftingTableCompleteItemSelectMessage.GetData(out CraftingTableObjectID, sizeof(long));
            S2C_CraftingTableCompleteItemSelectMessage.GetData(out SelectCompleteItemType, sizeof(short));

            GameObject FindGameObject = Managers.Object.FindById(CraftingTableObjectID);
            if (FindGameObject != null)
            {
                S2C_CraftingTableCompleteItemSelectMessage.GetData(out MaterialItemCount, sizeof(short));

                CItem[] MaterialItems = new CItem[MaterialItemCount];
                S2C_CraftingTableCompleteItemSelectMessage.GetData(MaterialItems, MaterialItemCount);

                PlayerObject ObjectTypeCheck = FindGameObject.GetComponent<PlayerObject>();

                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    switch (ObjectTypeCheck._GameObjectInfo.ObjectType)
                    {
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                            FurnaceController Furnace = FindGameObject.GetComponent<FurnaceController>();
                            if (Furnace != null)
                            {
                                Furnace._SelectCompleteItemCategory = (en_SmallItemCategory)SelectCompleteItemType;

                                Furnace._FurnaceMaterials.Clear();

                                foreach (CItem MaterialItem in MaterialItems)
                                {
                                    Furnace._FurnaceMaterials.Add(MaterialItem._ItemInfo.ItemSmallCategory, MaterialItem);

                                    if (GameSceneUI._FurnaceUI.gameObject.active == true)
                                    {
                                        GameSceneUI._FurnaceUI.FurnaceMaterialItemRefreshUI(Furnace._SelectCompleteItemCategory);
                                    }
                                }
                            }
                            break;
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                            SawmillController Sawmill = FindGameObject.GetComponent<SawmillController>();
                            if (Sawmill != null)
                            {
                                Sawmill._SelectCompleteItemCategory = (en_SmallItemCategory)SelectCompleteItemType;

                                Sawmill._SawmillMaterials.Clear();

                                foreach (CItem MaterialItem in MaterialItems)
                                {
                                    Sawmill._SawmillMaterials.Add(MaterialItem._ItemInfo.ItemSmallCategory, MaterialItem);

                                    if (GameSceneUI._SawmillUI.gameObject.active == true)
                                    {
                                        GameSceneUI._SawmillUI.SawmillMaterialItemRefreshUI(Sawmill._SelectCompleteItemCategory);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        public static void S2C_CraftingTableCraftingStartHandler(CMessage S2C_CraftingTableCraftingStartMessage)
        {
            long CraftingTableObjectID;
            st_ItemInfo CraftingItemInfo;

            S2C_CraftingTableCraftingStartMessage.GetData(out CraftingTableObjectID, sizeof(long));
            S2C_CraftingTableCraftingStartMessage.GetData(out CraftingItemInfo);

            GameObject FindGameObject = Managers.Object.FindById(CraftingTableObjectID);
            if (FindGameObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    PlayerObject BaseController = FindGameObject.GetComponent<PlayerObject>();
                    if (BaseController != null)
                    {
                        switch (BaseController._GameObjectInfo.ObjectType)
                        {
                            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                                FurnaceController Furnace = FindGameObject.GetComponent<FurnaceController>();
                                if (Furnace != null)
                                {
                                    UI_Furnace FurnaceUI = GameSceneUI._FurnaceUI;

                                    // 용광로 보유 제작 목록에 서버로 부터 받은 아이템 타입이 있는지 확인한다.                                
                                    foreach (st_ItemInfo ItemInfo in FurnaceUI._FurnaceCraftingTable.CraftingTableCompleteItems)
                                    {
                                        if (ItemInfo.ItemSmallCategory == CraftingItemInfo.ItemSmallCategory)
                                        {
                                            //UI_CraftingCompleteItem CraftingCompleteItemUI = FurnaceUI._UICraftingCompleteItems.Values.FirstOrDefault(
                                            //CraftingCompleteItem => CraftingCompleteItem._CraftingCompleteItem.ItemSmallCategory == CraftingItemInfo.ItemSmallCategory);
                                            //if (CraftingCompleteItemUI != null)
                                            //{
                                            //    CraftingCompleteItemUI.CraftingCompleteStart(CraftingItemInfo);
                                            //}
                                        }
                                    }
                                }
                                break;
                            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                                SawmillController Sawmill = FindGameObject.GetComponent<SawmillController>();
                                if (Sawmill != null)
                                {
                                    UI_Sawmill SawmillUI = GameSceneUI._SawmillUI;

                                    foreach (st_ItemInfo ItemInfo in SawmillUI._SawmillCraftingTable.CraftingTableCompleteItems)
                                    {
                                        if (ItemInfo.ItemSmallCategory == CraftingItemInfo.ItemSmallCategory)
                                        {
                                            //UI_CraftingCompleteItem CraftingCompleteItemUI = SawmillUI._UICraftingCompleteItems.Values.FirstOrDefault(
                                            //CraftingCompleteItem => CraftingCompleteItem._CraftingCompleteItem.ItemSmallCategory == CraftingItemInfo.ItemSmallCategory);
                                            //if (CraftingCompleteItemUI != null)
                                            //{
                                            //    CraftingCompleteItemUI.CraftingCompleteStart(CraftingItemInfo);
                                            //}
                                        }
                                    }
                                }

                                break;
                        }
                    }
                }
            }
        }

        public static void S2C_CraftingTableCraftingStopHandler(CMessage S2C_CraftingTableCraftingStopMessage)
        {
            long CraftingTableObjectID;
            st_ItemInfo CraftingItemInfo;

            S2C_CraftingTableCraftingStopMessage.GetData(out CraftingTableObjectID, sizeof(long));
            S2C_CraftingTableCraftingStopMessage.GetData(out CraftingItemInfo);

            GameObject FindGameObject = Managers.Object.FindById(CraftingTableObjectID);
            if (FindGameObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    PlayerObject BaseController = FindGameObject.GetComponent<PlayerObject>();
                    if (BaseController != null)
                    {
                        switch (BaseController._GameObjectInfo.ObjectType)
                        {
                            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                                FurnaceController Furnace = FindGameObject.GetComponent<FurnaceController>();
                                if (Furnace != null)
                                {
                                    UI_Furnace FurnaceUI = GameSceneUI._FurnaceUI;

                                    // 용광로 보유 제작 목록에 서버로 부터 받은 아이템 타입이 있는지 확인한다.                                
                                    foreach (st_ItemInfo ItemInfo in FurnaceUI._FurnaceCraftingTable.CraftingTableCompleteItems)
                                    {
                                        if (ItemInfo.ItemSmallCategory == CraftingItemInfo.ItemSmallCategory)
                                        {
                                            //UI_CraftingCompleteItem CraftingCompleteItemUI = FurnaceUI._UICraftingCompleteItems.Values.FirstOrDefault(
                                            //CraftingCompleteItem => CraftingCompleteItem._CraftingCompleteItem.ItemSmallCategory == CraftingItemInfo.ItemSmallCategory);
                                            //if (CraftingCompleteItemUI != null)
                                            //{
                                            //    CraftingCompleteItemUI.CraftingCompleteStop();
                                            //}
                                        }
                                    }
                                }
                                break;
                            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                                SawmillController Sawmill = FindGameObject.GetComponent<SawmillController>();
                                if (Sawmill != null)
                                {
                                    UI_Sawmill SawmillUI = GameSceneUI._SawmillUI;

                                    foreach (st_ItemInfo ItemInfo in SawmillUI._SawmillCraftingTable.CraftingTableCompleteItems)
                                    {
                                        if (ItemInfo.ItemSmallCategory == CraftingItemInfo.ItemSmallCategory)
                                        {
                                            //UI_CraftingCompleteItem CraftingCompleteItemUI = SawmillUI._UICraftingCompleteItems.Values.FirstOrDefault(
                                            //CraftingCompleteItem => CraftingCompleteItem._CraftingCompleteItem.ItemSmallCategory == CraftingItemInfo.ItemSmallCategory);
                                            //if (CraftingCompleteItemUI != null)
                                            //{
                                            //    CraftingCompleteItemUI.CraftingCompleteStop();
                                            //}
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        public static void S2C_CraftingTableMaterialItemListHandler(CMessage S2C_CraftingTableMaterialItemListMessage)
        {
            long CraftingTableObjectID;
            short CraftingTableObjectType;
            short SelectCompleteItemType;
            short MaterialItemCount;

            S2C_CraftingTableMaterialItemListMessage.GetData(out CraftingTableObjectID, sizeof(long));
            S2C_CraftingTableMaterialItemListMessage.GetData(out CraftingTableObjectType, sizeof(short));
            S2C_CraftingTableMaterialItemListMessage.GetData(out SelectCompleteItemType, sizeof(short));

            GameObject FindGameObject = Managers.Object.FindById(CraftingTableObjectID);
            if (FindGameObject != null)
            {
                S2C_CraftingTableMaterialItemListMessage.GetData(out MaterialItemCount, sizeof(short));

                CItem[] MaterialItems = new CItem[MaterialItemCount];
                S2C_CraftingTableMaterialItemListMessage.GetData(MaterialItems, MaterialItemCount);

                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    switch ((en_GameObjectType)CraftingTableObjectType)
                    {
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                            FurnaceController Furnace = FindGameObject.GetComponent<FurnaceController>();
                            if (Furnace != null)
                            {
                                Furnace._FurnaceMaterials.Clear();

                                foreach (CItem MaterialItem in MaterialItems)
                                {
                                    Furnace._FurnaceMaterials.Add(MaterialItem._ItemInfo.ItemSmallCategory, MaterialItem);

                                    // 용광로 UI가 활성화 되어 있고, 선택한 완성 아이템이 동일할 경우 재료 아이템을 업데이트 한것을 보여준다.
                                    if (GameSceneUI._FurnaceUI.gameObject.active == true
                                        && Furnace._SelectCompleteItemCategory == (en_SmallItemCategory)SelectCompleteItemType)
                                    {
                                        GameSceneUI._FurnaceUI.FurnaceMaterialItemRefreshUI(Furnace._SelectCompleteItemCategory);
                                    }
                                }
                            }
                            break;
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                            SawmillController Sawmill = FindGameObject.GetComponent<SawmillController>();
                            if (Sawmill != null)
                            {
                                Sawmill._SawmillMaterials.Clear();

                                foreach (CItem MaterialItem in MaterialItems)
                                {
                                    Sawmill._SawmillMaterials.Add(MaterialItem._ItemInfo.ItemSmallCategory, MaterialItem);

                                    // 용광로 UI가 활성화 되어 있고, 선택한 완성 아이템이 동일할 경우 재료 아이템을 업데이트 한것을 보여준다.
                                    if (GameSceneUI._SawmillUI.gameObject.active == true
                                        && Sawmill._SelectCompleteItemCategory == (en_SmallItemCategory)SelectCompleteItemType)
                                    {
                                        GameSceneUI._SawmillUI.SawmillMaterialItemRefreshUI(Sawmill._SelectCompleteItemCategory);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        public static void S2C_CraftingTableCompleteItemListHandler(CMessage S2C_CraftingTableCompleteItemListMessage)
        {
            long CraftingTableObjectID;
            short CraftingTableObjectType;
            short CompleteItemCount;

            S2C_CraftingTableCompleteItemListMessage.GetData(out CraftingTableObjectID, sizeof(long));
            S2C_CraftingTableCompleteItemListMessage.GetData(out CraftingTableObjectType, sizeof(short));

            GameObject FindGameObject = Managers.Object.FindById(CraftingTableObjectID);
            if (FindGameObject != null)
            {
                S2C_CraftingTableCompleteItemListMessage.GetData(out CompleteItemCount, sizeof(short));

                CItem[] CompleteItems = new CItem[CompleteItemCount];
                S2C_CraftingTableCompleteItemListMessage.GetData(CompleteItems, CompleteItemCount);

                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI != null)
                {
                    switch ((en_GameObjectType)CraftingTableObjectType)
                    {
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                            FurnaceController Furnace = FindGameObject.GetComponent<FurnaceController>();
                            if (Furnace != null)
                            {
                                Furnace._FurnaceCompleteItems.Clear();

                                foreach (CItem CompleteItem in CompleteItems)
                                {
                                    if (CompleteItem._ItemInfo.ItemCount > 0)
                                    {
                                        Furnace._FurnaceCompleteItems.Add(CompleteItem._ItemInfo.ItemSmallCategory, CompleteItem);
                                    }

                                    // 용광로 UI가 활성화 되어 있을 경우
                                    if (GameSceneUI._FurnaceUI.gameObject.active == true)
                                    {
                                        GameSceneUI._FurnaceUI.FurnaceCompleteItemRefreshUI();
                                    }
                                }
                            }
                            break;
                        case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                            SawmillController Sawmill = FindGameObject.GetComponent<SawmillController>();
                            if (Sawmill != null)
                            {
                                Sawmill._SawmillCompleteItems.Clear();

                                foreach (CItem CompleteItem in CompleteItems)
                                {
                                    if (CompleteItem._ItemInfo.ItemCount > 0)
                                    {
                                        Sawmill._SawmillCompleteItems.Add(CompleteItem._ItemInfo.ItemSmallCategory, CompleteItem);
                                    }

                                    if (GameSceneUI._SawmillUI.gameObject.active == true)
                                    {
                                        GameSceneUI._SawmillUI.SawmillCompleteItemRefreshUI();
                                    }
                                }
                            }
                            break;
                    }
                }
            }

        }


        public static void S2C_ItemSelectHandler(CMessage S2C_ItemSelectMessage)
        {
            long AccountId;
            long TargetId;
            CItem SelectItem;

            S2C_ItemSelectMessage.GetData(out AccountId, sizeof(long));
            S2C_ItemSelectMessage.GetData(out TargetId, sizeof(long));
            S2C_ItemSelectMessage.GetData(out SelectItem);

            GameObject FindGameObject = Managers.Object.FindById(TargetId);
            if (FindGameObject == null)
            {
                Debug.Log($"ItemSelect 게임 오브젝트를 찾을 수 없습니다. {TargetId}");
                return;
            }

            PlayerObject PC = FindGameObject.GetComponent<PlayerObject>();
            if (PC != null)
            {
                Vector2Int SelectTileGridPosition = new Vector2Int();
                SelectTileGridPosition.x = SelectItem._ItemInfo.ItemTileGridPositionX;
                SelectTileGridPosition.y = SelectItem._ItemInfo.ItemTileGridPositionY;

                Managers.MyInventory.ResSelectItem(SelectTileGridPosition);
            }

            S2C_ItemSelectMessage.Dispose();
        }

        public static void S2C_ItemPlaceHandler(CMessage S2CItemPlaceMessage)
        {
            long AccountId;
            long TargetId;
            CItem PlaceItem;
            CItem SelectItem;

            S2CItemPlaceMessage.GetData(out AccountId, sizeof(long));
            S2CItemPlaceMessage.GetData(out TargetId, sizeof(long));
            S2CItemPlaceMessage.GetData(out PlaceItem);
            S2CItemPlaceMessage.GetData(out SelectItem);

            GameObject FindGameObject = Managers.Object.FindById(TargetId);
            if (FindGameObject == null)
            {
                Debug.Log($"ItemSelect 게임 오브젝트를 찾을 수 없습니다. {TargetId}");
                return;
            }

            PlayerObject PC = FindGameObject.GetComponent<PlayerObject>();
            if (PC != null)
            {
                Managers.MyInventory.ResPlaceItem(PlaceItem._ItemInfo, SelectItem._ItemInfo);
            }

            S2CItemPlaceMessage.Dispose();
        }

        public static void S2C_SeedFarmingHandler(CMessage S2C_SeedFarmingMessage)
        {
            st_ItemInfo SeedItem;
            long SeedObjectID;

            S2C_SeedFarmingMessage.GetData(out SeedItem);
            S2C_SeedFarmingMessage.GetData(out SeedObjectID, sizeof(long));

            short SeedItemCategory;

            S2C_SeedFarmingMessage.GetData(out SeedItemCategory, sizeof(short));

            Managers.MyInventory.SelectedItem = Managers.MyInventory.FindItem(SeedItem.ItemSmallCategory);
        }

        public static void S2C_PlantGrowthCheckHandler(CMessage S2C_PlantGrowthCheckMessage)
        {
            long PlantObjectID;
            byte PlantObjectStep;
            float PlantGrowthRatio;

            S2C_PlantGrowthCheckMessage.GetData(out PlantObjectID, sizeof(long));
            S2C_PlantGrowthCheckMessage.GetData(out PlantObjectStep, sizeof(byte));
            S2C_PlantGrowthCheckMessage.GetData(out PlantGrowthRatio, sizeof(float));

            GameObject PlantGO = Managers.Object.FindById(PlantObjectID);
            if (PlantGO != null)
            {
                CropController CropPlant = PlantGO.GetComponent<CropController>();
                if (CropPlant != null)
                {
                    CropPlant.CropImageChange(PlantObjectStep);
                    CropPlant.SetCropBar(PlantGrowthRatio);
                }
            }
        }

        public static void S2C_ItemRotateHandler(CMessage S2C_ItemRotateMessage)
        {
            long AccountID;
            long PlayerID;

            S2C_ItemRotateMessage.GetData(out AccountID, sizeof(long));
            S2C_ItemRotateMessage.GetData(out PlayerID, sizeof(long));

            Managers.MyInventory.ResRotateItem();
        }

        public static void S2C_SyncPosition(CMessage S2CSyncPositionMessage)
        {
            long TargetId;
            st_PositionInfo SyncPosition;

            // 위치 조정할 타겟의 Id, 위치값을 얻고
            S2CSyncPositionMessage.GetData(out TargetId, sizeof(long));
            S2CSyncPositionMessage.GetData(out SyncPosition);

            // 타겟을 찾은 후에
            GameObject FindGameObject = Managers.Object.FindById(TargetId);
            if (FindGameObject != null && Managers.NetworkManager._PlayerDBId == TargetId)
            {
                // 위치를 조정하고
                CreatureObject CC = FindGameObject.GetComponent<CreatureObject>();
                CC.State = en_CreatureState.IDLE;
                CC._GameObjectInfo.ObjectPositionInfo = SyncPosition;
                CC.transform.position = new Vector3(SyncPosition.PositionX, SyncPosition.PositionY);

                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                UI_Minimap MiniMap = GameSceneUI._Minimap;

                MiniMap.MiniMapMyPositionUpdate(CC.PositionInfo.CollsitionPositionX, CC.PositionInfo.CollsitionPositionY);                
            }
            else
            {
                Debug.Log("SyncPosition 오브젝트를 찾을 수 없습니다.");
            }

            S2CSyncPositionMessage.Dispose();
        }

        public static void S2C_SelectSkillCharacteristic(CMessage S2C_SelectSkillChracteristicMessage)
        {
            bool IsSuccess;
            byte SkillCharacteristicIndex;
            byte SkillCharacteristicType;

            S2C_SelectSkillChracteristicMessage.GetData(out IsSuccess, sizeof(bool));

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (IsSuccess == true)
            {
                S2C_SelectSkillChracteristicMessage.GetData(out SkillCharacteristicIndex, sizeof(byte));
                S2C_SelectSkillChracteristicMessage.GetData(out SkillCharacteristicType, sizeof(byte));

                byte PassiveSkillCount;
                S2C_SelectSkillChracteristicMessage.GetData(out PassiveSkillCount, sizeof(byte));

                if (PassiveSkillCount > 0)
                {
                    st_SkillInfo[] PassiveSkills = new st_SkillInfo[PassiveSkillCount];
                    S2C_SelectSkillChracteristicMessage.GetData(PassiveSkills, PassiveSkillCount);

                    Managers.SkillBox.CreateChracteristicPassive(SkillCharacteristicIndex, (en_SkillCharacteristic)SkillCharacteristicType, PassiveSkillCount, PassiveSkills);
                }

                byte ActiveSkillCount;
                S2C_SelectSkillChracteristicMessage.GetData(out ActiveSkillCount, sizeof(byte));

                if (ActiveSkillCount > 0)
                {
                    st_SkillInfo[] ActiveSkills = new st_SkillInfo[ActiveSkillCount];
                    S2C_SelectSkillChracteristicMessage.GetData(ActiveSkills, ActiveSkillCount);

                    Managers.SkillBox.CreateChracteristicActive(SkillCharacteristicIndex, (en_SkillCharacteristic)SkillCharacteristicType, ActiveSkillCount, ActiveSkills);
                }

                GameSceneUI._SkillBoxUI.RefreshSkillBoxUI();

                GameSceneUI._SkillBoxUI.SkillBoxSelectCharacteristicShowClose(false);
                GameSceneUI._SkillBoxUI.SkillBoxButtonShowClose(true);
                GameSceneUI._SkillBoxUI.SkillBoxSkillCharacteristicShowClose(true);
                GameSceneUI._SkillBoxUI.CharacteristicSelectButtonShowClose(SkillCharacteristicIndex, false);
            }
            else
            {
                UI_GlobalMessageBox PersonalMessageBoxUI = GameSceneUI._GlobalMessageBoxUI;
                PersonalMessageBoxUI.NewStatusAbnormalMessage(en_GlobalMessageType.PERSONAL_MESSAGE_NON_SKILL_CHARACTERISTIC, "습득한 특성입니다. 다른 특성을 고르세요.");
            }
        }

        public static void S2C_SkillLearn(CMessage S2C_SkillLearnMessage)
        {
            bool IsSkillLearn;
            short LearnSkillType;
            byte SkillMaxPoint;
            byte SkillPoint;

            S2C_SkillLearnMessage.GetData(out IsSkillLearn, sizeof(bool));
            S2C_SkillLearnMessage.GetData(out LearnSkillType, sizeof(short));
            S2C_SkillLearnMessage.GetData(out SkillMaxPoint, sizeof(byte));
            S2C_SkillLearnMessage.GetData(out SkillPoint, sizeof(byte));

            GameObject PlayerGO = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
            PlayerObject Player = PlayerGO.GetComponent<PlayerObject>();

            Player._GameObjectInfo.ObjectSkillPoint = SkillPoint;

            Managers.SkillBox.SetSkillLearn(LearnSkillType, IsSkillLearn);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            GameSceneUI._SkillBoxUI.RefreshSkillBoxUI();
        }

        public static void S2C_SkillToSkillBox(CMessage S2CSkillToSkillBoxMessage)
        {
            long TargetId;
            st_SkillInfo SkillInfo;

            S2CSkillToSkillBoxMessage.GetData(out TargetId, sizeof(long));
            S2CSkillToSkillBoxMessage.GetData(out SkillInfo);

            GameObject FindGameObject = Managers.Object.FindById(TargetId);
            if (FindGameObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI == null)
                {
                    Debug.Log("GameSceneUI로 형변환 실패");
                }

                GameSceneUI._SkillBoxUI.RefreshSkillBoxUI();
            }
            else
            {
                Debug.Log("SkillToSkillBox 오브젝트를 찾을 수 없습니다.");
            }

            S2CSkillToSkillBoxMessage.Dispose();
        }

        public static void S2C_QuickSlotCreate(CMessage S2CQuickSlotCreateMessage)
        {
            byte QuickSlotBarSize;
            byte QuickSlotBarSlotSize;
            byte QuickSlotBarCount;

            S2CQuickSlotCreateMessage.GetData(out QuickSlotBarSize, sizeof(byte));
            S2CQuickSlotCreateMessage.GetData(out QuickSlotBarSlotSize, sizeof(byte));

            S2CQuickSlotCreateMessage.GetData(out QuickSlotBarCount, sizeof(byte));

            st_QuickSlotBarSlotInfo[] QuickSlotBarSlotInfos = new st_QuickSlotBarSlotInfo[QuickSlotBarCount];
            S2CQuickSlotCreateMessage.GetData(QuickSlotBarSlotInfos, QuickSlotBarCount);

            GameObject FindGameObject = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
            if (FindGameObject != null)
            {
                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI == null)
                {
                    Debug.Log("GameSceneUI로 형변환 실패");
                }

                // 퀵슬롯 생성
                Managers.QuickSlotBar.Init(QuickSlotBarSize, QuickSlotBarSlotSize);

                // 생성한 퀵슬롯 정보 업데이트
                for (int i = 0; i < QuickSlotBarCount; i++)
                {
                    Managers.QuickSlotBar.UpdateQuickSlotBarSlot(QuickSlotBarSlotInfos[i]);
                }

                // 퀵슬롯 UI 생성
                UI_QuickSlotBarBox QuickSlotBarBoxUI = GameSceneUI._QuickSlotBarBoxUI;
                QuickSlotBarBoxUI.UIQuickSlotBarBoxCreate(QuickSlotBarSize, QuickSlotBarSlotSize);

                // 퀵슬롯 정보 업데이트
                QuickSlotBarBoxUI.RefreshQuickSlotBarBoxUI();
            }

            S2CQuickSlotCreateMessage.Dispose();
        }

        public static void S2C_QuickSlotSaveHandler(CMessage S2CQuickSlotSaveMessage)
        {
            st_QuickSlotBarSlotInfo SaveQuickSlotBarSlotInfo;

            S2CQuickSlotSaveMessage.GetData(out SaveQuickSlotBarSlotInfo);

            GameObject FindGameObject = Managers.Object.FindById(SaveQuickSlotBarSlotInfo.PlayerDBId);
            if (FindGameObject != null)
            {
                // 서버로부터 받은 퀵슬롯 정보 저장 (= 업데이트 )
                Managers.QuickSlotBar.UpdateQuickSlotBarSlot(SaveQuickSlotBarSlotInfo);

                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                if (GameSceneUI == null)
                {
                    Debug.Log("GameSceneUI로 형변환 실패");
                }

                UI_QuickSlotBarBox QuickSlotBarBox = GameSceneUI._QuickSlotBarBoxUI;
                QuickSlotBarBox.RefreshQuickSlotBarBoxUI();

                if (SaveQuickSlotBarSlotInfo.QuickBarSkillInfo != null && SaveQuickSlotBarSlotInfo.QuickBarSkillInfo.SkillRemainTime > 0)
                {
                    QuickSlotBarBox.QuickSlotBarBoxCoolTimerStart(SaveQuickSlotBarSlotInfo.QuickSlotBarIndex,
                        SaveQuickSlotBarSlotInfo.QuickSlotBarSlotIndex, 1.0f);
                }
                else
                {
                    QuickSlotBarBox.QuickSlotBarBoxCoolTimeStop(SaveQuickSlotBarSlotInfo.QuickSlotBarIndex, SaveQuickSlotBarSlotInfo.QuickSlotBarSlotIndex);
                }
            }
            else
            {
                Debug.Log("QuickSlotSave Player를 찾을 수 없습니다.");
            }

            S2CQuickSlotSaveMessage.Dispose();
        }

        public static void S2C_CoolTimeStartHandler(CMessage S2C_CoolTimeStartMessage)
        {
            byte QuickSlotBarIndex;
            byte QuickSlotBarSlotIndex;
            float SkillCoolTimeSpeed;
            bool EmptySkill;
            st_SkillInfo QuickSlotSkillInfo = null;
            int CoolTime;

            S2C_CoolTimeStartMessage.GetData(out QuickSlotBarIndex, sizeof(byte));
            S2C_CoolTimeStartMessage.GetData(out QuickSlotBarSlotIndex, sizeof(byte));
            S2C_CoolTimeStartMessage.GetData(out SkillCoolTimeSpeed, sizeof(float));
            S2C_CoolTimeStartMessage.GetData(out EmptySkill, sizeof(bool));

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            UI_QuickSlotBarBox QuickSlotBarBoxUI = GameSceneUI._QuickSlotBarBoxUI;

            if (EmptySkill == true)
            {
                S2C_CoolTimeStartMessage.GetData(out CoolTime, sizeof(int));

                QuickSlotBarBoxUI.QuickSlotBarBoxCoolTimerStart(QuickSlotBarIndex, QuickSlotBarSlotIndex, CoolTime);
            }
            else
            {
                S2C_CoolTimeStartMessage.GetData(out QuickSlotSkillInfo);

                Managers.QuickSlotBar._SkillQuickSlotBars[QuickSlotBarIndex]
                ._QuickSlotBarSlotInfos[QuickSlotBarSlotIndex].QuickBarSkillInfo = QuickSlotSkillInfo;

                QuickSlotBarBoxUI.QuickSlotBarBoxCoolTimerStart(QuickSlotBarIndex, QuickSlotBarSlotIndex, SkillCoolTimeSpeed);
            }

            S2C_CoolTimeStartMessage.Dispose();
        }

        public static void S2C_QuickSlotSwapHandler(CMessage S2C_QuickSlotSwapMessage)
        {
            st_QuickSlotBarSlotInfo AQuickSlotInfo;
            st_QuickSlotBarSlotInfo BQuickSlotInfo;

            S2C_QuickSlotSwapMessage.GetData(out AQuickSlotInfo);
            S2C_QuickSlotSwapMessage.GetData(out BQuickSlotInfo);

            Managers.QuickSlotBar.SwapQuickSlot(AQuickSlotInfo, BQuickSlotInfo);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

            UI_QuickSlotBarBox QuickSlotBarBoxUI = GameSceneUI._QuickSlotBarBoxUI;
            QuickSlotBarBoxUI.RefreshQuickSlotBarBoxUI();

            if (AQuickSlotInfo.QuickBarSkillInfo != null && AQuickSlotInfo.QuickBarSkillInfo.SkillRemainTime > 0)
            {
                QuickSlotBarBoxUI.QuickSlotBarBoxCoolTimerStart(AQuickSlotInfo.QuickSlotBarIndex,
                    AQuickSlotInfo.QuickSlotBarSlotIndex, 1.0f);
            }
            else
            {
                QuickSlotBarBoxUI.QuickSlotBarBoxCoolTimeStop(AQuickSlotInfo.QuickSlotBarIndex, AQuickSlotInfo.QuickSlotBarSlotIndex);
            }

            if (BQuickSlotInfo.QuickBarSkillInfo != null && BQuickSlotInfo.QuickBarSkillInfo.SkillRemainTime > 0)
            {
                QuickSlotBarBoxUI.QuickSlotBarBoxCoolTimerStart(BQuickSlotInfo.QuickSlotBarIndex,
                    BQuickSlotInfo.QuickSlotBarSlotIndex, 1.0f);
            }
            else
            {
                QuickSlotBarBoxUI.QuickSlotBarBoxCoolTimeStop(BQuickSlotInfo.QuickSlotBarIndex, BQuickSlotInfo.QuickSlotBarSlotIndex);
            }

            S2C_QuickSlotSwapMessage.Dispose();
        }

        public static void S2C_QuickSlotEmptyHandler(CMessage S2C_QuickSlotEmptyMessage)
        {
            byte QuickSlotBarIndex;
            byte QuickSlotBarSlotIndex;

            S2C_QuickSlotEmptyMessage.GetData(out QuickSlotBarIndex, sizeof(byte));
            S2C_QuickSlotEmptyMessage.GetData(out QuickSlotBarSlotIndex, sizeof(byte));

            Managers.QuickSlotBar.QuickSlotBarEmpty(QuickSlotBarIndex, QuickSlotBarSlotIndex);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI == null)
            {
                Debug.Log("QuickSlotSwap GameSceneUI로 형변환 실패");
            }

            GameSceneUI._QuickSlotBarBoxUI.RefreshQuickSlotBarBoxUI();

            S2C_QuickSlotEmptyMessage.Dispose();
        }

        public static void S2C_CraftingListHandler(CMessage S2C_CraftingListMessage)
        {
            byte CraftingCategorySize;

            S2C_CraftingListMessage.GetData(out CraftingCategorySize, sizeof(byte));

            st_CraftingItemCategory[] CraftingItemCategory = new st_CraftingItemCategory[CraftingCategorySize];
            S2C_CraftingListMessage.GetData(CraftingItemCategory, CraftingCategorySize);

            // 제작템 정보 생성
            Managers.CraftingBox.Init(CraftingItemCategory);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI == null)
            {
                Debug.Log("CraftingListHandler GameSceneUI로 형변환 실패");
            }

            //UI_CraftingBox CraftingBoxUI = GameSceneUI._CraftingBoxUI;
            //if (CraftingBoxUI == null)
            //{
            //    Debug.Log("CraftingListHandler CraftingBoxUI를 찾을 수 없음");
            //}

            //CraftingBoxUI.gameObject.SetActive(false);
            //CraftingBoxUI.CraftingBoxCategoryUpdate(CraftingItemCategory);

            S2C_CraftingListMessage.Dispose();
        }

        public static void S2C_InventoryItemUpdateHandler(CMessage S2C_InventoryItemUpdateMessage)
        {
            long PlayerId;
            CItem UpdateItem;

            S2C_InventoryItemUpdateMessage.GetData(out PlayerId, sizeof(long));
            S2C_InventoryItemUpdateMessage.GetData(out UpdateItem);

            UI_InventoryItem FindItem = Managers.MyInventory.FindItem(UpdateItem._ItemInfo.ItemSmallCategory);
            FindItem._ItemInfo = UpdateItem._ItemInfo;

            FindItem.RefreshInventoryItemUI();

            if (FindItem._ItemInfo.ItemCount == 0)
            {
                Managers.MyInventory.InitItem(FindItem);
            }

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI == null)
            {
                Debug.Log("InventoryItemUpdateHandler GameSceneUI로 형변환 실패");
            }

            // 인벤토리 UI 재 갱신
            //UI_Inventory InventoryUI = GameSceneUI._InventoryUI;
            //if(InventoryUI == null)
            //{
            //    Debug.Log("InventoryItemUpdateHandler InventoryUI를 찾을 수 없음");
            //}

            //InventoryUI.InventoryBoxRefreshUI();

            // 제작창 UI 재 갱신
            //UI_CraftingBox CraftingBoxUI = GameSceneUI._CraftingBoxUI;
            //if(CraftingBoxUI == null)
            //{
            //    Debug.Log("InventoryItemUpdate CraftingBoxUI를 찾을 수 없음");
            //}

            //CraftingBoxUI.MaterialBoxUI();
            //CraftingBoxUI.CompleteItemConfirmCountCalculation();

            S2C_InventoryItemUpdateMessage.Dispose();
        }

        public static void S2C_OnEquipmentHandler(CMessage S2C_OnEquipmentMessage)
        {
            long PlayerId;
            CItem EquipementItem;

            S2C_OnEquipmentMessage.GetData(out PlayerId, sizeof(long));
            S2C_OnEquipmentMessage.GetData(out EquipementItem);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                UI_EquipmentBox EquipmentBoxUI = GameSceneUI._EquipmentBoxUI;

                if (EquipmentBoxUI != null)
                {
                    EquipmentBoxUI.OnEquipmentItem(EquipementItem._ItemInfo);

                    UI_InventoryItem FindItem = Managers.MyInventory.FindItem(EquipementItem._ItemInfo.ItemSmallCategory);
                    Managers.MyInventory.InitItem(FindItem);
                }
            }

            S2C_OnEquipmentMessage.Dispose();
        }

        public static void S2C_OffEquipmentHandler(CMessage S2C_OffEquipmentMessage)
        {
            long PlayerID;
            byte OffEquipmentParts;

            S2C_OffEquipmentMessage.GetData(out PlayerID, sizeof(long));
            S2C_OffEquipmentMessage.GetData(out OffEquipmentParts, sizeof(byte));

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                GameSceneUI._EquipmentBoxUI.OffEquipmentItem((en_EquipmentParts)OffEquipmentParts);
            }
        }

        public static void S2C_ExperienceHandler(CMessage S2C_ExperienceMessage)
        {
            long AccountId;
            long PlayerId;
            long GainExp;
            long CurrentExp;
            long RequireExp;
            long TotalExp;

            S2C_ExperienceMessage.GetData(out AccountId, sizeof(long));
            S2C_ExperienceMessage.GetData(out PlayerId, sizeof(long));
            S2C_ExperienceMessage.GetData(out GainExp, sizeof(long));
            S2C_ExperienceMessage.GetData(out CurrentExp, sizeof(long));
            S2C_ExperienceMessage.GetData(out RequireExp, sizeof(long));
            S2C_ExperienceMessage.GetData(out TotalExp, sizeof(long));

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI == null)
            {
                Debug.Log("ExperienceHandler GameSceneUI를 찾을 수 없음");
            }

            UI_PlayerExperience PlayerExperienceUI = GameSceneUI._PlayerExperienceUI;
            if (PlayerExperienceUI == null)
            {
                Debug.Log("ExperienceHandler PlayerExperienceUI를 찾을 수 없음");
            }

            PlayerExperienceUI.PlayerGainExp(GainExp, CurrentExp, RequireExp, TotalExp);

            S2C_ExperienceMessage.Dispose();
        }

        public static void S2C_BufDeBufHandler(CMessage S2C_BuDefMessage)
        {
            long TargetId;
            bool BufDeBuf;
            st_SkillInfo BufDeBufSkillInfo;

            S2C_BuDefMessage.GetData(out TargetId, sizeof(long));
            S2C_BuDefMessage.GetData(out BufDeBuf, sizeof(bool));
            S2C_BuDefMessage.GetData(out BufDeBufSkillInfo);

            GameObject FindGameObject = Managers.Object.FindById(TargetId);
            if (FindGameObject != null)
            {
                PlayerObject baseController = FindGameObject.GetComponent<PlayerObject>();

                if (BufDeBuf == true)
                {
                    if (baseController._Bufs.Count > 0)
                    {
                        st_SkillInfo FindBufSkillInfo = baseController._Bufs.Values
                            .FirstOrDefault(BufItem => BufItem.SkillType == BufDeBufSkillInfo.SkillType);
                        if (FindBufSkillInfo != null)
                        {
                            FindBufSkillInfo = BufDeBufSkillInfo;
                        }
                        else
                        {
                            baseController._Bufs.Add(BufDeBufSkillInfo.SkillType, BufDeBufSkillInfo);
                        }
                    }
                    else
                    {
                        baseController._Bufs.Add(BufDeBufSkillInfo.SkillType, BufDeBufSkillInfo);
                    }
                }
                else
                {
                    if (baseController._DeBufs.Count > 0)
                    {
                        st_SkillInfo FindDeBufSkillInfo = baseController._DeBufs.Values
                           .FirstOrDefault(DeBufItem => DeBufItem.SkillType == BufDeBufSkillInfo.SkillType);
                        if (FindDeBufSkillInfo != null)
                        {
                            FindDeBufSkillInfo = BufDeBufSkillInfo;
                        }
                        else
                        {
                            baseController._DeBufs.Add(BufDeBufSkillInfo.SkillType, BufDeBufSkillInfo);
                        }
                    }
                    else
                    {
                        baseController._DeBufs.Add(BufDeBufSkillInfo.SkillType, BufDeBufSkillInfo);
                    }
                }
            }

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                UI_MyCharacterHUD MyCharacterHUDUI = GameSceneUI._MyCharacterHUDUI;
                UI_TargetHUD TargetHUDUI = GameSceneUI._TargetHUDUI;

                if (TargetHUDUI._TargetObject && TargetHUDUI._TargetObject._GameObjectInfo.ObjectId == TargetId)
                {
                    if (BufDeBuf == true)
                    {
                        TargetHUDUI.TargetHUDBufUpdate(BufDeBufSkillInfo.SkillType);
                    }
                    else
                    {
                        TargetHUDUI.TargetHUDDeBufUpdate(BufDeBufSkillInfo.SkillType);
                    }
                }

                if (MyCharacterHUDUI._MyCharacterObject && MyCharacterHUDUI._MyCharacterObject._GameObjectInfo.ObjectId == TargetId)
                {
                    if (BufDeBuf == true)
                    {
                        MyCharacterHUDUI.MyCharacterBufUpdate(BufDeBufSkillInfo.SkillType);
                    }
                    else
                    {
                        MyCharacterHUDUI.MyCharacterDebufUpdate(BufDeBufSkillInfo.SkillType);
                    }
                }
            }

            S2C_BuDefMessage.Dispose();
        }

        public static void S2C_BufDeBufOffHandler(CMessage S2C_BufDeBufOffMessage)
        {
            long TargetId;
            bool BufDeBuf;
            short OffSkillType;

            S2C_BufDeBufOffMessage.GetData(out TargetId, sizeof(long));
            S2C_BufDeBufOffMessage.GetData(out BufDeBuf, sizeof(bool));
            S2C_BufDeBufOffMessage.GetData(out OffSkillType, sizeof(short));

            GameObject FindGameObject = Managers.Object.FindById(TargetId);
            if (FindGameObject != null)
            {
                PlayerObject baseController = FindGameObject.GetComponent<PlayerObject>();

                if (BufDeBuf == true)
                {
                    baseController._Bufs.Remove((en_SkillType)OffSkillType);
                }
                else
                {
                    baseController._DeBufs.Remove((en_SkillType)OffSkillType);
                }
            }

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                UI_MyCharacterHUD MyCharacterHUDUI = GameSceneUI._MyCharacterHUDUI;
                UI_TargetHUD TargetHUDUI = GameSceneUI._TargetHUDUI;

                if (TargetHUDUI._TargetObject && TargetHUDUI._TargetObject._GameObjectInfo.ObjectId == TargetId)
                {
                    if (BufDeBuf == true)
                    {
                        TargetHUDUI.TargetHUDBufUIDelete((en_SkillType)OffSkillType);
                    }
                    else
                    {
                        TargetHUDUI.TargetHUDDeBufUIDelete((en_SkillType)OffSkillType);
                    }
                }

                if (MyCharacterHUDUI._MyCharacterObject && MyCharacterHUDUI._MyCharacterObject._GameObjectInfo.ObjectId == TargetId)
                {
                    if (BufDeBuf == true)
                    {
                        MyCharacterHUDUI.MyCharacterBufUIDelete((en_SkillType)OffSkillType);
                    }
                    else
                    {
                        MyCharacterHUDUI.MyCharacterDeBufUIDelete((en_SkillType)OffSkillType);
                    }
                }
            }

            S2C_BufDeBufOffMessage.Dispose();
        }

        public static void S2C_ComboSkillOnMessageHandler(CMessage S2C_ComboSkillOnMessage)
        {
            byte ComboSkillQuickSlotPositionSize;

            st_SkillInfo ComboSkillInfo;

            S2C_ComboSkillOnMessage.GetData(out ComboSkillInfo);

            S2C_ComboSkillOnMessage.GetData(out ComboSkillQuickSlotPositionSize, sizeof(byte));

            for (byte i = 0; i < ComboSkillQuickSlotPositionSize; i++)
            {
                byte QuickSlotBarIndex;
                byte QuickSlotBarSlotIndex;

                S2C_ComboSkillOnMessage.GetData(out QuickSlotBarIndex, sizeof(byte));
                S2C_ComboSkillOnMessage.GetData(out QuickSlotBarSlotIndex, sizeof(byte));

                st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo = Managers.QuickSlotBar.FindQuickSlot(QuickSlotBarIndex, QuickSlotBarSlotIndex);
                QuickSlotBarSlotInfo.QuickBarSkillInfo = ComboSkillInfo;

                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

                // 서버에서 받은 연속기 스킬 UI 생성
                UI_QuickSlotBarItem ComboSkillQuickSlotItem = GameSceneUI._QuickSlotBarBoxUI._QuickSlotBars[QuickSlotBarIndex].QuickSlotCreate(QuickSlotBarSlotIndex);

                ComboSkillQuickSlotItem.SetQuickBarItem(QuickSlotBarSlotInfo);
                ComboSkillQuickSlotItem.QuickSlotBarComboSkillOn();

                GameSceneUI._QuickSlotBarBoxUI._QuickSlotBars[QuickSlotBarIndex]._QuickBarButtons[QuickSlotBarSlotIndex]._ComboSkillUI = ComboSkillQuickSlotItem;
            }
        }

        public static void S2C_ComboSkillOffMessageHandler(CMessage S2C_ComboSkillOffMessage)
        {
            byte ComboSkillQuickSlotPositionSize;

            byte QuickSlotBarIndex;
            byte QuickSlotBarSlotIndex;
            st_SkillInfo QuickSlotBarSlotSkillInfo;
            short OffComboSkillType;

            S2C_ComboSkillOffMessage.GetData(out QuickSlotBarSlotSkillInfo);
            S2C_ComboSkillOffMessage.GetData(out OffComboSkillType, sizeof(short));

            S2C_ComboSkillOffMessage.GetData(out ComboSkillQuickSlotPositionSize, sizeof(byte));

            for (byte i = 0; i < ComboSkillQuickSlotPositionSize; i++)
            {
                S2C_ComboSkillOffMessage.GetData(out QuickSlotBarIndex, sizeof(byte));
                S2C_ComboSkillOffMessage.GetData(out QuickSlotBarSlotIndex, sizeof(byte));

                st_QuickSlotBarSlotInfo FindQuickSlotBarSlotInfo = Managers.QuickSlotBar.FindQuickSlot(QuickSlotBarIndex, QuickSlotBarSlotIndex);
                FindQuickSlotBarSlotInfo.QuickBarSkillInfo = QuickSlotBarSlotSkillInfo;

                UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                UI_QuickSlotBarItem FindComboSkillOffItem = GameSceneUI._QuickSlotBarBoxUI._QuickSlotBars[QuickSlotBarIndex]._QuickBarButtons[QuickSlotBarSlotIndex]._ComboSkillUI;
                FindComboSkillOffItem.Destroy();

                GameSceneUI._QuickSlotBarBoxUI._QuickSlotBars[QuickSlotBarIndex]._QuickBarButtons[QuickSlotBarSlotIndex]._ComboSkillUI = null;
            }
        }

        public static void S2C_GlobalMessage(CMessage S2C_PersonalMessage)
        {
            byte PersonalMessageCount;

            S2C_PersonalMessage.GetData(out PersonalMessageCount, sizeof(byte));

            st_PersonalMessage[] PersonalMessages = new st_PersonalMessage[PersonalMessageCount];
            S2C_PersonalMessage.GetData(PersonalMessages, PersonalMessageCount);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI == null)
            {
                Debug.Log("GameSceneUI로 형변환 실패");
            }

            for (byte i = 0; i < PersonalMessageCount; i++)
            {
                UI_GlobalMessageBox PersonalMessageBoxUI = GameSceneUI._GlobalMessageBoxUI;
                PersonalMessageBoxUI.NewStatusAbnormalMessage(PersonalMessages[i].PersonalMessageType, PersonalMessages[i].PersonalMessage);
            }

            S2C_PersonalMessage.Dispose();
        }                

        public static void S2C_PartyInviteHandler(CMessage S2C_PartyInviteMessage)
        {
            long ReqPartyPlayerObjectID;
            string ReqPartyPlayerName;

            S2C_PartyInviteMessage.GetData(out ReqPartyPlayerObjectID, sizeof(long));
            S2C_PartyInviteMessage.GetData(out ReqPartyPlayerName);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                GameSceneUI._PartyReactionUI.SetPartyReactionBodytext(ReqPartyPlayerName, ReqPartyPlayerObjectID);
                GameSceneUI._PartyReactionUI.ShowCloseUI(true);
            }
        }

        public static void S2C_PartyAcceptHandler(CMessage S2C_PartyAcceptMessage)
        {
            byte PartyPlayerInfoSize;

            S2C_PartyAcceptMessage.GetData(out PartyPlayerInfoSize, sizeof(byte));

            st_GameObjectInfo[] PartyGameObjectInfos = new st_GameObjectInfo[PartyPlayerInfoSize];
            S2C_PartyAcceptMessage.GetData(PartyGameObjectInfos, PartyPlayerInfoSize);

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            GameSceneUI._PlayerOptionUI.ShowCloseUI(false);

            GameSceneUI._PartyFrameUI.PartyPlayerFrameAllDestroy();

            foreach (st_GameObjectInfo PartyPlayerGameObjectInfo in PartyGameObjectInfos)
            {
                GameSceneUI._PartyFrameUI.PartyPlayerFrameCreate(PartyPlayerGameObjectInfo);
            }
        }

        public static void S2C_PartyQuitHandler(CMessage S2C_PartyQuitMessage)
        {
            bool IsAllQuit;
            long PartyQuitPlayerID;

            S2C_PartyQuitMessage.GetData(out IsAllQuit, sizeof(long));
            S2C_PartyQuitMessage.GetData(out PartyQuitPlayerID, sizeof(long));

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                if (IsAllQuit == true)
                {
                    GameSceneUI._PartyFrameUI.PartyPlayerFrameAllDestroy();
                }
                else
                {
                    GameSceneUI._PartyFrameUI.PartyPlayerFrameDestory(PartyQuitPlayerID);
                }

                GameSceneUI._PartyPlayerOptionUI.ShowCloseUI(false);
            }
        }

        public static void S2C_PartyBanishHandler(CMessage S2C_PartyBanishMessage)
        {
            long PartyBanishPlayerID;

            S2C_PartyBanishMessage.GetData(out PartyBanishPlayerID, sizeof(long));

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {
                GameSceneUI._PartyFrameUI.PartyPlayerFrameDestory(PartyBanishPlayerID);

                GameSceneUI._PartyPlayerOptionUI.ShowCloseUI(false);
            }
        }

        public static void S2C_PartyLeaderManDate(CMessage S2C_PartyLeaderMandate)
        {
            long PartyLeaderMandatePlayerID;

            S2C_PartyLeaderMandate.GetData(out PartyLeaderMandatePlayerID, sizeof(long));

            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if (GameSceneUI != null)
            {

            }
        }

        public static void S2C_Ping(CMessage S2C_PingMessage)
        {
            CMessage PongMessage = new CMessage();
            PongMessage.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_PONG, sizeof(short));
            Managers.NetworkManager.GameServerSend(PongMessage);

            S2C_PingMessage.Dispose();
        }

        public static void S2C_AccountNew(CMessage S2C_AccountNewMessage)
        {
            bool AccountNewSuccess;

            S2C_AccountNewMessage.GetData(out AccountNewSuccess, sizeof(bool));

            UI_LoginScene LoginScene = Managers.UI._SceneUI as UI_LoginScene;

            UI_GlobalMessageBox PersonalMessageBoxUI = LoginScene._PersonalMessageBoxUI;
            if (AccountNewSuccess == true)
            {
                LoginScene.AccountPasswordFieldInit();
                //PersonalMessageBoxUI.NewPersonalMessage("회원가입 성공");
            }
            else
            {
                //PersonalMessageBoxUI.NewPersonalMessage("이미 있는 아이디 입니다.");
            }
        }

        public static void S2C_AccountLogin(CMessage S2C_LoginMessage)
        {
            byte LoginInfo;
            long AccountID;
            string AccountName;
            byte TokenLen;
            byte ServerListSize;

            S2C_LoginMessage.GetData(out LoginInfo, sizeof(byte));
            S2C_LoginMessage.GetData(out AccountID, sizeof(long));
            S2C_LoginMessage.GetData(out AccountName);
            S2C_LoginMessage.GetData(out TokenLen, sizeof(byte));

            byte[] Token = new byte[TokenLen];
            S2C_LoginMessage.GetData(Token, TokenLen);
            S2C_LoginMessage.GetData(out ServerListSize, sizeof(byte));

            st_ServerInfo[] ServerLists = new st_ServerInfo[ServerListSize];
            S2C_LoginMessage.GetData(ServerLists, ServerListSize);

            UI_LoginScene LoginScene = Managers.UI._SceneUI as UI_LoginScene;

            UI_GlobalMessageBox PersonalMessageBoxUI = LoginScene._PersonalMessageBoxUI;

            string PersonalMessage;
            switch ((en_LoginInfo)LoginInfo)
            {
                case en_LoginInfo.LOGIN_ACCOUNT_NOT_EXIST:
                    PersonalMessage = "존재하지 않는 아이디입니다.";
                    PersonalMessageBoxUI.NewStatusAbnormalMessage(en_GlobalMessageType.PERSONAL_MESSAGE_LOGIN_ACCOUNT_NOT_EXIST,
                    PersonalMessage);
                    break;
                case en_LoginInfo.LOGIN_ACCOUNT_OVERLAP:
                    PersonalMessage = "중복 로그인 입니다.";
                    PersonalMessageBoxUI.NewStatusAbnormalMessage(en_GlobalMessageType.PERSONAL_MESSAGE_LOGIN_ACCOUNT_OVERLAP,
                    PersonalMessage);
                    break;
                case en_LoginInfo.LOGIN_ACCOUNT_DB_WORKING:
                    PersonalMessage = "DB 작업중 입니다.";
                    PersonalMessageBoxUI.NewStatusAbnormalMessage(en_GlobalMessageType.PERSONAL_MESSAGE_LOGIN_ACCOUNT_DB_WORKING,
                    PersonalMessage);
                    break;
                case en_LoginInfo.LOGIN_ACCOUNT_DIFFERENT_PASSWORD:
                    PersonalMessage = "비밀번호가 다릅니다.";
                    PersonalMessageBoxUI.NewStatusAbnormalMessage(en_GlobalMessageType.PERSONAL_MESSAGE_LOGIN_ACCOUNT_DB_WORKING,
                    PersonalMessage);
                    break;
                case en_LoginInfo.LOGIN_ACCOUNT_LOGIN_SUCCESS:
                    LoginScene.AccountPasswordFieldInit();
                    LoginScene.LoginUIVisible(false);

                    Managers.NetworkManager._AccountId = AccountID;
                    Managers.NetworkManager._LoginID = AccountName;
                    Managers.NetworkManager._Token = Token;

                    // 서버선택 UI를 띄워준다.
                    UI_SelectServer SelectServerUI = LoginScene._SelectServerUI;
                    SelectServerUI.SetSelectServer(ServerLists, ServerListSize);

                    break;
            }

            S2C_LoginMessage.Dispose();
        }
    }

    public class MakePacket
    {
        public static CMessage ReqAccountNewPacket(string AccountName, string Password)
        {
            CMessage ReqAccountNewPacket = new CMessage();

            ReqAccountNewPacket.InsertData((short)Protocol.en_LOGIN_SERVER_PACKET_TYPE.en_LOGIN_SERVER_C2S_ACCOUNT_NEW, sizeof(short));
            ReqAccountNewPacket.InsertData(AccountName);
            ReqAccountNewPacket.InsertData(Password);

            return ReqAccountNewPacket;
        }

        public static CMessage ReqAccountServerLoginPacket(string AccountName, string Password)
        {
            CMessage AccountServerLoginPacket = new CMessage();

            AccountServerLoginPacket.InsertData((short)Protocol.en_LOGIN_SERVER_PACKET_TYPE.en_LOGIN_SERVER_C2S_ACCOUNT_LOGIN, sizeof(short));
            AccountServerLoginPacket.InsertData(AccountName);
            AccountServerLoginPacket.InsertData(Password);

            return AccountServerLoginPacket;
        }

        public static CMessage ReqGameServerLoginPacket(long AccountID, string AccountName, byte TokenLen, byte[] Token, bool IsDummy)
        {
            CMessage GameServerLoginPacket = new CMessage();

            GameServerLoginPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_GAME_REQ_LOGIN, sizeof(short));
            GameServerLoginPacket.InsertData(AccountID, sizeof(long));
            GameServerLoginPacket.InsertData(AccountName);
            GameServerLoginPacket.InsertData(IsDummy, sizeof(bool));
            GameServerLoginPacket.InsertData(TokenLen, sizeof(byte));
            GameServerLoginPacket.InsertData(Token, TokenLen);

            return GameServerLoginPacket;
        }

        public static CMessage ReqMakeAttackPacket(long AccountId, long PlayerId,
            byte QuickSlotBarIndex, byte QuickSlotBarSlotIndex,
            en_SkillCharacteristic ReqSkillCharacteristicType, en_SkillType ReqSkillType)
        {
            CMessage ReqAttackPacket = new CMessage();

            ReqAttackPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_ATTACK, sizeof(short));
            ReqAttackPacket.InsertData(AccountId, sizeof(long));
            ReqAttackPacket.InsertData(PlayerId, sizeof(long));            
            ReqAttackPacket.InsertData(QuickSlotBarIndex, sizeof(byte));
            ReqAttackPacket.InsertData(QuickSlotBarSlotIndex, sizeof(byte));
            ReqAttackPacket.InsertData((byte)ReqSkillCharacteristicType, sizeof(byte));
            ReqAttackPacket.InsertData((short)ReqSkillType, sizeof(short));

            return ReqAttackPacket;
        }

        public static CMessage ReqMakeMagicPacket(long AccountId, long PlayerId, en_SkillCharacteristic ReqSkillCharacteristicType, en_SkillType ReqSkillType)
        {
            CMessage ReqMagicPacket = new CMessage();

            ReqMagicPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_SPELL, sizeof(short));
            ReqMagicPacket.InsertData(AccountId, sizeof(long));
            ReqMagicPacket.InsertData(PlayerId, sizeof(long));            
            ReqMagicPacket.InsertData((byte)ReqSkillCharacteristicType, sizeof(byte));
            ReqMagicPacket.InsertData((short)ReqSkillType, sizeof(short));

            return ReqMagicPacket;
        }

        public static CMessage ReqMakeMagicCancelPacket(long AccountId, long PlayerId)
        {
            CMessage ReqMagicCancelPacket = new CMessage();

            ReqMagicCancelPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_MAGIC_CANCEL, sizeof(short));
            ReqMagicCancelPacket.InsertData(AccountId, sizeof(long));
            ReqMagicCancelPacket.InsertData(PlayerId, sizeof(long));

            return ReqMagicCancelPacket;
        }

        public static CMessage ReqMakeGatheringCancelPacket(long AccountID, long ObjectID)
        {
            CMessage ReqGatheringCancelPacket = new CMessage();

            ReqGatheringCancelPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_GATHERING_CANCEL, sizeof(short));
            ReqGatheringCancelPacket.InsertData(AccountID, sizeof(long));
            ReqGatheringCancelPacket.InsertData(ObjectID, sizeof(long));

            return ReqGatheringCancelPacket;
        }

        public static CMessage ReqMakeChattingPacket(long AccountId, long PlayerId, string ChattingText)
        {
            CMessage ReqChattingPacket = new CMessage();

            ReqChattingPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_MESSAGE, sizeof(short));
            ReqChattingPacket.InsertData(AccountId, sizeof(long));
            ReqChattingPacket.InsertData(PlayerId, sizeof(long));
            ReqChattingPacket.InsertData(ChattingText);

            return ReqChattingPacket;
        }

        public static CMessage ReqMakeLeftMouseWorldObjectInfoPacket(long AccountId, long PlayerId, long ObjectId, en_GameObjectType ObjectType)
        {
            CMessage ReqLeftMousePositionObjectInfoPacket = new CMessage();

            ReqLeftMousePositionObjectInfoPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_LEFT_MOUSE_OBJECT_INFO, sizeof(short));
            ReqLeftMousePositionObjectInfoPacket.InsertData(AccountId, sizeof(long));
            ReqLeftMousePositionObjectInfoPacket.InsertData(PlayerId, sizeof(long));
            ReqLeftMousePositionObjectInfoPacket.InsertData(ObjectId, sizeof(long));
            ReqLeftMousePositionObjectInfoPacket.InsertData((short)ObjectType, sizeof(short));


            return ReqLeftMousePositionObjectInfoPacket;
        }

        public static CMessage ReqMakeLeftMouseUIObjectInfoPacket(long AccountID, long PlayerID,
            long OwnerObjectID, en_GameObjectType OwnerObjectType,
            en_UIObjectInfo UIObjectinfo, en_SmallItemCategory LeftMouseItemCategory)
        {
            CMessage ReqLeftMouseUIObjectInfoPacket = new CMessage();

            ReqLeftMouseUIObjectInfoPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_LEFT_MOUSE_UI_OBJECT_INFO, sizeof(short));
            ReqLeftMouseUIObjectInfoPacket.InsertData(AccountID, sizeof(long));
            ReqLeftMouseUIObjectInfoPacket.InsertData(PlayerID, sizeof(long));
            ReqLeftMouseUIObjectInfoPacket.InsertData(OwnerObjectID, sizeof(long));
            ReqLeftMouseUIObjectInfoPacket.InsertData((short)OwnerObjectType, sizeof(short));
            ReqLeftMouseUIObjectInfoPacket.InsertData((short)UIObjectinfo, sizeof(short));
            ReqLeftMouseUIObjectInfoPacket.InsertData((short)LeftMouseItemCategory, sizeof(short));

            return ReqLeftMouseUIObjectInfoPacket;
        }

        public static CMessage ReqMakeRightMouseObjectInfoPacket(long AccountId, long PlayerId, long ObjectId, en_GameObjectType ObjectType)
        {
            CMessage ReqRightMouseObjectInfoPacket = new CMessage();

            ReqRightMouseObjectInfoPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_RIGHT_MOUSE_OBJECT_INFO, sizeof(short));
            ReqRightMouseObjectInfoPacket.InsertData(AccountId, sizeof(long));
            ReqRightMouseObjectInfoPacket.InsertData(PlayerId, sizeof(long));
            ReqRightMouseObjectInfoPacket.InsertData(ObjectId, sizeof(long));
            ReqRightMouseObjectInfoPacket.InsertData((short)ObjectType, sizeof(short));

            return ReqRightMouseObjectInfoPacket;
        }

        public static CMessage ReqMakeCraftingTableMaterialItemSubtractPacket(long AccountID, long PlayerID,
            long CraftingTableObjectID, en_GameObjectType OwnerObjectType,
            en_SmallItemCategory MaterialItemType)
        {
            CMessage ReqRightMouseUIObjectInfoPacket = new CMessage();

            ReqRightMouseUIObjectInfoPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_CRAFTING_TABLE_MATERIAL_ITEM_SUBTRACT, sizeof(short));
            ReqRightMouseUIObjectInfoPacket.InsertData(AccountID, sizeof(long));
            ReqRightMouseUIObjectInfoPacket.InsertData(PlayerID, sizeof(long));
            ReqRightMouseUIObjectInfoPacket.InsertData(CraftingTableObjectID, sizeof(long));
            ReqRightMouseUIObjectInfoPacket.InsertData((short)OwnerObjectType, sizeof(short));
            ReqRightMouseUIObjectInfoPacket.InsertData((short)MaterialItemType, sizeof(short));

            return ReqRightMouseUIObjectInfoPacket;
        }

        public static CMessage ReqMakeCraftingTableCompleteItemSubtractPacket(long AccountID, long PlayerID,
            long CraftingTableObjectID, en_GameObjectType OwnerObjectType,
            en_SmallItemCategory CompleteItemType)
        {
            CMessage ReqRightMouseUIObjectInfoPacket = new CMessage();

            ReqRightMouseUIObjectInfoPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_CRAFTING_TABLE_COMPLETE_ITEM_SUBTRACT, sizeof(short));
            ReqRightMouseUIObjectInfoPacket.InsertData(AccountID, sizeof(long));
            ReqRightMouseUIObjectInfoPacket.InsertData(PlayerID, sizeof(long));
            ReqRightMouseUIObjectInfoPacket.InsertData(CraftingTableObjectID, sizeof(long));
            ReqRightMouseUIObjectInfoPacket.InsertData((short)OwnerObjectType, sizeof(short));
            ReqRightMouseUIObjectInfoPacket.InsertData((short)CompleteItemType, sizeof(short));

            return ReqRightMouseUIObjectInfoPacket;
        }

        public static CMessage ReqMakeCraftingTableNonSelectPacket(long AccountID, long PlayerID, long CraftingTableObjectID, en_GameObjectType CraftingTableObjectType)
        {
            CMessage ReqCraftingTableNonSelectPacket = new CMessage();

            ReqCraftingTableNonSelectPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_CRAFTING_TABLE_NON_SELECT, sizeof(short));
            ReqCraftingTableNonSelectPacket.InsertData(AccountID, sizeof(long));
            ReqCraftingTableNonSelectPacket.InsertData(PlayerID, sizeof(long));
            ReqCraftingTableNonSelectPacket.InsertData(CraftingTableObjectID, sizeof(long));
            ReqCraftingTableNonSelectPacket.InsertData((short)CraftingTableObjectType, sizeof(short));

            return ReqCraftingTableNonSelectPacket;
        }

        public static CMessage ReqMakeGatheringPacket(long AccountID, long PlayerID, long ObjectID, en_GameObjectType GameObjectType)
        {
            CMessage ReqRightMousePositionPacket = new CMessage();

            ReqRightMousePositionPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_GATHERING, sizeof(short));
            ReqRightMousePositionPacket.InsertData(AccountID, sizeof(long));
            ReqRightMousePositionPacket.InsertData(PlayerID, sizeof(long));
            ReqRightMousePositionPacket.InsertData(ObjectID, sizeof(long));
            ReqRightMousePositionPacket.InsertData((short)GameObjectType, sizeof(short));

            return ReqRightMousePositionPacket;
        }

        public static CMessage ReqMakeMovePacket(float DirectionX, float DirectionY, float StartPositionX, float StartPositionY,  en_CreatureState MoveState)
        {
            CMessage ReqMovePacket = new CMessage();
            ReqMovePacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_MOVE, sizeof(short));
            ReqMovePacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqMovePacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));
            ReqMovePacket.InsertData(DirectionX, sizeof(float));
            ReqMovePacket.InsertData(DirectionY, sizeof(float));
            ReqMovePacket.InsertData(StartPositionX, sizeof(float));
            ReqMovePacket.InsertData(StartPositionY, sizeof(float));
            ReqMovePacket.InsertData((byte)MoveState, sizeof(byte));

            return ReqMovePacket;
        }

        public static CMessage ReqMakeMoveStopPacket(float StopPositionX, float StopPositionY, en_CreatureState StopState)
        {
            CMessage ReqMoveStopPacket = new CMessage();
            ReqMoveStopPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_MOVE_STOP, sizeof(short));
            ReqMoveStopPacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqMoveStopPacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));
            ReqMoveStopPacket.InsertData(StopPositionX, sizeof(float));
            ReqMoveStopPacket.InsertData(StopPositionY, sizeof(float));
            ReqMoveStopPacket.InsertData((byte)StopState, sizeof(byte));

            return ReqMoveStopPacket;
        }

        public static CMessage ReqMakeSelectItemPacket(long AccountId, long PlayerId,
            short SelectItemTileGridPositionX, short SelectItemTileGridPositionY)
        {
            CMessage ReqSelectItemPacket = new CMessage();
            ReqSelectItemPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_ITEM_SELECT, sizeof(short));
            ReqSelectItemPacket.InsertData(AccountId, sizeof(long));
            ReqSelectItemPacket.InsertData(PlayerId, sizeof(long));
            ReqSelectItemPacket.InsertData(SelectItemTileGridPositionX, sizeof(short));
            ReqSelectItemPacket.InsertData(SelectItemTileGridPositionY, sizeof(short));

            return ReqSelectItemPacket;
        }

        public static CMessage ReqMakePlaceItemPacket(long AccountId, long PlayerId, short TileGridPositionX, short TileGridPositionY)
        {
            CMessage ReqPlaceItemPacket = new CMessage();
            ReqPlaceItemPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_ITEM_PLACE, sizeof(short));
            ReqPlaceItemPacket.InsertData(AccountId, sizeof(long));
            ReqPlaceItemPacket.InsertData(PlayerId, sizeof(long));
            ReqPlaceItemPacket.InsertData(TileGridPositionX, sizeof(short));
            ReqPlaceItemPacket.InsertData(TileGridPositionY, sizeof(short));

            return ReqPlaceItemPacket;
        }

        public static CMessage ReqMakeRotateItemPacket(long AccountID, long PlayerID,
            en_SmallItemCategory RotateItemCategory)
        {
            CMessage ReqRotateItemPacket = new CMessage();
            ReqRotateItemPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_ITEM_ROTATE, sizeof(short));
            ReqRotateItemPacket.InsertData(AccountID, sizeof(long));
            ReqRotateItemPacket.InsertData(PlayerID, sizeof(long));
            ReqRotateItemPacket.InsertData((short)RotateItemCategory, sizeof(short));

            return ReqRotateItemPacket;
        }

        public static CMessage ReqMakeItemLootingPacket(long AccountId, st_PositionInfo ItemPositionInfo)
        {
            CMessage ReqItemLootingPacket = new CMessage();
            ReqItemLootingPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_LOOTING, sizeof(short));
            ReqItemLootingPacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqItemLootingPacket.InsertData((byte)ItemPositionInfo.State, sizeof(byte));
            ReqItemLootingPacket.InsertData(ItemPositionInfo.CollsitionPositionX, sizeof(int));
            ReqItemLootingPacket.InsertData(ItemPositionInfo.CollsitionPositionY, sizeof(int));            

            return ReqItemLootingPacket;
        }

        public static CMessage ReqMakeItemDropPacket(long AccountID, long PlayerID, en_SmallItemCategory DropItemType, int DropItemCount)
        {
            CMessage ReqItemDropPacket = new CMessage();

            ReqItemDropPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_ITEM_DROP, sizeof(short));
            ReqItemDropPacket.InsertData(AccountID, sizeof(long));
            ReqItemDropPacket.InsertData(PlayerID, sizeof(long));
            ReqItemDropPacket.InsertData((short)DropItemType, sizeof(short));
            ReqItemDropPacket.InsertData(DropItemCount, sizeof(int));

            return ReqItemDropPacket;
        }

        public static CMessage ReqMakeCraftingTableItemAddPacket(long AccountID, long PlayerID, long CraftingTableObjectID, en_SmallItemCategory InputItemSmallCateogry, short InputItemCount)
        {
            CMessage ReqCraftingTableItemAddPacket = new CMessage();

            ReqCraftingTableItemAddPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_CRAFTING_TABLE_ITEM_ADD, sizeof(short));
            ReqCraftingTableItemAddPacket.InsertData(AccountID, sizeof(long));
            ReqCraftingTableItemAddPacket.InsertData(PlayerID, sizeof(long));
            ReqCraftingTableItemAddPacket.InsertData(CraftingTableObjectID, sizeof(long));
            ReqCraftingTableItemAddPacket.InsertData((short)InputItemSmallCateogry, sizeof(short));
            ReqCraftingTableItemAddPacket.InsertData(InputItemCount, sizeof(short));

            return ReqCraftingTableItemAddPacket;
        }

        public static CMessage ReqMakeCraftingTableCraftingStartPacket(long AccountID, long PlayerID, long CraftingTableObjectID,
            en_SmallItemCategory CraftingCompleteItemType, short CraftingCount)
        {
            CMessage ReqCraftingTableCraftingStartPacket = new CMessage();

            ReqCraftingTableCraftingStartPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_CRAFTING_TABLE_CRAFTING_START, sizeof(short));
            ReqCraftingTableCraftingStartPacket.InsertData(AccountID, sizeof(long));
            ReqCraftingTableCraftingStartPacket.InsertData(PlayerID, sizeof(long));
            ReqCraftingTableCraftingStartPacket.InsertData(CraftingTableObjectID, sizeof(long));
            ReqCraftingTableCraftingStartPacket.InsertData((short)CraftingCompleteItemType, sizeof(short));
            ReqCraftingTableCraftingStartPacket.InsertData(CraftingCount, sizeof(short));

            return ReqCraftingTableCraftingStartPacket;
        }

        public static CMessage ReqMakeCraftingTableCraftingStopPacket(long AccountID, long PlayerID, long CraftingTableObjectID)
        {
            CMessage ReqCraftingTableCraftingStopPacket = new CMessage();

            ReqCraftingTableCraftingStopPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_CRAFTING_TABLE_CRAFTING_STOP, sizeof(short));
            ReqCraftingTableCraftingStopPacket.InsertData(AccountID, sizeof(long));
            ReqCraftingTableCraftingStopPacket.InsertData(PlayerID, sizeof(long));
            ReqCraftingTableCraftingStopPacket.InsertData(CraftingTableObjectID, sizeof(long));

            return ReqCraftingTableCraftingStopPacket;
        }

        public static CMessage ReqMakeQuickSlotSwapPacket(long AccountId, long PlayerId, byte SwapQuickSlotBarIndexA, byte SwapQuickSlotBarSlotIndexA, byte SwapQuickSlotBarIndexB, byte SwapQuickSlotBarSlotIndexB)
        {
            CMessage ReqQuickSlotSwapPacket = new CMessage();

            ReqQuickSlotSwapPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_QUICKSLOT_SWAP, sizeof(short));
            ReqQuickSlotSwapPacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqQuickSlotSwapPacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));

            ReqQuickSlotSwapPacket.InsertData(SwapQuickSlotBarIndexA, sizeof(byte));
            ReqQuickSlotSwapPacket.InsertData(SwapQuickSlotBarSlotIndexA, sizeof(byte));

            ReqQuickSlotSwapPacket.InsertData(SwapQuickSlotBarIndexB, sizeof(byte));
            ReqQuickSlotSwapPacket.InsertData(SwapQuickSlotBarSlotIndexB, sizeof(byte));

            return ReqQuickSlotSwapPacket;
        }

        public static CMessage ReqMakeItemUsePacket(long AccountId, long PlayerId, en_SmallItemCategory UseItemSmallCategory, short TilePositionX, short TilePositionY)
        {
            CMessage ReqItemUsePacket = new CMessage();

            ReqItemUsePacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_INVENTORY_ITEM_USE, sizeof(short));
            ReqItemUsePacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqItemUsePacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));
            ReqItemUsePacket.InsertData((short)UseItemSmallCategory, sizeof(short));
            ReqItemUsePacket.InsertData(TilePositionX, sizeof(short));
            ReqItemUsePacket.InsertData(TilePositionY, sizeof(short));

            return ReqItemUsePacket;
        }        

        public static CMessage ReqMakeCreateCharacterPacket(string CreateCharacterName, byte CreateCharacterSlotIndex)
        {
            CMessage ReqCreateCharacterPacket = new CMessage();
            ReqCreateCharacterPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_GAME_CREATE_CHARACTER, sizeof(short));            
            ReqCreateCharacterPacket.InsertData(CreateCharacterName);
            ReqCreateCharacterPacket.InsertData(CreateCharacterSlotIndex, sizeof(byte));

            return ReqCreateCharacterPacket;
        }

        public static CMessage ReqSelectSkillChracteristicPacket(long AccountID, long PlayerID, byte SkillCharacteristicIndex, en_SkillCharacteristic SelectSkillCharacteristicType)
        {
            CMessage ReqSelectSkillCharacteristicPacket = new CMessage();

            ReqSelectSkillCharacteristicPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_SELECT_SKILL_CHARACTERISTIC, sizeof(short));
            ReqSelectSkillCharacteristicPacket.InsertData(AccountID, sizeof(long));
            ReqSelectSkillCharacteristicPacket.InsertData(PlayerID, sizeof(long));
            ReqSelectSkillCharacteristicPacket.InsertData(SkillCharacteristicIndex, sizeof(byte));
            ReqSelectSkillCharacteristicPacket.InsertData((byte)SelectSkillCharacteristicType, sizeof(byte));

            return ReqSelectSkillCharacteristicPacket;
        }

        public static CMessage ReqMakeEnterGamePacket(long AccountId, string EnterGameCharacterName)
        {
            CMessage ReqEnterGamePacket = new CMessage();
            ReqEnterGamePacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_GAME_ENTER, sizeof(short));
            ReqEnterGamePacket.InsertData(AccountId, sizeof(long));
            ReqEnterGamePacket.InsertData(EnterGameCharacterName);

            return ReqEnterGamePacket;
        }

        public static CMessage ReqSkillLearnPacket(long AccountID, long PlayerID, bool SkillLearan, byte SkillCharacteristicIndex, en_SkillCharacteristic SkillCharacteristicType, en_SkillType SkillType)
        {
            CMessage ReqSkillLearnPacket = new CMessage();
            ReqSkillLearnPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_LEARN_SKILL, sizeof(short));
            ReqSkillLearnPacket.InsertData(AccountID, sizeof(long));
            ReqSkillLearnPacket.InsertData(PlayerID, sizeof(long));
            ReqSkillLearnPacket.InsertData(SkillLearan, sizeof(bool));
            ReqSkillLearnPacket.InsertData(SkillCharacteristicIndex, sizeof(byte));
            ReqSkillLearnPacket.InsertData((byte)SkillCharacteristicType, sizeof(byte));
            ReqSkillLearnPacket.InsertData((short)SkillType, sizeof(short));

            return ReqSkillLearnPacket;
        }

        public static CMessage ReqMakeQuickSlotSavePacket(long AccountId, long PlayerId, st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo)
        {
            CMessage ReqQuickSlotSavePacket = new CMessage();
            ReqQuickSlotSavePacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_QUICKSLOT_SAVE, sizeof(short));
            ReqQuickSlotSavePacket.InsertData(AccountId, sizeof(long));
            ReqQuickSlotSavePacket.InsertData(PlayerId, sizeof(long));
            ReqQuickSlotSavePacket.InsertData(QuickSlotBarSlotInfo.QuickSlotBarIndex, sizeof(byte));
            ReqQuickSlotSavePacket.InsertData(QuickSlotBarSlotInfo.QuickSlotBarSlotIndex, sizeof(byte));
            ReqQuickSlotSavePacket.InsertData((short)QuickSlotBarSlotInfo.QuickSlotKey, sizeof(short));

            if (QuickSlotBarSlotInfo.QuickBarSkillInfo != null)
            {
                ReqQuickSlotSavePacket.InsertData(true, sizeof(bool));
                ReqQuickSlotSavePacket.InsertData((byte)QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillLargeCategory, sizeof(byte));
                ReqQuickSlotSavePacket.InsertData((byte)QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillMediumCategory, sizeof(byte));
                ReqQuickSlotSavePacket.InsertData((byte)QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillCharacteristic, sizeof(byte));
                ReqQuickSlotSavePacket.InsertData((short)QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType, sizeof(short));
            }
            else
            {
                ReqQuickSlotSavePacket.InsertData(false, sizeof(bool));
            }

            if (QuickSlotBarSlotInfo.QuickBarItemInfo != null)
            {
                ReqQuickSlotSavePacket.InsertData(true, sizeof(bool));
                ReqQuickSlotSavePacket.InsertData((byte)QuickSlotBarSlotInfo.QuickBarItemInfo.ItemLargeCategory, sizeof(byte));
                ReqQuickSlotSavePacket.InsertData((byte)QuickSlotBarSlotInfo.QuickBarItemInfo.ItemMediumCategory, sizeof(byte));
                ReqQuickSlotSavePacket.InsertData((short)QuickSlotBarSlotInfo.QuickBarItemInfo.ItemSmallCategory, sizeof(short));
            }
            else
            {
                ReqQuickSlotSavePacket.InsertData(false, sizeof(bool));
            }

            return ReqQuickSlotSavePacket;
        }

        public static CMessage ReqMakeQuickSlotInitPacket(long AccountId, long PlayerId, en_SkillCharacteristic InitSkillCharacteristic, en_SkillType InitSkillType, byte QuickSlotBarIndex, byte QuickSlotBarSlotIndex)
        {
            CMessage ReqQuickSlotInitPacket = new CMessage();
            ReqQuickSlotInitPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_QUICKSLOT_EMPTY, sizeof(short));
            ReqQuickSlotInitPacket.InsertData(AccountId, sizeof(long));
            ReqQuickSlotInitPacket.InsertData(PlayerId, sizeof(long));
            ReqQuickSlotInitPacket.InsertData((byte)InitSkillCharacteristic, sizeof(byte));
            ReqQuickSlotInitPacket.InsertData((short)InitSkillType, sizeof(short));
            ReqQuickSlotInitPacket.InsertData(QuickSlotBarIndex, sizeof(byte));
            ReqQuickSlotInitPacket.InsertData(QuickSlotBarSlotIndex, sizeof(byte));

            return ReqQuickSlotInitPacket;
        }

        public static CMessage ReqMakeCraftingConfirmPacket(long AccountId, long PlayerId,
            en_LargeItemCategory CategoryType, en_SmallItemCategory CraftingItemType,
            short CraftingItemCount, byte MaterialCount,
            List<st_CraftingMaterialItemInfo> Materials)
        {
            CMessage ReqCraftingConfirmPacket = new CMessage();
            ReqCraftingConfirmPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_CRAFTING_CONFIRM, sizeof(short));
            ReqCraftingConfirmPacket.InsertData(AccountId, sizeof(long));
            ReqCraftingConfirmPacket.InsertData(PlayerId, sizeof(long));
            ReqCraftingConfirmPacket.InsertData((byte)CategoryType, sizeof(byte));
            ReqCraftingConfirmPacket.InsertData((short)CraftingItemType, sizeof(short));
            ReqCraftingConfirmPacket.InsertData(CraftingItemCount, sizeof(short));
            ReqCraftingConfirmPacket.InsertData(MaterialCount, sizeof(byte));

            st_CraftingMaterialItemInfo[] MaterialsArray = Materials.ToArray();
            for (int i = 0; i < MaterialCount; i++)
            {
                ReqCraftingConfirmPacket.InsertData((short)MaterialsArray[i].MaterialItemType, sizeof(short));

                // 보내야할 Material 개수
                short ReqMaterialCount = (short)(MaterialsArray[i].ItemCount * CraftingItemCount);

                // 인벤토리에 있는 Material 개수
                //short InventoryMaterialCount = Managers.Inventory.FindCount(MaterialsArray[i].MaterialItemType);

                //if(ReqMaterialCount <= InventoryMaterialCount)
                //{
                //    ReqCraftingConfirmPacket.InsertData(ReqMaterialCount, sizeof(short));
                //}
                //else
                //{
                //    // 보내야할 Material 개수 보다 인벤토리 안에 있는 아이템이 더 적을 경우
                //    return null;
                //}
            }

            return ReqCraftingConfirmPacket;
        }

        public static CMessage ReqMakeOffEquipmentPacket(long AccountID, long PlayerID, en_EquipmentParts OffEquipmentParts)
        {
            CMessage ReqOffEquipmentPacket = new CMessage();

            ReqOffEquipmentPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_OFF_EQUIPMENT, sizeof(short));
            ReqOffEquipmentPacket.InsertData(AccountID, sizeof(long));
            ReqOffEquipmentPacket.InsertData(PlayerID, sizeof(long));
            ReqOffEquipmentPacket.InsertData((byte)OffEquipmentParts, sizeof(byte));

            return ReqOffEquipmentPacket;
        }

        public static CMessage ReqMakeSeedFarmingPacket(long AccountID, long PlayerID, en_SmallItemCategory SeedSmallItemCategory)
        {
            CMessage ReqSeedFarmingPacket = new CMessage();

            ReqSeedFarmingPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_SEED_FARMING, sizeof(short));
            ReqSeedFarmingPacket.InsertData(AccountID, sizeof(long));
            ReqSeedFarmingPacket.InsertData(PlayerID, sizeof(long));
            ReqSeedFarmingPacket.InsertData((short)SeedSmallItemCategory, sizeof(short));

            return ReqSeedFarmingPacket;
        }

        public static CMessage ReqMakePlantGrowthPacket(long AccountID, long PlayerID, long PlantObjectID, en_GameObjectType PlantObjectType)
        {
            CMessage ReqPlantGrowthPacket = new CMessage();

            ReqPlantGrowthPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_PLANT_GROWTH_CHECK, sizeof(short));

            ReqPlantGrowthPacket.InsertData(AccountID, sizeof(long));
            ReqPlantGrowthPacket.InsertData(PlayerID, sizeof(long));
            ReqPlantGrowthPacket.InsertData(PlantObjectID, sizeof(long));
            ReqPlantGrowthPacket.InsertData((short)PlantObjectType, sizeof(short));

            return ReqPlantGrowthPacket;
        }

        public static CMessage ReqMakePartyInvitePacket(long PartyPlayerID)
        {
            CMessage ReqPartyInvitePacket = new CMessage();

            ReqPartyInvitePacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_PARTY_INVITE, sizeof(short));
            ReqPartyInvitePacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqPartyInvitePacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));
            ReqPartyInvitePacket.InsertData(PartyPlayerID, sizeof(long));

            return ReqPartyInvitePacket;
        }

        public static CMessage ReqMakePartyInviteAcceptPacket(long ReqPartyPlayerID)
        {
            CMessage ReqPartyInviteAcceptPacket = new CMessage();

            ReqPartyInviteAcceptPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_PARTY_INVITE_ACCEPT, sizeof(short));
            ReqPartyInviteAcceptPacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqPartyInviteAcceptPacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));
            ReqPartyInviteAcceptPacket.InsertData(ReqPartyPlayerID, sizeof(long));

            return ReqPartyInviteAcceptPacket;
        }

        public static CMessage ReqMakePartyInviteRejectPacket(long ReqPartyInvitePlayerID)
        {
            CMessage ReqPartyInviteRejectPacket = new CMessage();

            ReqPartyInviteRejectPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_PARTY_INVITE_REJECT, sizeof(short));
            ReqPartyInviteRejectPacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqPartyInviteRejectPacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));
            ReqPartyInviteRejectPacket.InsertData(ReqPartyInvitePlayerID, sizeof(long));

            return ReqPartyInviteRejectPacket;
        }

        public static CMessage ReqMakePartyQuitPacket()
        {
            CMessage ReqPartyQuitPacket = new CMessage();

            ReqPartyQuitPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_PARTY_QUIT, sizeof(short));
            ReqPartyQuitPacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqPartyQuitPacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));

            return ReqPartyQuitPacket;
        }

        public static CMessage ReqMakePartyBanishPacket(long PartyBanishPlayerID)
        {
            CMessage ReqPartyBanishPacket = new CMessage();

            ReqPartyBanishPacket.InsertData((short)Protocol.en_GAME_SERVER_PACKET_TYPE.en_PACKET_C2S_PARTY_BANISH, sizeof(short));
            ReqPartyBanishPacket.InsertData(Managers.NetworkManager._AccountId, sizeof(long));
            ReqPartyBanishPacket.InsertData(Managers.NetworkManager._PlayerDBId, sizeof(long));
            ReqPartyBanishPacket.InsertData(PartyBanishPlayerID, sizeof(long));

            return ReqPartyBanishPacket;
        }
    }
}
