using System.Collections.Generic;
using UnityEngine;

public abstract class DataService<TData> : MonoBehaviour
{
    public abstract IEnumerable<TData> RequestData();
}