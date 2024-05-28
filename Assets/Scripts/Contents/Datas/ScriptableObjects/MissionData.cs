using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Mission Data", fileName = "MissionData")]
public class MissionData : ScriptableObject
{
    public string Name;
    public string Description;
    public EnemyGroupData[] EnemyGroupArray;
    public int Count;
}
