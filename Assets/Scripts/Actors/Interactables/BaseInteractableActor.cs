using UnityEngine;

public abstract class BaseInteractableActor : BaseActor, IInteractable
{
    public Playable Character { get; private set; }
    public EventData Event { get; private set; }

    protected MainView view;
    private int _eventIndex;
    private int _contextIndex;

    private void Start()
    {
        view = GameManager.UI.GetCurrentView() as MainView;
        if (view == null)
            Debug.Log($"{name} need MainView!");
    }

    public bool Interact()
    {
        _eventIndex = 0;
        _contextIndex = 0;

        view.OnTouchChatEvent.RemoveAllListeners();
        view.OnTouchChatEvent.AddListener(ShowNextContextAction);

        return true;
    }

    private void ShowNextContextAction()
    {
        if (_eventIndex >= Event.ContextArray.Length)
        {
            EndOfInteract();
            return;
        }

        if (_contextIndex >= Event.ContextArray[_eventIndex].TalkArray.Length)
        {
            _eventIndex++;
            _contextIndex = 0;
            view.SendSubtitles();
            return;
        }

        view.SendSubtitles(Event.ContextArray[_eventIndex].TalkerArray[_contextIndex], Event.ContextArray[_eventIndex].TalkArray[_contextIndex]);
    }

    public virtual void EndOfInteract()
    {

    }
}
