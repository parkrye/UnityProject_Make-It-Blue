using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Actor Data", fileName = "ActorData")]
public class ActorData : ScriptableObject
{
    public Sprite Sprite;
    public GameObject Model;

    public CharacterEnum Character;
    public string[] Name = new string[2];
    public string Description;
}