using Utility.Scripts;
using UnityEngine;

public class SupplyDeposit : MonoBehaviour
{
    [SerializeField] private LayerMask dolboidLayer;
    [SerializeField] private DepositEffects depositEffects;

    private void OnTriggerEnter(Collider other)
    {
        if (!LayerHelper.LayerEqualsMask(other.gameObject.layer, dolboidLayer)) return;
        if (!other.gameObject.TryGetComponent<Dolboid>(out var dolboid)) return;

        SupplyManager.Instance.DepositDolboid(dolboid, depositEffects);
        Destroy(dolboid.gameObject);
    }
}