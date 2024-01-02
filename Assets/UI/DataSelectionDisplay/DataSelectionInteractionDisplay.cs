using UnityEngine;
using UnityEngine.Events;

public abstract class DataSelectionInteractionDisplay<TDisplayData> : MonoBehaviour
{
    public UnityEvent<TDisplayData> OnDataInteracted = new();

    private TDisplayData _displayData;
    
    public void Init(TDisplayData displayData)
    {
        _displayData = displayData;
        Display(_displayData);
    }

    protected abstract void Display(TDisplayData displayData);

    public void Interact()
    {
        OnDataInteracted.Invoke(_displayData);
    }
}