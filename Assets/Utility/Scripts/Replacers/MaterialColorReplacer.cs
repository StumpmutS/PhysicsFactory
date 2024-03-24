using UnityEngine;

public class MaterialColorReplacer : Replacer<Color>
{
    private static readonly int Color = Shader.PropertyToID("_Color");

    protected override bool TryGetCurrentData(out Color data)
    {
        data = default;
        if (!TryGetComponent<MaterialManager>(out var materialManager)) return false;

        data = materialManager.MaterialInstance.GetColor(Color);
        return true;
    }

    protected override bool TryReplace(Color data)
    {
        if (!TryGetComponent<MaterialManager>(out var materialManager)) return false;

        materialManager.ModifyMaterialPersistent(Color, 
            (id, mat) => mat.SetColor(id, data), this);
        return true;
    }
}