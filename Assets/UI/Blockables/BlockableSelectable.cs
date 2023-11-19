public class BlockableSelectable : ComponentGatherer<UnityEngine.UI.Selectable>, IUIBlockable
{
    public void Block(UIBlockableInfo info)
    {
        foreach (var selectable in _components)
        {
            selectable.interactable = false;
        }
    }
 
    public void Unblock()
    {
        foreach (var selectable in _components)
        {
            selectable.interactable = true;
        }
    }
}