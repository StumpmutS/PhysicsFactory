using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class PlacementManager : Singleton<PlacementManager>
{
    [SerializeField] private Grid3D gameGrid;

    public bool Loaded { get; private set; }
    
    private Builder _builder;

    public void Load(BuildingInfo info)
    {
        if (_builder != null) Unload();

        Loaded = true;
        
        gameGrid.Project2DGrid(0);
        
        _builder = info.BuildStyle switch
        {
            EBuildStyle.Chain => new ChainBuilder(gameGrid, info),
            EBuildStyle.Volume => new VolumeBuilder(gameGrid, info),
            _ => _builder
        };
        _builder.OnBuildComplete += HandleBuildComplete;
        
        SelectionManager.Instance.PrioritizeSelectables(gameGrid.ProjectedCells.Select(c => c.Selectable));
    }

    private void HandleBuildComplete(List<Cell3D> cells)
    {
        Unload();
    }

    public void Unload()
    {
        Loaded = false;
        SelectionManager.Instance.UnPrioritizeSelectables(gameGrid.ProjectedCells.Select(c => c.Selectable));
        gameGrid.UnProject2DGrid();
        _builder.OnBuildComplete -= HandleBuildComplete;
        _builder.Destroy();
        _builder = null;
    }
}