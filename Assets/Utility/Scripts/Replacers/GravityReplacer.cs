using UnityEngine;

public class GravityReplacer : Replacer<bool>
{
    protected override bool TryGetCurrentData(out bool data)
    {
        data = false;
        if (!TryGetComponent<Rigidbody>(out var rb)) return false;

        data = rb.useGravity;
        return true;
    }

    protected override bool TryReplace(bool data)
    {
        if (!TryGetComponent<Rigidbody>(out var rb)) return false;

        rb.useGravity = data;
        return true;
    }
}