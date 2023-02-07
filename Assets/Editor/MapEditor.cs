using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditor
{

#if UNITY_EDITOR    
    // % (Ctrl), # (Shift), & (Alt)
    [MenuItem("Tools/GenerateMap %#g")]
    private static void GenerateMap()
    {
        GenerateByPath("Assets/Resources/Map");
        GenerateByPath("../Common/MapData");        
    }

    private static void GenerateByPath(string PathPrefix)
    {
        //모든 맵 프리팹 로딩
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");

        //맵 순회
        foreach (GameObject Go in gameObjects)
        {
            //맵 정보
            Tilemap TileMapBase = Util.FindChild<Tilemap>(Go, "Tilemap_Base", true);
            //Tilemap_Collision 맵 정보를 받아와서 tileMap에 저장
            Tilemap TileMapCollision = Util.FindChild<Tilemap>(Go, "Tilemap_Collision", true);
            
            //파일 기록
            using (var Writer = File.CreateText($"{PathPrefix}/{Go.name}.txt"))
            {
                //타일맵 상하좌우 크기 기록
                Writer.WriteLine(TileMapBase.cellBounds.xMin);
                Writer.WriteLine(TileMapBase.cellBounds.xMax);
                Writer.WriteLine(TileMapBase.cellBounds.yMin);                
                Writer.WriteLine(TileMapBase.cellBounds.yMax);

                for (int y = TileMapBase.cellBounds.yMax; y >= TileMapBase.cellBounds.yMin; y--)
                {
                    for (int x = TileMapBase.cellBounds.xMin; x <= TileMapBase.cellBounds.xMax; x++)
                    {
                        //콜리전 맵 타일 정보를 가져와서 해당 값에 따라 1 0 기록 1 이면 벽
                        TileBase Tile = TileMapCollision.GetTile(new Vector3Int(x, y, 0));
                        if (Tile != null)
                        {
                            switch(Tile.name)
                            {
                                case "TILE_MAP_WALL":
                                    Writer.Write((int)en_TileMapEnvironment.TILE_MAP_WALL);
                                    break;
                                case "TILE_MAP_TREE":
                                    Writer.Write((int)en_TileMapEnvironment.TILE_MAP_TREE);
                                    break;
                                case "TILE_MAP_STONE":
                                    Writer.Write((int)en_TileMapEnvironment.TILE_MAP_STONE);
                                    break;
                                case "TILE_MAP_SLIME":
                                    Writer.Write((int)en_TileMapEnvironment.TILE_MAP_SLIME);
                                    break;
                                case "TILE_MAP_BEAR":
                                    Writer.Write((int)en_TileMapEnvironment.TILE_MAP_BEAR);
                                    break;                                
                            }
                        }
                        else
                        {
                            Writer.Write("0");
                        }
                    }
                    Writer.WriteLine();
                }
            }
        }
    }

#endif
}
