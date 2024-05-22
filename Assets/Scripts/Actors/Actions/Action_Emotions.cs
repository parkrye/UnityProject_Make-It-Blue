using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Emotions : BaseAction
{
    public Action_Emotions(ActionCode actionCode) : base(actionCode)
    {

    }

    public override void Action()
    {
        Debug.Log("Emotion");
    }
}
