using UnityEngine;

public class ContextDataContainer : DataService<ContextData>
{
    [SerializeField] private ContextData startingData;
    
    private ContextData _data;

    private void Awake()
    {
        SetData(startingData);
    }
    
    public override ContextData RequestData() => _data;
    
    public void SetData(ContextData contextData)
    {
        if (_data == contextData) return;
        
        _data = contextData;
        OnUpdated.Invoke(_data);
    }
}
