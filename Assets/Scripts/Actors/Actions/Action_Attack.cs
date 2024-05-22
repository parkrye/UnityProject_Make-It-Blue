using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : BaseAction
{
    public Action_Attack(ActionCode actionCode) : base(actionCode)
    {
    }

    public override void Action()
    {
        Debug.Log("Attack");
    }
}
