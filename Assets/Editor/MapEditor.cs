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
    [MenuItem("Tools/Map Collision Recoding %#g")]
    private static void GenerateMap()
    {
        GenerateByPath("Assets/Resources/Map");             
    }

    private static void GenerateByPath(string PathPrefix)
    {
        //모든 맵 프리팹 로딩
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map");        

        //맵 순회
        foreach (GameObject Go in gameObjects)
        {            
            //맵 정보
            Tilemap TileMapFloor = Go.transform.Find("Tilemap_Floor").gameObject.GetComponent<Tilemap>(); //Util.FindChild<Tilemap>(Go, "Tilemap_Floor", true);
            //Tilemap_Collision 맵 정보를 받아와서 tileMap에 저장
            Tilemap TileMapCollision = Go.transform.Find("Tilemap_Collision").gameObject.GetComponent<Tilemap>(); //Util.FindChild<Tilemap>(Go, "Tilemap_Collision", true);
            
            //파일 기록
            using (var Writer = File.CreateText($"{PathPrefix}/{Go.name}.txt"))
            {               
                //타일맵 상하좌우 크기 기록
                //Writer.WriteLine(TileMapFloor.cellBounds.xMin);
                //Writer.WriteLine(TileMapFloor.cellBounds.xMax);
                //Writer.WriteLine(TileMapFloor.cellBounds.yMin);                
                //Writer.WriteLine(TileMapFloor.cellBounds.yMax);

                for (int y = TileMapFloor.cellBounds.yMax; y >= TileMapFloor.cellBounds.yMin; y--)
                {
                    for (int x = TileMapFloor.cellBounds.xMin; x <= TileMapFloor.cellBounds.xMax; x++)
                    {
                        //콜리전 맵 타일 정보를 가져와서 해당 값에 따라 1 0 기록 1 이면 벽
                        TileBase Tile = TileMapCollision.GetTile(new Vector3Int(x, y, 0));
                        if (Tile != null)
                        {                            
                            switch(Tile.name)
                            {
                                case "TileMapWall":
                                    Writer.WriteLine("{");
                                    Writer.WriteLine($"X: {x},");
                                    Writer.WriteLine($"Y: {y}");
                                    Writer.WriteLine("},");
                                    break;                                                               
                            }
                        }                       
                    }                    
                }
            }
        }        

        Debug.Log("지도 정보 기록 완료");
    }

#endif
}
