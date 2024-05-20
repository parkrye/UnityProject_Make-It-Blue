using UnityEngine;

[CreateAssetMenu (menuName = "Datas/Playable Data", fileName = "PlayableData")]
public class PlayableData : ScriptableObject
{
    public CharacterEnum character;
    public string Name;

    public int Power;
    public int Agility;
    public int Tough;
    public int Dexterity;
}
