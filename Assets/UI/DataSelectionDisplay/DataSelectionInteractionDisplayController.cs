using UnityEngine;

public abstract class DataSelectionInteractionDisplayController<TDisplayData> : DataSelectionDisplayController<TDisplayData>
{
    [SerializeField] private DataSelectionInteractionDisplay<TDisplayData> interactionDisplayPrefab;
    [SerializeField] private LayoutDisplay interactionLayoutDisplay;

    private TDisplayData _currentData;

    protected override void PreDisplay()
    {
        interactionLayoutDisplay.Clear();
    }

    protected override void HandleToggle(object obj, bool value)
    {
        if (!value || obj is not TDisplayData displayData || displayData.Equals(_currentData)) return;

        _currentData = displayData;
        interactionLayoutDisplay.Clear();
        interactionLayoutDisplay.AddPrefab(interactionDisplayPrefab, 
            interactionDisplay =>
            {
                interactionDisplay.OnDataInteracted.AddListener(HandleDataInteraction);
                interactionDisplay.Init(displayData);
            });
    }

    protected abstract void HandleDataInteraction(TDisplayData displayData);
}