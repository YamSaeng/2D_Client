using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    //스크립트를 붙인 GameObject가 활성화가 되어 있어야 Start에 진입한다.
    //비활성화가 되잇을때의 Start역할을 하는 함수는 Awake()함수이다.

    public Define.en_Scene _SceneType
    {
        get;
        protected set;
    } = Define.en_Scene.None;

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object EventSystem = GameObject.FindObjectOfType(typeof(EventSystem));
        if(EventSystem == null)
        {
            Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_EVENT_SYTEM).name = "@EventSystem";
        }
    }

    public abstract void Clear();
}
