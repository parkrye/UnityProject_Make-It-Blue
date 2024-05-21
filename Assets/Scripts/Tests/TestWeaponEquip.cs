using UnityEngine;

public class TestWeaponEquip : MonoBehaviour
{
    [SerializeField] private EquipmentData _weaponData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Find Player");
            GameManager.System.PlayerActor.EquipWeapon(_weaponData);
        }
    }
}
