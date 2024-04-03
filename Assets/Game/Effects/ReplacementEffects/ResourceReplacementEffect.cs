using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Replacement/Resource")]
public class ResourceReplacementEffect : ReplacementEffect<ResourceData, ResourceReplacer>
{
    [SerializeField] private ResourceData resourceData;
    protected override ResourceData Data => resourceData;
}