using System.Linq;
using UnityEngine;

public class Action_Shot : BaseAction
{
    public Action_Shot(ActionCode actionCode) : base(actionCode)
    {
    }

    public override void Action()
    {
        var player = GameManager.System.PlayerActor;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit) == false)
            return;

        var targetArray = Physics.RaycastAll(player.transform.position, (player.Focus.position - player.transform.position))
            .Where(t => t.transform.GetComponent<Collider>() != null && t.transform.GetComponent<Rigidbody>() != null)
            .OrderBy(t => Vector3.SqrMagnitude(t.point - player.transform.position));

        var wallCount = 0;
        foreach (var target in targetArray)
        {
            var hitable = target.transform.GetComponent<IHitable>();
            if (hitable == null)
            {
                wallCount++;
                continue;
            }

            var damage = Calculator.CalcuateDamage(
                player.WeaponData.Damage, 
                GameManager.Data.Play.GetStatus(StatusEnum.Accuracy), 
                hitable.GetStatus(StatusEnum.Avoid), 
                wallCount, 
                hitable.GetCondition());
            hitable.Hit(damage);
        }
    }
}
