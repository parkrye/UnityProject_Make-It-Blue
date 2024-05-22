using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Enemy Data", fileName = "EnemyData")]
public class EnemyData : ActorData
{
    public EquipmentData Weapon;
    public ProductData[] RewardArray;
}
