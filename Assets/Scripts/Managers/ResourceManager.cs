using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 풀을 이용한 인스턴스 생성 및 삭제
/// </summary>
public class ResourceManager : BaseManager
{
    private Dictionary<string, Object> _resources = new Dictionary<string, Object>();

    public override void InitManager()
    {
        base.InitManager();


    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        T[] allResource = Resources.LoadAll<T>(path);
        return allResource;
    }

    public T Load<T>(string path) where T : Object
    {
        var key = $"{typeof(T)}.{path}";

        if (_resources.ContainsKey(key))
            return _resources[key] as T;

        var resource = Resources.Load<T>(path);
        _resources.Add(key, resource);
        return resource;
    }

    public T[] LoadAll<T>(DataEnum data) where T : Object
    {
        var path = new StringBuilder();
        switch (data)
        {
            default:
                return null;
            case DataEnum.Actor:
                path.Append("Datas/Actors");
                break;
            case DataEnum.Mission:
                path.Append("Datas/Missions");
                break;
            case DataEnum.Product:
                path.Append("Datas/Products");
                break;
            case DataEnum.Item:
                path.Append("Datas/Products/Items");
                break;
            case DataEnum.Weapon:
                path.Append("Datas/Products/Weapons");
                break;
            case DataEnum.Community:
                path.Append("Datas/Community");
                break;
        }

        T[] allResource = Resources.LoadAll<T>(path.ToString());
        return allResource;
    }

    public T Load<T>(DataEnum data, string name) where T : Object
    {
        var path = new StringBuilder();
        switch (data)
        {
            default:
                return null;
            case DataEnum.Actor:
                path.Append("Datas/Actors/");
                break;
            case DataEnum.Mission:
                path.Append("Datas/Missions/");
                break;
            case DataEnum.Product:
                path.Append("Datas/Products/");
                break;
            case DataEnum.Item:
                path.Append("Datas/Products/Items/");
                break;
            case DataEnum.Weapon:
                path.Append("Datas/Products/Weapons/");
                break;
        }
        path.Append(name);

        var key = $"{typeof(T)}.{path}";

        if (_resources.ContainsKey(key))
            return _resources[key] as T;

        var resource = Resources.Load<T>(path.ToString());
        _resources.Add(key, resource);
        return resource;
    }

    public CommunityData Load(CharacterEnum data)
    {
        var path = new StringBuilder("Datas/Community/");
        switch (data)
        {
            default:
                return null;
            case CharacterEnum.Ayane:
                path.Append("AyaneCommunity");
                break;
            case CharacterEnum.Hoshino:
                path.Append("HoshinoCommunity");
                break;
            case CharacterEnum.Nonomi:
                path.Append("NonomiCommunity");
                break;
            case CharacterEnum.Serika:
                path.Append("SerikaCommunity");
                break;
            case CharacterEnum.Shiroko:
                path.Append("ShirokoCommunity");
                break;
        }

        var key = $"{typeof(CommunityData)}.{path}";

        if (_resources.ContainsKey(key))
            return _resources[key] as CommunityData;

        var resource = Resources.Load< CommunityData>(path.ToString());
        _resources.Add(key, resource);
        return resource;
    }

    public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent, bool pooling = false) where T : Object
    {
        if (pooling)
            return GameManager.Pool.Get(original, position, rotation, parent);
        else
            return Object.Instantiate(original, position, rotation, parent);
    }

    public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, bool pooling = false) where T : Object
    {
        return Instantiate<T>(original, position, rotation, null, pooling);
    }

    public new T Instantiate<T>(T original, Transform parent, bool pooling = false) where T : Object
    {
        return Instantiate<T>(original, Vector3.zero, Quaternion.identity, parent, pooling);
    }

    public T Instantiate<T>(T original, bool pooling = false) where T : Object
    {
        return Instantiate<T>(original, Vector3.zero, Quaternion.identity, null, pooling);
    }

    public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, Transform parent, bool pooling = false) where T : Object
    {
        T original = Load<T>(path);
        return Instantiate<T>(original, position, rotation, parent, pooling);
    }

    public T Instantiate<T>(string path, Vector3 position, Quaternion rotation, bool pooling = false) where T : Object
    {
        return Instantiate<T>(path, position, rotation, null, pooling);
    }

    public T Instantiate<T>(string path, Transform parent, bool pooling = false) where T : Object
    {
        return Instantiate<T>(path, Vector3.zero, Quaternion.identity, parent, pooling);
    }

    public T Instantiate<T>(string path, bool pooling = false) where T : Object
    {
        return Instantiate<T>(path, Vector3.zero, Quaternion.identity, null, pooling);
    }

    public T InstantiateDontDestroyOnLoad<T>(string path, Transform parent = null, bool pooling = false) where T : Object
    {
        T instantiated = Instantiate<T>(path, Vector3.zero, Quaternion.identity, parent, pooling);
        DontDestroyOnLoad(instantiated);
        return instantiated;
    }

    public async void Destroy(GameObject go, int delay = 0)
    {
        if (go)
        {
            if (GameManager.Pool.IsContain(go))
                await DelayReleaseRoutine(go, delay);
            else
                GameObject.Destroy(go, delay);
        }
    }

    private async UniTask DelayReleaseRoutine(GameObject go, int delay)
    {
        await UniTask.Delay(delay);
        if (go)
        {
            GameManager.Pool.Release(go);
        }
    }

    public void Destroy(Component component, int delay = 0)
    {
        Destroy(component.gameObject, delay);
    }
}