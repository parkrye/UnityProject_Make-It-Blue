using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Enemy Data", fileName = "EnemyData")]
public class EnemyData : ActorData
{
    public ProductData[] RewardArray;
    public int[] RewardCountAaray;

    public int Tier;
    public int HP;
    public int Damage;
    public int Accuracy;
    public int Avoid;

    public ProductEnum Weapon;
    public bool Shield;

    public float GetRange 
    { 
        get 
        { 
            switch(Weapon)
            {
                default:
                case ProductEnum.Weapon_HG:
                    return 50f;
                case ProductEnum.Weapon_AR:
                case ProductEnum.Item_Shield:
                case ProductEnum.Weapon_SG:
                    return 100f;
                case ProductEnum.Weapon_MG:
                    return 200f;
            } 
        } 
    }

    public float GetShotDelay
    { 
        get 
        { 
            switch(Weapon)
            {
                default:
                case ProductEnum.Weapon_HG:
                    return 1.5f;
                case ProductEnum.Weapon_AR:
                    return 1f;
                case ProductEnum.Item_Shield:
                case ProductEnum.Weapon_SG:
                    return 2f;
                case ProductEnum.Weapon_MG:
                    return 0.5f;
            } 
        } 
    }
}
