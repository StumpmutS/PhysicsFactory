using UnityEngine;
using Utility.Scripts;
using Utility.Scripts.Extensions;

[CreateAssetMenu(menuName = "Defaults/MaterialPairs")]
public class MaterialPairs : ScriptableObject
{
    [SerializeField, Tooltip("ZWrite On first, ZWrite off second")] private SerializableDictionary<Material, Material> materials;

    public bool TryGetZWriteOnMaterial(Material material, out Material foundMaterial)
    {
        return materials.TryGetKeyFromValue(material, out foundMaterial);
    }

    public bool TryGetZWriteOffMaterial(Material material, out Material foundMaterial)
    {
        return materials.TryGetValue(material, out foundMaterial);
    }

    public bool IsPair(Material materialA, Material materialB)
    {
        TryGetZWriteOffMaterial(materialA, out var foundB);
        TryGetZWriteOffMaterial(materialB, out var foundA);

        return foundA == materialA || foundB == materialB;
    }
}