using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIScene : BaseScene
{
    public override async void LoadAsync(params Object[] parameters)
    {
        await LoadingRoutine();
    }

    protected override async UniTask LoadingRoutine()
    {
        await UniTask.Delay(0);
    }
}
