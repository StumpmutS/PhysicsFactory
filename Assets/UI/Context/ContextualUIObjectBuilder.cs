using UnityEngine;
using Utility.Scripts.Extensions;

public static class ContextualUIObjectBuilder
{
    public static void BuildObject(GameObject gameObject, ContextualUIObjectData contextualObjectData, ContextData contextData = null)
    {
        if (contextData != null) SetContext(gameObject, contextData);
        if (contextualObjectData.HoverDisplay) BuilderHoverRelay(gameObject, contextualObjectData.HoverTime);
        if (contextualObjectData.LabelDriven && contextualObjectData.SummaryDriven) BuildLabelSummaryService(gameObject, contextualObjectData.LabelSummarySeparator);
        else if (contextualObjectData.LabelDriven) BuildLabelService(gameObject);
        else if (contextualObjectData.SummaryDriven) BuildSummaryService(gameObject);
    }

    private static void SetContext(GameObject gameObject, ContextData contextData)
    {
        var container = gameObject.AddOrGetComponent<ContextDataContainer>();
        container.SetData(contextData);
    }

    private static void BuilderHoverRelay(GameObject gameObject, float hoverTime)
    {
        gameObject.AddOrGetComponent<UIHoverable>();
        var hoverRelay = gameObject.AddOrGetComponent<ContextPanelHoverRelay>();
        var hoverEvent = gameObject.AddOrGetComponent<TimedHoverEvent>();
        hoverEvent.Init(hoverTime);
        hoverEvent.OnWaitFinished.AddListener(hoverRelay.Display);
    }

    private static void BuildLabelService(GameObject gameObject)
    {
        gameObject.AddOrGetComponent<ContextLabelService>();
        BuildLabelDriver(gameObject);
    }

    private static void BuildSummaryService(GameObject gameObject)
    {
        gameObject.AddOrGetComponent<ContextSummaryService>();
        BuildLabelDriver(gameObject);
    }

    private static void BuildLabelSummaryService(GameObject gameObject, string separator)
    {
        var service = gameObject.AddOrGetComponent<ContextLabelSummaryService>();
        service.Init(separator);
        BuildLabelDriver(gameObject);
    }
    
    private static void BuildLabelDriver(GameObject gameObject)
    {
        gameObject.AddOrGetComponent<LabelDriver>();
    }
}