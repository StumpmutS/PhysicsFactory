using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Replacement/MaterialColor")]
public class MaterialColorReplacementEffect : ReplacementEffect<Color, MaterialColorReplacer>
{
    [SerializeField, ColorUsage(true, true)] private Color color;
    protected override Color Data => color;
}