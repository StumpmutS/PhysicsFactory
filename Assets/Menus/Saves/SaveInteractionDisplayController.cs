using UnityEngine.Events;

public class SaveInteractionDisplayController : DataSelectionInteractionDisplayController<SaveDisplayData>
{
    public UnityEvent<SaveData> OnDataInteracted = new();

    protected override ContextData GenerateContext(SaveDisplayData data)
    {
        var info = data.SaveData.SaveInfo;
        return new ContextData($"{info.Name} - {info.DateTime}");
    }

    protected override bool DataSelected(SaveDisplayData _) => false;

    protected override void HandleDataInteraction(SaveDisplayData displayData)
    {
        OnDataInteracted.Invoke(displayData.SaveData);
    }
}