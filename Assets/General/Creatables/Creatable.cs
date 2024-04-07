using System;
using UnityEngine;

public abstract class Creatable : MonoBehaviour
{
    public abstract Creatable Create();
    
    public abstract Creatable Create(Transform parent);
    
    public abstract Creatable Create(Transform parent, bool instantiateInWorldSpace);
    
    public abstract Creatable Create(Vector3 position, Quaternion rotation);
    
    public abstract Creatable Create(Vector3 position, Quaternion rotation, Transform parent);

    public abstract void Dispose();
}