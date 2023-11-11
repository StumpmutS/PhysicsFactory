public class PreviewNodeHighlighter : NodeHighlighter
{
    private void Update()
    {
        Activate();
    }

    private void OnDestroy()
    {
        Deactivate();
    }
}