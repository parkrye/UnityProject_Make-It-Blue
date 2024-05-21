using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Play Data", fileName = "PlayData")]
public class PlayData : ScriptableObject
{
    public string Name;
    public GameObject Model;
    public Color[] Colors;

    public int Debt;
    public int Stamina;
    public int Energy;

    public int[] Status;
    public ProductData[] ProductArray;

    public DateTime StartDate;
    public DateTime LastConnectedTime;
    public EventData[] EventArray;
}
