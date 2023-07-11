using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    private GameObject _Root;

    Dictionary<en_ResourceName, CObjectPool> _ObjectPools = new Dictionary<en_ResourceName, CObjectPool>();
    Dictionary<long, GameObject> _Objects = new Dictionary<long, GameObject>();    
    
    public class CPoolObject
    {
        public GameObject Object;        
    }

    class CObjectPool
    {
        en_ResourceName _ObjectPoolType;

        Stack<CPoolObject> _PoolStacks = new Stack<CPoolObject>();
        int _AllocCount;
        int _UseCount;

        public CObjectPool(en_ResourceName ObjectPoolType)
        {
            _ObjectPoolType = ObjectPoolType;
            _AllocCount = 0;
            _UseCount = 0;
        }

        public void Push(CPoolObject PoolObject)
        {
            if(PoolObject.Object != null)
            {
                _PoolStacks.Push(PoolObject);
                _UseCount--;
            }
        }

        public CPoolObject Pop()
        {   
            CPoolObject PopObject = new CPoolObject();            

            if(_AllocCount > _UseCount)
            {                
                PopObject = _PoolStacks.Pop();
            }
            else
            {                
                PopObject.Object = Managers.Resource.Instantiate(_ObjectPoolType);

                _AllocCount++;
            }

            _UseCount++;

            PopObject.Object.gameObject.SetActive(true);            

            return PopObject;
        }
    }

    public void Init()
    {
        if(_Root == null)
        {
            _Root = new GameObject { name = "@ObjectPool" };
            UnityEngine.Object.DontDestroyOnLoad(_Root);

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_PLAYER, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_PLAYER));                        
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_NON_PLAYER_GENERAL_MERCHANT, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_NON_PLAYER_GENERAL_MERCHANT));                        
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_GOBLIN, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_GOBLIN));            

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_STONE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_STONE));            
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_TREE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_TREE));

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_CROP_POTATO, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_CROP_POTATO));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_CROP_CORN, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_CROP_CORN));

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_SWORD_BLADE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_SWORD_BLADE));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_FLAME_BOLT, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_FLAME_BOLT));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_DIVINE_BOLT, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_SKILL_DIVINE_BOLT));

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_SAWMILL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_SAWMILL));

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_LEATHER, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_LEATHER));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_STONE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_STONE));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_CHARCOAL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_CHARCOAL));

            _ObjectPools.Add(en_ResourceName.CLIENT_COLLISION_RECT, new CObjectPool(en_ResourceName.CLIENT_COLLISION_RECT));

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_LEFT_RIGHT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_LEFT_RIGHT_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_UP_DOWN_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_UP_DOWN_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_UP_TO_LEFT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_UP_TO_LEFT_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_UP_TO_RIGHT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_UP_TO_RIGHT_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_DOWN_TO_LEFT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_DOWN_TO_LEFT_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_DOWN_TO_RIGHT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_DOWN_TO_RIGHT_WALL));                                
        }
    }

    public GameObject RectCollisionWorldSpawn(byte CollisionPositionType, float PositionX, float PositionY, float DirectionX, float DirectionY, float SizeX, float SizeY)
    {
        GameObject RectCollisionGO = _ObjectPools[en_ResourceName.CLIENT_COLLISION_RECT].Pop().Object;
        
        RectCollision rectCollision = RectCollisionGO.GetComponent<RectCollision>();
        if(rectCollision != null)
        {
            Vector2 Position = new Vector2(PositionX, PositionY);
            Vector2 Direction = new Vector2(DirectionX, DirectionY);            
            Vector2 Size = new Vector2(SizeX, SizeY);

            RectCollisionGO.transform.position = new Vector3(PositionX, PositionY, 0);            

            rectCollision.SetPositionDirection((en_CollisionPosition)CollisionPositionType, Position, Direction, Size);
        }

        return RectCollisionGO;
    }

    public GameObject Add(st_GameObjectInfo Info)
    {
        // 이미 추가한 오브젝트인지 확인
        if (_Objects.ContainsKey(Info.ObjectId))
        {
            return null;
        }        

        switch (Info.ObjectType)
        {
            case en_GameObjectType.OBJECT_PLAYER:
                GameObject PlayerGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_PLAYER].Pop().Object;
                PlayerGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, PlayerGO);
                
                PlayerObject Player = PlayerGO.GetComponent<PlayerObject>();
                Player._GameObjectInfo = Info;
                Player.Init();                                

                return PlayerGO;
            case en_GameObjectType.OBJECT_NON_PLAYER_GENERAL_MERCHANT:
                GameObject GeneralMerchantGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_NON_PLAYER_GENERAL_MERCHANT].Pop().Object;
                GeneralMerchantGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, GeneralMerchantGO);

                GeneralMerchantNPC GeneralMerchant = GeneralMerchantGO.GetComponent<GeneralMerchantNPC>();
                GeneralMerchant._GameObjectInfo = Info;
                GeneralMerchant.Init();

                return GeneralMerchantGO;
            case en_GameObjectType.OBJECT_GOBLIN:
                GameObject GoblinGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_GOBLIN].Pop().Object;
                GoblinGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, GoblinGO);

                CGoblinObject Goblin = GoblinGO.GetComponent<CGoblinObject>();
                Goblin._GameObjectInfo = Info;
                Goblin.Init();

                Goblin.CreatureSpriteShowClose(true);

                if (Goblin._GameObjectInfo.ObjectStatInfo.HP > 0)
                {
                    Goblin.CreatureObjectWeaponShowClose(true);
                    Goblin.CreatureObjectNameShowClose(true);

                    GameObject RightWeaponParent = Goblin.transform.Find("RightWeaponParent").gameObject;
                    if (RightWeaponParent != null)
                    {
                        PlayerWeapon Weapon = RightWeaponParent.GetComponent<PlayerWeapon>();
                        if (Weapon != null)
                        {
                            Weapon.Init();
                        }
                    }
                }
                else
                {
                    Goblin.CreatureObjectWeaponShowClose(false);
                    Goblin.CreatureObjectNameShowClose(false);
                }                

                Goblin.GetComponent<GameObjectMovement>().SetOwner(Goblin);                
                return GoblinGO;            
            case en_GameObjectType.OBJECT_STONE:
                GameObject StoneGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_STONE].Pop().Object;
                StoneGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, StoneGo);

                EnvironmentController StoneController = StoneGo.GetComponent<EnvironmentController>();
                StoneController._GameObjectInfo = Info;
                StoneController.Init();
                
                return StoneGo;
            case en_GameObjectType.OBJECT_TREE:
                GameObject TreeGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_TREE].Pop().Object;
                TreeGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, TreeGo);

                EnvironmentController TreeController = TreeGo.GetComponent<EnvironmentController>();
                TreeController._GameObjectInfo = Info;
                TreeController.Init();
                
                return TreeGo;
            case en_GameObjectType.OBJECT_CROP_POTATO:
                GameObject PotatoGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CROP_POTATO].Pop().Object;
                PotatoGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, PotatoGo);

                PotatoController PotatoController = PotatoGo.GetComponent<PotatoController>();
                PotatoController._GameObjectInfo = Info;
                PotatoController.Init();                

                return PotatoGo;
            case en_GameObjectType.OBJECT_CROP_CORN:
                GameObject CornGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CROP_CORN].Pop().Object;
                CornGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, CornGO);

                CornController Corn = CornGO.GetComponent<CornController>();
                Corn._GameObjectInfo = Info;
                Corn.Init();                

                return CornGO;
            case en_GameObjectType.OBJECT_SKILL_SWORD_BLADE:
                GameObject SwordBladeGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_SKILL_SWORD_BLADE].Pop().Object;
                SwordBladeGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, SwordBladeGO);

                SwordBladeObject SwordBlade = SwordBladeGO.GetComponent<SwordBladeObject>();
                SwordBlade._GameObjectInfo = Info;
                SwordBlade.Init();

                SwordBlade.CreatureSpriteShowClose(true);

                return SwordBladeGO;
            case en_GameObjectType.OBJECT_SKILL_FLAME_BOLT:
                GameObject FlameBoltGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_SKILL_FLAME_BOLT].Pop().Object;
                FlameBoltGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, FlameBoltGO);

                FlameBoltObject FlameBolt = FlameBoltGO.GetComponent<FlameBoltObject>();
                FlameBolt._GameObjectInfo = Info;
                FlameBolt.Init();

                FlameBolt.CreatureSpriteShowClose(true);

                return FlameBoltGO;
            case en_GameObjectType.OBJECT_SKILL_DIVINE_BOLT:
                GameObject DivineBoltGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_SKILL_DIVINE_BOLT].Pop().Object;
                DivineBoltGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, DivineBoltGo);

                DivineBoltObject DivineBolt = DivineBoltGo.GetComponent<DivineBoltObject>();
                DivineBolt._GameObjectInfo = Info;
                DivineBolt.Init();

                DivineBolt.CreatureSpriteShowClose(true);

                return DivineBoltGo;
            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                GameObject FurnaceGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE].Pop().Object;                
                FurnaceGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, FurnaceGO);

                FurnaceController Furnace = FurnaceGO.GetComponent<FurnaceController>();
                Furnace._GameObjectInfo = Info;
                Furnace.Init();                

                return FurnaceGO;
            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                GameObject SawmillGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_SAWMILL].Pop().Object;
                SawmillGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, SawmillGO);

                SawmillController Sawmill = SawmillGO.GetComponent<SawmillController>();
                Sawmill._GameObjectInfo = Info;

                Sawmill.Init();

                float OriginalX = Sawmill._GameObjectInfo.ObjectPositionInfo.Position.x;
                float SumX = 0;

                float OriginalY = Sawmill._GameObjectInfo.ObjectPositionInfo.Position.y;
                float SumY = 0;

                for (int i = 0; i < Sawmill._GameObjectInfo.ObjectWidth; i++)
                {
                    SumX = SumX + (OriginalX + i);
                }

                for (int i = 0; i < Sawmill._GameObjectInfo.ObjectHeight; i++)
                {
                    SumY = SumY + (OriginalY + i);
                }

                Sawmill.transform.position = new Vector3(SumX / Sawmill._GameObjectInfo.ObjectWidth, SumY / Sawmill._GameObjectInfo.ObjectHeight);

                return SawmillGO;            
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_LEATHER:
                GameObject LeatherGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_LEATHER].Pop().Object;
                LeatherGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, LeatherGo);

                CItemObject LeatherController = LeatherGo.GetComponent<CItemObject>();
                LeatherController._GameObjectInfo = Info;
                LeatherController.Init();                

                return LeatherGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_BRONZE_COIN:                
                GameObject CopperCoinGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN].Pop().Object;
                CopperCoinGo.name = Info.ObjectName;                

                _Objects.Add(Info.ObjectId, CopperCoinGo);                

                CItemObject CopperCoinController = CopperCoinGo.GetComponent<CItemObject>();
                CopperCoinController._GameObjectInfo = Info;
                CopperCoinController.Init();

                CopperCoinController.CreatureSpriteShowClose(true);

                return CopperCoinGo;            
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_WOOD_LOG:
                GameObject WoodLogGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG].Pop().Object;
                WoodLogGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, WoodLogGo);

                CItemObject WoodLogController = WoodLogGo.GetComponent<CItemObject>();
                WoodLogController._GameObjectInfo = Info;
                WoodLogController.Init();

                return WoodLogGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_STONE:
                GameObject ItemStonGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_STONE].Pop().Object;
                ItemStonGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, ItemStonGo);

                CItemObject ItemStoneController = ItemStonGo.GetComponent<CItemObject>();
                ItemStoneController._GameObjectInfo = Info;
                ItemStoneController.Init();               

                return ItemStonGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_WOOD_FLANK:
                GameObject WoodFlankGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK].Pop().Object;
                WoodFlankGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, WoodFlankGo);

                CItemObject WoodFlankController = WoodFlankGo.GetComponent<CItemObject>();
                WoodFlankController._GameObjectInfo = Info;
                WoodFlankController.Init();                

                return WoodFlankGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_CHAR_COAL:
                GameObject CharCoalGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_CHARCOAL].Pop().Object;
                CharCoalGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, CharCoalGo);

                CItemObject CharCoalController = CharCoalGo.GetComponent<CItemObject>();
                CharCoalController._GameObjectInfo = Info;
                CharCoalController.Init();
                
                return CharCoalGo;
            case en_GameObjectType.OBJECT_ITEM_CROP_FRUIT_POTATO:
                GameObject PotatoItemGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_POTATO].Pop().Object;
                PotatoItemGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, PotatoItemGo);

                CItemObject PotatoItemController = PotatoItemGo.GetComponent<CItemObject>();
                PotatoItemController._GameObjectInfo = Info;
                PotatoItemController.Init();                

                return PotatoItemGo;            
            case en_GameObjectType.OBJECT_PLAYER_DUMMY:
                GameObject DummyPlayerGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_PLAYER].Pop().Object;
                DummyPlayerGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, DummyPlayerGo);
                
                PlayerObject DummyPlayerController = DummyPlayerGo.GetComponent<PlayerObject>();
                DummyPlayerController._GameObjectInfo = Info;
                DummyPlayerController.Init();                

                return DummyPlayerGo;
        }

        return null;
    }
 
    public void Remove(long RemoveId)
    {
        if (_Objects.ContainsKey(RemoveId) == false)
        {
            return;
        }

        //제거할 오브젝트를 찾아주고 
        GameObject RemoveGo = FindById(RemoveId);
        if (RemoveGo == null)
        {
            Debug.Log($"삭제할 {RemoveId}가 존재하지 않음");
            return;
        }        

        CBaseObject BaseObject = RemoveGo.GetComponent<CBaseObject>();
        if(BaseObject != null)
        {
            CreatureObject Creature = BaseObject.GetComponent<CreatureObject>();
            if (Creature != null)
            {
                Creature.CreatureShowClose(false);
            }

            CPoolObject PoolObject = new CPoolObject();
            PoolObject.Object = RemoveGo;            

            //딕셔너리에서 제거후
            _Objects.Remove(RemoveId);

            Transform RightWeaponParentTransform = Creature.transform.Find("RightWeaponParent");
            if(RightWeaponParentTransform != null)
            {
                PlayerWeapon RightWeaponParent = RightWeaponParentTransform.GetComponent<PlayerWeapon>();
                if (RightWeaponParent != null)
                {
                    GameObject RightWeaponGO = RightWeaponParent.transform.Find("WeaponLongSwordWood").gameObject;
                    if (RightWeaponGO != null)
                    {
                        UnityEngine.Object.Destroy(RightWeaponGO);
                    }
                }
            }           

            switch (BaseObject._GameObjectInfo.ObjectType)
            {
                case en_GameObjectType.OBJECT_PLAYER:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_PLAYER].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_NON_PLAYER_GENERAL_MERCHANT:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_NON_PLAYER_GENERAL_MERCHANT].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_GOBLIN:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_GOBLIN].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_STONE:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_STONE].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_TREE:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_TREE].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_CROP_POTATO:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CROP_POTATO].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_CROP_CORN:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CROP_CORN].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_SKILL_SWORD_BLADE:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_SKILL_SWORD_BLADE].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_SAWMILL].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_ITEM_MATERIAL_LEATHER:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_LEATHER].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_ITEM_MATERIAL_BRONZE_COIN:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN].Push(PoolObject);
                    break;                                      
                case en_GameObjectType.OBJECT_ITEM_MATERIAL_WOOD_LOG:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_ITEM_MATERIAL_STONE:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_STONE].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_ITEM_MATERIAL_WOOD_FLANK:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_ITEM_MATERIAL_CHAR_COAL:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_CHARCOAL].Push(PoolObject);
                    break;                    
                case en_GameObjectType.OBJECT_ITEM_CROP_FRUIT_POTATO:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CROP_POTATO].Push(PoolObject);
                    break;
                case en_GameObjectType.OBJECT_ITEM_CROP_FRUIT_CORN:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_CROP_CORN].Push(PoolObject);
                    break;                
            }

            ////리소스에서도 제거
            //Managers.Resource.Destroy(RemoveGo);
        }
    }

    public GameObject FindById(long Id)
    {
        GameObject Go = null;
        _Objects.TryGetValue(Id, out Go);
        return Go;
    }

    //해당 포지션에 Object가 있는지 확인
    public GameObject FindCreature(Vector2Int CellPosition)
    {
        foreach (GameObject Obj in _Objects.Values)
        {
            CreatureObject creatureController = Obj.GetComponent<CreatureObject>();
            if (creatureController == null)
            {
                Debug.Log("CreatureControllder을 찾을 수 없습니다.");
                continue;
            }

            if (creatureController._GameObjectInfo.ObjectPositionInfo.CollisitionPosition == CellPosition)
            {
                return Obj;
            }
        }

        return null;
    }

    public GameObject Find(Func<GameObject, bool> Condition)
    {
        foreach (GameObject Obj in _Objects.Values)
        {
            if (Condition.Invoke(Obj) == true)
            {
                return Obj;
            }
        }

        return null;
    }

    public void Clear()
    {
        foreach (GameObject Go in _Objects.Values)
        {
            Managers.Resource.Destroy(Go);
        }

        _Objects.Clear();
    }
}
