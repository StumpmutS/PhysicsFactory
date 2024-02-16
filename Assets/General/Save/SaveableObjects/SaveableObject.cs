using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveableObject : MonoBehaviour, ISaveable<LevelData>, ILoadable<SaveableObjectSaveData>
{
    [SerializeField] private bool sceneObject;
    [SerializeField, ShowIf(nameof(sceneObject), true)] private SaveableObjectIdManager idManager;
    [SerializeField, ShowIf(nameof(sceneObject), true)] private int startId = -1;
    [SerializeField] private Transform mainTransform;
    
    public int Id { get; private set; } = -1;

    private void Awake()
    {
        if (!sceneObject) return;
        
        Id = startId;
        idManager.IdentifyObject(this, Id);
    }

    private void Start()
    {
        if (Id == -1) Id = SaveableObjectIdManager.Instance.AddObject(this);
    }

    private void OnDestroy()
    {
        SaveableObjectIdManager.Instance.RemoveObject(this);
    }

    public void Save(LevelData data, AssetRefCollection assetRefCollection)
    {
        var saveableObjectSaveData = new SaveableObjectSaveData(Id);
        SaveHelpers.GroupSave(mainTransform.GetComponentsInChildren<ISaveable<SaveableObjectSaveData>>(),
            saveableObjectSaveData, assetRefCollection);
        
        data.SaveableObjectSaveData ??= new List<SaveableObjectSaveData>();
        data.SaveableObjectSaveData.Add(saveableObjectSaveData);
    }

    public LoadingInfo Load(SaveableObjectSaveData data, AssetRefCollection _)
    {
        Id = data.Id;
        SaveableObjectIdManager.Instance.IdentifyObject(this, Id);

        return LoadingInfo.Completed(Id, ELoadCompletionStatus.Succeeded);
    }
}