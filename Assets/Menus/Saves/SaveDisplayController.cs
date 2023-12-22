using System.Collections.Generic;
using UnityEngine;

public class SaveDisplayController : MonoBehaviour
{
    [SerializeField] private SaveDisplayService service;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private SaveDisplayToggle saveDisplayTogglePrefab;
    [SerializeField] private SaveInteractionDisplay saveInteractionDisplayPrefab;
    [SerializeField] private LayoutDisplay toggleLayoutDisplay;
    [SerializeField] private LayoutDisplay interactionLayoutDisplay;

    private SaveDisplayData _currentData;
    
    public void Display()
    {
        toggleLayoutDisplay.Clear();
        interactionLayoutDisplay.Clear();
        
        foreach (var data in service.RequestData())
        {
            toggleLayoutDisplay.AddPrefab(saveDisplayTogglePrefab,
                toggle => toggle.Init(data, new CallbackToggleData(HandleToggle, data, false)));
        }
    }

    private void HandleToggle(object obj, bool value)
    {
        if (obj is not SaveDisplayData saveDisplayData || _currentData == saveDisplayData) return;

        _currentData = saveDisplayData;
        interactionLayoutDisplay.Clear();
        interactionLayoutDisplay.AddPrefab(saveInteractionDisplayPrefab, 
            interactionDisplay =>
            {
                interactionDisplay.OnLoadRequest.AddListener(HandleLoadRequest);
                interactionDisplay.Init(saveDisplayData);
            });
    }

    private void HandleLoadRequest(SaveDisplayData data)
    {
        levelLoader.LoadLevel(data.SaveData.LevelData);
    }
}