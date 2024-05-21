using System.Resources;
using UnityEngine;

public class GameManager : BaseManager
{
    private static GameManager _instance;

    private static PoolManager _poolManager;
    private static ResourceManager _resourceManager;
    private static DataManager _dataManager;
    private static SceneManager _sceneManager;
    private static AudioManager _audioManager;
    private static UIManager _uiManager;
    private static SystemManager _systemManager;

    public static GameManager Instance { get { return _instance; } }
    public static PoolManager Pool { get { return _poolManager; } }
    public static ResourceManager Resource { get { return _resourceManager; } }
    public static DataManager Data { get { return _dataManager; } }
    public static SceneManager Scene { get { return _sceneManager; } }
    public static AudioManager Audio { get { return _audioManager; } }
    public static UIManager UI { get { return _uiManager; } }
    public static SystemManager System { get { return _systemManager; } }

    public override void InitManager()
    {
        base.InitManager();

        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    private void InitManagers()
    {
        var resourceObj = new GameObject();
        resourceObj.name = "ResourceManager";
        resourceObj.transform.parent = transform;
        _resourceManager = resourceObj.AddComponent<ResourceManager>();
        _resourceManager.InitManager();

        var poolObj = new GameObject();
        poolObj.name = "PoolManager";
        poolObj.transform.parent = transform;
        _poolManager = poolObj.AddComponent<PoolManager>();
        _poolManager.InitManager();

        var dataObj = new GameObject();
        dataObj.name = "DataManager";
        dataObj.transform.parent = transform;
        _dataManager = dataObj.AddComponent<DataManager>();
        _dataManager.InitManager();

        var sceneObj = new GameObject();
        sceneObj.name = "SceneManager";
        sceneObj.transform.parent = transform;
        _sceneManager = sceneObj.AddComponent<SceneManager>();
        _sceneManager.InitManager();

        var audioObj = new GameObject();
        audioObj.name = "AudioManager";
        audioObj.transform.parent = transform;
        _audioManager = audioObj.AddComponent<AudioManager>();
        _audioManager.InitManager();

        var uiObj = new GameObject();
        uiObj.name = "UIManager";
        uiObj.transform.parent = transform;
        _uiManager = uiObj.AddComponent<UIManager>();
        _uiManager.InitManager();

        var systemObj = new GameObject();
        systemObj.name = "SystemManager";
        systemObj.transform.parent = transform;
        _systemManager = systemObj.AddComponent<SystemManager>();
        _systemManager.InitManager();
    }
}