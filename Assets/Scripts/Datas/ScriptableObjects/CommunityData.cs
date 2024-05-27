using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Community Data", fileName = "CommunityData")]
public class CommunityData : EventData
{
    public int Favor;
    public ProductData[] RewardArray;
}
