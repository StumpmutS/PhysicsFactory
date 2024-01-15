using UnityEngine;

public class ContextPanelRelay : MonoBehaviour
{
    [SerializeField] private ContextDataContainer container;

    public void Display()
    {
        ContextPanelManager.Instance.DisplayPanel(this, container.RequestData());
    }

    public void RemoveDisplay()
    {
        ContextPanelManager.Instance.RemoveDisplay(this);
    }
}