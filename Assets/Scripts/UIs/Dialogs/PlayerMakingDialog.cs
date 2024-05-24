using Cysharp.Threading.Tasks;
using System.Xml.Linq;

public class PlayerMakingDialog : Dialog
{
    private int _statusPoint;

    private string[] _names = new string[2];
    private int[] _status = new int[4] { 1, 1, 1, 1 };
    private int _weapon = 0;

    public override async UniTask OnInit()
    {
        await base.OnInit();

        _statusPoint = 8;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        GameManager.System.PlayerActor.IsControllable = false;

        if (GetButton("NameButton", out var nButton))
        {
            nButton.InitButton(isClick:true);
            nButton.OnClickEnd.AddListener(OnEnterNameButton);
        }

        if (GetButton("StatusButton", out var sButton))
        {
            sButton.InitButton(isClick: true);
            sButton.OnClickEnd.AddListener(OnEnterStatusButton);
        }

        if (GetButton("StrengthUpButton", out var suButton))
        {
            suButton.InitButton(isClick: true);
            suButton.OnClickEnd.AddListener(() => OnStatusModifyButton(0, 1));
        }
        if (GetButton("StrengthDownButton", out var sdButton))
        {
            sdButton.InitButton(isClick: true);
            sdButton.OnClickEnd.AddListener(() => OnStatusModifyButton(0, -1));
        }
        if (GetButton("DexterityUpButton", out var duButton))
        {
            duButton.InitButton(isClick: true);
            duButton.OnClickEnd.AddListener(() => OnStatusModifyButton(1, 1));
        }
        if (GetButton("DexterityDownButton", out var ddButton))
        {
            ddButton.InitButton(isClick: true);
            ddButton.OnClickEnd.AddListener(() => OnStatusModifyButton(1, -1));
        }
        if (GetButton("PowerUpButton", out var puButton))
        {
            puButton.InitButton(isClick: true);
            puButton.OnClickEnd.AddListener(() => OnStatusModifyButton(2, 1));
        }
        if (GetButton("PowerDownButton", out var pdButton))
        {
            pdButton.InitButton(isClick: true);
            pdButton.OnClickEnd.AddListener(() => OnStatusModifyButton(2, -1));
        }
        if (GetButton("AgilityUpButton", out var auButton))
        {
            auButton.InitButton(isClick: true);
            auButton.OnClickEnd.AddListener(() => OnStatusModifyButton(3, 1));
        }
        if (GetButton("AgilityDownButton", out var adButton))
        {
            adButton.InitButton(isClick: true);
            adButton.OnClickEnd.AddListener(() => OnStatusModifyButton(3, -1));
        }

        if (GetButton("WeaponButton", out var wButton))
        {
            wButton.InitButton(isClick: true);
            wButton.OnClickEnd.AddListener(OnEnterWeaponButton);
        }

        ShowSection(0);
    }

    public override void OnClose()
    {
        base.OnClose();

        GameManager.System.PlayerActor.IsControllable = true;
    }

    private void OnEnterNameButton()
    {
        if (GetInputField("Name1InputField", out var name1))
            _names[0] = name1.text;

        if (GetInputField("Name2InputField", out var name2))
            _names[1] = name2.text;

        ShowSection(1);
    }

    private void OnStatusModifyButton(int index, int value)
    {
        if (value < 0)
        {
            if (_status[index] == 1)
                return;

            _statusPoint++;
            _status[index]--;
        }
        else if (_statusPoint > 0)
        {
            _statusPoint--;
            _status[index]++;
        }

        if (index == 0)
        {
            if (GetText("StrengthText", out var sText))
                sText.text = _status[index].ToString();
        }
        else if (index == 1)
        {
            if (GetText("DexterityText", out var dText))
                dText.text = _status[index].ToString();
        }
        else if (index == 2)
        {
            if (GetText("PowerText", out var pText))
                pText.text = _status[index].ToString();
        }
        else
        {
            if (GetText("AgilityText", out var aText))
                aText.text = _status[index].ToString();
        }

        if (GetText("RemainPointText", out var rpText))
            rpText.text = $"ÀÜ¿© : {_statusPoint}";
    }

    private void OnEnterStatusButton()
    {
        if (_statusPoint == 0)
            ShowSection(2);
    }

    private void OnEnterWeaponButton()
    {
        if (GetDropDown("WeaponDropdown", out var wDd))
            _weapon = wDd.value;

        OnMakingEnd();
    }

    private void OnMakingEnd()
    {
        if (_names[0].Length > 0)
            GameManager.Data.Play.Name[0] = _names[0];
        if (_names[1].Length > 0)
            GameManager.Data.Play.Name[1] = _names[1];

        GameManager.Data.Play.Status[0] = _status[0];
        GameManager.Data.Play.Status[1] = _status[1];
        GameManager.Data.Play.Status[2] = _status[2];
        GameManager.Data.Play.Status[3] = _status[3];

        switch (_weapon)
        {
            default:
            case 0:
                GameManager.Data.Play.ProductList.Add(
                    GameManager.Resource.Load<EquipmentData>("Datas/Products/Equipments/HG_Tier0"));
                break;
            case 1:
                GameManager.Data.Play.ProductList.Add(
                    GameManager.Resource.Load<EquipmentData>("Datas/Products/Equipments/AR_Tier0"));
                break;
            case 2:
                GameManager.Data.Play.ProductList.Add(
                    GameManager.Resource.Load<EquipmentData>("Datas/Products/Equipments/SG_Tier0"));
                break;
            case 3:
                GameManager.Data.Play.ProductList.Add(
                    GameManager.Resource.Load<EquipmentData>("Datas/Products/Equipments/MG_Tier0"));
                break;
        }

        GameManager.UI.CloseCurrentDialog();
    }

    private void ShowSection(int index)
    {
        if (GetContent("NameSection", out var nSection))
            nSection.gameObject.SetActive(index == 0);

        if (GetContent("StatusSection", out var sSection))
            sSection.gameObject.SetActive(index == 1);

        if (GetContent("WeaponSection", out var wSection))
            wSection.gameObject.SetActive(index == 2);
    }
}
