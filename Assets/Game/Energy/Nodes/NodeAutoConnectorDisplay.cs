using UnityEngine;
using UnityEngine.UI;

public class NodeAutoConnectorDisplay : SelectableDisplay<EnergyNodeAutoConnector>
{
    [SerializeField] private GameObject container;
    [SerializeField] private LayoutDisplay layout;
    [SerializeField] private Toggle togglePrefab;

    private EnergyNodeAutoConnector _nodeAutoConnector;
    
    protected override void SetupSelectionDisplay(EnergyNodeAutoConnector connector)
    {
        _nodeAutoConnector = connector;
        container.SetActive(true);
        var toggle = Instantiate(togglePrefab);
        toggle.isOn = connector.Locked;
        toggle.onValueChanged.AddListener(HandleToggleChanged);
        layout.Add(toggle.transform);
    }

    private void HandleToggleChanged(bool value)
    {
        _nodeAutoConnector.Locked = value;
    }

    protected override void RemoveSelectionDisplay()
    {
        layout.Clear();
        container.SetActive(false);
    }
}