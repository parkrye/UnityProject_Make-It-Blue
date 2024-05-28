using System.Linq;
using UnityEngine;

public class Action_Shot : BaseAction
{
    public Action_Shot(ActionEnum actionCode) : base(actionCode)
    {
    }

    public override void Action()
    {
        var player = GameManager.System.PlayerActor;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit) == false)
        {
            if (Physics.Raycast(player.CenterPosition, player.Focus.position - player.CenterPosition, out hit) == false)
                return;
        }

        var targetArray = Physics.RaycastAll(player.CenterPosition, (hit.point - player.CenterPosition).normalized)
            .Where(t => t.collider.CompareTag("Player") == false)
            .OrderBy(t => Vector3.SqrMagnitude(hit.point - player.CenterPosition))
            .ToArray();

        var wallCount = 0;
        foreach (var target in targetArray)
        {
            var hitable = target.transform.GetComponent<IHitable>();
            if (hitable == null)
            {
                wallCount++;
                continue;
            }

            int damage;
            var conditialable = target.transform.GetComponent<IConditionalbe>();
            if (conditialable == null)
                damage = Calculator.CalcuateDamage(
                player.WeaponData.Damage, GameManager.Data.Play.GetStatus(StatusEnum.Accuracy), hitable.GetStatus(StatusEnum.Avoid), wallCount, 0);
            else
                damage = Calculator.CalcuateDamage(
                player.WeaponData.Damage, GameManager.Data.Play.GetStatus(StatusEnum.Accuracy), hitable.GetStatus(StatusEnum.Avoid), wallCount, conditialable.GetConditionCount());
            hitable.Hit(damage);
            break;
        }
    }
}
