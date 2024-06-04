using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEventActor : BaseInteractableActor
{
    [SerializeField] public int SceneEventID;

    public ActorAnimationController AnimController { get; private set; }
    public EventData Event;
    public UnityEvent<int> EndOfContextEvent = new UnityEvent<int>();

    protected MainView _view;
    private int _eventIndex;
    private int _contextIndex;

    private void Awake()
    {
        AnimController = GetComponent<ActorAnimationController>();
        if (AnimController == null)
            Debug.Log($"{name} lost AnimController!");
    }

    public override void InitForWorld()
    {
        base.InitForWorld();

        _view = GameManager.UI.GetCurrentView() as MainView;
        if (_view == null)
            Debug.Log($"{name} need MainView!");
    }

    public override void InitForBattle()
    {
        base.InitForBattle();

        Destroy(gameObject);
    }

    public override bool Interact()
    {
        OnInteract();
        transform.LookAt(GameManager.System.PlayerActor.transform);
        GameManager.System.PlayerActor.Model.LookAt(transform);
        GameManager.System.PlayerActor.Model.localEulerAngles = new Vector3(0f, GameManager.System.PlayerActor.Model.localEulerAngles.y, 0f);
        transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;

        _view.OnTouchChatEvent?.RemoveAllListeners();
        _view.OnTouchChatEvent?.AddListener(ShowNextContextAction);

        ShowNextContextAction();

        return base.Interact();
    }

    private void ShowNextContextAction()
    {
        if (_contextIndex >= Event.ContextArray[_eventIndex].TalkArray.Length)
        {
            EndOfContextEvent?.Invoke(_eventIndex);

            _eventIndex++;
            _contextIndex = 0;
            _view.SendSubtitles();
            GameManager.System.PlayerActor.Model.localEulerAngles = Vector3.zero;

            if (_eventIndex >= Event.ContextArray.Length)
                ResetInteract();
            return;
        }

        _view.SendSubtitles(Event.ContextArray[_eventIndex].TalkerArray[_contextIndex], Event.ContextArray[_eventIndex].TalkArray[_contextIndex]);
        _contextIndex++;
    }

    public virtual void ResetInteract()
    {
        _view.OnTouchChatEvent.RemoveAllListeners();

        _eventIndex = 0;
        _contextIndex = 0;
    }

    protected virtual void OnInteract()
    {

    }
}
