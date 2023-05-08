using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Minimap : UI_Base
{
    enum en_MiniMapText
    {
        MyCharacterPosition
    }

    public override void Init()
    {
        
    }

    public override void Binding()
    {
        Bind<Text>(typeof(en_MiniMapText));
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    // 캐릭터 위치 정보 업데이트
    public void MiniMapMyPositionUpdate(int X, int Y)
    {
        GetText((int)en_MiniMapText.MyCharacterPosition).text = "X : " + X.ToString() + " Y : " + Y.ToString(); 
    }    
}
