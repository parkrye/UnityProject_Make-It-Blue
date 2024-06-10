using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerMakingView : View
{
    private int _sectionIndex;
    private int _statusPoint;
    private int _modelIndex;
    private int _haloShapeIndex;
    private int _haloColorIndex;

    private string[] _names = new string[2];
    private int[] _status = new int[4] { 1, 1, 1, 1 };
    private int _weapon = 0;

    public override async UniTask OnInit()
    {
        await base.OnInit();

        _sectionIndex = 0;
        _statusPoint = StaticValues.DefaultStartStatusPoint;
    }

    public override void OnOpen()
    {
        base.OnOpen();

        GameManager.System.PlayerActor.IsControllable = false;

        if (GetButton("NextButton", out var nButton))
        {
            nButton.InitButton(isClick:true);
            nButton.OnClickEnd.AddListener(() => OnSectionMoveButton(1));
        }

        if (GetButton("PrevButton", out var pButton))
        {
            pButton.InitButton(isClick:true);
            pButton.OnClickEnd.AddListener(() => OnSectionMoveButton(-1));
        }

        if (GetButton("ModelButton1", out var mButton1))
        {
            mButton1.InitButton(isClick: true);
            mButton1.OnClickEnd.AddListener(() => _modelIndex = 0);
        }

        if (GetButton("ModelButton2", out var mButton2))
        {
            mButton2.InitButton(isClick: true);
            mButton2.OnClickEnd.AddListener(() => _modelIndex = 1);
        }

        if (GetButton("ModelButton3", out var mButton3))
        {
            mButton3.InitButton(isClick: true);
            mButton3.OnClickEnd.AddListener(() => _modelIndex = 2);
        }

        if (GetButton("StrengthUpButton", out var suButton))
        {
            suButton.InitButton(isClick: true);
            suButton.OnClickEnd.AddListener(() => OnStatusModifyButtoClicked(0, 1));
        }
        if (GetButton("StrengthDownButton", out var sdButton))
        {
            sdButton.InitButton(isClick: true);
            sdButton.OnClickEnd.AddListener(() => OnStatusModifyButtoClicked(0, -1));
        }
        if (GetButton("DexterityUpButton", out var duButton))
        {
            duButton.InitButton(isClick: true);
            duButton.OnClickEnd.AddListener(() => OnStatusModifyButtoClicked(1, 1));
        }
        if (GetButton("DexterityDownButton", out var ddButton))
        {
            ddButton.InitButton(isClick: true);
            ddButton.OnClickEnd.AddListener(() => OnStatusModifyButtoClicked(1, -1));
        }
        if (GetButton("PowerUpButton", out var puButton))
        {
            puButton.InitButton(isClick: true);
            puButton.OnClickEnd.AddListener(() => OnStatusModifyButtoClicked(2, 1));
        }
        if (GetButton("PowerDownButton", out var pdButton))
        {
            pdButton.InitButton(isClick: true);
            pdButton.OnClickEnd.AddListener(() => OnStatusModifyButtoClicked(2, -1));
        }
        if (GetButton("AgilityUpButton", out var auButton))
        {
            auButton.InitButton(isClick: true);
            auButton.OnClickEnd.AddListener(() => OnStatusModifyButtoClicked(3, 1));
        }
        if (GetButton("AgilityDownButton", out var adButton))
        {
            adButton.InitButton(isClick: true);
            adButton.OnClickEnd.AddListener(() => OnStatusModifyButtoClicked(3, -1));
        }

        ShowSection(0);
    }

    public override void OnClose()
    {
        base.OnClose();

        GameManager.System.PlayerActor.IsControllable = true;
    }

    private void OnSectionMoveButton(int value)
    {
        var refuse = false;

        switch (_sectionIndex)
        {
            default:
            case 0:
                break;
            case 1:
                if (GetDropDown("HaloShapeDropdown", out var hsDd))
                    _haloShapeIndex = hsDd.value;
                if (GetDropDown("HaloColorDropdown", out var hcDd))
                    _haloColorIndex = hcDd.value;
                break;
            case 2:
                if (GetInputField("Name1InputField", out var name1))
                    _names[0] = name1.text;

                if (GetInputField("Name2InputField", out var name2))
                    _names[1] = name2.text;
                break;
            case 3:
                if (_statusPoint > 0)
                    refuse = true;
                break;
            case 4:
                if (GetDropDown("WeaponDropdown", out var wDd))
                    _weapon = wDd.value;

                switch(_weapon)
                {
                    default:
                    case 0:
                        break;
                    case 1:
                    case 2:
                        if (_status[0] < 2)
                            refuse = true;
                        break;
                    case 3:
                        if (_status[0] < 3)
                            refuse = true;
                        break;
                }
                break;
        }

        if (value > 0)
        {
            if (_sectionIndex < 4)
            {
                if (refuse)
                {
                    if (GetText("RemainPointNoticeText", out var wnText))
                    {
                        wnText.text = TextTransfer.GetPointRemainText();
                        wnText.DoDisappear(1f);
                    }
                    return;
                }
                _sectionIndex++;
            }
            else
            {
                if (refuse)
                {
                    if (GetText("WeaponNoticeText", out var wnText))
                    {
                        wnText.text = TextTransfer.GetPowerlessText();
                        wnText.DoDisappear(1f);
                    }
                    return;
                }
                else
                {

                    OnMakingEnd();
                }
            }
        }
        else
        {
            if (_sectionIndex > 0)
                _sectionIndex--;
        }

        ShowSection(_sectionIndex);
    }

    private void OnStatusModifyButtoClicked(int index, int value)
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
            rpText.text = TextTransfer.GetRemainPointText(_statusPoint);
    }

    private void OnMakingEnd()
    {
        GameManager.Data.Play.Model = GameManager.Resource.Load<Transform>($"Actors/Model{_modelIndex}").GetChild(0).gameObject;

        if (_names[0].Length > 0)
            GameManager.Data.Play.Name[0] = _names[0];
        if (_names[1].Length > 0)
            GameManager.Data.Play.Name[1] = _names[1];

        GameManager.Data.Play.Status[0] = _status[0];
        GameManager.Data.Play.Status[1] = _status[1];
        GameManager.Data.Play.Status[2] = _status[2];
        GameManager.Data.Play.Status[3] = _status[3];

        GameManager.Data.Play.HaloShape = GameManager.Resource.Load<Transform>($"Actors/Halos/Halo{_haloShapeIndex}").gameObject;

        switch (_haloColorIndex)
        {
            default:
            case 0:
                GameManager.Data.Play.HaloColor = Color.red;
                break;
            case 1:
                GameManager.Data.Play.HaloColor = Color.blue;
                break;
            case 2:
                GameManager.Data.Play.HaloColor = Color.yellow;
                break;
            case 3:
                GameManager.Data.Play.HaloColor = Color.green;
                break;
            case 4:
                GameManager.Data.Play.HaloColor = Color.cyan;
                break;
            case 5:
                GameManager.Data.Play.HaloColor = Color.magenta;
                break;
            case 6:
                GameManager.Data.Play.HaloColor = Color.gray;
                break;
            case 7:
                GameManager.Data.Play.HaloColor = Color.white;
                break;
        }

        switch (_weapon)
        {
            default:
            case 0:
                GameManager.Resource.Load<WeaponData>(DataEnum.Weapon, "HG_Tier0").Count = 1;
                break;
            case 1:
                GameManager.Resource.Load<WeaponData>(DataEnum.Weapon, "AR_Tier0").Count = 1;
                break;
            case 2:
                GameManager.Resource.Load<WeaponData>(DataEnum.Weapon, "SG_Tier0").Count = 1;
                break;
            case 3:
                GameManager.Resource.Load<WeaponData>(DataEnum.Weapon, "MG_Tier0").Count = 1;
                break;
        }

        GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out _);
    }

    private void ShowSection(int index)
    {
        if (GetContent("ModelSection", out var mSection))
            mSection.gameObject.SetActive(index == 0);

        if (GetContent("HaloSection", out var hSection))
            hSection.gameObject.SetActive(index == 1);

        if (GetContent("NameSection", out var nSection))
            nSection.gameObject.SetActive(index == 2);

        if (GetContent("StatusSection", out var sSection))
            sSection.gameObject.SetActive(index == 3);

        if (GetContent("WeaponSection", out var wSection))
            wSection.gameObject.SetActive(index == 4);
    }
}
