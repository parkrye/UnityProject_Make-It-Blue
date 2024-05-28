using UnityEngine;

public abstract class BaseEquipment : MonoBehaviour
{
    public BaseAction MainAction;
    public BaseAction SubAction;

    public virtual void Init()
    {

    }

    public virtual void Use()
    {

    }
}
