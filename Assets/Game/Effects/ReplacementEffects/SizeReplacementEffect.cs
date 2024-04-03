using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Replacement/Size")]
public class SizeReplacementEffect : ReplacementEffect<Vector3, SizeReplacer>
{
    [SerializeField] private Vector3 size;
    protected override Vector3 Data => size;
}