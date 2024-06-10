public class MainScene : WorldScene
{
    public override void ValueTrackAction(ValueTrackEnum valueEnum)
    {
        base.ValueTrackAction(valueEnum);
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
                        if (GameManager.UI.OpenUI<BattleSettingView>(PublicUIEnum.BattleSetting, out var bView))
                        {
                            bView.OnCloseEvent.AddListener(() => _eventActorArray[id].Interact());
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
                        if (GameManager.UI.OpenUI<CommunityView>(PublicUIEnum.Community, out var cView))
                        {
                            cView.OpenWithTarget(_eventActorArray[id].Event.Actor.Character);
                            cView.OnCloseEvent.AddListener(() => _eventActorArray[id].Interact());
                        }
                        break;
                    case 1:
                        break;
                }
                break;
            case 2:
                switch (index)
                {
                    default:
                    case 0:
                        if (GameManager.UI.OpenUI<ShopView>(PublicUIEnum.Shop, out var sView))
                        {

                        }
                        break;
                }
                break;
            case 3:
                switch (index)
                {
                    default:
                    case 0:
                        if (GameManager.UI.OpenUI<ArbeitView>(PublicUIEnum.Arbeit, out var aView))
                        {
                            aView.OnCloseEvent.AddListener(() => _eventActorArray[id].Interact());
                        }
                        break;
                    case 1:
                        break;
                }
                break;
            case 4:
                switch (index)
                {
                    default:
                    case 0:
                        if (GameManager.UI.OpenUI<FarmView>(PublicUIEnum.Farm, out var fView))
                        {
                            fView.OnCloseEvent.AddListener(() => _eventActorArray[id].Interact());
                        }
                        break;
                    case 1:
                        break;
                }
                break;
        }
    }
}
