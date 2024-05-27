using System;
using System.Collections.Generic;

public class DataManager : BaseManager
{
    public PlayData Play;
    public Dictionary<CharacterEnum, ActorData> Actors = new Dictionary<CharacterEnum, ActorData>();
    public Dictionary<string, ProductData> Products = new Dictionary<string, ProductData>();
    public Dictionary<string, WeaponData> Weapons = new Dictionary<string, WeaponData>();
    public Dictionary<string, ItemData> Items = new Dictionary<string, ItemData>();

    public override void InitManager()
    {
        base.InitManager();

        Play = GameManager.Resource.Load<PlayData>("Datas/PlayData");

        var actors = GameManager.Resource.LoadAll<ActorData>(DataEnum.Actor);
        foreach (var actor in actors)
        {
            if (Actors.ContainsKey(actor.Character) == false)
                Actors[actor.Character] = actor;
        }

        var products = GameManager.Resource.LoadAll<ProductData>(DataEnum.Product);
        foreach (var product in products)
        {
            if (Products.ContainsKey(product.Name) == false)
                Products[product.Name] = product;
        }

        var weapons = GameManager.Resource.LoadAll<WeaponData>(DataEnum.Weapon);
        foreach (var weapon in weapons)
        {
            if (Weapons.ContainsKey(weapon.Name) == false)
                Weapons[weapon.Name] = weapon;
        }

        var items = GameManager.Resource.LoadAll<ItemData>(DataEnum.Weapon);
        foreach (var item in items)
        {
            if (Items.ContainsKey(item.Name) == false)
                Items[item.Name] = item;
        }

        ResetPlayData();
    }

    public void ResetPlayData()
    {
        Play.Name = new string[2] { "마보로시", "비비아" };
        Play.Model = null;

        Play.Debt = int.MaxValue;
        Play.Energy = 10;
        Play.Stamina = 180;

        Play.Level = 0;
        Play.Status = new int[4];

        Play.Farm.PlantList = new List<ProductData>();
        Play.Farm.DateList = new List<int>();


        Play.StartDate = DateTime.Now;
        Play.LastConnectedTime = DateTime.Now;
    }
}