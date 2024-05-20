using System.IO;
using UnityEngine;

public static class SystemPath
{
    public static string GetPath(string fileName)
    {
        string path = GetPath();
        return Path.Combine(path, fileName);
    }

    public static string GetPath()
    {
        string path = null;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(Application.persistentDataPath, "Contents/");
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Assets", "Contents/");
            case RuntimePlatform.WindowsEditor:
                path = Application.dataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Assets", "Contents/");
            default:
                path = Application.dataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, "Contents/");
        }
    }
}
