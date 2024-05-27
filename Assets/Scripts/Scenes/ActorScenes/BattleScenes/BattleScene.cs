using Cysharp.Threading.Tasks;

public abstract class BattleScene : ActorScene, IValueTrackable
{
    protected override async UniTask LoadingRoutine()
    {
        GameManager.System.AddValueTrackAction(ValueTrackEvent);
        await UniTask.Delay(100);

        Progress = 1f;
    }

    public virtual void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        switch (valueEnum)
        {
            default:
                break;
            case ValueTrackEnum.CrossHead:
                if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
                    mainView.TurnCrossHead(StaticValues.CrossHead);
                break;
        }
    }

    protected override void InitScene()
    {

    }

    protected override void InitActors()
    {
        _actors = FindObjectsOfType<BaseActor>();
        foreach (var actor in _actors)
        {
            actor.InitForBattle();
        }
    }
}
