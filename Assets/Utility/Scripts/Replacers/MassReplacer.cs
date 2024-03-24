using UnityEngine;

public class MassReplacer : Replacer<float>
{
    protected override bool TryGetCurrentData(out float data)
    {
        data = -1;
        if (!TryGetComponent<Rigidbody>(out var rb)) return false;

        data = rb.mass;
        return true;
    }

    protected override bool TryReplace(float data)
    {
        if (!TryGetComponent<Rigidbody>(out var rb)) return false;

        rb.mass = data;
        return true;
    }
}