using UnityEngine;

public class SelectedMenuDisplay : SelectableDisplay<Component>
{
    protected override void SetupSelectionDisplay(Component _)
    {
        GameMenuManager.Instance.ActivatePriorityMenu(EGameMenuType.Selected);
    }
    
    protected override void RemoveSelectionDisplay()
    {
        GameMenuManager.Instance.DeactivateMenu(EGameMenuType.Selected);
    }
}