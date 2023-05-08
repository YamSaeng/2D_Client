using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _Order = 10;
        
    public UI_Scene _SceneUI { get; private set; }

    public void SetCanvas(GameObject Go, bool Sort = true)
    {
        Canvas Canvas = Util.GetOrAddComponent<Canvas>(Go);
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //부모의 sortingOrder를 따르지 않고 각각의 sortingOrder값을 정한다는 의미
        Canvas.overrideSorting = true;

        if (Sort == true)
        {
            Canvas.sortingOrder = _Order;
            _Order++;
        }
        else
        {
            Canvas.sortingOrder = 0;
        }
    }   

    public T ShowSceneUI<T>(en_ResourceName SceneName) where T : UI_Scene
    {
        //if (string.IsNullOrEmpty(Name) == true)
        //{
        //    Name = typeof(T).Name;
        //}

        GameObject Go = Managers.Resource.Instantiate(SceneName);
        T SceneUI = Util.GetOrAddComponent<T>(Go);
        _SceneUI = SceneUI;

        GameObject UIRoot = GameObject.Find("@UI_Root");
        if (UIRoot == null)
        {
            UIRoot = new GameObject { name = "@UI_Root" };
        }

        Go.transform.SetParent(UIRoot.transform);

        return SceneUI;
    }   

    public void Clear()
    {        
        _SceneUI = null;
    }
}
