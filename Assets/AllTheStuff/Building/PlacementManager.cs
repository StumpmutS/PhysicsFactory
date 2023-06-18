using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class PlacementManager : Singleton<PlacementManager>
{
    [SerializeField] private GridProjector gridProjector;
    [SerializeField] private YLevelManager yLevelManager;

    public bool Loaded { get; private set; }
    
    private Builder _builder;

    public void Load(BuildingInfo info)
    {
        if (_builder != null) Unload();

        Loaded = true;
        
        gridProjector.Project2DGrid(yLevelManager.YLevel);

        _builder = new Builder(gridProjector, info);
        _builder.OnBuildComplete += HandleBuildComplete;
        
        SelectionManager.Instance.PrioritizeSelectables(gridProjector.ProjectedCells.Select(c => c.Selectable));
    }

    private void HandleBuildComplete()
    {
        Unload();
    }

    public void Unload()
    {
        Loaded = false;
        SelectionManager.Instance.UnPrioritizeSelectables(gridProjector.ProjectedCells.Select(c => c.Selectable));
        gridProjector.UnProject2DGrid();
        _builder.OnBuildComplete -= HandleBuildComplete;
        _builder.Destroy();
        _builder = null;
    }
}