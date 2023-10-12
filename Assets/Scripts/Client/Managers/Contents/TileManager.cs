using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    Dictionary<en_WorldMapInfo, List<st_TileInfo>> _TileInfos;

    public void Init()
    {
        _TileInfos = new Dictionary<en_WorldMapInfo, List<st_TileInfo>>();
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
        LeftTopBuildingPosition.x = (int)MousePosition.x - BuildingInfo.BuildingWidth;
        LeftTopBuildingPosition.y = (int)MousePosition.y - BuildingInfo.BuildingHeight;

        List<st_TileInfo> ReturnTiles = new List<st_TileInfo>();
        List<st_TileInfo> Tiles = _TileInfos[WorldMapInfo];

        for (int X = LeftTopBuildingPosition.x; X < LeftTopBuildingPosition.x + BuildingInfo.BuildingWidth; X++)
        {
            for (int Y = LeftTopBuildingPosition.y; Y > LeftTopBuildingPosition.y - BuildingInfo.BuildingHeight; Y--)
            {
                foreach (st_TileInfo TileInfo in Tiles)
                {
                    Vector2Int CheckPosition = new Vector2Int();
                    CheckPosition.x = X;
                    CheckPosition.y = Y;
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
