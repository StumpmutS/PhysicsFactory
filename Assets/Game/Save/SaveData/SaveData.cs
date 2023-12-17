using System;

[Serializable]
public class SaveData
{
    public SaveInfo SaveInfo;
    public LevelData LevelData = new();

    public SaveData(SaveInfo saveInfo)
    {
        SaveInfo = saveInfo;
    }
}