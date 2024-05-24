using Cysharp.Threading.Tasks;

public class StartView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();

        if (GetButton("Button", out var button))
        {
            button.InitButton(isClick: true);
            if (GameManager.Data.Play.Level == 0)
                button.OnClick.AddListener(() => GameManager.Scene.LoadScene("OpeningScene"));
            else
                button.OnClick.AddListener(() => GameManager.Scene.LoadScene("TestScene"));
        }
    }
}
