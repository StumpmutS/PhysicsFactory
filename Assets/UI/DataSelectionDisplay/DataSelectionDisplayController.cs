using UnityEngine;

public abstract class DataSelectionDisplayController<TData> : MonoBehaviour
{
    [SerializeField] private DataService<TData> service;
    [SerializeField] private DisplayCallbackToggle<TData> displayTogglePrefab;
    [SerializeField] private LayoutDisplay toggleLayoutDisplay;

    public void Display()
    {
        toggleLayoutDisplay.Clear();
        PreDisplay();
        
        foreach (var data in service.RequestData())
        {
            toggleLayoutDisplay.AddPrefab(displayTogglePrefab,
                toggle => toggle.Init(data, new CallbackToggleData(HandleToggle, data, DataSelected(data))));
        }
    }

    protected abstract bool DataSelected(TData data);

    protected abstract void HandleToggle(object data, bool value);

    protected virtual void PreDisplay() { }
}