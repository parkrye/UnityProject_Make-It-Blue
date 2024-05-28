using UnityEngine;

[CreateAssetMenu(menuName = "Datas/EnemyGroup Data", fileName = "EnemyGroupData")]
public class EnemyGroupData : ScriptableObject
{
    public string Name;
    public string Description;
    public EnemyActor[] EnemyArray;
}
