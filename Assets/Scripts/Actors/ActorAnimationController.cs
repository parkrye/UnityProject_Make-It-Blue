using UnityEngine;

public class ActorAnimationController : MonoBehaviour
{
    [SerializeField] private Transform _rightHand, _leftHand;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError($"{gameObject.name} lost animator!");
    }

    private bool PlayMoveAnimation(Vector2 input)
    {
        try
        {
            if (_animator.GetFloat("DanceType") >= 0f)
                _animator.SetFloat("DanceType", -1f);

            _animator.SetFloat("OnFowardMove", input.y);
            _animator.SetFloat("OnSideMove", input.x);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool PlayTurnAnimation(float input)
    {
        try
        {
            if (_animator.GetFloat("DanceType") >= 0f)
                _animator.SetFloat("DanceType", -1f);

            _animator.SetFloat("OnTurn", input);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool PlayActionAnimation(ActionEnum actionCode)
    {
        try
        {
            if (_animator.GetFloat("DanceType") >= 0f)
                _animator.SetFloat("DanceType", -1f);

            if (actionCode == ActionEnum.None)
                return false;
            if (actionCode == ActionEnum.Dance)
            {
                _animator.SetFloat("DanceType", Random.Range(0f, 1f));
                return true;
            }
            _animator.SetTrigger($"{actionCode}");
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool SetBoolValue(string name, params bool[] isOn)
    {
        try
        {
            if (_animator.GetFloat("DanceType") >= 0f)
                _animator.SetFloat("DanceType", -1f);

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

    private bool SetIntValue(string name, int value)
    {
        try
        {
            if (_animator.GetFloat("DanceType") >= 0f)
                _animator.SetFloat("DanceType", -1f);

            _animator.SetInteger(name, value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Transform GetHand(bool isRightHand)
    {
        if (isRightHand)
            return _rightHand;
        return _leftHand;
    }

    public void PlayMove(Vector2 input)
    {
        PlayMoveAnimation(input);
    }

    public void PlayTurn(float input)
    {
        PlayTurnAnimation(input);
    }

    public void PlayAction(BaseAction action)
    {
        if (action == null)
            return;
        PlayActionAnimation(action.ActionCode);
    }

    public void ToggleLoopValue(params bool[] isOn)
    {
        SetBoolValue("OnLoop", isOn);
    }

    public void ToggleBattleValue(params bool[] isOn)
    {
        SetBoolValue("OnBattle", isOn);
    }

    public void EquipWeapon(WeaponData equipmentData)
    {
        var equipment = GameManager.Resource.Instantiate(equipmentData.Weapon);
        if (equipmentData.Type >= ProductEnum.Weapon_HG && equipmentData.Type <= ProductEnum.Weapon_MG)
        {
            equipment.transform.SetParent(_rightHand, true);
            SetIntValue("WeaponType", (int)equipmentData.Type);
        }
        else if (equipmentData.Type == ProductEnum.Item_Shield)
        {
            equipment.transform.SetParent(_leftHand, true);
            SetBoolValue("OnShield", true);
        }

        equipment.transform.localPosition = Vector3.zero;
        equipment.transform.localRotation = Quaternion.identity;
    }
}
