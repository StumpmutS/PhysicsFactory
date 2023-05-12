using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyDeposit : MonoBehaviour
{
    [SerializeField] private LayerMask dolboidLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & dolboidLayer) == 0) return;
        if (!other.gameObject.TryGetComponent<Dolboid>(out var dolboid)) return;

        SupplyManager.Instance.DepositDolboid(dolboid);
        Destroy(dolboid.gameObject);
    }
}
