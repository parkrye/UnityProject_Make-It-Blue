using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : BaseManager
{
    private Dictionary<string, ObjectPool<GameObject>> _poolDic = new Dictionary<string, ObjectPool<GameObject>>();
    private Dictionary<string, Transform> _poolContainer = new Dictionary<string, Transform>();
    private Transform _sceneTransform;

    public override void InitManager()
    {
        base.InitManager();


    }

    public void Reset()
    {
        _poolDic.Clear();
        _poolContainer.Clear();
    }

    private Transform SceneTransform()
    {
        if (_sceneTransform)
        {
            return _sceneTransform;
        }
        else
        {
            _sceneTransform = GameManager.Resource.Instantiate<Transform>("Container");
            return _sceneTransform;
        }
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        if (original is GameObject)
        {
            var prefab = original as GameObject;
            var key = prefab.name;

            if (!_poolDic.ContainsKey(key))
                CreatePool(key, prefab);

            var obj = _poolDic[key].Get();
            obj.transform.parent = parent;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj as T;
        }
        else if (original is Component)
        {
            var component = original as Component;
            var key = component.gameObject.name;

            if (!_poolDic.ContainsKey(key))
                CreatePool(key, component.gameObject);

            var obj = _poolDic[key].Get();
            obj.transform.parent = parent;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return Get<T>(original, position, rotation, null);
    }

    public T Get<T>(T original, Transform parent) where T : Object
    {
        return Get<T>(original, Vector3.zero, Quaternion.identity, parent);
    }

    public T Get<T>(T original) where T : Object
    {
        return Get<T>(original, Vector3.zero, Quaternion.identity, null);
    }

    public bool Release<T>(T instance) where T : Object
    {
        if (instance is GameObject)
        {
            var go = instance as GameObject;
            var key = go.name;

            if (!_poolDic.ContainsKey(key))
                return false;

            try
            {
                _poolDic[key].Release(go);
            }
            catch
            {
                return false;
            }
            return true;
        }
        else if (instance is Component)
        {
            var component = instance as Component;
            var key = component.gameObject.name;

            if (!_poolDic.ContainsKey(key))
                return false;

            try
            {
                _poolDic[key].Release(component.gameObject);
            }
            catch
            {
                return false;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsContain<T>(T original) where T : Object
    {
        if (original is GameObject)
        {
            var prefab = original as GameObject;
            var key = prefab.name;

            if (_poolDic.ContainsKey(key))
                return true;
            else
                return false;

        }
        else if (original is Component)
        {
            var component = original as Component;
            var key = component.gameObject.name;

            if (_poolDic.ContainsKey(key))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    private void CreatePool(string key, GameObject prefab)
    {
        var root = new GameObject();
        root.gameObject.name = $"{key}Container";
        root.transform.parent = transform;
        _poolContainer.Add(key, root.transform);

        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var obj = GameManager.Resource.Instantiate(prefab);
                obj.gameObject.name = key;
                return obj;
            },
            actionOnGet: (GameObject obj) =>
            {
                obj.gameObject.SetActive(true);
                if (!obj.GetComponent<Transform>())
                    obj.transform.parent = SceneTransform();
            },
            actionOnRelease: (GameObject obj) =>
            {
                obj.gameObject.SetActive(false);
                obj.transform.parent = _poolContainer[key];
            },
            actionOnDestroy: (GameObject obj) =>
            {
                GameManager.Resource.Destroy(obj);
            }
        );
        _poolDic.Add(key, pool);
    }
}