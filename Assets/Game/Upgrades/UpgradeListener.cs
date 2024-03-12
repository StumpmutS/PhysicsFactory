using UnityEngine;

[RequireComponent(typeof(Upgradeable))]
public abstract class UpgradeListener : MonoBehaviour
{
    private Upgradeable _upgradeable;

    private void Awake()
    {
        _upgradeable = GetComponent<Upgradeable>();
        _upgradeable.OnUpgrade.AddListener(Refresh);
        _upgradeable.OnDowngrade.AddListener(Refresh);
        _upgradeable.OnRefresh += Refresh;
    }

    private void Refresh()
    {
        HandleUpgradeLevel(_upgradeable.Level);
    }

    protected abstract void HandleUpgradeLevel(int level);

    private void OnDestroy()
    {
        if (_upgradeable != null) _upgradeable.OnUpgrade.RemoveListener(Refresh);
        if (_upgradeable != null) _upgradeable.OnDowngrade.RemoveListener(Refresh);
        if (_upgradeable != null) _upgradeable.OnRefresh -= Refresh;
    }
}