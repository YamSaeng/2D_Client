using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class PlayerObject : CreatureObject
{
    public override void Init()
    {
        base.Init();        

        _IsChattingFocus = false;

        if (_HPBarUI == null)
        {
            AddHPBar(0.5f, 0.35f);
        }

        if(_SpellBarUI == null)
        {
            AddSpellBar(0.5f, -1.2f);
        }

        if (_NameUI == null)
        {
            AddNameBar(0.5f, -0.2f);
        }

        GetComponent<GameObjectMovement>().SetOwner(this);
        GetComponent<GameObjectInput>().SetOwner(this);
    }   

    protected override void UpdateController()
    {
        base.UpdateController();        
    }         
}