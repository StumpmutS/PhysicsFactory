using System.Globalization;
using TMPro;
using UnityEngine;

public class SaveInteractionDisplay : DataSelectionInteractionDisplay<SaveDisplayData>
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text saveTypeText;
    [SerializeField] private TMP_Text dateTimeText;
    
    protected override void Display(SaveDisplayData data)
    {
        nameText.text = data.SaveData.SaveInfo.Name;
        saveTypeText.text = $"{data.SaveData.SaveInfo.SaveType} Save";
        dateTimeText.text = data.SaveData.SaveInfo.DateTime.ToString(CultureInfo.InvariantCulture);
    }
}