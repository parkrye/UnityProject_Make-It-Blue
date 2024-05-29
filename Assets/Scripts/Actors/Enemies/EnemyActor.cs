using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : BaseActor, IHitable, IConditionalbe
{
    public EnemyData EnemyData;
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

        _animController.ToggleBattleValue();

        _nowHP = GetStatus(StatusEnum.HP);
    }

    public override void Hit(int damage)
    {
        _nowHP -= damage;
        if (_nowHP <= 0)
        {
            ActorDiedEvent?.Invoke(false);
            Destroy(gameObject);
        }
    }

    public override float GetStatus(StatusEnum status)
    {
        float result;
        switch (status)
        {
            default:
                result = 0f;
                break;
            case StatusEnum.HP:
                result = EnemyData.HP * EnemyData.Tier;
                result *= GameManager.System.CurrentMission.Level;
                break;
            case StatusEnum.Damage:
                result = EnemyData.Damage + EnemyData.Tier;
                result *= GameManager.System.CurrentMission.Level;
                break;
            case StatusEnum.Accuracy:
                result = EnemyData.Accuracy + EnemyData.Tier * 5;
                result *= (1 + GameManager.System.CurrentMission.Level * 0.2f);
                break;
            case StatusEnum.Avoid:
                result = EnemyData.Avoid + EnemyData.Tier * 5;
                result *= (1 + GameManager.System.CurrentMission.Level * 0.2f);
                break;
        }
        return result;
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

    ConditionEnum IConditionalbe.GetCondition()
    {
        var result = ConditionEnum.None;
        foreach (var (condition, value) in _conditions)
        {
            if (value)
                result &= condition;
        }
        return result;
    }

    public int GetConditionCount()
    {
        var result = 0;
        foreach (var (_, value) in _conditions)
        {
            if (value)
                result++;
        }
        return result;
    }
}
