using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConditionalbe
{
    public void OccurCondition(ConditionEnum condition);

    public void RemoveCondition(ConditionEnum condition);
}
