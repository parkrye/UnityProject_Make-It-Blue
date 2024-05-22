using UnityEngine;

[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public abstract class BaseInteractableActor : BaseActor, IInteractable
{
    public NormalAnimationController Animator { get; private set; }
    public EventData Event;

    protected MainView _view;
    private int _eventIndex;
    private int _contextIndex;

    private void Start()
    {
        Animator = GetComponent<NormalAnimationController>();
        if (Animator == null)
            Debug.Log($"{name} lost NormalAnimationController!");
    }

    public bool Interact()
    {
        if (_view == null)
        {
            _view = GameManager.UI.GetCurrentView() as MainView;
            if (_view == null)
                Debug.Log($"{name} need MainView!");
        }

        transform.LookAt(GameManager.System.PlayerActor.transform);
        transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;

        _view.OnTouchChatEvent.RemoveAllListeners();
        _view.OnTouchChatEvent.AddListener(ShowNextContextAction);

        ShowNextContextAction();

        return true;
    }

    private void ShowNextContextAction()
    {
        if (_contextIndex >= Event.ContextArray[_eventIndex].TalkArray.Length)
        {
            _eventIndex++;
            _contextIndex = 0;
            _view.SendSubtitles();

            if (_eventIndex >= Event.ContextArray.Length)
                EndOfInteract();
            return;
        }

        _view.SendSubtitles(Event.ContextArray[_eventIndex].TalkerArray[_contextIndex], Event.ContextArray[_eventIndex].TalkArray[_contextIndex]);
        _contextIndex++;
    }

    public virtual void EndOfInteract()
    {
        _view.OnTouchChatEvent.RemoveAllListeners();

        _eventIndex = 0;
        _contextIndex = 0;
    }
}
