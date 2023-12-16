using System;

[Serializable]
public class SaveDisplayInfo
{
    public string Name;
    public string Type;
    public SerializableDateTime DateTime;
    
    public SaveDisplayInfo(string name, string saveType, SerializableDateTime dateTime)
    {
        Name = name;
        Type = saveType;
        DateTime = dateTime;
    }
}