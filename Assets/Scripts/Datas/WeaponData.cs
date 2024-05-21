using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Weapon Data", fileName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    public string Name;
    public string Description;
    public int Damage;
    public int Range;
    public int ShotDelay;
    public int LoadDelay;
    public WeaponEnum Type;
    public GameObject Prefab;
}
