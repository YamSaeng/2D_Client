using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawmillController : CreatureObject
{
    public Dictionary<en_SmallItemCategory, st_ItemInfo> _SawmillMaterials = new Dictionary<en_SmallItemCategory, st_ItemInfo>();
    public Dictionary<en_SmallItemCategory, st_ItemInfo> _SawmillCompleteItems = new Dictionary<en_SmallItemCategory, st_ItemInfo>();
    public en_SmallItemCategory _SelectCompleteItemCategory;

    public override void Init()
    {
        base.Init();
    }  
}
