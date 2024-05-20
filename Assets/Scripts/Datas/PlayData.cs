using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Play Data", fileName = "PlayData")]
public class PlayData : ScriptableObject
{
    public DateTime StartDate;
    public int Debt;
    public EventData[] EventArray;
}
