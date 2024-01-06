using UnityEngine;
using UnityEngine.Serialization;

public class Extractor : Spawner<Resource>
{
    [SerializeField] private Transform mainTransform;
    [SerializeField] private LayerMask placementCollisionLayer;

    protected override Resource SpawnedPrefab => _extractionData.Prefab;
    
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

            _extractionData = extractable.Extract();
            return;
        }
    }

    protected override void InitCallback(Resource obj)
    {
        obj.Init(_extractionData.Data);
    }
}