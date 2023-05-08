using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBoxManager
{
    //st_CraftingItemCategory[] CraftingCategories
    public List<st_CraftingItemCategory> _CraftingItemCategories = new List<st_CraftingItemCategory>();
    
    public void Init(st_CraftingItemCategory[] CraftingItemCategories)
    {
        foreach(st_CraftingItemCategory CraftingItemCategory in CraftingItemCategories)
        {
            _CraftingItemCategories.Add(CraftingItemCategory);
        }
    }
}
