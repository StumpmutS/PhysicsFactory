using System;
using UnityEngine;
using UnityEngine.UI;

public class NodeAutoConnectorDisplay : SelectableDisplay<EnergyNodeAutoConnector>
{
    [SerializeField] private GameObject container;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private Toggle togglePrefab;

    private Toggle _toggle;
    private EnergyNodeAutoConnector _nodeAutoConnector;
    
    protected override void SetupSelectionDisplay(EnergyNodeAutoConnector connector)
    {
        _nodeAutoConnector = connector;
        container.SetActive(true);
        _toggle = Instantiate(togglePrefab);
        _toggle.isOn = connector.Locked;
        _toggle.onValueChanged.AddListener(HandleToggleChanged);
        layout.Add(_toggle.transform);
        _nodeAutoConnector.OnLockChanged.AddListener(HandleLockedChanged);
    }

    private void HandleLockedChanged()
    {
        if (_toggle.isOn != _nodeAutoConnector.Locked) _toggle.isOn = _nodeAutoConnector.Locked;
    }

    private void HandleToggleChanged(bool value)
    {
        _nodeAutoConnector.Locked = value;
    }

    protected override void RemoveSelectionDisplay()
    {
        RemoveListeners();
        layout.Clear();
        container.SetActive(false);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        if (_nodeAutoConnector != null) _nodeAutoConnector.OnLockChanged.AddListener(HandleLockedChanged);
        if (_toggle != null) _toggle.onValueChanged.RemoveListener(HandleToggleChanged);
    }
}