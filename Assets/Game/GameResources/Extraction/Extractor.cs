using System;
using UnityEngine;
using Utility.Scripts.Extensions;

public class Extractor : Spawner<Resource>, ISaveable<SaveableObjectSaveData>, ILoadable<ExtractorSaveData>
{
    [SerializeField] private Transform mainTransform;
    [SerializeField] private LayerMask placementCollisionLayer;

    protected override Resource SpawnedPrefab => _extractionData.Prefab;

    private Extractable _extractable;
    private ExtractionData _extractionData;
    
    private static readonly Collider[] Colliders = new Collider[1];
    
    private void Start()
    {
        var offset = -mainTransform.forward * (mainTransform.localScale.z / 2 + .5f);
        var searchPosition = mainTransform.position + offset;
        var found = Physics.OverlapSphereNonAlloc(searchPosition, .1f, Colliders, placementCollisionLayer);
        if (found < 1)
        {
            Debug.LogWarning("Extractor could not find extraction site");
            return;
        }

        for (int i = 0; i < found; i++)
        {
            if (!Colliders[i].TryGetComponent<Extractable>(out var extractable)) continue;

            ExtractFrom(extractable);
            return;
        }
    }

    protected override void InitCallback(Resource resource)
    {
        resource.Init(_extractionData.Data);
    }

    private void ExtractFrom(Extractable extractable)
    {
        _extractable = extractable;
        _extractionData = _extractable.Extract();
    }

    public void Save(SaveableObjectSaveData data, AssetRefCollection assetRefCollection)
    {
        data.ExtractorSaveData ??= new ExtractorSaveData();
        
        if (_extractable.TryGetComponent<SaveableObject>(out var saveableObject))
        {
            data.ExtractorSaveData.ExtractableId = saveableObject.Id;
        }
    }

    public LoadingInfo Load(ExtractorSaveData data, AssetRefCollection assetRefCollection)
    {
        if (SaveableObjectIdManager.Instance.TryGet(data.ExtractableId, out var obj))
        {
            if (obj.TryGetComponent<Extractable>(out var extractable))
            {
                ExtractFrom(extractable);
                return LoadingInfo.Completed(data, ELoadCompletionStatus.Succeeded);
            }
        }

        var exception = new Exception($"Issue loading extractable from referenced save object ID: {data.ExtractableId}");
        return LoadingInfo.Completed(data, ELoadCompletionStatus.Failed, exception);
    }
}
