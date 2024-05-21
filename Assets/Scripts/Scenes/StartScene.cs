using Cysharp.Threading.Tasks;

public class StartScene : BaseScene
{
    protected override async UniTask LoadingRoutine()
    {
        await UniTask.Delay(100);
        Progress = 1f;
        GameManager.UI.OpenView<StartView>("StartView", out _);
    }
}
