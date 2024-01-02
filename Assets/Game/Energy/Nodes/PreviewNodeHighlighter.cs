using System;

public class PreviewNodeHighlighter : NodeHighlighter
{
    private void Start()
    {
        TryActivate();
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