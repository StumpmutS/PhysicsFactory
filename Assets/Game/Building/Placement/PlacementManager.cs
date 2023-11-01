using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class PlacementManager : Singleton<PlacementManager>
{
    [SerializeField] private Grid3D grid;

    public bool Loaded { get; private set; }
    
    private Builder _builder;

    public void Load(BuildingInfo info)
    {
        if (_builder != null) Unload();

        SelectionDisabler.Disable(this);
        Loaded = true;
        _builder = new Builder(grid, info);
        _builder.OnBuildComplete += HandleBuildComplete;
    }

    private void Update()
    {
        if (Loaded) _builder.Tick();
    }

    private void HandleBuildComplete()
    {
        Unload();
    }

    public void Unload()
    {
        if (!Loaded) return;

        SelectionDisabler.Enable(this);
        Loaded = false;
        _builder.OnBuildComplete -= HandleBuildComplete;
        _builder.Destroy();
        _builder = null;
    }
}