using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager
{
    Dictionary<en_WorldMapInfo, List<st_TileInfo>> _TileInfos;

    public void Init()
    {
        _TileInfos = new Dictionary<en_WorldMapInfo, List<st_TileInfo>>();        
    }

    public void LoadTileMap()
    {
        GameObject[] MapGameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");       

        foreach (GameObject MapGO in MapGameObjects)
        {
            using (var Writer = File.CreateText($"Assets/Resources/MapData/{MapGO.name}.txt"))
            {
                List<st_TileInfo> MapTileInfos = new List<st_TileInfo>();
                Tilemap TileMapFloor = MapGO.transform.Find("Tilemap_Floor")?.gameObject.GetComponent<Tilemap>();
                if (TileMapFloor != null)
                {
                    for (int Y = TileMapFloor.cellBounds.yMax; Y >= TileMapFloor.cellBounds.yMin; Y--)
                    {
                        for (int X = TileMapFloor.cellBounds.xMin; X <= TileMapFloor.cellBounds.xMax; X++)
                        {
                            TileBase Tile = TileMapFloor.GetTile(new Vector3Int(X, Y, 0));
                            if (Tile != null)
                            {
                                switch (Tile.name)
                                {
                                    case "TX Tileset Grass 0":
                                        Writer.WriteLine("{");
                                        Writer.WriteLine($"X: {X},");
                                        Writer.WriteLine($"Y: {Y}");
                                        Writer.WriteLine("},");

                                        st_TileInfo TileInfo = new st_TileInfo();
                                        TileInfo.Position.x = X;
                                        TileInfo.Position.y = Y;                                        
                                        
                                        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
                                        if(GameSceneUI != null)
                                        {
                                            GameObject TileGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_MAP_TILE, GameSceneUI._Object.transform);
                                            if (TileGO != null)
                                            {
                                                TileInfo.TileGO = TileGO;
                                                TileGO.transform.position = new Vector3(X + 0.5f, Y + 0.5f, 0);                                                

                                                CTile tile = TileGO.GetComponent<CTile>();
                                                if (tile != null)
                                                {
                                                    tile.TileOff();
                                                }

                                                MapTileInfos.Add(TileInfo);
                                            }
                                        }
                                        
                                        break;
                                }
                            }                            
                        }
                    }

                    _TileInfos.Add(en_WorldMapInfo.WORLD_MAP_INFO_MAIN_FIELD, MapTileInfos);
                }
            }            
        }        
    }

    public void SetTileInfo(en_WorldMapInfo WorldMapInfo, st_TileInfo[] S2C_TileInfos)
    {
        List<st_TileInfo> ClientTileInfos = _TileInfos[WorldMapInfo];

        if (S2C_TileInfos.Length > 0)
        {
            foreach(st_TileInfo ClientTileInfo in ClientTileInfos)
            {
                foreach (st_TileInfo ServerTileInfo in S2C_TileInfos)
                {
                    if(ClientTileInfo.Position.x == ServerTileInfo.Position.x
                        && ClientTileInfo.Position.y == ServerTileInfo.Position.y)
                    {
                        ClientTileInfo.IsOccupation = ServerTileInfo.IsOccupation;
                        ClientTileInfo.OwnerObjectID = ServerTileInfo.OwnerObjectID;

                        break;
                    }                    
                }
            }            
        }
    }

    // 좌표 위치 타일 정보 반환
    public st_TileInfo FindTile(en_WorldMapInfo WorldMapInfo, Vector2Int TilePosition)
    {
        List<st_TileInfo> TileInfos = _TileInfos[WorldMapInfo];
        if (TileInfos.Count > 0)
        {
            foreach (st_TileInfo TileInfo in TileInfos)
            {
                if (TileInfo.Position == TilePosition)
                {
                    return TileInfo;
                }
            }
        }

        return null;
    }

    public List<st_TileInfo> FindTiles(en_WorldMapInfo WorldMapInfo, Vector2 MousePosition, st_BuildingInfo BuildingInfo)
    {
        Vector2Int LeftTopBuildingPosition = new Vector2Int();
        LeftTopBuildingPosition.x = (int)MousePosition.x - BuildingInfo.BuildingWidth / 2;
        LeftTopBuildingPosition.y = (int)MousePosition.y + BuildingInfo.BuildingHeight / 2;                

        List<st_TileInfo> ReturnTiles = new List<st_TileInfo>();
        List<st_TileInfo> Tiles = _TileInfos[WorldMapInfo];

        for (int X = LeftTopBuildingPosition.x; X < LeftTopBuildingPosition.x + BuildingInfo.BuildingWidth; X++)
        {
            for (int Y = LeftTopBuildingPosition.y; Y > LeftTopBuildingPosition.y - BuildingInfo.BuildingHeight; Y--)
            {
                Vector2Int CheckPosition = new Vector2Int();
                CheckPosition.x = X;
                CheckPosition.y = Y;

                foreach (st_TileInfo TileInfo in Tiles)
                {
                    if (TileInfo.Position == CheckPosition)
                    {
                        ReturnTiles.Add(TileInfo);                       
                    }                  
                }
            }
        }

        return ReturnTiles;
    }
}
