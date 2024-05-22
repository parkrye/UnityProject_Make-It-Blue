public abstract class BaseAction
{
    public ActionCode ActionCode;

    public BaseAction(ActionCode actionCode)
    {
        ActionCode = actionCode;
    }

    public abstract void Action();
}
