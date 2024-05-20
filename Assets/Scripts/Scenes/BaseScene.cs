using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public float Progress { get; protected set; }
    protected abstract UniTask LoadingRoutine();

    public PlayerActor Player { get; private set; }
    protected Dictionary<string, View> _views = new Dictionary<string, View>();
    protected Dictionary<string, Dialog> _dialogs = new Dictionary<string, Dialog>();

    public MainCamera Camera { get; private set; }

    protected Transform[] _startPositions;

    public async void LoadAsync(int startPositionIndex = 0)
    {
        var views = FindObjectsOfType<View>();
        foreach (var view in views)
        {
            _views.Add(view.name, view);
            view.gameObject.SetActive(false);
        }
        var dialogs = FindObjectsOfType<Dialog>();
        foreach (var dialog in dialogs)
        {
            _dialogs.Add(dialog.name, dialog);
            dialog.gameObject.SetActive(false);
        }

        var startPosition = GameObject.Find("StartPositions");
        if (startPosition != null)
            _startPositions = startPosition.GetComponentsInChildren<Transform>();

        Player = FindObjectOfType<PlayerActor>();
        if (Player != null && startPositionIndex != 0)
            Player.transform.position = _startPositions[startPositionIndex].position;

        Camera = FindObjectOfType<MainCamera>();

        await LoadingRoutine();
    }

    public bool OpenView<T>(string name, out T result) where T : View
    {
        if (_views.TryGetValue(name, out var view))
        {
            result = (T)view;
            GameManager.UI.OpenView(view);
            return true;
        }
        result = null;
        return false;
    }

    public bool OpenDialog(string name)
    {
        if (_dialogs.TryGetValue(name, out Dialog dialog))
        {
            GameManager.UI.OpenDialog(dialog);
            return true;
        }
        return false;
    }
}