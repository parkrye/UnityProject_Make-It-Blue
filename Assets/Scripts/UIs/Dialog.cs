using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : BaseUI
{
    protected UnityEvent<bool> ControlEvent = new UnityEvent<bool>();
    protected bool _isChangeControl = false;

    public override async UniTask OnInit()
    {
        await base.OnInit();

        if (_isChangeControl && GameManager.System.PlayerActor != null)
        {
            ControlEvent.AddListener(control => { GameManager.System.PlayerActor.IsControllable = control; });
        }
    }

    public virtual void OnOpenDialog()
    {
        if (_isChangeControl && GameManager.System.PlayerActor != null)
        {
            ControlEvent?.Invoke(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public virtual void OnCloseDialog()
    {
        if (_isChangeControl && GameManager.System.PlayerActor != null)
        {
            ControlEvent?.Invoke(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}