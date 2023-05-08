using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceController : CreatureObject
{
    public Dictionary<en_SmallItemCategory, CItem> _FurnaceMaterials = new Dictionary<en_SmallItemCategory, CItem>();
    public Dictionary<en_SmallItemCategory, CItem> _FurnaceCompleteItems = new Dictionary<en_SmallItemCategory,CItem>();
    public en_SmallItemCategory _SelectCompleteItemCategory;

    public override void Init()
    {
        base.Init();
    }
}
