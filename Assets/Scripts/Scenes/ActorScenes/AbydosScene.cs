using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbydosScene : WorldScene
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
        _actors = FindObjectsOfType<BaseActor>();
        foreach (var actor in _actors)
        {
            actor.InitForWorld();
        }
    }

    protected override void InitScene()
    {
        if (GameManager.UI.OpenView<MainView>("MainView", out var mainView))
        {
            mainView.SendSubtitles();
        }
    }
}
