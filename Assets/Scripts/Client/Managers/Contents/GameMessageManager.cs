using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageManager
{
    public UI_GlobalMessageBox _GlobalMessageBox;
    
    public void Init()
    {
        GameObject UIRoot = GameObject.Find("@UI_Root");
        if (UIRoot == null)
        {
            UIRoot = new GameObject { name = "@UI_Root" };            
        }
    }

    public void GlobalMessageBoxFind()
    {
        GameObject GlobalMessageBoxGO = GameObject.Find("UI_GlobalMessageBox");
        if(GlobalMessageBoxGO != null)
        {
            _GlobalMessageBox = GlobalMessageBoxGO.GetComponent<UI_GlobalMessageBox>();
        }
    }
}
