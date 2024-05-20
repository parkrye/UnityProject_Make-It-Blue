using Cysharp.Threading.Tasks;

public class View : BaseUI
{
    public override async UniTask OnInit()
    {
        await base.OnInit();
    }

    public virtual void OnOpenView()
    {

    }

    public virtual void OnCloseView()
    {

    }
}