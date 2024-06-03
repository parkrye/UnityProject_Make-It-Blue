using Cysharp.Threading.Tasks;

public class BattleResultView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();

        if (GetButton("EnterButton", out var eButton))
        {
            eButton.OnClick.AddListener(() =>
            {
                GameManager.UI.CloseCurrentView();
                GameManager.Scene.LoadScene(GameManager.Data.Play.LastEnteredScene);
            });
        }
    }

    public void Setting(bool isWin, int timer)
    {
        if (GetText("ResultText", out var rText))
            rText.text = isWin ? "Victory!" : "Lose...";

        if (GetText("TimeText", out var tText))
        {
            var mins = timer / 60;
            var secs = timer % 60;

            tText.text = $"{mins}:{secs}";
        }
    }
}
