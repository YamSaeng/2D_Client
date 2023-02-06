using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{    
    Dictionary<long, GameObject> _Objects = new Dictionary<long, GameObject>();    

    public GameObject Add(st_GameObjectInfo Info)
    {
        // 이미 추가한 오브젝트인지 확인
        if (_Objects.ContainsKey(Info.ObjectId))
        {
            return null;
        }        

        switch (Info.ObjectType)
        {
            case en_GameObjectType.OBJECT_WARRIOR_PLAYER:
                GameObject WarriorPlayerGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_PLAYER_WARRIOR);                
                WarriorPlayerGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, WarriorPlayerGO);
                
                PlayerObject WarriorPlayerController = WarriorPlayerGO.GetComponent<PlayerObject>();
                WarriorPlayerController._GameObjectInfo = Info;
                WarriorPlayerController.Init();                

                return WarriorPlayerGO;
            case en_GameObjectType.OBJECT_SHAMAN_PLAYER:
                GameObject ShmanPlayerGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_PLAYER_SHAMAN);
                ShmanPlayerGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, ShmanPlayerGO);

                PlayerObject ShmanPlayerPlayerController = ShmanPlayerGO.GetComponent<PlayerObject>();
                ShmanPlayerPlayerController._GameObjectInfo = Info;
                ShmanPlayerPlayerController.Init();

                return ShmanPlayerGO;
            case en_GameObjectType.OBJECT_TAIOIST_PLAYER:
                GameObject TaioistPlayerGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_PLAYER_TAIOIST);
                TaioistPlayerGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, TaioistPlayerGO);                
                
                PlayerObject TaioistPlayerController = TaioistPlayerGO.GetComponent<PlayerObject>();
                TaioistPlayerController._GameObjectInfo = Info;
                TaioistPlayerController.Init();

                return TaioistPlayerGO;
            case en_GameObjectType.OBJECT_THIEF_PLAYER:
                GameObject ThiefPlayerGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_PLAYER_THIEF);
                ThiefPlayerGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, ThiefPlayerGO);
                
                PlayerObject ThiefPlayerController = ThiefPlayerGO.GetComponent<PlayerObject>();
                ThiefPlayerController._GameObjectInfo = Info;
                ThiefPlayerController.Init();           

                return ThiefPlayerGO;
            case en_GameObjectType.OBJECT_SLIME:
                GameObject SlimeGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_MONSTER_SLIME);
                SlimeGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, SlimeGo);

                SlimeController Slime = SlimeGo.GetComponent<SlimeController>();
                Slime._GameObjectInfo = Info;                
                Slime.Init();                

                return SlimeGo;
            case en_GameObjectType.OBJECT_STONE:
                GameObject StoneGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_STONE);
                StoneGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, StoneGo);

                EnvironmentController StoneController = StoneGo.GetComponent<EnvironmentController>();
                StoneController._GameObjectInfo = Info;
                StoneController.Init();
                
                return StoneGo;
            case en_GameObjectType.OBJECT_TREE:
                GameObject TreeGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ENVIRONMENT_TREE);
                TreeGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, TreeGo);

                EnvironmentController TreeController = TreeGo.GetComponent<EnvironmentController>();
                TreeController._GameObjectInfo = Info;
                TreeController.Init();
                
                return TreeGo;
            case en_GameObjectType.OBJECT_CROP_POTATO:
                GameObject PotatoGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_CROP_POTATO);
                PotatoGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, PotatoGo);

                PotatoController PotatoController = PotatoGo.GetComponent<PotatoController>();
                PotatoController._GameObjectInfo = Info;
                PotatoController.Init();                

                return PotatoGo;
            case en_GameObjectType.OBJECT_CROP_CORN:
                GameObject CornGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_CROP_CORN);
                CornGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, CornGO);

                CornController Corn = CornGO.GetComponent<CornController>();
                Corn._GameObjectInfo = Info;
                Corn.Init();                

                return CornGO;
            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_FURNACE:
                GameObject FurnaceGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_FURNACE);
                FurnaceGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, FurnaceGO);

                FurnaceController Furnace = FurnaceGO.GetComponent<FurnaceController>();
                Furnace._GameObjectInfo = Info;
                Furnace.Init();                

                return FurnaceGO;
            case en_GameObjectType.OBJECT_ARCHITECTURE_CRAFTING_TABLE_SAWMILL:
                GameObject SawmillGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_CRAFTING_TABLE_SWAMILL);
                SawmillGO.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, SawmillGO);

                SawmillController Sawmill = SawmillGO.GetComponent<SawmillController>();
                Sawmill._GameObjectInfo = Info;

                Sawmill.Init();

                float OriginalX = Sawmill._GameObjectInfo.ObjectPositionInfo.PositionX;
                float SumX = 0;

                float OriginalY = Sawmill._GameObjectInfo.ObjectPositionInfo.PositionY;
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
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_SLIME_GEL:
                GameObject SlimeGelGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_SLIME_GEL);
                SlimeGelGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, SlimeGelGo);

                ItemController SlimeGelController = SlimeGelGo.GetComponent<ItemController>();
                SlimeGelController._GameObjectInfo = Info;
                SlimeGelController.Init();                
                
                GameObject SlimeGelTargetGo = Managers.Object.FindById(Info.OwnerObjectId);

                PlayerObject SlimeGelTarget = SlimeGelTargetGo.GetComponent<PlayerObject>();
                SlimeGelController._Target = SlimeGelTarget;

                return SlimeGelGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_LEATHER:
                GameObject LeatherGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_LEATHER);
                LeatherGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, LeatherGo);

                ItemController LeatherController = LeatherGo.GetComponent<ItemController>();
                LeatherController._GameObjectInfo = Info;
                LeatherController.Init();                

                return LeatherGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_BRONZE_COIN:
                GameObject CopperCoinGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_BRONZE_COIN);
                CopperCoinGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, CopperCoinGo);

                ItemController CopperCoinController = CopperCoinGo.GetComponent<ItemController>();
                CopperCoinController._GameObjectInfo = Info;
                CopperCoinController.Init();

                return CopperCoinGo;            
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_WOOD_LOG:
                GameObject WoodLogGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_LOG);
                WoodLogGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, WoodLogGo);

                ItemController WoodLogController = WoodLogGo.GetComponent<ItemController>();
                WoodLogController._GameObjectInfo = Info;
                WoodLogController.Init();

                return WoodLogGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_STONE:
                GameObject ItemStonGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_STONE);
                ItemStonGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, ItemStonGo);

                ItemController ItemStoneController = ItemStonGo.GetComponent<ItemController>();
                ItemStoneController._GameObjectInfo = Info;
                ItemStoneController.Init();               

                return ItemStonGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_WOOD_FLANK:
                GameObject WoodFlankGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_WOOD_FLANK);
                WoodFlankGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, WoodFlankGo);

                ItemController WoodFlankController = WoodFlankGo.GetComponent<ItemController>();
                WoodFlankController._GameObjectInfo = Info;
                WoodFlankController.Init();                

                return WoodFlankGo;
            case en_GameObjectType.OBJECT_ITEM_MATERIAL_CHAR_COAL:
                GameObject CharCoalGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_CHARCOAL);
                CharCoalGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, CharCoalGo);

                ItemController CharCoalController = CharCoalGo.GetComponent<ItemController>();
                CharCoalController._GameObjectInfo = Info;
                CharCoalController.Init();
                
                return CharCoalGo;
            case en_GameObjectType.OBJECT_ITEM_CROP_FRUIT_POTATO:
                GameObject PotatoItemGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_ITEM_POTATO);
                PotatoItemGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, PotatoItemGo);

                ItemController PotatoItemController = PotatoItemGo.GetComponent<ItemController>();
                PotatoItemController._GameObjectInfo = Info;
                PotatoItemController.Init();                

                return PotatoItemGo;            
            case en_GameObjectType.OBJECT_PLAYER_DUMMY:
                GameObject DummyPlayerGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_GAMEOBJECT_PLAYER_WARRIOR);
                DummyPlayerGo.name = Info.ObjectName;

                _Objects.Add(Info.ObjectId, DummyPlayerGo);
                
                PlayerObject DummyPlayerController = DummyPlayerGo.GetComponent<PlayerObject>();
                DummyPlayerController._GameObjectInfo = Info;
                DummyPlayerController.Init();                

                return DummyPlayerGo;
        }

        return null;
    }

    public void Add(int AddId, GameObject AddGo)
    {
        _Objects.Add(AddId, AddGo);
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

        //딕셔너리에서 제거후
        _Objects.Remove(RemoveId);
        //리소스에서도 제거
        Managers.Resource.Destroy(RemoveGo);
    }

    public GameObject FindById(long Id)
    {
        GameObject Go = null;
        _Objects.TryGetValue(Id, out Go);
        return Go;
    }

    //해당 포지션에 Object가 있는지 확인
    public GameObject FindCreature(Vector3Int CellPosition)
    {
        foreach (GameObject Obj in _Objects.Values)
        {
            CreatureObject creatureController = Obj.GetComponent<CreatureObject>();
            if (creatureController == null)
            {
                Debug.Log("CreatureControllder을 찾을 수 없습니다.");
                continue;
            }

            if (creatureController._CellPosition == CellPosition)
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
