using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BaseInteractableActor : BaseActor, IInteractable
{
    public override void InitForWorld()
    {
        base.InitForWorld();
    }

    public override void InitForBattle()
    {
        base.InitForBattle();
    }

    public virtual bool Interact()
    {
        return true;
    }
}
