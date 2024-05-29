using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Space
{
    public Space Parent;
    public float Size;
    public List<Space> Children = new List<Space>();
    public Vector3 Position;

    public Space(Space parent, float size, Vector3 position)
    {
        Parent = parent;
        Size = size;
        Position = position;
    }
}

public class SpaceDivider : MonoBehaviour
{
    [SerializeField] private Transform _center, _xSide;
    private Space _rootSpace;

    public UnityEvent<Space> DivideEndEvent = new UnityEvent<Space>();

    public void DivdeSpace()
    {
        _rootSpace = new Space(null, Mathf.Abs(_center.position.x - _xSide.position.x) * 2, _center.position);
        DivideRoutine(_rootSpace).Forget();
    }

    private async UniTask DivideRoutine(Space parentSpace)
    {
        await UniTask.Delay(0);
        var size = parentSpace.Size * 0.5f;
        if (size >= 1f)
        {
            for (int x = -1; x <= 1; x += 2)
            {
                for (int z = -1; z <= 1; z += 2)
                {
                    Space space = new Space(parentSpace, size, parentSpace.Position + (Vector3.right * x + Vector3.forward * z) * size * 0.5f);
                    if (Physics.CheckBox(space.Position, 0.5f * size * Vector3.one, Quaternion.identity, LayerMask.GetMask("Wall")))
                        DivideRoutine(space).Forget();
                    parentSpace.Children.Add(space);
                }
            }
        }

        if (parentSpace == _rootSpace)
            DivideEndEvent?.Invoke(_rootSpace);
    }
}
