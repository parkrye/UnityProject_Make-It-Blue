using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldScene : ActorScene, IValueTrackable
{
    [SerializeField] protected Dictionary<int, BaseEventActor> _eventActorArray = new Dictionary<int, BaseEventActor>();
    
    protected override async UniTask LoadingRoutine()
    {
        GameManager.System.AddValueTrackAction(ValueTrackEvent);
        await UniTask.Delay(100);

        Progress = 1f;
    }

    public virtual void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        switch (valueEnum)
        {
            default:
                break;
            case ValueTrackEnum.CrossHead:
                if (GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out var mainView))
                    mainView.TurnCrossHead(StaticValues.CrossHead);
                break;
        }
    }

    protected override void InitActors()
    {
        _actors = FindObjectsOfType<BaseActor>();
        foreach (var actor in _actors)
        {
            actor.InitForWorld();
            if (actor.TryGetComponent<BaseEventActor>(out var eventActor))
            {
                if (_eventActorArray.ContainsKey(eventActor.SceneEventID))
                {
                    Debug.Log($"Scene Event ID {eventActor.SceneEventID} Already Contains!");
                    continue;
                }

                _eventActorArray.Add(eventActor.SceneEventID, eventActor);
                eventActor.EndOfContextEvent.AddListener((t) => OnEventAction(eventActor.SceneEventID, t));
            }
        }
    }

    protected virtual void OnEventAction(int id, int index)
    {

    }
}