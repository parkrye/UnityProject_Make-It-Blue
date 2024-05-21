using UnityEngine;
using UnityEngine.Events;

public class SystemManager : BaseManager, IValueTrackable
{
    private static UnityEvent<ValueTrackEnum> _valueTrackEvent = new UnityEvent<ValueTrackEnum>();
    public PlayerActor PlayerActor { get; set; }

    public override void InitManager()
    {
        base.InitManager();

        _valueTrackEvent.AddListener(ValueTrackEvent);
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
