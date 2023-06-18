using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defaults/Colors")]
public class ColorInfo : ScriptableObject
{
    [SerializeField] private List<Color> colors;
    public List<Color> Colors => colors;
}