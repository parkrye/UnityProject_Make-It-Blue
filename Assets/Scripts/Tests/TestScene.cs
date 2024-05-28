public class TestScene : BattleScene
{
    private void Awake()
    {
        //LoadAsync();
    }

    protected override void InitScene()
    {
        if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
        {
            mainView.SendSubtitles();
        }
    }

    protected override void InitActors()
    {
        _actors = FindObjectsOfType<BaseActor>();
        foreach (var actor in _actors)
        {
            actor.InitForBattle();
        }
    }

    public override void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        base.ValueTrackEvent(valueEnum);
        switch (valueEnum)
        {
            default:
                break;
        }
    }

    protected override void OnPlayerDefeat()
    {

    }

    protected override void OnPlayerWin()
    {

    }
}
