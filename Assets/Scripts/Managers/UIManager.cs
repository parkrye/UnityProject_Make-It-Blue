using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager
{
    private Canvas _rootCanvas;

    private View _currentView;
    private Stack<Dialog> _dialogStack = new Stack<Dialog>();

    public override void InitManager()
    {
        base.InitManager();


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

    public void OpenView(View view)
    {
        CloseCurrentView();

        _currentView = view;
        _currentView.gameObject.SetActive(true);
        _currentView.OnOpenView();
    }

    public void CloseCurrentView()
    {
        if (_currentView == null)
            return;

        _currentView.OnCloseView();
        _currentView.gameObject.SetActive(false);
        _currentView = null;
    }

    public void OpenDialog(Dialog dialog)
    {
        if (_dialogStack.Count > 0)
        {
            var peek = _dialogStack.Peek();
            if (_dialogStack.Contains(dialog) && peek.Equals(dialog) == false)
                return;

            peek.OnCloseDialog();
            peek.gameObject.SetActive(false);
            if (peek.Equals(dialog))
            {
                _dialogStack.Pop();
                return;
            }
        }

        _dialogStack.Push(dialog);
        dialog.gameObject.SetActive(true);
        dialog.OnOpenDialog();
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