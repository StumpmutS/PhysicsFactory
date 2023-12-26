using System;

[Serializable]
public class ConnectionSaveData
{
    public int FromId;
    public int ToId;

    public ConnectionSaveData(int fromId, int toId)
    {
        FromId = fromId;
        ToId = toId;
    }
}