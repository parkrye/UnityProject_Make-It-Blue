using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Action_Interaction : BaseAction
{
    public Action_Interaction(ActionCode actionCode) : base(actionCode)
    {

    }

    public override void Action()
    {
        var castArray = Physics.SphereCastAll(GameManager.System.PlayerActor.Focus.position, 1f, Vector3.zero)
            .Where(t => t.collider.GetComponent<IInteractable>() != null)
            .OrderBy(t => Vector3.SqrMagnitude(t.point - GameManager.System.PlayerActor.transform.position))
            .ToArray();

        foreach (var target in castArray)
        {
            if (target.collider.GetComponent<IInteractable>().Interact())
                break;
        }
    }
}
