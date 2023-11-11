using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergyNodeFinder : MonoBehaviour
{
    [SerializeField] private float startingRange;
    [SerializeField] private float maxRange;
    public float MaxRange => maxRange;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 offset;
    
    private float _range;
    public float Range
    {
        get => _range;
        set
        {
            value = Mathf.Clamp(value, 0, maxRange);
            if (value == _range) return;
            _range = value;
            OnRangeUpdated.Invoke(_range);
        }
    }
    
    public List<EnergyNode> Nodes => NodeFinderHelper.FindNodesInRange(_range, transform, offset, layerMask);

    public UnityEvent<float> OnRangeUpdated;

    private void Awake()
    {
        _range = Mathf.Clamp(startingRange, 0, maxRange);
    }
}