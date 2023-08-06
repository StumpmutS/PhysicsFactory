using TMPro;
using UnityEngine;

public class EnergyGeneratorDisplay : SelectableDisplay<EnergyGenerator>
{
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text text;

    protected override void SetupSelectionDisplay(Selectable selectable, EnergyGenerator generator)
    {
        container.SetActive(true);
        SetText(generator.EnergyGenerated);
    }

    private void SetText(float charge)
    {
        text.text = $"Charge Generated: {charge:F2}";
    }

    protected override void RemoveSelectionDisplay(Selectable selectable)
    {
        container.SetActive(false);
    }
}