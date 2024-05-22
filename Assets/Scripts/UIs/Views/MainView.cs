using Cysharp.Threading.Tasks;

public class MainView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();

        GameManager.System.PlayerActor.HPRatioEvent.AddListener(ModifyHP);
        GameManager.System.PlayerActor.SPRatioEvent.AddListener(ModifySP);

        if (GetButton("MainButton", out var mButton))
        {
            mButton.InitButton(isClick: true, isDrag: false);
            mButton.OnClickEnd.AddListener(GameManager.System.PlayerActor.OnMainAction);
            mButton.OnClick.AddListener(GameManager.System.PlayerActor.OnLoopActionStart);
            mButton.OnClickEnd.AddListener(GameManager.System.PlayerActor.OnLoopActionEnd);
        }

        if (GetButton("SubButton", out var sButton))
        {
            sButton.InitButton(isClick: true, isDrag: true);
            sButton.OnClickEnd.AddListener(GameManager.System.PlayerActor.OnSubAction);
            sButton.OnDrag.AddListener(GameManager.System.PlayerActor.OnDragSubAction);
        }

        if (GetButton("OptionButton", out var oButton))
        {
            oButton.InitButton(isClick: true);
        }
    }

    public void SetFaceImage(CharacterEnum character)
    {
        if (GetImage("FaceImage", out var fImage))
        {
            
        }
    }

    public void SendSubtitles(string sender = "", string content = "")
    {
        if (GetContent("SubtitleBG", out var subtitle) == false)
            return;

        if (sender.Equals(string.Empty) && content.Equals(string.Empty))
        {
            subtitle.gameObject.SetActive(false);
            return;
        }

        subtitle.gameObject.SetActive(true);

        if (GetText("Talker", out var talkerText))
        {
            talkerText.text = sender;
        }

        if (GetText("Content", out var contentText))
        {
            contentText.text = content;
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
