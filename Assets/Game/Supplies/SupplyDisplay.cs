using TMPro;
using UnityEngine;

public class SupplyDisplay : MonoBehaviour
{
    [SerializeField] private ScrollingNumberText text;

    private void Start()
    {
        SupplyManager.Instance.OnSupplyChanged += HandleSupplyChanged;
        HandleSupplyChanged(SupplyManager.Instance.CurrentSupplyCount);
    }

    private void HandleSupplyChanged(float amount)
    {
        text.SetValue(amount);
    }

    private void OnDestroy()
    {
        if (SupplyManager.Instance != null)
        {
            SupplyManager.Instance.OnSupplyChanged -= HandleSupplyChanged;
        }
    }
}
