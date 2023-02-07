using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuickSlotItemDrag : UI_Base
{
    enum en_QuickSlotItemDragImage
    {
        QuickSlotBarSlotBackGround,
        QuickSlotIconImage
    }

    public override void Init()
    {
        Bind<Image>(typeof(en_QuickSlotItemDragImage));

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

    public void QuickSlotItemSet(Sprite QuickSlotItemSprite)
    {
        GetImage((int)en_QuickSlotItemDragImage.QuickSlotIconImage).sprite = QuickSlotItemSprite;
    }

    public void SetRaycast(bool Active)
    {
        GetImage((int)en_QuickSlotItemDragImage.QuickSlotBarSlotBackGround).raycastTarget = Active;
        GetImage((int)en_QuickSlotItemDragImage.QuickSlotIconImage).raycastTarget = Active;
    }
}
