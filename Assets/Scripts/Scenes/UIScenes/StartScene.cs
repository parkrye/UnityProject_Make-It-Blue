using Cysharp.Threading.Tasks;

public class StartScene : UIScene
{
    private void Start()
    {
        LoadAsync();
    }

    protected override async UniTask LoadingRoutine()
    {
        await UniTask.Delay(100);
        Progress = 1f;
        GameManager.UI.OpenView<StartView>("StartView", out _);
    }
}
