using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : BaseManager
{
    private BaseScene _curScene;

    public bool ReadyToPlay { get; private set; }

    public BaseScene CurScene
    {
        get
        {
            if (!_curScene)
                _curScene = FindObjectOfType<BaseScene>();

            return _curScene;
        }
    }

    public override void InitManager()
    {
        base.InitManager();


    }

    public async void LoadScene(string sceneName, params Object[] parameters)
    {
        await LoadingRoutine(sceneName);
    }

    private async UniTask LoadingRoutine(string sceneName)
    {
        ReadyToPlay = false;
        GameManager.UI.ResetUI();
        Time.timeScale = 1f;
        if (GameManager.UI.OpenView<LoadingView>("LoadingView", out var loadingView))
            await UniTask.WaitUntil(() => loadingView.isActiveAndEnabled == true);
        var oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while (!oper.isDone)
        {
            await UniTask.NextFrame();
        }
        _curScene = null;

        if (CurScene)
        {
            CurScene.LoadAsync();
            while (CurScene.Progress < 1f)
            {
                await UniTask.NextFrame();
            }
        }

        await UniTask.NextFrame();
        ReadyToPlay = true;
    }
}