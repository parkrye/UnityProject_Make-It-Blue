using UnityEngine;

public class NormalAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError($"{gameObject.name} lost animator!");
    }

    public bool PlayMoveAnimation(Vector2 input)
    {
        try
        {
            _animator.SetFloat("OnFowardMove", input.y);
            _animator.SetFloat("OnSideMove", input.x);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool PlayActionAnimation(int index)
    {
        try
        {
            _animator.SetTrigger($"OnAction{index}");
            return true;
        }
        catch
        {
            return false;
        }
    }
}