using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility.Scripts;

public class PlacementManager : Singleton<PlacementManager>, IPoppable
{
    [SerializeField] private Grid3D grid;
    [SerializeField] private PoppableStack escapeStack;

    private bool _loaded;
    private Placer _placer;

    public UnityEvent OnUnload;

    public void Load(PlacementData data)
    {
        _loaded = true;
        escapeStack.RegisterPoppable(this);
        SelectionDisabler.Disable(this);
        ClearBuilder();
        _placer = new Placer(grid, data);
        _placer.OnBuildComplete += HandleBuildComplete;
        _placer.OnBuildFailure += HandleBuildFailure;
    }

    private void Update()
    {
        if (_loaded) _placer.Tick();
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

        escapeStack.DeregisterPoppable(this);
        SelectionDisabler.Enable(this);
        _loaded = false;
        ClearBuilder();
        OnUnload.Invoke();
    }

    private void ClearBuilder()
    {
        if (_placer == null) return;
        
        _placer.OnBuildComplete -= HandleBuildComplete;
        _placer.OnBuildFailure -= HandleBuildFailure;
        _placer.Destroy();
        _placer = null;
    }
    
    public void Pop()
    {
        Unload();
    }
}