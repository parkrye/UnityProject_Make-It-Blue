using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Farm Data", fileName = "FarmData")]
public class FarmData : ScriptableObject
{
    public ProductData[] PlantArray;
    public int[] DataArray;
}
