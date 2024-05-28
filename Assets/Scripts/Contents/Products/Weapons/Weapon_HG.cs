public class Weapon_HG : BaseEquipment
{
    public override void Init()
    {
        base.Init();

        MainAction = new Action_Shot(ActionEnum.OnAction1);
        SubAction = new Action_Attack(ActionEnum.OnAction0);
    }
}
