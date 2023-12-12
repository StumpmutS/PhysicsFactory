using System.Collections.Generic;
using UnityEngine;

public class PlacementDisplay : MonoBehaviour
{
    [SerializeField] private List<BuildingData> buildings;
    [SerializeField] private LayoutDisplay layoutDisplay;
    [SerializeField] private LabeledCallbackToggle togglePrefab;

    private HashSet<LabeledCallbackToggle> _toggles = new();
    private BuildingInfo _activeInfo;
    private bool _listeningUnload;
    
    private void Start()
    {
        foreach (var building in buildings)
        {
            var toggle = Instantiate(togglePrefab);
            toggle.Init(HandleToggled, building, false);
            toggle.SetText(building.Info.Label);
            if (toggle.transform is not RectTransform rectTransform) return;
            layoutDisplay.Add(rectTransform);
            _toggles.Add(toggle);
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
        _activeInfo = null;
        
        foreach (var toggle in _toggles)
        {
            toggle.Toggle.isOn = false;
        }
    }

    private void HandleToggled(object obj, bool value)
    {
        if (obj is not BuildingData data) return;

        if (!value && data.Info == _activeInfo)
        {
            StopListenOnUnload();
            PlacementManager.Instance.Unload();
            _activeInfo = null;
        }

        if (value && data.Info != _activeInfo)
        {
            ListenOnUnload();
            PlacementManager.Instance.Load(data.Info);
            _activeInfo = data.Info;
        }
    }

    private void OnDestroy()
    {
        if (PlacementManager.Instance != null) StopListenOnUnload();
    }
}