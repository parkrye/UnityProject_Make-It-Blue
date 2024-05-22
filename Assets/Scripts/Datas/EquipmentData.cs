using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Equipment Data", fileName = "EquipmentData")]
public class EquipmentData : ProductData
{
    public GameObject Prefab;
    public BaseAction MainAction, SubAction;
    public int Damage;
    public int Range;
    public int ShotDelay;
    public int LoadDelay;
}
