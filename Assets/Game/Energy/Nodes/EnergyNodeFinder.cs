using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnergyNodeFinder : MonoBehaviour
{
    [SerializeField] private float startingRange;
    [FormerlySerializedAs("maxRange")] [SerializeField] private float startingMaxRange;
    
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 offset;

    private float _maxRange;
    public float MaxRange
    {
        get => _maxRange;
        set
        {
            if (value == _maxRange) return;
            _maxRange = value;
            OnMaxRangeUpdated.Invoke(_maxRange);
        }
    }
    private float _range;
    public float Range
    {
        get => _range;
        set
        {
            value = Mathf.Clamp(value, 0, MaxRange);
            if (value == _range) return;
            _range = value;
            OnRangeUpdated.Invoke(_range);
        }
    }
    
    public List<EnergyNode> Nodes => NodeFinderHelper.FindNodesInRange(_range, transform, offset, layerMask);

    public UnityEvent<float> OnRangeUpdated = new();
    public UnityEvent<float> OnMaxRangeUpdated = new();

    private void Awake()
    {
        _range = Mathf.Clamp(startingRange, 0, startingMaxRange);
    }
}