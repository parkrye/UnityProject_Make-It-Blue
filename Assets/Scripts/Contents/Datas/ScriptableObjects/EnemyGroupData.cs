using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/EnemyGroup Data", fileName = "EnemyGroupData")]
public class EnemyGroupData : ScriptableObject
{
    public string Name;
    public string Description;
    public EnemyActor[] EnemyArray;

    public (ProductData[], int[]) GetReward()
    {
        var dict = new Dictionary<ProductData, int>();

        foreach (var enemyActor in EnemyArray)
        {
            for (int i = 0; i < enemyActor.EnemyData.RewardArray.Length; i++)
            {
                if (dict.ContainsKey(enemyActor.EnemyData.RewardArray[i]))
                    dict[enemyActor.EnemyData.RewardArray[i]] += enemyActor.EnemyData.RewardCountAaray[i];
                else
                    dict[enemyActor.EnemyData.RewardArray[i]] = enemyActor.EnemyData.RewardCountAaray[i];
            }
        }

        return (dict.Keys.ToArray(), dict.Values.ToArray());
    }
}
