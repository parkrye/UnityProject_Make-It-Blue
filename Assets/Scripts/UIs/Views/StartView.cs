using Cysharp.Threading.Tasks;
using UnityEngine;

public class StartView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();
        if (GetButton("StartButton", out var startButton))
            startButton.onClick.AddListener(() => GameManager.Scene.LoadScene("MainScene", 1));

        if (GetButton("LoadButton", out var loadButton))
            loadButton.onClick.AddListener(() => GameManager.Scene.LoadScene("MainScene", 1));

        if (GetButton("OptionButton", out var optionButton))
            optionButton.onClick.AddListener(() => GameManager.Scene.CurScene.OpenDialog("OptionDialog"));

        if (GetButton("QuitButton", out var quitButton))
            quitButton.onClick.AddListener(() => Application.Quit());
    }
}
