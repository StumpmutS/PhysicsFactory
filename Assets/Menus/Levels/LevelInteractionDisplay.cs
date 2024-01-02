using TMPro;
using UnityEngine;

public class LevelInteractionDisplay : DataSelectionInteractionDisplay<LevelDisplayData>
{
    [SerializeField] private TMP_Text nameText;
    
    protected override void Display(LevelDisplayData data)
    {
        nameText.text = data.LevelData.LevelInfo.Name;
    }
}