using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Dictionary<string, Object> resourceCache = new Dictionary<string, Object>();

    public T LoadAsset<T>(string path) where T : Object
    {
        if (resourceCache.ContainsKey(path))
        {
            return resourceCache[path] as T;
        }

        T asset = Resources.Load<T>(path);
        if(asset == null)
        {
            Debug.LogError($"Failed to load asset at path : {path}");
            return null;
        }

        resourceCache.Add(path, asset);

        return asset;
    }
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
}
