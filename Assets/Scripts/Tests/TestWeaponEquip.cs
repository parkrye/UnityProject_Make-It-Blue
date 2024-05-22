using UnityEngine;

public class TestWeaponEquip : MonoBehaviour
{
    [SerializeField] private EquipmentData _weaponData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.System.PlayerActor.EquipEquipment(_weaponData);
    }
}
