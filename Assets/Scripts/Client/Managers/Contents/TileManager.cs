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
}
