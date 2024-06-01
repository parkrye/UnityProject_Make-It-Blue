using Cysharp.Threading.Tasks;

public class OpeningScene : WorldScene
{
    public override void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        base.ValueTrackEvent(valueEnum);
        switch (valueEnum)
        {
            default:
                break;
        }
    }

    protected override void InitActors()
    {
        base.InitActors();


    }

    protected override void InitScene()
    {
        if (GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out var mainView))
        {
            mainView.SendSubtitles();
        }
    }

    protected override void OnEventAction(int id, int index)
    {
        switch (id)
        {
            default:
            case 0:
                switch (index)
                {
                    default:
                    case 0:
                        if (GameManager.UI.OpenDialog<PlayerMakingDialog>("PlayerMakingDialog", out var pmDialog))
                        {
                            pmDialog.OnCloseEvent.AddListener(() => _eventActorArray[id].Interact());
                        }
                        break;
                    case 1:
                        GameManager.Data.Play.Level = 1;
                        GameManager.Scene.LoadScene("02_MainScene");
                        break;
                }
                break;
        }
    }
}
