using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Community Data", fileName = "CommunityData")]
public class CommunityData : ScriptableObject
{
    public string Name;
    public string Description;
    public int Favor;
    public ProductData[] RewardArray;
    public ContextData[] ContextArray;
}
