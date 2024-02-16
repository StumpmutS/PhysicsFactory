using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class EnergyGeneratorDisplay : SelectableDisplay<EnergyGenerator>
{
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text text;
    
    protected override void SetupSelectionDisplay(EnergyGenerator generator)
    {
        container.SetActive(true);
        SetText(generator.ChargeGenerated);
    }
    
    private void SetText(float charge)
    {
        text.text = $"Charge Generated: {charge:F2}";
    }

    protected override void RemoveSelectionDisplay()
    {
        container.SetActive(false);
    }
}