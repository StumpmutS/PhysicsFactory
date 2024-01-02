using System.IO;
using UnityEngine;

public static class PathHelpers
{
    public static string PersistentPath
    {
        get
        {
            #pragma warning disable CS0162
            #if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
                return Application.persistentDataPath;
            #endif
            
            #if UNITY_WEBGL
                Debug.Log("WebGL");
                return Application.persistentDataPath;
            #endif

            return Application.persistentDataPath;
            #pragma warning restore CS0162
        }
    }

    public static string FullDirectoryPath(string contextPath)
    {
        return Path.Combine(PersistentPath, contextPath);
    }
}