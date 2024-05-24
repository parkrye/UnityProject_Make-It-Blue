using Cysharp.Threading.Tasks;

public class PlayerMakingDialog : Dialog
{
    private int _statusPoint;
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
            nButton.OnClickEnd.AddListener(OnEnterNameButton);

        if (GetButton("StatusButton", out var sButton))
            sButton.OnClickEnd.AddListener(OnEnterStatusButton);

        if (GetButton("StrengthUpButton", out var suButton))
            suButton.OnClickEnd.AddListener(() => OnStatusModifyButton(0, 1));
        if (GetButton("StrengthDownButton", out var sdButton))
            suButton.OnClickEnd.AddListener(() => OnStatusModifyButton(0, -1));
        if (GetButton("DexterityUpButton", out var duButton))
            duButton.OnClickEnd.AddListener(() => OnStatusModifyButton(1, 1));
        if (GetButton("DexterityDownButton", out var ddButton))
            ddButton.OnClickEnd.AddListener(() => OnStatusModifyButton(1, -1));
        if (GetButton("PowerUpButton", out var puButton))
            puButton.OnClickEnd.AddListener(() => OnStatusModifyButton(2, 1));
        if (GetButton("PowerDownButton", out var pdButton))
            pdButton.OnClickEnd.AddListener(() => OnStatusModifyButton(2, -1));
        if (GetButton("AgilityUpButton", out var auButton))
            auButton.OnClickEnd.AddListener(() => OnStatusModifyButton(3, 1));
        if (GetButton("AgilityDownButton", out var adButton))
            adButton.OnClickEnd.AddListener(() => OnStatusModifyButton(3, -1));

        if (GetButton("WeaponButton", out var wButton))
            wButton.OnClickEnd.AddListener(OnEnterWeaponButton);

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
            GameManager.Data.Play.Name[0] = name1.text;

        if (GetInputField("Name2InputField", out var name2))
            GameManager.Data.Play.Name[1] = name2.text;

        ShowSection(1);
    }

    private void OnStatusModifyButton(int index, int value)
    {
        if (value < 0)
        {
            if (GameManager.Data.Play.Status[index] == 1)
                return;

            _statusPoint++;
            GameManager.Data.Play.Status[index]--;
        }
        else if (_statusPoint > 0)
        {
            _statusPoint--;
            GameManager.Data.Play.Status[index]++;
        }

        if (index == 0)
        {
            if (GetText("StrengthText", out var sText))
                sText.text = GameManager.Data.Play.Status[index].ToString();
        }
        else if (index == 1)
        {
            if (GetText("DexterityText", out var dText))
                dText.text = GameManager.Data.Play.Status[index].ToString();
        }
        else if (index == 2)
        {
            if (GetText("PowerText", out var pText))
                pText.text = GameManager.Data.Play.Status[index].ToString();
        }
        else
        {
            if (GetText("AgilityText", out var aText))
                aText.text = GameManager.Data.Play.Status[index].ToString();
        }

        if (GetText("RemainPointText", out var rpText))
            rpText.text = $"¿‹ø© : {_statusPoint}";
    }

    private void OnEnterStatusButton()
    {
        ShowSection(2);
    }

    private void OnEnterWeaponButton()
    {
        int weaponIndex = 0;
        if (GetDropDown("WeaponDropdown", out var wDd))
            weaponIndex = wDd.value;

        switch (weaponIndex)
        {
            default:
            case 0:
                GameManager.Data.Play.ProductArray.Add(
                    GameManager.Resource.Load<EquipmentData>("Datas/Products/Equipments/HG_Tier0"));
                break;
            case 1:
                GameManager.Data.Play.ProductArray.Add(
                    GameManager.Resource.Load<EquipmentData>("Datas/Products/Equipments/AR_Tier0"));
                break;
            case 2:
                GameManager.Data.Play.ProductArray.Add(
                    GameManager.Resource.Load<EquipmentData>("Datas/Products/Equipments/SG_Tier0"));
                break;
            case 3:
                GameManager.Data.Play.ProductArray.Add(
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
