using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Arbeit Data", fileName = "ArbeitData")]
public class ArbeitData : EventData
{
    public string Name;
    public string Description;
    public ProductData[] RewardArray;
    public int[] RewardCountArray;
}
