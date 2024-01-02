using TMPro;
using UnityEngine;

public class LevelDisplayToggle : DisplayCallbackToggle<LevelDisplayData>
{
    [SerializeField] private TMP_Text text;

    protected override void DisplayData(LevelDisplayData displayData)
    {
        var info = displayData.LevelData.LevelInfo;
        text.text = $"{info.Name}";
    }
}