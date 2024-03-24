using UnityEngine;

public class SizeReplacer : Replacer<Vector3>
{
    protected override bool TryGetCurrentData(out Vector3 data)
    {
        data = transform.localScale;
        return true;
    }

    protected override bool TryReplace(Vector3 data)
    {
        transform.localScale = data;
        return true;
    }
}