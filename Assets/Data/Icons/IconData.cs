using UnityEngine;

[CreateAssetMenu(menuName = "Defaults/IconData")]
public class IconData : ScriptableObject
{
    [SerializeField] private IconInfo icon;
    public IconInfo Icon => icon;
}
