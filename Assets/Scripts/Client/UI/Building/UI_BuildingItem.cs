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

        if(_BuildingInfo.IsImageChange == true)
        {
            Vector2 ImageSize = new Vector2();
            ImageSize.x = _BuildingInfo.BuildingWidth * UI_Inventory.TileSizeWidth;
            ImageSize.y = _BuildingInfo.BuildingHeight * UI_Inventory.TileSizeHeight;

            GetImage((int)en_BuildingItemImage.BuildingItemImage).GetComponent<RectTransform>().sizeDelta = ImageSize;
        }        
    }      
}
