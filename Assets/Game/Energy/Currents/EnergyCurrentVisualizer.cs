using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public abstract class EnergyCurrentVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject currentVisualsPrefab;

    protected abstract HashSet<EnergyCurrent> Currents { get; }

    private HashSet<GameObject> _currentVisuals = new();

    public void HandleEngaged()
    {
        foreach (var current in Currents)
        {
            VisualizeCurrent(current);
        }
    }

    public void HandleDisengaged()
    {
        foreach (var current in _currentVisuals)
        {
            Destroy(current.gameObject);
        }
        _currentVisuals.Clear();
    }

    private void VisualizeCurrent(EnergyCurrent current)
    {
        var origin = current.Sender.transform.position;
        var destination = current.Receiver.transform.position;
        var direction = destination - origin;
        var visuals = Instantiate(currentVisualsPrefab);
        visuals.transform.forward = direction;
        visuals.transform.position = Vector3.Lerp(origin, destination, .5f);
        visuals.transform.localScale = new Vector3(visuals.transform.localScale.x, visuals.transform.localScale.y,
            direction.magnitude);
        
        _currentVisuals.Add(visuals);
    }
}