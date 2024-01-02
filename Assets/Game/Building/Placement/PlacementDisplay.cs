using System.Collections.Generic;
using UnityEngine;

public class PlacementDisplay : MonoBehaviour
{
    [SerializeField] private DataService<BuildingPlacementData> placementService;
    [SerializeField] private LayoutDisplay layoutDisplay;
    [SerializeField] private LabeledCallbackToggle togglePrefab;

    private HashSet<LabeledCallbackToggle> _toggles = new();
    private BuildingPlacementData _activeData;
    private bool _listeningUnload;

    public void HandleBuildingsReady() => Display(placementService.RequestData());

    private void Display(IEnumerable<BuildingPlacementData> buildings)
    {
        _toggles.Clear();
        layoutDisplay.Clear();
        
        foreach (var building in buildings)
        {
            layoutDisplay.AddPrefab(togglePrefab, toggle =>
            {
                toggle.Init(new CallbackToggleData(HandleToggled, building, false));
                toggle.SetText(building.Label); 
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
        if (obj is not BuildingPlacementData data) return;

        if (!value && data == _activeData)
        {
            StopListenOnUnload();
            PlacementManager.Instance.Unload();
            _activeData = null;
        }

        if (value && data != _activeData)
        {
            ListenOnUnload();
            PlacementManager.Instance.Load(data);
            _activeData = data;
        }
    }

    private void OnDestroy()
    {
        if (PlacementManager.Instance != null) StopListenOnUnload();
    }
}