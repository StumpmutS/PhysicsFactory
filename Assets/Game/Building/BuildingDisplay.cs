public class BuildingDisplay : SelectableDisplay<Building>
{
    protected override void SetupSelectionDisplay(Building building)
    {
        GameMenuManager.Instance.ActivatePriorityMenu(EGameMenuType.Building);
    }
    
    protected override void RemoveSelectionDisplay()
    {
        GameMenuManager.Instance.DeactivateMenu(EGameMenuType.Building);
    }
}