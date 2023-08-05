using Utility.Scripts;

public class BuildingDisplay : SelectableDisplay<Building>
{
    protected override void SetupSelectionDisplay(Selectable selectable, Building building)
    {
        GameMenuManager.Instance.ActivatePriorityMenu(EGameMenuType.Building);
    }
    
    protected override void RemoveSelectionDisplay(Selectable selectable)
    {
        GameMenuManager.Instance.DeactivateMenu(EGameMenuType.Building);
    }
}