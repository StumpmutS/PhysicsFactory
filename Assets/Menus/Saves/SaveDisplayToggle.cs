using TMPro;
using UnityEngine;

public class SaveDisplayToggle : DisplayCallbackToggle<SaveDisplayData>
{
    [SerializeField] private TMP_Text text;

    protected override void DisplayData(SaveDisplayData displayData)
    {
        var info = displayData.SaveData.SaveInfo;
        text.text = $"{info.Name} - {info.DateTime}";
    }
}