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
            AddHPBar(0, 0.95f);
        }

        if(_SpellBarUI == null)
        {
            AddSpellBar(0, -0.7f);
        }

        if (_NameUI == null)
        {
            AddNameBar(0, 0.35f);
        }

        GetComponent<GameObjectMovement>().SetOwner(this);
        GetComponent<GameObjectInput>().SetOwner(this);
    }   

    protected override void UpdateController()
    {
        base.UpdateController();        
    }         
}