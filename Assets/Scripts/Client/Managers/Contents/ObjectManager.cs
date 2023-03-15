﻿using System;
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
        public bool IsUsing;
    }

    class CObjectPool
    {
        en_ResourceName _ObjectPoolType;

        Stack<CPoolObject> _PoolStacks = new Stack<CPoolObject>();               

        public CObjectPool(en_ResourceName ObjectPoolType)
        {
            _ObjectPoolType = ObjectPoolType;
        }

        public void Push(CPoolObject PoolObject)
        {
            PoolObject.Object.gameObject.SetActive(false);
            PoolObject.IsUsing = false;

            _PoolStacks.Push(PoolObject);
        }

        public CPoolObject Pop()
        {   
            CPoolObject PopObject = new CPoolObject();

            if(_PoolStacks.Count > 0)
            {
                PopObject = _PoolStacks.Pop();
            }
            else
            {                
                PopObject.Object = Managers.Resource.Instantiate(_ObjectPoolType);                
            }

            PopObject.Object.gameObject.SetActive(true);
            PopObject.IsUsing = true;

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
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_GOBLIN, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_GOBLIN));            

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_STONE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_STONE));            
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_TREE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_TREE));

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_CROP_POTATO, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_CROP_POTATO));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_CROP_CORN, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_CROP_CORN));

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_SAWMILL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_SAWMILL));

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_LEATHER, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_LEATHER));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_STONE, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_STONE));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_CHARCOAL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_CHARCOAL));            

            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_LEFT_RIGHT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_LEFT_RIGHT_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_UP_DOWN_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_UP_DOWN_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_UP_TO_LEFT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_UP_TO_LEFT_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_UP_TO_RIGHT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_UP_TO_RIGHT_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_DOWN_TO_LEFT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_DOWN_TO_LEFT_WALL));
            _ObjectPools.Add(en_ResourceName.CLIENT_GAMEOBJECT_DOWN_TO_RIGHT_WALL, new CObjectPool(en_ResourceName.CLIENT_GAMEOBJECT_DOWN_TO_RIGHT_WALL));                                
        }
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
                GameObject WarriorPlayerGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_PLAYER].Pop().Object;
                WarriorPlayerGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, WarriorPlayerGO);
                
                PlayerObject WarriorPlayerController = WarriorPlayerGO.GetComponent<PlayerObject>();
                WarriorPlayerController._GameObjectInfo = Info;
                WarriorPlayerController.Init();                

                return WarriorPlayerGO;
            case en_GameObjectType.OBJECT_GOBLIN:
                GameObject GoblinGO = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_GOBLIN].Pop().Object;
                GoblinGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, GoblinGO);

                GoblinObject Goblin = GoblinGO.GetComponent<GoblinObject>();
                Goblin._GameObjectInfo = Info;
                Goblin.Init();

                GameObject RightWeaponParent = Goblin.transform.Find("RightWeaponParent").gameObject;
                if(RightWeaponParent != null)
                {
                    PlayerWeapon Weapon = RightWeaponParent.GetComponent<PlayerWeapon>();
                    if(Weapon != null)
                    {
                        Weapon.Init();
                    }
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

                ItemController LeatherController = LeatherGo.GetComponent<ItemController>();
                LeatherController._GameObjectInfo = Info;
                LeatherController.Init();                

                return LeatherGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_BRONZE_COIN:
                GameObject CopperCoinGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN].Pop().Object;
                CopperCoinGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, CopperCoinGo);

                ItemController CopperCoinController = CopperCoinGo.GetComponent<ItemController>();
                CopperCoinController._GameObjectInfo = Info;
                CopperCoinController.Init();

                return CopperCoinGo;            
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_WOOD_LOG:
                GameObject WoodLogGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG].Pop().Object;
                WoodLogGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, WoodLogGo);

                ItemController WoodLogController = WoodLogGo.GetComponent<ItemController>();
                WoodLogController._GameObjectInfo = Info;
                WoodLogController.Init();

                return WoodLogGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_STONE:
                GameObject ItemStonGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_STONE].Pop().Object;
                ItemStonGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, ItemStonGo);

                ItemController ItemStoneController = ItemStonGo.GetComponent<ItemController>();
                ItemStoneController._GameObjectInfo = Info;
                ItemStoneController.Init();               

                return ItemStonGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_WOOD_FLANK:
                GameObject WoodFlankGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK].Pop().Object;
                WoodFlankGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, WoodFlankGo);

                ItemController WoodFlankController = WoodFlankGo.GetComponent<ItemController>();
                WoodFlankController._GameObjectInfo = Info;
                WoodFlankController.Init();                

                return WoodFlankGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_CHARCOAL:
                GameObject CharCoalGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_CHARCOAL].Pop().Object;
                CharCoalGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, CharCoalGo);

                ItemController CharCoalController = CharCoalGo.GetComponent<ItemController>();
                CharCoalController._GameObjectInfo = Info;
                CharCoalController.Init();
                
                return CharCoalGo;
            case en_GameObjectType.OBJECT_ITEM_CROP_FRUIT_POTATO:
                GameObject PotatoItemGo = _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_ITEM_POTATO].Pop().Object;
                PotatoItemGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, PotatoItemGo);

                ItemController PotatoItemController = PotatoItemGo.GetComponent<ItemController>();
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
            CPoolObject PoolObject = new CPoolObject();
            PoolObject.Object = RemoveGo;
            PoolObject.IsUsing = false;

            //딕셔너리에서 제거후
            _Objects.Remove(RemoveId);

            PoolObject.Object.gameObject.SetActive(false);

            switch (BaseObject._GameObjectInfo.ObjectType)
            {
                case en_GameObjectType.OBJECT_PLAYER:
                    _ObjectPools[en_ResourceName.CLIENT_GAMEOBJECT_PLAYER].Push(PoolObject);
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
                case en_GameObjectType.OBJECT_ITEM_MATERIAL_CHARCOAL:
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
