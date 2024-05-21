using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainScene : BaseScene, IValueTrackable
{
    protected override async UniTask LoadingRoutine()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        await UniTask.Delay(100);
        Progress = 1f;

        GameManager.System.PlayerActor.Init();
        if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
            mainView.SendSubtitles();
        GameManager.System.AddValueTrackAction(ValueTrackEvent);
    }

    public void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        switch (valueEnum)
        {
            case ValueTrackEnum.CrossHead:
                if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
                    mainView.TurnCrossHead(StaticValues.CrossHead);
                break;
            default:
                break;
        }
    }
}
