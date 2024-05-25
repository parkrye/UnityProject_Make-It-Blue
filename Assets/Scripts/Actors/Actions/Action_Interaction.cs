using System.Linq;
using UnityEngine;

public class Action_Interaction : BaseAction
{
    public Action_Interaction(ActionEnum actionCode) : base(actionCode)
    {

    }

    public override void Action()
    {
        var player = GameManager.System.PlayerActor;
        var castArray = Physics.SphereCastAll(player.Focus.position, 1f, Vector3.up, 3f)
            .Where(t => t.collider.GetComponent<IInteractable>() != null)
            .OrderBy(t => Vector3.SqrMagnitude(t.point - player.Focus.position) + Vector3.SqrMagnitude(t.point - player.transform.position))
            .ToArray();
        
        if (castArray.Length == 0)
        {
            castArray = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 4f)
            .Where(t => t.collider.GetComponent<IInteractable>() != null)
            .OrderBy(t => Vector3.SqrMagnitude(t.point - player.Focus.position) + Vector3.SqrMagnitude(t.point - player.transform.position))
            .ToArray();
        }

        foreach (var target in castArray)
        {
            if (target.collider.GetComponent<IInteractable>().Interact())
                break;
        }
    }
}
