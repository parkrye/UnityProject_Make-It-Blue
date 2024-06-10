using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : BaseManager
{
    public PlayData Play;
    public ShopData CurrentShop;

    public Dictionary<CharacterEnum, ActorData> Actors = new Dictionary<CharacterEnum, ActorData>();
    public Dictionary<string, ProductData> Specials = new Dictionary<string, ProductData>();
    public Dictionary<string, WeaponData> Weapons = new Dictionary<string, WeaponData>();
    public Dictionary<string, ItemData> Items = new Dictionary<string, ItemData>();
    public Dictionary<string, ProductData> Plants = new Dictionary<string, ProductData>();

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

        var weapons = GameManager.Resource.LoadAll<WeaponData>(DataEnum.Weapon);
        foreach (var weapon in weapons)
        {
            if (Weapons.ContainsKey(weapon.name) == false)
                Weapons[weapon.name] = weapon;
        }

        var items = GameManager.Resource.LoadAll<ItemData>(DataEnum.Item);
        foreach (var item in items)
        {
            if (Items.ContainsKey(item.name) == false)
                Items[item.name] = item;
        }

        var plants = GameManager.Resource.LoadAll<ProductData>(DataEnum.Plant);
        foreach (var plant in plants)
        {
            if (Plants.ContainsKey(plant.name) == false)
                Plants[plant.name] = plant;
        }

        var specials = GameManager.Resource.LoadAll<ProductData>(DataEnum.Product);
        foreach (var special in specials)
        {
            if (Weapons.ContainsKey(special.name))
                continue;
            if (Items.ContainsKey(special.name))
                continue;
            if (Plants.ContainsKey(special.name))
                continue;
            if (Specials.ContainsKey(special.name) == false)
                Specials[special.name] = special;
        }

        //ResetPlayData();
    }

    public void ResetPlayData()
    {
        Play.Name = TextTransfer.GetDefaultName();
        Play.Model = null;
        Play.HaloShape = null;
        Play.HaloColor = Color.red;

        Play.Debt = int.MaxValue;
        Play.Energy = 10;
        Play.Stamina = 180;
        Play.Day = DayFlow.Noon;

        Play.Level = 0;
        Play.Status = new int[4];

        Play.Farm.PlantArray = new ProductData[4];
        Play.Farm.DateArray = new int[4];

        Play.StartDate = DateTime.Now;
        Play.LastConnectedTime = DateTime.Now;

        var weapons = GameManager.Resource.LoadAll<WeaponData>(DataEnum.Weapon);
        foreach (var weapon in weapons)
        {
            weapon.Count = 0;
        }

        var items = GameManager.Resource.LoadAll<ItemData>(DataEnum.Item);
        foreach (var item in items)
        {
            item.Count = -1;
        }

        var products = GameManager.Resource.LoadAll<ProductData>(DataEnum.Product);
        foreach (var product in products)
        {
            product.Count = -1;
        }

        var missions = GameManager.Resource.LoadAll<MissionData>(DataEnum.Mission);
        foreach (var mission in missions)
        {
            mission.Count = -1;
        }

        var communities = GameManager.Resource.LoadAll<CommunityData>(DataEnum.Community);
        foreach (var community in communities)
        {
            community.Count = -1;
            community.Favor = 0;
        }
    }
}