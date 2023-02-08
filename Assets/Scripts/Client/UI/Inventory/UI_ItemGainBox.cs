using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_ItemGainBox : UI_Scene
{   
    enum en_ItemGainBoxGameObject
    {
        ItemGainPannel
    }

    public class CItemGainMessage
    {
        public Sprite ItemImage;
        public Text Count;
    }    

    public override void Init()
    {
        base.Init();
    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_ItemGainBoxGameObject));
    }

    public void NewItemGainMessage(st_ItemInfo GainItemInfo, int Count)
    {   
        // 새로운 Gain 메세지 생성
        GameObject ItemGainGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_INVENTORY_ITEM_GAIN, Get<GameObject>((int)en_ItemGainBoxGameObject.ItemGainPannel).transform);

        UI_ItemGain ItemGain = ItemGainGo.GetComponent<UI_ItemGain>();
        ItemGain.SetItemGain(GainItemInfo, Count);
    }
}