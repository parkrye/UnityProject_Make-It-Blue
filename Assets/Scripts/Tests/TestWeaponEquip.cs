using UnityEngine;

public class TestWeaponEquip : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.System.PlayerActor.EquipEquipments(_weaponData);
    }
}
