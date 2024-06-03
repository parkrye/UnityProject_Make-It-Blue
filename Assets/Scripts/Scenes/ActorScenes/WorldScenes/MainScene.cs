public class MainScene : WorldScene
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
        base.InitScene();
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
                        if (GameManager.UI.OpenUI<BattleSettingView>(PublicUIEnum.BattleSetting, out var bsDialog))
                        {
                            bsDialog.OnCloseEvent.AddListener(() => _eventActorArray[id].Interact());
                        }
                        break;
                    case 1:
                        GameManager.Scene.LoadScene("03_BattleScene");
                        break;
                }
                break;
            case 1:
                switch (index)
                {
                    default:
                    case 0:
                        if (GameManager.UI.OpenUI<CommunityView>(PublicUIEnum.Community, out var cDialog))
                        {
                            cDialog.OpenWithTarget(_eventActorArray[id].Event.Actor.Character);
                            cDialog.OnCloseEvent.AddListener(() => _eventActorArray[id].Interact());
                        }
                        break;
                    case 1:
                        break;
                }
                break;
        }
    }
}
