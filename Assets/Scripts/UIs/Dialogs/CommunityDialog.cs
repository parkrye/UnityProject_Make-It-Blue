using Cysharp.Threading.Tasks;
using UnityEngine;

public class CommunityDialog : Dialog
{
    private CommunityData _communityData;

    public override UniTask OnInit()
    {
        if (GetButton("StartButton", out var sButton))
        {
            sButton.InitButton(isClick: true);
            sButton.OnClick.AddListener(() => GameManager.UI.CloseCurrentDialog());
        }

        if (GetButton("CancelButton", out var cButton))
        {
            cButton.InitButton(isClick: true);
            cButton.OnClick.AddListener(() => GameManager.UI.CloseCurrentDialog());
        }
        return base.OnInit();
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

        if (GetText("NameText", out var nText))
        {
            nText.text = $"이름 : {_communityData.Actor.Name[0]} {_communityData.Actor.Name[1]}";
        }

        if (GetText("FavorText", out var fText))
        {
            fText.text = $"호감도 : {_communityData.Favor}";
        }

        if (GetText("DescriptionText", out var dText))
        {
            dText.text = $"설명 : {_communityData.Actor.Description}";
        }
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
