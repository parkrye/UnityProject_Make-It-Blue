using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Enemy Data", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject Prefab;

    public string Name;
    public string Description;
    public EquipmentData Weapon;
    public ProductData[] RewardArray;
}
