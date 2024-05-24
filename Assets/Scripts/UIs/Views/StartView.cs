using Cysharp.Threading.Tasks;

public class StartView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();
        if (GetButton("Button", out var button))
        {
            button.InitButton(isClick: true);
            //button.OnClick.AddListener(() => GameManager.Scene.LoadScene("TestScene"));
            button.OnClick.AddListener(() => GameManager.Scene.LoadScene("OpeningScene"));
        }
    }
}
