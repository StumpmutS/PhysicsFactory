using System.Collections.Generic;
using UnityEngine;

public class BuildingDisplay : MonoBehaviour
{
    [SerializeField] private List<BuildingData> buildings;
    [SerializeField] private LayoutDisplay layoutDisplay;
    [SerializeField] private LabeledCallbackButton buttonPrefab;

    private BuildingInfo _activeInfo;
    
    private void Start()
    {
        foreach (var building in buildings)
        {
            var button = Instantiate(buttonPrefab);
            button.Init(HandleClicked, building);
            button.SetText(building.Info.Label);
            layoutDisplay.Add(button.transform);
        }
    }

    private void HandleClicked(object obj)
    {
        if (obj is not BuildingData data) return;

        if (data.Info == _activeInfo && PlacementManager.Instance.Loaded)
        {
            _activeInfo = null;
            return;
        }
        
        _activeInfo = data.Info;
        PlacementManager.Instance.Load(data.Info);
    }
}