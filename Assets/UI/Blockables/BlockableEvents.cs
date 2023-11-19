using UnityEngine.EventSystems;

public class BlockableEvents : ComponentGatherer<EventTrigger>, IUIBlockable
{
    public void Block(UIBlockableInfo info)
    {
        foreach (var eventTrigger in _components)
        {
            eventTrigger.enabled = false;
        }
    }
    
    public void Unblock()
    {
        foreach (var eventTrigger in _components)
        {
            eventTrigger.enabled = true;
        }
    }
}