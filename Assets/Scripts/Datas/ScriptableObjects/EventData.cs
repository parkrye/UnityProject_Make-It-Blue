using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Event Data", fileName = "EventData")]
public class EventData : ScriptableObject
{
    public string Name;
    public string Description;
    public int Count;
    public ContextData[] ContextArray;
}
