using Cysharp.Threading.Tasks;
using UnityEngine;

public class CommunityView : View
{
    private CommunityData _communityData;

    public override async UniTask OnInit()
    {
        await base.OnInit();

        if (GetButton("StartButton", out var sButton))
        {
            sButton.InitButton(isClick: true);
            sButton.OnClick.AddListener(() => GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out _));
        }

        if (GetButton("CancelButton", out var cButton))
        {
            cButton.InitButton(isClick: true);
            cButton.OnClick.AddListener(() => GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out _));
        }
    }

    public void InitCommunityTarget(CommunityData communityData)
    {
        _communityData = communityData;
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public void OpenWithTarget(CharacterEnum target)
    {
        base.OnOpen();

        _communityData = GameManager.Resource.Load(target);
        if (_communityData == null)
        {
            Debug.Log("Community Data is Null!");
            return;
        }

        if (GetImage("Image", out var image))
        {
            image.sprite = _communityData.Actor.Sprite;
        }

        var texts = TextTransfer.GetCommunityText(_communityData);

        if (GetText("NameText", out var nText))
        {
            nText.text = texts[0];
        }

        if (GetText("FavorText", out var fText))
        {
            fText.text = texts[1];
        }

        if (GetText("DescriptionText", out var dText))
        {
            dText.text = texts[2];
        }
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
