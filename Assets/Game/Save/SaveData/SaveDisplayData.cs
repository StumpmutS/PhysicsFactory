using System;

[Serializable]
public class SaveDisplayData
{
    public string Name;
    public string Type;
    public SerializableDateTime DateTime;
    
    public SaveDisplayData(string name, string saveType, SerializableDateTime dateTime)
    {
        Name = name;
        Type = saveType;
        DateTime = dateTime;
    }
}