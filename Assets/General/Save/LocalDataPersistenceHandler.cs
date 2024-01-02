using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LocalDataPersistenceHandler
{
    public static void SaveTo(object data, string dirId, LocalPathData pathData)
    {
        var json = JsonUtility.ToJson(data, true);
        var dirPath = Path.Combine(PathHelpers.FullDirectoryPath(pathData.DirectoryPath), dirId);
        Directory.CreateDirectory(dirPath);
        var fullPath = Path.Combine(dirPath, pathData.DefaultLocalFileName);
        
        using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(json);
            }
        }
        
        Debug.Log($"Data saved, path: {fullPath}\ndata:\n{json}");
    }

    public static IEnumerable<T> GetPathJsonData<T>(LocalPathData pathData)
    {
        var fullDirPath = PathHelpers.FullDirectoryPath(pathData.DirectoryPath);
        Directory.CreateDirectory(fullDirPath);
        
        foreach (var info in new DirectoryInfo(fullDirPath).EnumerateDirectories())
        {
            var id = info.Name;
            var filePath = Path.Combine(fullDirPath, id, pathData.DefaultLocalFileName);
            if (!File.Exists(filePath))
            {
                Debug.LogError($"Could not find file at path {filePath}");
                continue;
            }
            
            string json;
            
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
        
            Debug.Log($"Data loaded, path: {filePath}\ndata:\n{json}");

            yield return JsonUtility.FromJson<T>(json);
        }
    }
}