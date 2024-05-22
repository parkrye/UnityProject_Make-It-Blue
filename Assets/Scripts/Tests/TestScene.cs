using Cysharp.Threading.Tasks;

public class TestScene : ActorScene
{
    private void Awake()
    {
        //LoadAsync();
    }

    protected override async UniTask LoadingRoutine()
    {
        await UniTask.DelayFrame(1);
        Progress = 1f;

        //GameManager.System.PlayerActor.InitForBattle();
        GameManager.System.PlayerActor.InitForWorld();
        if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
        {
            mainView.SendSubtitles();
        }
        GameManager.System.AddValueTrackAction(ValueTrackEvent);
    }

    public void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        switch (valueEnum)
        {
            case ValueTrackEnum.CrossHead:
                if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
                    mainView.TurnCrossHead(StaticValues.CrossHead);
                break;
            default:
                break;
        }
    }
}
