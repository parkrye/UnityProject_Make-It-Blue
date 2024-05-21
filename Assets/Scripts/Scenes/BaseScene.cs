using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public float Progress { get; protected set; }
    protected abstract UniTask LoadingRoutine();

    public MainCamera Camera { get; private set; }

    protected Transform[] _startPositions;

    public async void LoadAsync(int startPositionIndex = 0)
    {
        GameManager.System.PlayerActor = FindObjectOfType<PlayerActor>();

        var startPosition = GameObject.Find("StartPositions");
        if (startPosition != null)
            _startPositions = startPosition.GetComponentsInChildren<Transform>();

        if (GameManager.System.PlayerActor != null && startPositionIndex != 0)
            GameManager.System.PlayerActor.transform.position = _startPositions[startPositionIndex].position;

        Camera = FindObjectOfType<MainCamera>();

        await LoadingRoutine();
    }
}