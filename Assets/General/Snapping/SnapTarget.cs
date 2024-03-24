using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts.Extensions;

public class SnapTarget : MonoBehaviour
{
    [SerializeField] private ESnapType snapType;
    [SerializeField] private Transform mainTransform;

    public UnityEvent OnSnap = new();
    
    public bool TrySnap(ESnapType otherType, Transform snappable, Vector3 snapPoint)
    {
        if (!CanSnap(otherType)) return false;
        
        Snap(snappable, snapPoint);
        return true;
    }

    public bool CanSnap(ESnapType otherType) => snapType == otherType;

    private void Snap(Transform snappable, Vector3 snapPoint)
    {
        var extents = (mainTransform.parent.rotation * mainTransform.localScale / 2).Abs();
        var snapDirection = snapPoint - mainTransform.position;
        var weightedDir = Vector3.Scale(snapDirection, extents.Invert());
        var face = weightedDir.IsolateAxis().normalized;

        var snapOffset = Vector3.Scale(face, extents) + face * snappable.localScale.z / 2;
        // Only includes face axis
        var snapPos = Vector3.Scale(mainTransform.position + snapOffset, face.Abs());
        // Only includes non-face axes
        var cellPos = Vector3.Scale(Grid3D.ConvertToCellPosition(snapPoint), Vector3.one - face.Abs());
        
        snappable.rotation = Quaternion.LookRotation(face);
        snappable.position = snapPos + cellPos;

        OnSnap.Invoke();
    }
}