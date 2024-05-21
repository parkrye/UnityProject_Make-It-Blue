using Cysharp.Threading.Tasks;
using UnityEngine;

public class ActorScene : BaseScene
{
    public MainCamera Camera { get; private set; }

    protected Transform[] _startPositions;

    public override async void LoadAsync(params int[] datas)
    {
        GameManager.System.PlayerActor = FindObjectOfType<PlayerActor>();

        var startPosition = GameObject.Find("StartPositions");
        if (startPosition != null)
            _startPositions = startPosition.GetComponentsInChildren<Transform>();

        if (GameManager.System.PlayerActor != null && datas.Length > 0 && datas[0] != 0)
            GameManager.System.PlayerActor.transform.position = _startPositions[datas[0]].position;

        Camera = FindObjectOfType<MainCamera>();

        await LoadingRoutine();
    }

    protected override async UniTask LoadingRoutine()
    {

    }
}
