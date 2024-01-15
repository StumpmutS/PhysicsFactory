using UnityEngine;

public class SaveDisplayToggle : DisplayCallbackToggle<SaveDisplayData>
{
    [SerializeField] private ContextDataContainer contextContainer;

    protected override void DisplayData(SaveDisplayData displayData)
    {
        var info = displayData.SaveData.SaveInfo;
        contextContainer.SetData(new ContextData($"{info.Name} - {info.DateTime}"));
    }
}