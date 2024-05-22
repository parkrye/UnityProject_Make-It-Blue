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

    public bool PlayActionAnimation(ActionCode actionCode)
    {
        try
        {
            _animator.SetTrigger($"{actionCode}");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SetBoolValue(string name, params bool[] isOn)
    {
        try
        {
            if (isOn.Length > 0)
            {
                _animator.SetBool(name, isOn[0]);
            }
            else
            {
                var currentState = _animator.GetBool(name);
                _animator.SetBool(name, currentState == false);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SetIntValue(string name, int value)
    {
        try
        {
            _animator.SetInteger(name, value);
            return true;
        }
        catch
        {
            return false;
        }
    }
}