using System.Collections.Generic;
using Utility.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class Deposit : MonoBehaviour
{
    [SerializeField] private LayerMask resourceLayer;
    [SerializeField] private List<ResourceModifier> modifiers;

    private void OnTriggerEnter(Collider other)
    {
        if (!LayerHelper.LayerEqualsMask(other.gameObject.layer, resourceLayer)) return;
        if (!other.gameObject.TryGetComponent<Resource>(out var resource)) return;

        var modifiedResourceData = ResourceModifierHelpers.ModifiedResourceData(resource.Data, modifiers);
        SupplyManager.Instance.DepositResource(modifiedResourceData);
        Destroy(resource.gameObject);
    }
}