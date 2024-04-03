using System;
using UnityEngine;

public class CreatableController : MonoBehaviour
{
    [SerializeField] private Transform containerTransform;

    private IdentityDictionary<Creatable, Creatable, object> _creatables = new();
    
    public void Create(Creatable creatablePrefab, object caller)
    {
        _creatables[creatablePrefab] = new Tuple<Creatable, object>(creatablePrefab.Create(containerTransform), caller);
    }

    public void Dispose(Creatable creatablePrefab, object caller)
    {
        if (!_creatables.TryGetValue(creatablePrefab, caller, out var created)) return;
        
        created.Dispose();
        _creatables.Remove(creatablePrefab, caller);
    }
}