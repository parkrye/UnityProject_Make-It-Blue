using UnityEngine;

public class Weapon : MonoBehaviour
{
    public EquipmentData WeaponData;

    public int Bullets { get; private set; }

    public void LoadBullet(int count)
    {
        Bullets += count;
    }

    public void Use()
    {

    }
}
