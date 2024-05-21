using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : BaseManager
{
    private Canvas _canvas;
    private Transform _viewRoot, _dialogRoot;

    private View _currentView;
    private Stack<View> _viewgStack = new Stack<View>();
    private Stack<Dialog> _dialogStack = new Stack<Dialog>();

    public override void InitManager()
    {
        base.InitManager();

        _canvas = GameManager.Resource.Instantiate<Canvas>("UIs/BaseCanvas");
        _canvas.gameObject.transform.SetParent(transform, false);
        _viewRoot = _canvas.transform.GetChild(0);
        _dialogRoot = _canvas.transform.GetChild(1);
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
        if (_viewgStack.Count > 0 && _viewgStack.Contains(result))
        {
            var peek = _viewgStack.Peek();
            if (peek.Equals(result))
            {
                _viewgStack.Pop();
                peek.OnCloseView();
                peek.gameObject.SetActive(false);
                return true;
            }
        }
        else
        {

        }

        _viewgStack.Push(result);
        result.gameObject.transform.SetParent(_viewRoot, false);
        result.gameObject.SetActive(true);
        result.OnOpenView();
        return true;
    }

    public void CloseCurrentView()
    {
        if (_currentView == null)
            return;

        _currentView.OnCloseView();
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
                peek.OnCloseDialog();
                peek.gameObject.SetActive(false);
                return true;
            }
        }

        _dialogStack.Push(result);
        result.gameObject.transform.SetParent(_dialogRoot, false);
        result.gameObject.SetActive(true);
        result.OnOpenDialog();
        return true;
    }

    public void CloseCurrentDialog()
    {
        if (_dialogStack.Count == 0)
            return;
        var pop = _dialogStack.Pop();
        pop.OnCloseDialog();
        pop.gameObject.SetActive(false);

        if (_dialogStack.Count == 0)
            return;
        var peek = _dialogStack.Peek();
        peek.gameObject.SetActive(true);
        peek.OnOpenDialog();
    }

    public void CloseAllDialog()
    {
        while (_dialogStack.Count > 0)
        {
            var top = _dialogStack.Pop();
            top.OnCloseDialog();
            top.gameObject.SetActive(false);
        }
    }

    public void ResetUI()
    {
        CloseCurrentView();
        CloseAllDialog();
    }
}