using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Inventory Data", fileName = "InventoryData")]
public class InventoryData : ScriptableObject
{
    public ProductData[] ProductArray;
}
