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

public enum PersonalCameraPosition
{
    Back,
    Front,
    Up,
}

public enum Direction
{
    Up,
    Dowm,
    Left,
    Right,
}

public enum ActionCode
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