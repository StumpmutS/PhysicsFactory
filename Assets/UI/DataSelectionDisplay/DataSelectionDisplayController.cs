using System.Collections.Generic;
using UnityEngine;

public abstract class DataSelectionDisplayController<TData> : MonoBehaviour
{
    [SerializeField] private DataService<IEnumerable<TData>> service;
    [SerializeField] private CallbackToggle togglePrefab;
    [SerializeField] private ContextualUIObjectData toggleContextualData;
    [SerializeField] private LayoutDisplay toggleLayoutDisplay;

    public void Display()
    {
        toggleLayoutDisplay.Clear();
        PreDisplay();
        
        foreach (var data in service.RequestData())
        {
            toggleLayoutDisplay.AddPrefab(togglePrefab,
                toggle =>
                {
                    toggle.Init(new CallbackToggleData(HandleToggle, data, DataSelected(data)));
                    ContextualUIObjectBuilder.BuildObject(toggle.gameObject, toggleContextualData, GenerateContext(data));
                });
        }
    }

    protected abstract ContextData GenerateContext(TData data);

    protected abstract bool DataSelected(TData data);

    protected abstract void HandleToggle(object data, bool value);

    protected virtual void PreDisplay() { }
}