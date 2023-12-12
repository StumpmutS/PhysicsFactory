using System.IO;
using UnityEngine;

public class LocalSaveHandler
{
    private string _fileName;
    private string _filePath;

    public LocalSaveHandler(string fileName, string filePath)
    {
        _fileName = fileName;
        _filePath = filePath;
    }
    
    public void Save(SaveData data)
    {
        var json = JsonUtility.ToJson(data, true);
        Directory.CreateDirectory(_filePath);
        var fullPath = Path.Combine(_filePath, _fileName);
        
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
        var fullPath = Path.Combine(_fileName, _filePath);
        if (!File.Exists(fullPath))
        {
            Debug.LogError($"Could not find file at path {fullPath}");
            return new SaveData();
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