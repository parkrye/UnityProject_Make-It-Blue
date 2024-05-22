using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Enemy Data", fileName = "EnemyData")]
public class EnemyData : ActorData
{
    public EquipmentData Weapon;
    public ProductData[] RewardArray;

    public int Tier;
    public int HP;
    public int Accuracy;
    public int Avoid;
}
