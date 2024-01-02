using UnityEngine.Events;

public class SaveInteractionDisplayController : DataSelectionInteractionDisplayController<SaveDisplayData>
{
    public UnityEvent<SaveData> OnDataInteracted = new();

    protected override bool DataSelected(SaveDisplayData _) => false;

    protected override void HandleDataInteraction(SaveDisplayData displayData)
    {
        OnDataInteracted.Invoke(displayData.SaveData);
    }
}