using Cysharp.Threading.Tasks;

public class UIScene : BaseScene
{
    public override async void LoadAsync(params int[] datas)
    {
        await LoadingRoutine();
    }

    protected override async UniTask LoadingRoutine()
    {

    }
}
