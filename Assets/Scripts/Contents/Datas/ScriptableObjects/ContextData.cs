using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Context Data", fileName = "ContextData")]
public class ContextData : ScriptableObject
{
    public CharacterEnum[] TalkerArray;
    public string[] TalkArray;
}
