using System.IO;
using UnityEngine;

public static class PathHelpers
{
    public static string PersistentPath => Application.persistentDataPath;
    
    public static string FullDirectoryPath(string contextPath)
    {
        return Path.Combine(PersistentPath, contextPath);
    }
}