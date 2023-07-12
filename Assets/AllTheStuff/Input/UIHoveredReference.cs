using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility.Scripts;

public class UIHoveredReference : Singleton<UIHoveredReference>
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private List<GraphicRaycaster> raycasters;

    public bool OverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        
        foreach (var raycaster in raycasters)
        {
            raycaster.Raycast(pointerEventData, results);
            if (results.Count > 0) return true;
        }

        return false;
    }
}