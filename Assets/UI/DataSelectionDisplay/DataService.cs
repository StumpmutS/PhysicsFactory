using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DataService<TData> : MonoBehaviour
{
    public UnityEvent<TData> OnUpdated = new();
    
    public abstract TData RequestData();
}