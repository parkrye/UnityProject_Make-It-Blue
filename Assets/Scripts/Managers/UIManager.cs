using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : BaseManager
{
    private Canvas _canvas;
    private Transform _viewRoot, _dialogRoot, _notificationRoot;

    private View _currentView;
    private Stack<Dialog> _dialogStack = new Stack<Dialog>();
    private Dictionary<string, Notification> _notifications = new Dictionary<string, Notification>();

    public override void InitManager()
    {
        base.InitManager();

        _canvas = GameManager.Resource.Instantiate<Canvas>("UIs/BaseCanvas");
        _canvas.gameObject.transform.SetParent(transform, false);
        _viewRoot = _canvas.transform.GetChild(0);
        _dialogRoot = _canvas.transform.GetChild(1);
        _notificationRoot = _canvas.transform.GetChild(2);
        GameManager.Resource.Instantiate<EventSystem>("UIs/EventSystem", _canvas.transform);
    }

    public View GetCurrentView()
    {
        return _currentView;
    }

    public Dialog GetCurrentDialog()
    {
        _dialogStack.TryPeek(out var current);
        return current;
    }

    public bool OpenView<T>(string viewName, out T result) where T : View
    {
        result = GameManager.Resource.Instantiate<T>($"UIs/Views/{viewName}");
        if (result == null)
            return false;

        CloseCurrentView();

        result.gameObject.transform.SetParent(_viewRoot, false);
        result.gameObject.SetActive(true);
        result.OnOpen();
        _currentView = result;
        return true;
    }

    public void CloseCurrentView()
    {
        if (_currentView == null)
            return;

        _currentView.OnClose();
        _currentView.gameObject.SetActive(false);
        _currentView = null;
    }

    public bool OpenDialog<T>(string dialogName, out T result) where T : Dialog
    {
        result = GameManager.Resource.Instantiate<T>($"UIs/Dialogs/{dialogName}");
        if (result == null)
            return false;

        if (_dialogStack.Count > 0 && _dialogStack.Contains(result))
        {
            var peek = _dialogStack.Peek();
            if (peek.Equals(result))
            {
                _dialogStack.Pop();
                peek.OnClose();
                peek.gameObject.SetActive(false);
                return true;
            }
        }

        _dialogStack.Push(result);
        result.gameObject.transform.SetParent(_dialogRoot, false);
        result.gameObject.SetActive(true);
        result.OnOpen();
        return true;
    }

    public void CloseCurrentDialog()
    {
        if (_dialogStack.Count == 0)
            return;
        var pop = _dialogStack.Pop();
        pop.OnClose();
        pop.gameObject.SetActive(false);

        if (_dialogStack.Count == 0)
            return;
        var peek = _dialogStack.Peek();
        peek.gameObject.SetActive(true);
        peek.OnOpen();
    }

    public void CloseAllDialog()
    {
        while (_dialogStack.Count > 0)
        {
            var top = _dialogStack.Pop();
            top.OnClose();
            top.gameObject.SetActive(false);
        }
    }

    public bool OpenNotification<T>(string notificationName, out T result) where T : Notification
    {
        if (_notifications.ContainsKey(notificationName))
        {
            result = _notifications[notificationName] as T;
            result.gameObject.SetActive(true);
            result.OnOpen();
            return true;
        }

        result = GameManager.Resource.Instantiate<T>($"UIs/Notifications/{notificationName}");
        if (result == null)
            return false;

        _notifications[notificationName] = result;
        result.gameObject.transform.SetParent(_notificationRoot, false);
        result.gameObject.SetActive(true);
        result.OnOpen();
        return true;
    }

    public void CloseNotification<T>(string notificationName) where T : Notification
    {
        if (_notifications.ContainsKey(notificationName) == false)
            return;

        var result = _notifications[notificationName];
        result.gameObject.SetActive(false);
        result.OnClose();
    }

    public void ResetUI()
    {
        CloseCurrentView();
        CloseAllDialog();
    }
}