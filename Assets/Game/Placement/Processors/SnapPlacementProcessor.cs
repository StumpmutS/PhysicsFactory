using UnityEngine;
using Utility.Scripts;

[CreateAssetMenu(menuName = "Placement/Processors/Snap")]
public class SnapPlacementProcessor : PlacementProcessor
{
    [SerializeField] private ESnapType snapType;
    [SerializeField] private LayerMask snapLayer;
    [SerializeField] private float maxSearchDistance = 200f;
    [SerializeField] private Vector3 defaultDirection = Vector3.forward;

    private static readonly RaycastHit[] Hits = new RaycastHit[256];
    
    public override void Process(PlacementProcessingData data)
    {
        var ray = MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition);
        var found = Physics.RaycastNonAlloc(ray, Hits, maxSearchDistance, snapLayer);

        for (int i = 0; i < found; i++)
        {
            var hit = Hits[i];

            if (!hit.collider.TryGetComponent<SnapTarget>(out var target) ||
                !target.TrySnap(snapType, data.Preview.transform, hit.point)) continue;
            
            data.Position = data.Preview.transform.position;
            return;
        }

        data.Preview.transform.position = data.Position;
        data.Preview.transform.forward = defaultDirection;
    }
}