using TMPro;
using UnityEngine;

public class SupplyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        SupplyManager.Instance.OnSupplyChanged += HandleSupplyChanged;
        HandleSupplyChanged(SupplyManager.Instance.CurrentSupplyCount);
    }

    private void HandleSupplyChanged(int amount)
    {
        text.text = $"Supply: ${amount}";
    }

    private void OnDestroy()
    {
        if (SupplyManager.Instance != null)
        {
            SupplyManager.Instance.OnSupplyChanged -= HandleSupplyChanged;
        }
    }
}
