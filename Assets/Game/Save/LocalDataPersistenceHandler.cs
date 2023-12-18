using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LocalDataPersistenceHandler
{
    public static void SaveTo(SaveData data, string storagePath, string defaultFileName)
    {
        var json = JsonUtility.ToJson(data, true);
        var dirPath = Path.Combine(storagePath, data.SaveInfo.Id);
        Directory.CreateDirectory(dirPath);
        var fullPath = Path.Combine(dirPath, defaultFileName);
        
        using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(json);
            }
        }
        
        Debug.Log($"Data saved, path: {fullPath}\ndata:\n{json}");
    }

    public static IEnumerable<SaveData> GetSaves(string defaultFileName, string storagePath)
    {
        foreach (var info in new DirectoryInfo(storagePath).EnumerateDirectories())
        {
            var id = info.Name;
            var saveFilePath = Path.Combine(storagePath, id, defaultFileName);
            if (!File.Exists(saveFilePath))
            {
                Debug.LogError($"Could not find file at path {saveFilePath}");
                continue;
            }
            
            string json;
            
            using (var stream = new FileStream(saveFilePath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
        
            Debug.Log($"Data loaded, path: {saveFilePath}\ndata:\n{json}");

            yield return JsonUtility.FromJson<SaveData>(json);
        }
    }
}