using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class BuildingDisplay : MonoBehaviour
{
    [SerializeField] private List<BuildingInfo> buildings;
    [SerializeField] private LayoutDisplay layoutDisplay;
    [SerializeField] private LabeledCallbackButton buttonPrefab;

    private BuildingInfo _activeInfo;
    
    private void Start()
    {
        foreach (var building in buildings)
        {
            var button = Instantiate(buttonPrefab);
            button.Init(HandleClicked, building);
            button.SetText(building.Label);
            layoutDisplay.Add(button.transform);
        }
    }

    private void HandleClicked(object obj)
    {
        if (obj is not BuildingInfo info) return;

        if (info == _activeInfo && PlacementManager.Instance.Loaded)
        {
            _activeInfo = null;
            return;
        }
        
        _activeInfo = info;
        PlacementManager.Instance.Load(info);
    }
}