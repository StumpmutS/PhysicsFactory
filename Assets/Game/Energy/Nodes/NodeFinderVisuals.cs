using System;
using UnityEngine;
using UnityEngine.Serialization;

public class NodeFinderVisuals : MonoBehaviour
{
    [SerializeField] private EnergyNodeFinder finder;
    [SerializeField] private GameObject visuals;

    private void Awake()
    {
        finder.OnRangeUpdated.AddListener(HandleRangeUpdate);
    }

    private void Start()
    {
        HandleRangeUpdate(finder.Range);
    }

    private void HandleRangeUpdate(float range)
    {
        visuals.transform.localScale = (range * 2 + 1.01f) * Vector3.one;
    }

    public void ActivateVisuals()
    {
        visuals.SetActive(true);
    }

    public void DeactivateVisuals()
    {
        visuals.SetActive(false);
    }
}