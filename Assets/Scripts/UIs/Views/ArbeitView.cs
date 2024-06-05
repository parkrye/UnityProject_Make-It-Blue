using Cysharp.Threading.Tasks;

public class ArbeitView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();

        if (GetButton("Button", out var button))
        {
            button.InitButton();
            button.OnClickEnd.AddListener(() => GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out _));
        }
    }
}
