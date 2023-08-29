using UnityEngine;

public class EnergyNodeAutoConnector : MonoBehaviour
{
    [SerializeField] private EnergyNode node;
    [SerializeField] private Vector3 range;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask layerMask;
    
    private void Start()
    {
        BuildingManager.Instance.OnBuildingAdded.AddListener(HandleBuildingAdded);
        ConnectAllInRange();
    }

    private void HandleBuildingAdded()
    {
        ConnectAllInRange();
    }

    private void ConnectAllInRange()
    {
        var colliders = new Collider[100];
        Physics.OverlapBoxNonAlloc(offset + transform.position, range, colliders, Quaternion.identity, layerMask);
        foreach (var collider in colliders)
        {
            if (collider == null || !collider.TryGetComponent<EnergyNode>(out var other)) continue;

            node.TryConnect(other);
        }
    }
}