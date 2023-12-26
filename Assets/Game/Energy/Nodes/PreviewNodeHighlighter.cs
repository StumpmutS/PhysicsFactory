using System;

public class PreviewNodeHighlighter : NodeHighlighter
{
    private void Start()
    {
        HandleActivation();
    }

    private void Update()
    {
        RefreshActivation();
    }

    private void OnDestroy()
    {
        TryDeactivate();
    }
}