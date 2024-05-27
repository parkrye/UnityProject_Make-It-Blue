public class MainScene : WorldScene
{
    public SimpleEventActor Hoshino;

    public override void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        base.ValueTrackEvent(valueEnum);
        switch (valueEnum)
        {
            default:
                break;
        }
    }

    protected override void InitActors()
    {
        _actors = FindObjectsOfType<BaseActor>();
        foreach (var actor in _actors)
        {
            actor.InitForWorld();
        }

        Hoshino.EndOfContextEvent.AddListener(OnEventAction);
    }

    protected override void InitScene()
    {
        if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
            mainView.SendSubtitles();
    }

    private void OnEventAction(int index)
    {
        switch (index)
        {
            default:
            case 0:
                BattleSetting();
                break;
            case 1:
                GameManager.Scene.LoadScene("MainScene");
                break;
        }
    }

    private void BattleSetting()
    {
        if (GameManager.UI.OpenDialog<BattleSettingDialog>("BattleSettingDialog", out var bsDialog))
        {

        }
    }
}
