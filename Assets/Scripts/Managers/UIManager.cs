using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : BaseManager
{
    private Canvas _canvas;
    private Transform _viewRoot, _dialogRoot, _notificationRoot;

    private View _currentView;
    private Stack<Dialog> _dialogStack = new Stack<Dialog>();

    private Dictionary<string, View> _views = new Dictionary<string, View>();
    private Dictionary<string, Dialog> _dialogs = new Dictionary<string, Dialog>();
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
        if (_views.ContainsKey(viewName))
        {
            result = _views[viewName] as T;
        }
        else
        {
            result = GameManager.Resource.Instantiate<T>($"UIs/Views/{viewName}");
            _views.Add(viewName, result);
        }
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
        if (_dialogs.ContainsKey(dialogName))
        {
            result = _dialogs[dialogName] as T;
        }
        else
        {
            result = GameManager.Resource.Instantiate<T>($"UIs/Dialogs/{dialogName}");
            _dialogs.Add(dialogName, result);
        }
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
        }
        else
        {
            result = GameManager.Resource.Instantiate<T>($"UIs/Notifications/{notificationName}");
            _notifications.Add(notificationName, result);
        }
        if (result == null)
            return false;

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

    public bool OpenUI<T>(PublicUIEnum uiEnum, out T ui) where T : BaseUI
    {
        ui = null;
        switch (uiEnum)
        {
            default:
                return false;
            case PublicUIEnum.Main:
                if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
                    ui = mainView as T;
                return true;
            case PublicUIEnum.Option:
                break;
            case PublicUIEnum.BattleSetting:
                if (OpenDialog<BattleSettingDialog>("BattleSettingDialog", out var bsDialog))
                    ui = bsDialog as T;
                return true;
            case PublicUIEnum.Community:
                if (OpenView<CommunityView>("CommunityView", out var cView))
                    ui = cView as T;
                return true;
            case PublicUIEnum.Shop:
                return true;
            case PublicUIEnum.BattleResult:
                if (OpenView<BattleResultView>("BattleResultView", out var brVeiw))
                    ui = brVeiw as T;
                return true;
        }
        return false;
    }
}