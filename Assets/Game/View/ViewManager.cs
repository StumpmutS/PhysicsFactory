using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Scripts;

public class ViewManager : Singleton<ViewManager>
{
    [SerializeField] private EView defaultView;

    public EView CurrentView { get; private set; }

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
        if (view == CurrentView) return;
        SetView(view);
    }

    private void SetView(EView view)
    {
        CurrentView = view;
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