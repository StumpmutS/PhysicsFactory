﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility.Scripts;

public class PlacementManager : Singleton<PlacementManager>, IEscapable
{
    [SerializeField] private Grid3D grid;

    private bool _loaded;
    private Builder _builder;

    public UnityEvent OnUnload;

    public void Load(BuildingInfo info)
    {
        _loaded = true;
        EscapeManager.Instance.RegisterEscapable(this);
        SelectionDisabler.Disable(this);
        ClearBuilder();
        _builder = new Builder(grid, info);
        _builder.OnBuildComplete += HandleBuildComplete;
        _builder.OnBuildFailure += HandleBuildFailure;
    }

    private void Update()
    {
        if (_loaded) _builder.Tick();
    }

    private void HandleBuildComplete()
    {
        Unload();
    }

    private void HandleBuildFailure(RestrictionFailureInfo failureInfo)
    {
        RestrictionFailureDisplay.Instance.DisplayFailure(failureInfo);
    }

    public void Unload()
    {
        if (!_loaded) return;

        EscapeManager.Instance.DeregisterEscapable(this);
        SelectionDisabler.Enable(this);
        _loaded = false;
        ClearBuilder();
        OnUnload.Invoke();
    }

    private void ClearBuilder()
    {
        if (_builder == null) return;
        
        _builder.OnBuildComplete -= HandleBuildComplete;
        _builder.OnBuildFailure -= HandleBuildFailure;
        _builder.Destroy();
        _builder = null;
    }
    
    public void Escape()
    {
        Unload();
    }
}