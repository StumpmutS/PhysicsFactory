using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility.Scripts;

public class PlaceableManager : Singleton<PlaceableManager>
{
    private HashSet<Placeable> _placeables = new();

    public UnityEvent OnPlaceableAdded = new();

    public void AddPlaceable(Placeable placeable)
    {
        if (!_placeables.Add(placeable)) return;
        
        OnPlaceableAdded.Invoke();
    }

    public void RemovePlaceable(Placeable placeable)
    {
        _placeables.Remove(placeable);
    }
}