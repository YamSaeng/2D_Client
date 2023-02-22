using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinObject : CreatureObject
{
    public override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {

    }

    public override void OnDamaged()
    {
        //Managers.Object.Remove(_Id);
        //Managers.Resource.Destroy(gameObject);
    }
}
