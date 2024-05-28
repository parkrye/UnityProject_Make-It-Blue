using System.Collections.Generic;

public class CurrentMission
{
    public MissionData Mission;
    public WeaponData Weapon;
    public int Level;
    public List<ItemData> Items;

    public CurrentMission(MissionData mission, WeaponData weapon, int level, List<ItemData> items)
    {
        Mission = mission;
        Weapon = weapon;
        Level = level;
        Items = items;
    }
}
