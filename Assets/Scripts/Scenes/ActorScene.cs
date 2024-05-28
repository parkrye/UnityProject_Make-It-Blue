using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ActorScene : BaseScene
{
    public MainCamera Camera { get; private set; }

    protected BaseActor[] _actors;

    public override async void LoadAsync(params Object[] parameters)
    {
        GameManager.System.PlayerActor = FindObjectOfType<PlayerActor>();

        InitScene();
        InitActors();

        Camera = FindObjectOfType<MainCamera>();

        await LoadingRoutine();
    }

    protected override async UniTask LoadingRoutine()
    {
        await UniTask.Delay(0);
    }

    protected virtual void InitScene()
    {
        if (GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out var mainView))
        {
            mainView.SendSubtitles();
        }
    }

    protected abstract void InitActors();
}
