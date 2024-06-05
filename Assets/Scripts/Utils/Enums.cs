using System;

public enum GameFrameEnum
{
    Thirty = 0,
    Sixty,
    Ninety,
    Hundread
}

public enum ValueTrackEnum
{
    MasterVolume,
    BGMVolume,
    SFXVolume,
    PostExposure,
    MotionBlur,
    VSync,
    GameFrame,
    CrossHead,

    Minitues,
}

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
    Weapon = 10,
    Weapon_HG,
    Weapon_AR,
    Weapon_SG,
    Weapon_MG,
    Event = 100,
    Item = 1000,
    Item_Shield,
    Item_BulletHG,
    Item_BulletAR,
    Item_BulletSG,
    Item_BulletMG,
    Plant = 10000,
}

public enum DataEnum
{
    Actor,
    Mission,
    Product,
    Item,
    Weapon,
    Plant,
    Community,
}

public enum PersonalCameraPositionEnum
{
    Back,
    Front,
    Up,
    FirstView,
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

    HP,
    Damage,
}

[Flags]
public enum ConditionEnum
{
    None,
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
    BattleResult,
    Arbeit,
    Farm,
}

public enum AnimationEnum
{
    Stand,
    Move,

    Idle,
    Aim,
    Shot,
    Die,
}