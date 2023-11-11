using UnityEngine;
using Utility.Scripts;

public class SelectableSquasher : MonoBehaviour
{
    [SerializeField] private Transform mainTransform;
#pragma warning disable CS0108, CS0114
    [SerializeField] private BoxCollider collider;
#pragma warning restore CS0108, CS0114

    private Vector3 _defaultSize;
    private Vector3 _defaultCenter;
    
    private void Awake()
    {
        _defaultSize = collider.size;
        _defaultCenter = collider.center;
    }

    public void Squash(int yLevel)
    {
        YLevelConverter.Instance.WorldMinMax(yLevel, yLevel + 1, out var min, out var max);
        
        var adjustedScale = mainTransform.rotation * mainTransform.localScale;
        if (max > mainTransform.position.y + adjustedScale.y / 2 ||
            min < mainTransform.position.y - adjustedScale.y / 2)
        {
            collider.enabled = false;
            return;
        }
        collider.enabled = true;

        //move count
        var offset = min + (max - min) / 2f - mainTransform.position.y;
        //move increment
        var moveAmount = new Vector3(_defaultCenter.x, _defaultSize.y / adjustedScale.y * offset, _defaultCenter.z);
        var absAdjustedMoveAmount = (mainTransform.rotation * moveAmount).Abs();
        collider.center = _defaultCenter + absAdjustedMoveAmount * Mathf.Sign(offset);
        
        //find required collider size by dividing by world y scale
        var colliderAdjustedSize = new Vector3(_defaultSize.x, _defaultSize.y / adjustedScale.y, _defaultSize.z);
        //undo the undo of the rotation
        var colliderUnadjustedSize = Quaternion.Inverse(mainTransform.rotation) * colliderAdjustedSize;
        collider.size = colliderUnadjustedSize.Abs();
    }

    public void Unsquash()
    {
        collider.enabled = true;
        collider.size = _defaultSize;
        collider.center = _defaultCenter;
    }
}