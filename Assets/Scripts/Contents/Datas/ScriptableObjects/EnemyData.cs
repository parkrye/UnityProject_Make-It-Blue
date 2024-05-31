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

    public int GetBullets
    {
        get
        {
            switch (Weapon)
            {
                default:
                case ProductEnum.Weapon_HG:
                    return 30;
                case ProductEnum.Weapon_AR:
                    return 60;
                case ProductEnum.Item_Shield:
                case ProductEnum.Weapon_SG:
                    return 10;
                case ProductEnum.Weapon_MG:
                    return 90;
            }
        }
    }

    public float GetRange 
    { 
        get 
        { 
            switch(Weapon)
            {
                default:
                case ProductEnum.Weapon_HG:
                    return 10f;
                case ProductEnum.Weapon_AR:
                case ProductEnum.Item_Shield:
                case ProductEnum.Weapon_SG:
                    return 20f;
                case ProductEnum.Weapon_MG:
                    return 40f;
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
