using System.IO;
using UnityEngine;

public class LocalSaveHandler
{
    private string _defaultFileName;
    private string _storagePath;

    public LocalSaveHandler(string defaultFileName, string storagePath)
    {
        _defaultFileName = defaultFileName;
        _storagePath = storagePath;
    }
    
    public void Save(SaveData data)
    {
        var json = JsonUtility.ToJson(data, true);
        var dirPath = Path.Combine(_storagePath, data.SaveInfo.Id);
        Directory.CreateDirectory(dirPath);
        var fullPath = Path.Combine(dirPath, _defaultFileName);
        
        using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(json);
            }
        }
        
        Debug.Log($"Data saved, path: {fullPath}\ndata:\n{json}");
    }

    public SaveData Load()
    {
        var fullPath = Path.Combine(_defaultFileName, _storagePath);
        if (!File.Exists(fullPath))
        {
            Debug.LogError($"Could not find file at path {fullPath}");
            return null;
        }
        
        string json;
        
        using (var stream = new FileStream(fullPath, FileMode.Open))
        {
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
        }
        
        Debug.Log($"Data loaded, path: {fullPath}\ndata:\n{json}");

        return JsonUtility.FromJson<SaveData>(json);
    }
}