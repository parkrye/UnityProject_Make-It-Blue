using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Events;

public class SystemManager : BaseManager, IValueTrackable
{
    public PlayerActor PlayerActor { get; set; }
    public CurrentMission CurrentMission { get; set; }

    private static UnityEvent<ValueTrackEnum> _valueTrackEvent = new UnityEvent<ValueTrackEnum>();

    public override void InitManager()
    {
        base.InitManager();

        _valueTrackEvent.AddListener(ValueTrackEvent);

        SessionTimer().Forget();
    }

    private async UniTask SessionTimer()
    {
        while (true)
        {
            _valueTrackEvent?.Invoke(ValueTrackEnum.Minitues);
            await UniTask.Delay(60000);
        }
    }

    public void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        switch (valueEnum)
        {
            case ValueTrackEnum.VSync:
                QualitySettings.vSyncCount = StaticValues.VSync ? 1 : 0;
                break;
            case ValueTrackEnum.GameFrame:
                Application.targetFrameRate = (int)(StaticValues.GameFrame + 1) * 30;
                break;
            case ValueTrackEnum.Minitues:
                if (GameManager.Data == null)
                    break;
                var now = DateTime.Now;
                var diff = (now - GameManager.Data.Play.LastConnectedTime);
                if (diff.Days > 0)
                {
                    GameManager.Data.Play.Energy += 10;
                    if (GameManager.Data.Play.Energy > 30)
                        GameManager.Data.Play.Energy = 30;
                }
                GameManager.Data.Play.Stamina += 1;
                if (GameManager.Data.Play.Stamina > 300)
                    GameManager.Data.Play.Stamina = 300;
                GameManager.Data.Play.LastConnectedTime = now;
                break;
            default:
                break;
        }
    }

    public void AddValueTrackAction(UnityAction<ValueTrackEnum> action)
    {
        _valueTrackEvent.AddListener(action);
    }

    public void RemoveValueTrackAction(UnityAction<ValueTrackEnum> action)
    {
        _valueTrackEvent.RemoveListener(action);
    }

    public void TriggerValueTrackEvent(ValueTrackEnum valueTrack)
    {
        _valueTrackEvent?.Invoke(valueTrack);
    }
}
