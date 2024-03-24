using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class Resource : MonoBehaviour
{
    private ResourceData _data;
    public ResourceData Data
    {
        get => _data;
        set
        {
            if (value == _data) return;

            _data = value;
            OnResourceDataChanged.Invoke(_data);
        }
    }

    public UnityEvent<ResourceData> OnResourceDataChanged = new();

    public void Init(ResourceData data)
    {
        Data = data;
    }
}