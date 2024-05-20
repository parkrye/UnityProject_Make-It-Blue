using Cysharp.Threading.Tasks;

public class MainView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();
        GameManager.Scene.CurScene.Player.HPRatioEvent.AddListener(ModifyHP);
        GameManager.Scene.CurScene.Player.SPRatioEvent.AddListener(ModifySP);
    }

    public void SendSubtitles(string sender = "", string content = "")
    {
        GetContent("Subtitle", out var subtitle);

        if (sender.Equals(string.Empty) && content.Equals(string.Empty))
        {
            subtitle?.gameObject.SetActive(false);
            return;
        }

        subtitle?.gameObject.SetActive(true);

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

    public void ModifyItem(int itemId, int count)
    {

    }

    public void TurnCrossHead(bool isOn)
    {
        if (GetImage("CrossHead", out var image))
        {
            image.gameObject.SetActive(isOn);
        }
    }
}
