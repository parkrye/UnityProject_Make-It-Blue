using Cysharp.Threading.Tasks;

public class TitleScene : BaseScene
{
    private void Start()
    {
        LoadAsync();
    }

    protected override async UniTask LoadingRoutine()
    {
        OpenView<TitleView>("TitleView", out _);
        await UniTask.Delay(2000);
        GameManager.Scene.LoadScene("StartScene");
    }
}
