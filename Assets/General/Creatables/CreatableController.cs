using System;
using UnityEngine;

public class CreatableController : MonoBehaviour
{
    [SerializeField] private Transform containerTransform;

    private IdentityDictionary<Creatable, Creatable, object> _creatables = new();
    
    public Creatable Create(Creatable creatablePrefab, object caller)
    {
        var created = creatablePrefab.Create(containerTransform);
        _creatables[creatablePrefab] = new Tuple<Creatable, object>(created, caller);
        return created;
    }
    
    public Creatable Create(Creatable creatablePrefab, object caller, Vector3 position, Quaternion rotation)
    {
        var created = creatablePrefab.Create(position, rotation, containerTransform);
        _creatables[creatablePrefab] = new Tuple<Creatable, object>(created, caller);
        return created;
    }
    
    public void Dispose(Creatable creatablePrefab, object caller)
    {
        if (!_creatables.TryGetValue(creatablePrefab, caller, out var created)) return;
        
        if (created != null) created.Dispose();
        _creatables.Remove(creatablePrefab, caller);
    }
}