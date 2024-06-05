using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();

        if (GetContent("Content", out var content))
        {
            if (GetTemplate("PlantTemplate", out var pTemplate))
            {
                foreach (var plant in GameManager.Data.Plants.Values)
                {
                    if (plant.Count <= 0)
                        continue;

                    var instant = GameManager.Resource.Instantiate(pTemplate, content);
                    if (instant.GetImage("Icon", out var icon))
                    {
                        icon.sprite = plant.Image;
                    }
                    if (instant.GetText("NameText", out var nText))
                    {
                        nText.text = plant.Name;
                    }
                    if(instant.GetText("CountText", out var cText))
                    {
                        cText.text = $"{plant.Count}";
                    }
                    if(instant.GetButton("Button", out var button))
                    {
                        button.InitButton();
                        Debug.Log($"Plant {plant.Name}!");
                    }
                }
                pTemplate.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (GetTemplate($"FarmSlot{i+1}", out var farmSlot))
            {
                var isPlanted = GameManager.Data.Play.Farm.PlantArray[i] != null;

                if (farmSlot.GetImage("Icon", out var icon))
                {
                    if (isPlanted)
                        icon.sprite = GameManager.Data.Play.Farm.PlantArray[i].Image;
                    else
                        icon.sprite = null;
                }

                if (farmSlot.GetContent("FillImage", out var fImage))
                {
                    if (isPlanted)
                        fImage.sizeDelta = new Vector2(0f, 400f * ((10 - GameManager.Data.Play.Farm.DateArray[i]) / 10));
                    else
                        fImage.sizeDelta = new Vector2(0f, 0f);
                }
            }
        }

        if (GetButton("ExitButton", out var eButton))
        {
            eButton.InitButton();
            eButton.OnClickEnd.AddListener(() => GameManager.UI.OpenUI<MainView>(PublicUIEnum.Main, out _));
        }
    }
}
