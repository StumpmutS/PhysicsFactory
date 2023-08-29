using System.Linq;
using UnityEngine;
using UnityEngine.Events;
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
        ProjectGrid();
        _builder = new Builder(gridProjector, info);
        _builder.OnBuildComplete += HandleBuildComplete;
    }

    private void HandleBuildComplete()
    {
        Unload();
    }

    public void Unload()
    {
        if (!Loaded) return;
    
        Loaded = false;
        UnProjectGrid();
        _builder.OnBuildComplete -= HandleBuildComplete;
        _builder.Destroy();
        _builder = null;
    }

    private void ProjectGrid()
    {
        gridProjector.Project2DGrid(yLevelManager.YLevel);
        SelectionManager.Instance.PrioritizeSelectables(
            gridProjector.Cells.SelectMany(l => l).Select(c => c.Selectable));
    }

    private void UnProjectGrid()
    {
        SelectionManager.Instance.UnPrioritizeSelectables(gridProjector.Cells.SelectMany(l => l)
            .Select(c => c.Selectable));
        gridProjector.UnProject2DGrid();
    }

    public void HandleYLevelChanged(int value)
    {
        if (!Loaded) return;
        
        gridProjector.Project2DGrid(value);
    }
}