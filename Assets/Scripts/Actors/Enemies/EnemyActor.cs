using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyActor : BaseActor, IHitable, IConditionalbe
{
    public EnemyData EnemyData;
    public UnityEvent EnemyDiedEvent = new UnityEvent();
    private ActorAnimationController _animController;

    private Dictionary<ConditionEnum, bool> _conditions = new Dictionary<ConditionEnum, bool>();

    public override void InitForWorld()
    {
        base.InitForWorld();

        Destroy(gameObject);
    }

    public override void InitForBattle()
    {
        base.InitForBattle();

        _animController = GetComponent<ActorAnimationController>();
        if (_animController == null)
            Debug.Log($"{name} lost AnimController!");

        _nowHP = EnemyData.HP;
    }

    public void Hit(int damage)
    {
        _nowHP -= damage;
        if (_nowHP <= 0)
        {
            EnemyDiedEvent?.Invoke();
        }
    }

    public float GetStatus(StatusEnum status)
    {
        switch (status)
        {
            default:
                return 0;
            case StatusEnum.Accuracy:
                return EnemyData.Accuracy;
            case StatusEnum.Avoid:
                return EnemyData.Avoid;
        }
    }

    public int GetCondition()
    {
        return _conditions.Count;
    }

    public void OccurCondition(ConditionEnum condition)
    {
        if (_conditions.ContainsKey(condition))
            return;
        _conditions[condition] = true;
    }

    public void RemoveCondition(ConditionEnum condition)
    {
        if (_conditions.ContainsKey(condition) == false)
            return;
        _conditions.Remove(condition);
    }
}
