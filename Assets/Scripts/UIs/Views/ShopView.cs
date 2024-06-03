using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : View
{
    private Dictionary<int, UITemplate> _weapons = new Dictionary<int, UITemplate>();
    private Dictionary<int, UITemplate> _items = new Dictionary<int, UITemplate>();
    private Dictionary<int, UITemplate> _plants = new Dictionary<int, UITemplate>();
    private Dictionary<int, UITemplate> _specials = new Dictionary<int, UITemplate>();

    private enum ShopPanel { All, Special, Weapon, Item, Plant, }

    public override UniTask OnInit()
    {
        if (GetButton("exitButton", out var eButton))
        {
            eButton.InitButton();
            eButton.OnClickEnd.AddListener(() => GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out _));
        }

        if (GetText("MoneyText", out var mText))
        {
            mText.text = $"{GameManager.Data.Specials["Credit"].Count} C";
        }

        if (GetButton("AllButton", out var apButton))
        {
            apButton.InitButton();
            apButton.OnClickEnd.AddListener(() => ShowPanel(ShopPanel.All));
        }

        if (GetButton("SpecialButton", out var spButton))
        {
            spButton.InitButton();
            spButton.OnClickEnd.AddListener(() => ShowPanel(ShopPanel.Special));
        }

        if (GetButton("WeaponButton", out var wpButton))
        {
            wpButton.InitButton();
            wpButton.OnClickEnd.AddListener(() => ShowPanel(ShopPanel.Weapon));
        }

        if (GetButton("ItemButton", out var ipButton))
        {
            ipButton.InitButton();
            ipButton.OnClickEnd.AddListener(() => ShowPanel(ShopPanel.Item));
        }

        if (GetButton("PlantButton", out var ppButton))
        {
            ppButton.InitButton();
            ppButton.OnClickEnd.AddListener(() => ShowPanel(ShopPanel.Plant));
        }

        if (GetTemplate("ProductTemplate", out var pTemplate))
        {
            if (GetContent("Content", out var content))
            {
                var index = 0;
                foreach (var weapon in GameManager.Data.Weapons.Values)
                {
                    var newWeapon = GameManager.Resource.Instantiate(pTemplate, content);
                    if (newWeapon.GetText("ProductText", out var pText))
                    {
                        pText.text = weapon.Name;
                    }
                    if (newWeapon.GetText("CostText", out var cText))
                    {
                        cText.text = $"{weapon.Price}C";
                    }
                    if (newWeapon.GetImage("ProductIcon", out var pIcon))
                    {
                        pIcon.sprite = weapon.Image;
                    }
                    if (newWeapon.GetButton("ProductButton", out var pButton))
                    {
                        pButton.InitButton();
                        pButton.OnClickEnd.AddListener(() => Debug.Log($"Buy {weapon.Name}!"));
                    }
                    _weapons.Add(index, newWeapon);
                    index++;
                }
                index = 0;

                foreach (var item in GameManager.Data.Items.Values)
                {
                    var newItem = GameManager.Resource.Instantiate(pTemplate, content);
                    if (newItem.GetText("ProductText", out var pText))
                    {
                        pText.text = item.Name;
                    }
                    if (newItem.GetText("CostText", out var cText))
                    {
                        cText.text = $"{item.Price}C";
                    }
                    if (newItem.GetImage("ProductIcon", out var pIcon))
                    {
                        pIcon.sprite = item.Image;
                    }
                    if (newItem.GetButton("ProductButton", out var pButton))
                    {
                        pButton.InitButton();
                        pButton.OnClickEnd.AddListener(() => Debug.Log($"Buy {item.Name}!"));
                    }
                    _items.Add(index, newItem);
                    index++;
                }
                index = 0;

                foreach (var plant in GameManager.Data.Plants.Values)
                {
                    var newPlant = GameManager.Resource.Instantiate(pTemplate, content);
                    if (newPlant.GetText("ProductText", out var pText))
                    {
                        pText.text = plant.Name;
                    }
                    if (newPlant.GetText("CostText", out var cText))
                    {
                        cText.text = $"{plant.Price}C";
                    }
                    if (newPlant.GetImage("ProductIcon", out var pIcon))
                    {
                        pIcon.sprite = plant.Image;
                    }
                    if (newPlant.GetButton("ProductButton", out var pButton))
                    {
                        pButton.InitButton();
                        pButton.OnClickEnd.AddListener(() => Debug.Log($"Buy {plant.Name}!"));
                    }
                    _plants.Add(index, newPlant);
                    index++;
                }
                index = 0;

                foreach (var special in GameManager.Data.Specials.Values)
                {
                    if (special.Type == ProductEnum.Credit)
                        continue;

                    var newSpecial = GameManager.Resource.Instantiate(pTemplate, content);
                    if (newSpecial.GetText("ProductText", out var pText))
                    {
                        pText.text = special.Name;
                    }
                    if (newSpecial.GetText("CostText", out var cText))
                    {
                        cText.text = $"{special.Price}C";
                    }
                    if (newSpecial.GetImage("ProductIcon", out var pIcon))
                    {
                        pIcon.sprite = special.Image;
                    }
                    if (newSpecial.GetButton("ProductButton", out var pButton))
                    {
                        pButton.InitButton();
                        pButton.OnClickEnd.AddListener(() => Debug.Log($"Buy {special.Name}!"));
                    }
                    _specials.Add(index, newSpecial);
                    index++;
                }
            }

            pTemplate.gameObject.SetActive(false);
        }

        return base.OnInit();
    }

    public override void OnOpen()
    {
        base.OnOpen();

        ShowPanel(ShopPanel.All);
    }

    public override void OnClose()
    {
        base.OnClose();

        ShowPanel(ShopPanel.All);
    }

    private void ShowPanel(ShopPanel next)
    {
        switch (next)
        {
            default:
            case ShopPanel.All:
                foreach (var special in _specials.Values)
                    special.gameObject.SetActive(true);
                foreach (var weapon in _weapons.Values)
                    weapon.gameObject.SetActive(true);
                foreach (var item in _items.Values)
                    item.gameObject.SetActive(true);
                foreach (var plant in _plants.Values)
                    plant.gameObject.SetActive(true);
                break;
            case ShopPanel.Special:
                foreach (var special in _specials.Values)
                    special.gameObject.SetActive(true);
                foreach (var weapon in _weapons.Values)
                    weapon.gameObject.SetActive(false);
                foreach (var item in _items.Values)
                    item.gameObject.SetActive(false);
                foreach (var plant in _plants.Values)
                    plant.gameObject.SetActive(false);
                break;
            case ShopPanel.Weapon:
                foreach (var special in _specials.Values)
                    special.gameObject.SetActive(false);
                foreach (var weapon in _weapons.Values)
                    weapon.gameObject.SetActive(true);
                foreach (var item in _items.Values)
                    item.gameObject.SetActive(false);
                foreach (var plant in _plants.Values)
                    plant.gameObject.SetActive(false);
                break;
            case ShopPanel.Item:
                foreach (var special in _specials.Values)
                    special.gameObject.SetActive(false);
                foreach (var weapon in _weapons.Values)
                    weapon.gameObject.SetActive(false);
                foreach (var item in _items.Values)
                    item.gameObject.SetActive(true);
                foreach (var plant in _plants.Values)
                    plant.gameObject.SetActive(false);
                break;
            case ShopPanel.Plant:
                foreach (var special in _specials.Values)
                    special.gameObject.SetActive(false);
                foreach (var weapon in _weapons.Values)
                    weapon.gameObject.SetActive(false);
                foreach (var item in _items.Values)
                    item.gameObject.SetActive(false);
                foreach (var plant in _plants.Values)
                    plant.gameObject.SetActive(true);
                break;
        }
    }
}
