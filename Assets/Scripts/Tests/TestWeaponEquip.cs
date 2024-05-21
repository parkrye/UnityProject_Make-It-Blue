using UnityEngine;

public class TestWeaponEquip : MonoBehaviour
{
    [SerializeField] WeaponEnum _weapon;
    private WeaponData _weaponData;

    private void Awake()
    {
        switch (_weapon)
        {
            case WeaponEnum.HG:
                _weaponData = GameManager.Resource.Load<WeaponData>("Datas/Weapons/HGData");
                break;
            case WeaponEnum.AR:
                _weaponData = GameManager.Resource.Load<WeaponData>("Datas/Weapons/ARData");
                break;
            case WeaponEnum.SG:
                _weaponData = GameManager.Resource.Load<WeaponData>("Datas/Weapons/SGData");
                break;
            case WeaponEnum.MG:
                _weaponData = GameManager.Resource.Load<WeaponData>("Datas/Weapons/MGData");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Find Player");
            GameManager.System.PlayerActor.EquipWeapon(_weaponData);
        }
    }
}
