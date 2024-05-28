public interface IConditionalbe
{
    public void OccurCondition(ConditionEnum condition);

    public void RemoveCondition(ConditionEnum condition);

    public ConditionEnum GetCondition();

    public int GetConditionCount();
}
