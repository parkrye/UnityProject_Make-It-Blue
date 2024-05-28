using System.Linq;
using UnityEngine;

public class Action_Attack : BaseAction
{
    public Action_Attack(ActionEnum actionCode, string actionName = null) : base(actionCode, actionName)
    {

    }

    public override void Action()
    {
        var player = GameManager.System.PlayerActor;

        var targetArray = Physics.SphereCastAll(player.Focus.position, 1f, Vector3.up, 3f)
            .Where(t => t.collider.GetComponent<IHitable>() != null && t.collider.CompareTag("Player") == false)
            .OrderBy(t => Vector3.SqrMagnitude(t.point - player.transform.position))
            .ToArray();

        foreach (var target in targetArray)
        {
            var hitable = target.transform.GetComponent<IHitable>();
            if (hitable == null)
                return;

            int damage;
            var conditialable = target.transform.GetComponent<IConditionalbe>();
            if (conditialable == null)
                damage = Calculator.CalcuateDamage(
                GameManager.Data.Play.Level, 100, hitable.GetStatus(StatusEnum.Avoid), 0, 0);
            else
                damage = Calculator.CalcuateDamage(
                GameManager.Data.Play.Level, 100, hitable.GetStatus(StatusEnum.Avoid), 0, conditialable.GetConditionCount());
            hitable.Hit(damage);
        }
    }
}
