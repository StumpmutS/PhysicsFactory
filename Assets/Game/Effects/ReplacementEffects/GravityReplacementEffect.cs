using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Replacement/Gravity")]
public class GravityReplacementEffect : ReplacementEffect<bool, GravityReplacer>
{
    
    [SerializeField] private bool useGravity;
    protected override bool Data => useGravity;
}