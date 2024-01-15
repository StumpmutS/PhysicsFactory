using TMPro;
using UnityEngine;

public class LevelDisplayToggle : DisplayCallbackToggle<LevelDisplayData>
{
    [SerializeField] private ContextDataContainer contextContainer;

    protected override void DisplayData(LevelDisplayData displayData)
    {
        var info = displayData.LevelData.LevelInfo;
        contextContainer.SetData(new ContextData(info.Name));
    }
}