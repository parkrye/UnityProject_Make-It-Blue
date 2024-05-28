using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Weapon Data", fileName = "WeaponData")]
public class WeaponData : ProductData
{
    public BaseEquipment Weapon;

    public int Tier;
    public int Enhance;

    public int Damage;
    public int Range;
    public int Bullets;
    public int ShotDelay;
    public int LoadDelay;
}
