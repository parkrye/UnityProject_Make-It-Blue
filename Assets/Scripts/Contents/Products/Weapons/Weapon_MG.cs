public class Weapon_MG : BaseEquipment
{
    public override void Init()
    {
        base.Init();

        MainAction = new Action_Shot(ActionEnum.OnAction2);
        SubAction = new Action_Attack(ActionEnum.OnAction0);
    }
}
