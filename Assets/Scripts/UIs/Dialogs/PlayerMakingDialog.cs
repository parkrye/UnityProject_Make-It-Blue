using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMakingDialog : Dialog
{
    private int _sectionIndex;
    private int _statusPoint;

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
                if (GetInputField("Name1InputField", out var name1))
                    _names[0] = name1.text;

                if (GetInputField("Name2InputField", out var name2))
                    _names[1] = name2.text;
                break;
            case 1:
                if (_statusPoint > 0)
                    refuse = true;
                break;
            case 2:
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
            if (_sectionIndex < 2)
            {
                if (refuse)
                {
                    if (GetText("RemainPointNoticeText", out var wnText))
                    {
                        wnText.text = "포인트가 남습니다!";
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
                        wnText.text = "근력이 부족합니다!";
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
            rpText.text = $"잔여 : {_statusPoint}";
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
