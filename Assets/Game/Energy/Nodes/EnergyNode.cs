using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnergyNode : MonoBehaviour, ISaveable<SaveableObjectSaveData>, ILoadable<CurrentSaveData>
{
    [SerializeField] private CurrentContainer currentContainer;
    public CurrentContainer CurrentContainer => currentContainer;

    public bool TryConnect(EnergyNode other)
    {
        if (!CanConnect(other, out var sender, out var receiver)) return false;
        
        InitiateCurrent(sender, receiver);
        return true;
    }

    public abstract bool CanConnect(EnergyNode other, out CurrentContainer sender, out CurrentContainer receiver);

    public bool TryDisconnect(EnergyNode other)
    {
        bool disconnected = false;
        var currentsCopy = currentContainer.Currents.ToList();
        foreach (var current in currentsCopy)
        {
            if ((current.Sender == currentContainer && current.Receiver == other.currentContainer) ||
                (current.Sender == other.currentContainer && current.Receiver == currentContainer))
            {
                disconnected = true;
                current.ShutDown();
            }
        }

        return disconnected;
    }
    
    private void InitiateCurrent(CurrentContainer from, CurrentContainer to)
    {
        if (currentContainer.Currents.Any(current => current.Sender == from && current.Receiver == to)) return;
        var current = new EnergyCurrent(from, to);
        from.AddCurrent(current);
        to.AddCurrent(current);
    }

    public void Save(SaveableObjectSaveData data, AssetRefCollection assetRefCollection)
    {
        data.CurrentSaveData ??= new CurrentSaveData();
        data.CurrentSaveData.ConnectionSaveData ??= new List<ConnectionSaveData>();
        foreach (var current in CurrentContainer.Currents)
        {
            if (!current.Sender.TryGetComponent<SaveableObject>(out var fromObj) ||
                !current.Receiver.TryGetComponent<SaveableObject>(out var toObj)) continue;
            data.CurrentSaveData.ConnectionSaveData.Add(new ConnectionSaveData(fromObj.Id, toObj.Id));
        }
    }

    public LoadingInfo Load(CurrentSaveData data, AssetRefCollection assetRefCollection)
    {
        foreach (var connectionSaveData in data.ConnectionSaveData)
        {
            if (!SaveableObjectIdManager.Instance.TryGet(connectionSaveData.FromId, out var fromObj) ||
                !fromObj.TryGetComponent<CurrentContainer>(out var fromContainer) ||
                !SaveableObjectIdManager.Instance.TryGet(connectionSaveData.ToId, out var toObj) ||
                !toObj.TryGetComponent<CurrentContainer>(out var toContainer)) continue;
            
            InitiateCurrent(fromContainer, toContainer);
        }

        return LoadingInfo.Completed(data, ELoadCompletionStatus.Succeeded);
    }
}