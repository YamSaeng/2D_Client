using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager
{
    public ClickObject _ClickObject;

    public void Init()
    {
        _ClickObject = Managers.GetInstance.gameObject.GetComponent<ClickObject>();
    }
}
