public class ResourceReplacer : Replacer<ResourceData>
{
    protected override bool TryGetCurrentData(out ResourceData data)
    {
        data = null;
        if (!TryGetComponent<Resource>(out var resource)) return false;

        data = resource.Data;
        return true;
    }

    protected override bool TryReplace(ResourceData data)
    {
        if (!TryGetComponent<Resource>(out var resource)) return false;

        resource.Data = data;
        return true;
    }
}