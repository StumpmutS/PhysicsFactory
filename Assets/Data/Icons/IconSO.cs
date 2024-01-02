using UnityEngine;

[CreateAssetMenu(menuName = "Defaults/IconData")]
public class IconSO : ScriptableObject
{
    [SerializeField] private IconData icon;
    public IconData Icon => icon;
}
