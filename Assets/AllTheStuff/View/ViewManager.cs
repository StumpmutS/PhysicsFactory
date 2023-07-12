using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class ViewManager : Singleton<ViewManager>
{
    [SerializeField] private EView defaultView;

    private EView _currentView;
    private HashSet<Viewable> _viewables = new();

    private void Start()
    {
        SetView(defaultView);
    }

    public void RegisterViewable(Viewable viewable)
    {
        _viewables.Add(viewable);
    }

    public void DeregisterViewable(Viewable viewable)
    {
        _viewables.Remove(viewable);
    }

    public void TrySetView(EView view)
    {
        if (view == _currentView) return;
        SetView(view);
    }

    private void SetView(EView view)
    {
        _currentView = view;
        foreach (var viewable in _viewables)
        {
            if (viewable.View == view) continue;
            viewable.Deactivate();
        }
        foreach (var viewable in _viewables)
        {
            if (viewable.View != view) continue;
            viewable.Activate();
        }
    }
}