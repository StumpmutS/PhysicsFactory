using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public class UpgradeData : ScriptableObject
{
    [SerializeField] private UpgradeInfo info;
    public UpgradeInfo Info => info;
}