using UnityEngine;

public class Playable : MonoBehaviour
{
    public PlayableData Data;
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

    public void ToggleEquipAnimation()
    {
        _animator.PlayBoolAnimation("OnEquip");
    }

    public void ToggleEquipAnimation(bool isEquip)
    {
        _animator.PlayBoolAnimation("OnEquip", isEquip);
    }

    public void SetGunAnimationValue(int value)
    {
        _animator.SetIntValue("WeaponType", value);
    }

    public void EquipWeapon(WeaponData weaponData)
    {
        var weapon = GameManager.Resource.Instantiate(weaponData.Prefab);
        if (weaponData.Type == WeaponEnum.Shield)
        {
            weapon.transform.SetParent(_leftHand, true);
            _animator.PlayBoolAnimation("OnShield", true);
        }
        else
        {
            weapon.transform.SetParent(_rightHand, true);
            SetGunAnimationValue((int)weaponData.Type);
        }
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }
}
