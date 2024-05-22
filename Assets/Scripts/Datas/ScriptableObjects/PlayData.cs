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

    public int Level;
    public int[] Status;
    public ProductData[] ProductArray;
    public FarmData Farm;

    public DateTime StartDate;
    public DateTime LastConnectedTime;
    public EventData[] EventArray;

    public float GetStatus(StatusEnum status)
    {
        switch(status)
        {
            default:
            case StatusEnum.Strength:
                return Status[0];
            case StatusEnum.Dexterity:
                return Status[1];
            case StatusEnum.Power:
                return Status[2];
            case StatusEnum.Agility:
                return Status[3];

            case StatusEnum.LimitWeight:
                return Status[0] * 2;
            case StatusEnum.LoadSpeed:
                return 1 - Status[1] * 0.015f;
            case StatusEnum.Accuracy:
                return 80 + Status[1] * 0.05f;
            case StatusEnum.MaxHP:
                return Status[2] * 20;
            case StatusEnum.SPRecovery:
                return 1 + Status[2] * 0.05f;
            case StatusEnum.MoveSpeed:
                return 1 + Status[3] * 0.05f;
            case StatusEnum.Avoid:
                return 20 + Status[3] * 0.015f;
        }
    }
}
