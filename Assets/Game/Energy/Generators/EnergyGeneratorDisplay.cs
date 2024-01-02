using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class EnergyGeneratorDisplay : SelectableDisplay<EnergyGenerator>
{
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text text;

    public UnityEvent<SignedFloat> OnChange;
    
    protected override void SetupSelectionDisplay(EnergyGenerator generator)
    {
        container.SetActive(true);
        SetText(generator.EnergyGenerated);
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