using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentGatherer<T> : MonoBehaviour where T : Component
{
    [SerializeField] private List<T> startingComponents;

    protected HashSet<T> _components = new();

    private void Awake()
    {
        _components = GetComponents<T>().ToHashSet();
        
        if (startingComponents == null) return;
        foreach (var startingComponent in startingComponents)
        {
            _components.Add(startingComponent);
        }
    }
}