using System;

[Serializable]
public class SaveData
{
    public SaveInfo SaveInfo;
    public LevelData LevelData;

    public SaveData(SaveInfo saveInfo)
    {
        SaveInfo = saveInfo;
        LevelData = new LevelData();
    }
}