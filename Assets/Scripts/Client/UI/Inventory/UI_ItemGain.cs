using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_ItemGain : UI_Base
{
    public long _ItemGainID = 0;
    private st_ItemInfo _GainItemInfo;

    enum en_ItemGainImage
    {
        ItemImage
    }

    enum en_ItemGainText
    {
        ItemNameText,
        ItemCountText
    }    

    public override void Init()
    {   
        Bind<Text>(typeof(en_ItemGainText));
        Bind<Image>(typeof(en_ItemGainImage));
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void SetItemGain(st_ItemInfo GainItemInfo, int Count)
    {
        _GainItemInfo = GainItemInfo;

        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;        
        if(GameSceneUI == null)
        {
            return;           
        }

        Get<Image>((int)en_ItemGainImage.ItemImage).sprite = Managers.Sprite._ItemSprite[_GainItemInfo.ItemSmallCategory];    

        // 아이템 이름 셋팅
        Get<Text>((int)en_ItemGainText.ItemNameText).text = GainItemInfo.ItemName;

        // 개수 셋팅
        Get<Text>((int)en_ItemGainText.ItemCountText).text = Count.ToString() + " 개 획득!";

        StartCoroutine(ItemGainUIDestory());
    }

    IEnumerator ItemGainUIDestory()
    {
        yield return new WaitForSeconds(1.0f);

        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if(GameSceneUI != null)
        {
            UI_ItemGainBox ItemGainBoxUI = GameSceneUI._ItemGainBox;
            if(ItemGainBoxUI != null)
            {
                ItemGainBoxUI.ItemGainDestory(this);
            }
        }
    }
}