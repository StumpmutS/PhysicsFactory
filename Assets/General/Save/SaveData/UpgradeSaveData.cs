using System;
using System.Collections.Generic;

[Serializable]
public class UpgradeSaveData
{
    public int Level;

    public UpgradeSaveData(int level)
    {
        Level = level;
    }
}