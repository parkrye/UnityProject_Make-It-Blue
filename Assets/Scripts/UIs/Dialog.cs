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

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}