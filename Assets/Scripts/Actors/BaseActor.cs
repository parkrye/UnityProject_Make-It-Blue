using UnityEngine;
using UnityEngine.Events;

public enum ActorType
{
    PC,
    NPC,
}

public enum ActorState
{
    Ready,
    Alive,
    Dead,
}

public class BaseActor : MonoBehaviour, IHitable
{
    protected ActorType _type;
    protected ActorState _state;

    public ActorType Type { get { return _type; } }
    public ActorState State { get { return _state; } }

    protected float _nowHP;
    protected float _nowSP;

    public UnityEvent<bool> ActorDiedEvent = new UnityEvent<bool>();

    public virtual void InitForWorld()
    {
        _state = ActorState.Ready;
    }

    public virtual void InitForBattle()
    {
        _state = ActorState.Ready;
    }

    public virtual void Hit(int damage)
    {

    }

    public virtual float GetStatus(StatusEnum status)
    {
        return 0f;
    }
}