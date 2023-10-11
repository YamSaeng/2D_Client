using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildingItem : UI_Base
{
    private st_BuildingInfo _BuildingInfo;

    enum en_BuildingItemImage
    {
        BuildingItemImage
    }

    public override void Init()
    {
        
    }

    public override void Binding()
    {
        Bind<Image>(typeof(en_BuildingItemImage));
    }    

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public st_BuildingInfo GetBuildingInfo()
    {
        return _BuildingInfo;
    }

    public void SetBuildingInfo(st_BuildingInfo BuildingInfo)
    {
        _BuildingInfo = BuildingInfo;

        RefreshBuildingItemUI();
    }    
    
    public void RefreshBuildingItemUI()
    {
        GetImage((int)en_BuildingItemImage.BuildingItemImage).sprite = Managers.Sprite._BuildingSprite[_BuildingInfo.BuildinSmallCategory];        
    }   
}
