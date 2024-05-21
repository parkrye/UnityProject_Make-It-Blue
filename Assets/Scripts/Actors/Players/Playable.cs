using UnityEngine;

public class Playable : MonoBehaviour
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

    public void PlayMoveAnimation(Vector2 input)
    {
        _animator.PlayMoveAnimation(input);
    }

    public void PlayActionAnimation(int index)
    {
        _animator.PlayActionAnimation(index);
    }

    public void ToggleLoopAnimation()
    {
        _animator.PlayBoolAnimation("OnLoop");
    }

    public void ToggleLoopAnimation(bool isOn)
    {
        _animator.PlayBoolAnimation("OnLoop", isOn);
    }

    public void ToggleEquipAnimation()
    {
        _animator.PlayBoolAnimation("OnEquip");
    }

    public void ToggleEquipAnimation(bool isOn)
    {
        _animator.PlayBoolAnimation("OnEquip", isOn);
    }

    public void SetGunAnimationValue(int value)
    {
        _animator.SetIntValue("WeaponType", value);
    }

    public void EquipWeapon(EquipmentData equipmentData)
    {
        var equipment = GameManager.Resource.Instantiate(equipmentData.Prefab);
        if (equipmentData.Type == ProductEnum.Equipment_Shield)
        {
            equipment.transform.SetParent(_leftHand, true);
            _animator.PlayBoolAnimation("OnShield", true);
        }
        else
        {
            equipment.transform.SetParent(_rightHand, true);
            SetGunAnimationValue((int)equipmentData.Type);
        }
        equipment.transform.localPosition = Vector3.zero;
        equipment.transform.localRotation = Quaternion.identity;
    }
}
