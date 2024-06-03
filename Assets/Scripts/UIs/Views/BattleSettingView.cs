using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class BattleSettingView : View
{
    private List<MissionData> _missions = new List<MissionData>();
    private List<WeaponData> _weapons = new List<WeaponData>();
    private List<ItemData> _items = new List<ItemData>();

    private int _selectMission;
    private int _selectWeapon;
    private Dictionary<int, int> _selectItem = new Dictionary<int, int>();
    private int _selectLevel;

    public override UniTask OnInit()
    {
        if (GetTemplate("MissionTemplate", out var missionTemplate))
        {
            if (GetContent("MissionContent", out var missionContent))
            {
                var missions = GameManager.Resource.LoadAll<MissionData>(DataEnum.Mission);

                var index = 0;
                for (int i = 0; i < missions.Length; i++)
                {
                    if (missions[i].Count < 0)
                        continue;

                    var current = index;
                    var currentMissionTemplate = GameManager.Resource.Instantiate(missionTemplate, missionContent, true);
                    if (currentMissionTemplate.GetText("NameText", out var nameText))
                        nameText.text = missions[i].Name;
                    if (currentMissionTemplate.GetButton("Button", out var button)) { }
                        button.OnClick.AddListener(() =>
                        {
                            _selectMission = current;
                            if (GetContent("WeaponSection", out var weaponSection))
                                weaponSection.gameObject.SetActive(true);
                        });
                    _missions.Add(missions[i]);
                    index++;
                }
            }
            missionTemplate.gameObject.SetActive(false);
        }

        if (GetTemplate("WeaponTemplate", out var weaponTemplate))
        {
            if (GetContent("WeaponContent", out var weaponContent))
            {
                var equipments = GameManager.Resource.LoadAll<WeaponData>(DataEnum.Weapon);

                var index = 0;
                for (int i = 0; i < equipments.Length; i++)
                {
                    if (equipments[i].Count <= 0)
                        continue;

                    var current = index;
                    var currentWeaponTemplate = GameManager.Resource.Instantiate(weaponTemplate, weaponContent, true);
                    if (currentWeaponTemplate.GetText("NameText", out var nameText))
                        nameText.text = equipments[i].Name;
                    if (currentWeaponTemplate.GetButton("Button", out var button))
                        button.OnClick.AddListener(() =>
                        {
                            _selectWeapon = current;
                            if (GetContent("ItemSection", out var itemSection))
                                itemSection.gameObject.SetActive(true);
                            if (GetContent("LevelSection", out var levelSection))
                                levelSection.gameObject.SetActive(true);
                        });
                    _weapons.Add(equipments[i]);
                    index++;
                }
            }
            weaponTemplate.gameObject.SetActive(false);
        }

        if (GetTemplate("ItemTemplate", out var itemTemplate))
        {
            if (GetContent("ItemContent", out var itemContent))
            {
                var items = GameManager.Resource.LoadAll<ItemData>(DataEnum.Item);

                var index = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].Count <= 0)
                        continue;

                    var current = index;
                    var currentItemTemplate = GameManager.Resource.Instantiate(itemTemplate, itemContent, true);
                    if (currentItemTemplate.GetText("NameText", out var nameText))
                        nameText.text = items[i].Name;
                    if (currentItemTemplate.GetText("CountText", out var countText))
                        countText.text = $"0/{items[i].Count}";
                    if (currentItemTemplate.GetButton("AddButton", out var aButton))
                        aButton.OnClick.AddListener(() =>
                        {
                            if (_selectItem.ContainsKey(i) && _selectItem[index] < items[i].Count)
                                _selectItem[current]++;
                            else
                                _selectItem[current] = 1;
                        });
                    if (currentItemTemplate.GetButton("RemoveButton", out var rButton))
                        rButton.OnClick.AddListener(() =>
                        {
                            if (_selectItem.ContainsKey(index) && _selectItem[index] > 0)
                                _selectItem[current]--;
                        });
                    _items.Add(items[i]);
                    index++;
                }
            }
            itemTemplate.gameObject.SetActive(false);
        }

        if (GetTemplate("LevelTemplate", out var levelTemplate))
        {
            if (GetContent("LevelContent", out var levelContent))
            {
                for (int i = 1; i < StaticValues.DefaultLimitLevel + 1; i++)
                {
                    var index = i;
                    var currentLevelTemplate = GameManager.Resource.Instantiate(levelTemplate, levelContent, true);
                    if (currentLevelTemplate.GetText("NameText", out var nameText))
                        nameText.text = $"³­ÀÌµµ {index}";
                    if (currentLevelTemplate.GetButton("Button", out var button))
                        button.OnClick.AddListener(() =>
                        {
                            _selectLevel = index;
                            if (GetButton("StartButton", out var sButton))
                                sButton.gameObject.SetActive(true);
                        });
                }
            }
            levelTemplate.gameObject.SetActive(false);
        }

        if (GetButton("CancelButton", out var cButton))
        {
            cButton.InitButton(isClick: true);
            cButton.OnClick.AddListener(() => GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out _));
        }

        if (GetButton("StartButton", out var sButton))
        {
            sButton.InitButton(isClick: true);
            sButton.OnClick.AddListener(() =>
            {
                GameManager.System.CurrentMission = new CurrentMission(
                    _missions[_selectMission], _weapons[_selectWeapon], _selectLevel, _items);
                GameManager.UI.CloseCurrentDialog();
            });
            sButton.OnClick.AddListener(() => GameManager.UI.CloseCurrentView());
        }

        return base.OnInit();
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void OnClose()
    {
        base.OnClose();
    }


}
