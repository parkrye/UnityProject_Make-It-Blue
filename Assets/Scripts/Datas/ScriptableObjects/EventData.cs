using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Event Data", fileName = "EventData")]
public class EventData : ScriptableObject
{
    public ActorData Actor;
    public int Count;
    public ContextData[] ContextArray;
}
