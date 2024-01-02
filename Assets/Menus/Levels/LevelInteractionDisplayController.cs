﻿using UnityEngine.Events;

public class LevelInteractionDisplayController : DataSelectionInteractionDisplayController<LevelDisplayData>
{
    public UnityEvent<LevelData> OnDataInteracted = new();

    protected override bool DataSelected(LevelDisplayData _) => false;

    protected override void HandleDataInteraction(LevelDisplayData displayData)
    {
        OnDataInteracted.Invoke(displayData.LevelData);
    }
}