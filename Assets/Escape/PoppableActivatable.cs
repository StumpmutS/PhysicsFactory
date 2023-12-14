using UnityEngine;

public class PoppableActivatable : Activatable, IPoppable
{
    [SerializeField] private PoppableStack escapeStack;
    
    protected override void HandleActivation()
    {
        escapeStack.RegisterPoppable(this);
    }

    protected override void HandleDeactivation()
    {
        escapeStack.DeregisterPoppable(this);
    }

    public void Pop()
    {
        TryDeactivate();
    }
}
