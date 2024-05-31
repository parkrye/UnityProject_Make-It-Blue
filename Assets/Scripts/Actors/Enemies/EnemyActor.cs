using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : BaseActor, IHitable, IConditionalbe
{
    public EnemyData EnemyData;

    private EnemyActorController _controller;
    private ActorAnimationController _animController;

    private Dictionary<ConditionEnum, bool> _conditions = new Dictionary<ConditionEnum, bool>();

    private void Update()
    {
        if (_nowHP > 0)
            _controller.Tick();
    }

    public override void InitForWorld()
    {
        base.InitForWorld();

        Destroy(gameObject);
    }

    public override void InitForBattle()
    {
        base.InitForBattle();

        _controller = GetComponent<EnemyActorController>();
        if (_controller == null)
            Debug.Log($"{name} lost EnemyActorController!");

        _animController = GetComponent<ActorAnimationController>();
        if (_animController == null)
            Debug.Log($"{name} lost AnimController!");

        _animController.ToggleBattleValue();
        if (EnemyData.Weapon == ProductEnum.Item_Shield)
            _animController.ToggleValue("OnShield", true);
        _controller.AnimationChangedEvent.AddListener(OnAnimatonChangeAction);

        _nowHP = GetStatus(StatusEnum.HP);
    }

    public override void Hit(int damage)
    {
        _nowHP -= damage;
        if (_nowHP <= 0)
        {
            _animController.ToggleValue("OnDead", true);
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
                result = EnemyData.HP * (EnemyData.Tier + 1);
                result *= GameManager.System.CurrentMission.Level;
                break;
            case StatusEnum.Damage:
                result = EnemyData.Damage + (EnemyData.Tier + 1);
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

    private void OnAnimatonChangeAction(AnimationEnum upper, AnimationEnum lower)
    {
        switch (upper)
        {
            default:
            case AnimationEnum.Idle:
                break;
            case AnimationEnum.Aim:
                break;
            case AnimationEnum.Shot:
                switch(EnemyData.Weapon)
                {
                    default:
                    case ProductEnum.Weapon_HG:
                    case ProductEnum.Weapon_SG:
                        _animController.PlayAction(ActionEnum.OnAction1);
                        break;
                    case ProductEnum.Weapon_AR:
                    case ProductEnum.Weapon_MG:
                        _animController.PlayAction(ActionEnum.OnAction2);
                        break;
                }
                break;
            case AnimationEnum.Die:
                break;
        }

        switch (lower)
        {
            default:
            case AnimationEnum.Stand:
                _animController.PlayMove(Vector2.zero);
                break;
            case AnimationEnum.Move:
                _animController.PlayMove(Vector2.up);
                break;
        }
    }
}
