using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawmillController : CreatureObject
{
    public Dictionary<en_SmallItemCategory, CItem> _SawmillMaterials = new Dictionary<en_SmallItemCategory, CItem>();
    public Dictionary<en_SmallItemCategory, CItem> _SawmillCompleteItems = new Dictionary<en_SmallItemCategory, CItem>();
    public en_SmallItemCategory _SelectCompleteItemCategory;

    public override void Init()
    {
        base.Init();
    }  
}
