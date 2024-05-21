using UnityEngine;

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

public class BaseActor : MonoBehaviour
{
    protected ActorType _type;
    protected ActorState _state;

    public ActorType Type { get { return _type; } }
    public ActorState State { get { return _state; } }

    protected float _nowHP;
    protected float _nowSP;

    public virtual void InitForWorld()
    {
        _state = ActorState.Ready;
    }

    public virtual void InitForBattle()
    {
        _state = ActorState.Ready;
    }
}