using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceController : CreatureObject
{
    public Dictionary<en_SmallItemCategory, st_ItemInfo> _FurnaceMaterials = new Dictionary<en_SmallItemCategory, st_ItemInfo>();
    public Dictionary<en_SmallItemCategory, st_ItemInfo> _FurnaceCompleteItems = new Dictionary<en_SmallItemCategory, st_ItemInfo>();
    public en_SmallItemCategory _SelectCompleteItemCategory;

    public override void Init()
    {
        base.Init();
    }
}
