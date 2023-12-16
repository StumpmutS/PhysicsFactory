using System;

[Serializable]
public class LevelData
{
    public LevelInfo LevelInfo;
    public SupplySaveInfo SupplyInfo;

    public LevelData()
    {
        LevelInfo = new LevelInfo();
        SupplyInfo = new SupplySaveInfo();
    }
}