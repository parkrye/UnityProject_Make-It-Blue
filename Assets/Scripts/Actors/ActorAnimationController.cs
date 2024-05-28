using UnityEngine;

public class ActorAnimationController : MonoBehaviour
{
    [SerializeField] private Transform _rightHand, _leftHand;

    private NormalAnimationController _animator;

    private void Awake()
    {
        _animator = GetComponent<NormalAnimationController>();
        if (_animator == null)
            Debug.Log(gameObject.name + " lost NormalAnimationController");
    }

    public Transform GetHand(bool isRightHand)
    {
        if (isRightHand)
            return _rightHand;
        return _leftHand;
    }

    public void PlayMove(Vector2 input)
    {
        _animator.PlayMoveAnimation(input);
    }

    public void PlayTurn(float input)
    {
        _animator.PlayTurnAnimation(input);
    }

    public void PlayAction(BaseAction action)
    {
        if (action == null)
            return;
        _animator.PlayActionAnimation(action.ActionCode);
    }

    public void ToggleLoopValue(params bool[] isOn)
    {
        _animator.SetBoolValue("OnLoop", isOn);
    }

    public void ToggleBattleValue(params bool[] isOn)
    {
        _animator.SetBoolValue("OnBattle", isOn);
    }

    public void EquipWeapon(WeaponData equipmentData)
    {
        var equipment = GameManager.Resource.Instantiate(equipmentData.Weapon);
        if (equipmentData.Type >= ProductEnum.Weapon_HG && equipmentData.Type <= ProductEnum.Weapon_MG)
        {
            equipment.transform.SetParent(_rightHand, true);
            _animator.SetIntValue("WeaponType", (int)equipmentData.Type);
        }
        else if (equipmentData.Type == ProductEnum.Item_Shield)
        {
            equipment.transform.SetParent(_leftHand, true);
            _animator.SetBoolValue("OnShield", true);
        }

        equipment.transform.localPosition = Vector3.zero;
        equipment.transform.localRotation = Quaternion.identity;
    }
}
