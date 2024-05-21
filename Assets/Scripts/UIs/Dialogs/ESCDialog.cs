using Cysharp.Threading.Tasks;
using UnityEngine;

public class ESCDialog : Dialog
{
    public override async UniTask OnInit()
    {
        _isChangeControl = true;

        await base.OnInit();
        if (GetButton("ResumeButton", out var resumeButton))
            resumeButton.onClick.AddListener(() => GameManager.UI.CloseCurrentDialog());

        if (GetButton("OptionButton", out var optionButton))
            optionButton.onClick.AddListener(() => GameManager.UI.OpenView<OptionView>("OptionView", out _));

        if (GetButton("QuitButton", out var quitButton))
            quitButton.onClick.AddListener(() => Application.Quit());
    }

    public override void OnOpenDialog()
    {
        base.OnOpenDialog();
    }

    public override void OnCloseDialog()
    {
        base.OnCloseDialog();
    }
}
