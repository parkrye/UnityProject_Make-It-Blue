using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : BaseUI
{
    public override async UniTask OnInit()
    {
        await base.OnInit();
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