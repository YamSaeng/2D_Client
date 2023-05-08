using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : CreatureObject
{
    public override void Init()
    {
        base.Init();

        _HPBarUI = transform.Find("UI_HPBar").gameObject.GetComponent<UI_HPBar>();
        _HPBarUI.gameObject.SetActive(false);

        UpdateHPBar();
    }   
}
