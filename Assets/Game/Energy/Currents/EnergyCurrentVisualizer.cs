using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class EnergyCurrentVisualizer : MonoBehaviour
{
    [SerializeField] private CurrentContainer currentContainer;
    [SerializeField] private GameObject currentVisualsPrefab;

    private HashSet<GameObject> _currentVisuals = new();
    private bool _active;

    public void TryActivate()
    {
        if (_active) return;
        currentContainer.OnCurrentsChanged.AddListener(Refresh);
        Activate();
    }

    private void Activate()
    {
        _active = true;
        foreach (var current in currentContainer.Currents)
        {
            VisualizeCurrent(current);
        }
    }
    
    private void Refresh()
    {
        Deactivate();
        Activate();
    }
    
    public void TryDeactivate()
    {
        if (!_active) return;
        currentContainer.OnCurrentsChanged.RemoveListener(Refresh);
        Deactivate();
    }

    private void Deactivate()
    {
        _active = false;
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