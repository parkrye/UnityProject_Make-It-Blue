public abstract class BaseAction
{
    public ActionEnum ActionCode;

    public BaseAction(ActionEnum actionCode)
    {
        ActionCode = actionCode;
    }

    public abstract void Action();
}
