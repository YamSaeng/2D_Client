using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillItemDrag : UI_Base
{
    public st_SkillInfo _DragSkillInfo;

    enum en_SkillItemDragImage
    {        
        SkillImage        
    }      
       
    public override void Init()
    {
        Bind<Image>(typeof(en_SkillItemDragImage));

        gameObject.SetActive(false);
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);

        if (IsShowClose == true)
        {
            gameObject.transform.SetAsLastSibling();
        }
    }

    public void DragSkillItemSet(Sprite DragSkillItemSprite)
    {
        GetImage((int)en_SkillItemDragImage.SkillImage).sprite = DragSkillItemSprite;
    }

    public void SetSkillItemDragRaycast(bool Active)
    {      
        GetImage((int)en_SkillItemDragImage.SkillImage).raycastTarget = Active;
    }
}
