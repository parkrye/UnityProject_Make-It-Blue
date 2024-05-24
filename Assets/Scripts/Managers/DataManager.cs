using System;
using System.Collections.Generic;

public class DataManager : BaseManager
{
    public PlayData Play;
    public Dictionary<CharacterEnum, ActorData> Actors = new Dictionary<CharacterEnum, ActorData>();
    public Dictionary<string, EnemyData> Enemies = new Dictionary<string, EnemyData>();
    public Dictionary<string, ProductData> Products = new Dictionary<string, ProductData>();
    public Dictionary<string, EquipmentData> Equipments = new Dictionary<string, EquipmentData>();

    public override void InitManager()
    {
        base.InitManager();

        Play = GameManager.Resource.Load<PlayData>("Datas/PlayData");

        var actors = GameManager.Resource.LoadAll<ActorData>("Datas/Actors");
        foreach (var actor in actors)
        {
            if (Actors.ContainsKey(actor.Character) == false)
                Actors[actor.Character] = actor;
        }

        var enemies = GameManager.Resource.LoadAll<EnemyData>("Datas/Actors/Enemies");
        foreach (var enemy in enemies)
        {
            if (Enemies.ContainsKey(enemy.Name) == false)
                Enemies[enemy.Name] = enemy;
        }

        var products = GameManager.Resource.LoadAll<ProductData>("Datas/Products");
        foreach (var product in products)
        {
            if (Products.ContainsKey(product.Name) == false)
                Products[product.Name] = product;
        }

        var equipments = GameManager.Resource.LoadAll<EquipmentData>("Datas/Products/Equipments");
        foreach (var equipment in equipments)
        {
            if (Equipments.ContainsKey(equipment.Name) == false)
                Equipments[equipment.Name] = equipment;
        }

        ResetPlayData();
    }

    public void ResetPlayData()
    {
        Play.Name = new string[2] { "마보로시", "비비아" };
        Play.Model = null;

        Play.Debt = int.MaxValue;
        Play.Stamina = 0;
        Play.Energy = 0;

        Play.Level = 0;
        Play.Status = new int[4];
        Play.ProductList = new List<ProductData>();

        Play.Farm.PlantList = new List<ProductData>();
        Play.Farm.DateList = new List<int>();


        Play.StartDate = DateTime.Now;
        Play.LastConnectedTime = DateTime.Now;
    }
}