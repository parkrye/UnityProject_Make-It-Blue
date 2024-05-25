using Cysharp.Threading.Tasks;

public class OpeningScene : WorldScene
{
    public SimpleEventActor Serika;

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

        Serika.EndOfContextEvent.AddListener(OnEventAction);
    }

    protected override void InitScene()
    {
        if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
        {
            mainView.SendSubtitles();
        }
    }

    private void OnEventAction(int index)
    {
        switch (index)
        {
            default:
            case 0:
                PlayerMaking();
                break;
            case 1:
                GameManager.Scene.LoadScene("AbydosScene");
                break;
        }
    }

    private void PlayerMaking()
    {
        if (GameManager.UI.OpenDialog<PlayerMakingDialog>("PlayerMakingDialog", out var pmDialog))
        {
            
        }
    }
}
