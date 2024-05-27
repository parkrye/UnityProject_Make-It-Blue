using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Enemy Data", fileName = "EnemyData")]
public class EnemyData : ActorData
{
    public WeaponData Weapon;
    public ProductData[] RewardArray;
    public int[] RewardCountAaray;

    public int Tier;
    public int HP;
    public int Accuracy;
    public int Avoid;
}
