using UnityEngine;

public class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        InitGameManager();
        InitGameSettings();
        InitGameData();
        InitPlayerData();
    }

    private static void InitGameManager()
    {
        if (!GameManager.Instance)
        {
            var gameManager = new GameObject();
            gameManager.name = "GameManager";
            gameManager.AddComponent<GameManager>();
        }
        GameManager.Instance.InitManager();
    }

    private static void InitGameSettings()
    {
        QualitySettings.vSyncCount = StaticValues.VSync ? 1 : 0;
    }

    private static void InitGameData()
    {

    }

    private static void InitPlayerData()
    {

    }
}