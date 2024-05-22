using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Action_Attack : BaseAction
{
    public Action_Attack(ActionCode actionCode) : base(actionCode)
    {
    }

    public override void Action()
    {
        var player = GameManager.System.PlayerActor;

        var targetArray = Physics.SphereCastAll(player.Focus.position, 1f, Vector3.up, 3f)
            .Where(t => t.collider.GetComponent<IHitable>() != null)
            .OrderBy(t => Vector3.SqrMagnitude(t.point - player.transform.position))
            .ToArray();

        foreach (var target in targetArray)
        {
            var hitable = target.transform.GetComponent<IHitable>();
            if (hitable == null)
                return;

            var damage = Calculator.CalcuateDamage(
                GameManager.Data.Play.Level, 1f, hitable.GetStatus(StatusEnum.Avoid), 0, hitable.GetCondition());
            hitable.Hit(damage);
        }
    }
}
