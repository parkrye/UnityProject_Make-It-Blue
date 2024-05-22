using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_HG : BaseEquipment
{
    public override void Init()
    {
        base.Init();

        MainAction = new Action_Shot(ActionCode.OnAction1);
        SubAction = new Action_Attack(ActionCode.OnAction0);
    }
}
