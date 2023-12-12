using System;

[Serializable]
public class SaveData
{
    public SupplySaveInfo SupplyInfo;
    //public SerializableDictionary<string, BuildingSaveInfo> PlacedBuildings;

    public SaveData()
    {
        SupplyInfo = new SupplySaveInfo();
    }
}