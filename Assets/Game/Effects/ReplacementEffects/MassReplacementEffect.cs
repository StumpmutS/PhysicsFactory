using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Replacement/Mass")]
public class MassReplacementEffect : ReplacementEffect<float, MassReplacer>
{
    [SerializeField] private float mass;
    protected override float Data => mass;
}