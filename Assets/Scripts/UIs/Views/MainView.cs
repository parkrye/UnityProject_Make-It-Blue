using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class MainView : View
{
    public UnityEvent OnTouchChatEvent = new UnityEvent();

    public override async UniTask OnInit()
    {
        await base.OnInit();
    }

    public override void OnOpen()
    {
        base.OnOpen();

        GameManager.System.PlayerActor.HPRatioEvent.AddListener(ModifyHP);
        GameManager.System.PlayerActor.SPRatioEvent.AddListener(ModifySP);

        if (GetButton("MainButton", out var mButton))
        {
            mButton.InitButton(isClick: true, isDrag: false);
            mButton.OnClick.RemoveAllListeners();
            mButton.OnClick.AddListener(GameManager.System.PlayerActor.OnLoopActionStart);
            mButton.OnClickEnd.RemoveAllListeners();
            mButton.OnClickEnd.AddListener(GameManager.System.PlayerActor.OnMainAction);
            mButton.OnClickEnd.AddListener(GameManager.System.PlayerActor.OnLoopActionEnd);
        }

        if (GetButton("SubButton", out var sButton))
        {
            sButton.InitButton(isClick: true, isDrag: true);
            sButton.OnClickEnd.RemoveAllListeners();
            sButton.OnClickEnd.AddListener(GameManager.System.PlayerActor.OnSubAction);
            sButton.OnDrag.RemoveAllListeners();
            sButton.OnDrag.AddListener(GameManager.System.PlayerActor.OnDragSubAction);
        }

        if (GetButton("OptionButton", out var oButton))
        {
            oButton.InitButton(isClick: true);
        }

        if (GetButton("SubtitleBG", out var subtitle))
        {
            subtitle.InitButton(isClick: true);
            subtitle.OnClick.RemoveAllListeners();
            subtitle.OnClick.AddListener(() => OnTouchChatEvent?.Invoke());
        }
    }

    public void SetFaceImage(CharacterEnum character)
    {
        if (GetImage("FaceImage", out var fImage))
        {
            
        }
    }

    public void SendSubtitles(CharacterEnum talker = CharacterEnum.None, string content = "")
    {
        if (GetButton("SubtitleBG", out var subtitle) == false)
            return;

        if (content.Equals(string.Empty))
        {
            subtitle.gameObject.SetActive(false);
            return;
        }

        subtitle.gameObject.SetActive(true);

        if (GetImage("Talker", out var talkerImage))
        {
            talkerImage.enabled = talker != CharacterEnum.None;
            if (GameManager.Data.Actors.TryGetValue(talker, out var actor))
                talkerImage.sprite = actor.Sprite;
        }

        if (GetText("Content", out var contentText))
        {
            contentText.DOText(StaticValues.GetContext(content), StaticValues.TextDuration);
        }
    }

    public void ModifyHP(float value)
    {
        if(value < 0f)
        {
            if (GetImage("HP", out var hpImage))
            {
                hpImage.gameObject.SetActive(false);
            }
            return;
        }

        if (GetImage("HP", out var hp))
        {
            hp.fillAmount = value;
        }
    }

    public void ModifySP(float value)
    {
        if (value < 0f)
        {
            if (GetImage("SP", out var spImage))
            {
                spImage.gameObject.SetActive(false);
            }
            return;
        }

        if (GetImage("SP", out var sp))
        {
            sp.fillAmount = value;
        }
    }

    public void ModifyBullets(int max, int count)
    {
        if (GetImage("Bullets", out var bt))
        {
            bt.fillAmount = count / max;
        }
    }

    public void TurnCrossHead(bool isOn)
    {
        if (GetImage("CrossHead", out var image))
        {
            image.gameObject.SetActive(isOn);
        }
    }
}
