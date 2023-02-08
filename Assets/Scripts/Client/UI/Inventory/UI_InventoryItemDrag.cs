using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryItemDrag : UI_Base
{
    public st_ItemInfo _DragItemInfo;
    
    enum en_ItemDragImage
    {
        InventoryDragItemImage
    }

    public override void Init()
    {
        Bind<Image>(typeof(en_ItemDragImage));

        gameObject.SetActive(false);        
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);

        if(IsShowClose == true)
        {
            gameObject.transform.SetAsLastSibling();
        }        
    }

    public void DragItemSet(Sprite DragItemSprite)
    {
        GetImage((int)en_ItemDragImage.InventoryDragItemImage).sprite = DragItemSprite;
    }

    public void SetSkillItemDragRaycast(bool Active)
    {
        GetImage((int)en_ItemDragImage.InventoryDragItemImage).raycastTarget = Active;
    }  
}
