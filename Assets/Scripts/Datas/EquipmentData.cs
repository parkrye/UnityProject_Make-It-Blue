using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Equipment Data", fileName = "EquipmentData")]
public class EquipmentData : ProductData
{
    public BaseEquipment Equipment;

    public int Tier;
    public int Enhance;

    public int Damage;
    public int Range;
    public int Bullets;
    public int ShotDelay;
    public int LoadDelay;
}
