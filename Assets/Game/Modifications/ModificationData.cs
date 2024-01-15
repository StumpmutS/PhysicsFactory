using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModificationData
{
    [SerializeField] private Modification modificationPrefab;
    public Modification ModificationPrefab => modificationPrefab;
    [SerializeField] private ContextData context;
    public ContextData Context => context;
    [SerializeField] private List<Restriction<PlaceableRestrictionData>> activationRestrictions;
    public List<Restriction<PlaceableRestrictionData>> ActivationRestrictions => activationRestrictions;
    [SerializeField] private float price;
    public float Price => price;
    [SerializeField, Range(0, 1)] private float saleMultiplier = 1;
    public float SaleMultiplier => saleMultiplier;
}