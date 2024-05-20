using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Arbeit Data", fileName = "ArbeitData")]
public class ArbeitData : ScriptableObject
{
    public string Name;
    public string Description;
    public int Count;
    public ProductData[] RewardArray;
    public ContextData[] ContextArray;
}
