using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModificationInfo
{
    [SerializeField] private Modification modificationPrefab;
    public Modification ModificationPrefab => modificationPrefab;
    [SerializeField] private string label;
    public string Label => label;
    [SerializeField] private List<Restriction<ModificationRestrictionInfo>> activationRestrictions;
    public List<Restriction<ModificationRestrictionInfo>> ActivationRestrictions => activationRestrictions;
    [SerializeField] private List<Restriction<ModificationRestrictionInfo>> saleRestrictions;
    public List<Restriction<ModificationRestrictionInfo>> SaleRestrictions => saleRestrictions;
    [SerializeField] private float price;
    public float Price => price;
}