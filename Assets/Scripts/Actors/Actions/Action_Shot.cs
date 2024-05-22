using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Shot : BaseAction
{
    public Action_Shot(ActionCode actionCode) : base(actionCode)
    {
    }

    public override void Action()
    {
        Debug.Log("Shot");
    }
}
