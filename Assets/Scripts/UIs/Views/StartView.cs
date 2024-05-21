using Cysharp.Threading.Tasks;
using UnityEngine;

public class StartView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();
        if (GetButton("StartButton", out var startButton))
            startButton.OnClick.AddListener(() => GameManager.Scene.LoadScene("MainScene", 1));

        if (GetButton("LoadButton", out var loadButton))
            loadButton.OnClick.AddListener(() => GameManager.Scene.LoadScene("MainScene", 1));

        if (GetButton("OptionButton", out var optionButton))
            optionButton.OnClick.AddListener(() => GameManager.UI.OpenView<OptionView>("OptionView", out _));

        if (GetButton("QuitButton", out var quitButton))
            quitButton.OnClick.AddListener(() => Application.Quit());
    }
}
