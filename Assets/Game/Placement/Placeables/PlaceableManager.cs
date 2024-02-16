using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility.Scripts;

public class PlaceableManager : Singleton<PlaceableManager>
{
    private HashSet<Placeable> _placeables = new();

    public UnityEvent OnPlaceablesUpdated = new();

    public void AddPlaceable(Placeable placeable)
    {
        if (!_placeables.Add(placeable)) return;
        
        OnPlaceablesUpdated.Invoke();
    }

    public void RemovePlaceable(Placeable placeable)
    {
        if (!_placeables.Remove(placeable)) return;
        
        OnPlaceablesUpdated.Invoke();
    }
}