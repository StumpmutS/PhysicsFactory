using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts.Extensions;

public class PlacementDisplay : MonoBehaviour
{
    [SerializeField] private DataService<IEnumerable<PlacementData>> placementService;
    [SerializeField] private LayoutDisplay layoutDisplay;
    [SerializeField] private CallbackToggle togglePrefab;

    private HashSet<CallbackToggle> _toggles = new();
    private PlacementData _activeData;
    private bool _listeningUnload;

    public void HandlePlacementReady() => Display(placementService.RequestData());

    private void Display(IEnumerable<PlacementData> placementData)
    {
        _toggles.Clear();
        layoutDisplay.Clear();
        
        foreach (var data in placementData)
        {
            layoutDisplay.AddPrefab(togglePrefab, toggle =>
            {
                toggle.Init(new CallbackToggleData(HandleToggled, data, false));
                var container = toggle.AddOrGetComponent<ContextDataContainer>();
                container.SetData(data.Context);
                _toggles.Add(toggle);
            });
        }
        
        ListenOnUnload();
    }

    private void ListenOnUnload()
    {
        if (_listeningUnload) return;
        _listeningUnload = true;
        PlacementManager.Instance.OnUnload.AddListener(HandlePlacementUnload);
    }

    private void StopListenOnUnload()
    {
        _listeningUnload = false;
        PlacementManager.Instance.OnUnload.RemoveListener(HandlePlacementUnload);
    }
    
    private void HandlePlacementUnload()
    {
        _activeData = null;
        
        foreach (var toggle in _toggles)
        {
            toggle.Toggle.isOn = false;
        }
    }

    private void HandleToggled(object obj, bool value)
    {
        if (obj is not PlacementData data) return;

        if (!value && data == _activeData)
        {
            StopListenOnUnload();
            PlacementManager.Instance.Unload();
            _activeData = null;
            ContextPanelManager.Instance.RemoveLockedDisplay(this);
        }

        if (value && data != _activeData)
        {
            ListenOnUnload();
            PlacementManager.Instance.Load(data);
            _activeData = data;
            ContextPanelManager.Instance.DisplayLockedPanel(this, data.Context);
        }
    }

    private void OnDestroy()
    {
        if (PlacementManager.Instance != null) StopListenOnUnload();
    }
}