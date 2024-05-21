using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public float Progress { get; protected set; }
    protected abstract UniTask LoadingRoutine();

    public virtual async void LoadAsync(params int[] datas)
    {
        await LoadingRoutine();
    }
}