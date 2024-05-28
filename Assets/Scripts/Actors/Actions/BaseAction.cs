public abstract class BaseAction
{
    public string ActionName;
    public ActionEnum ActionCode;

    public BaseAction(ActionEnum actionCode, string actionName = null)
    {
        ActionCode = actionCode;
        if (actionName != null)
            ActionName = actionName;
    }

    public abstract void Action();
}
