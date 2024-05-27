public enum CharacterEnum
{
    None,
    Player,
    Ayane,
    Hoshino,
    Shiroko,
    Nonomi,
    Serika,
}

public enum ProductEnum
{
    Credit,
    Equipment = 10,
    Equipment_HG,
    Equipment_AR,
    Equipment_SG,
    Equipment_MG,
    Equipment_BulletHG,
    Equipment_BulletAR,
    Equipment_BulletSG,
    Equipment_BulletMG,
    Equipment_Other,
    Equipment_Shield,
    Item = 100,
    Plant = 1000,
}

public enum DataEnum
{
    Actor,
    Mission,
    Product,
    Item,
    Weapon,
}

public enum PersonalCameraPositionEnum
{
    Back,
    Front,
    Up,
}

public enum DirectionEnum
{
    Up,
    Dowm,
    Left,
    Right,
}

public enum ActionEnum
{
    None,
    OnAction0,  // kick
    OnAction1,  // hg/sg
    OnAction2,  // ar/mg
    OnAction3,  // throw
    OnAction4,  // burf
    OnAction5,  // hi
    OnAction6,  // jump
    OnAction7,  // Clap
    OnAction8,  // Question
    OnAction9,  // talk
    Dance,
}

public enum StatusEnum
{
    Strength,
    Dexterity,
    Power,
    Agility,

    LimitWeight,
    LoadSpeed,
    Accuracy,
    MaxHP,
    SPRecovery,
    MoveSpeed,
    Avoid,
}

public enum ConditionEnum
{
    Stun,
    Slow,
    Burn,
    Poison,
    Blind,
    Bind,
    Trip,
}

public enum PublicUIEnum
{
    Main,
    Option,
    BattleSetting,
    Community,
    Shop,
}