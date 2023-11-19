using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class PlacementManager : Singleton<PlacementManager>
{
    [SerializeField] private Grid3D grid;

    private bool _loaded;
    private Builder _builder;

    public UnityEvent OnPlacementFinished;

    public void Load(BuildingInfo info)
    {
        if (_builder != null) Unload();

        SelectionDisabler.Disable(this);
        _loaded = true;
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
        OnPlacementFinished.Invoke();
    }

    private void HandleBuildFailure(RestrictionFailureInfo failureInfo)
    {
        RestrictionFailureDisplay.Instance.DisplayFailure(failureInfo);
    }

    public void Unload()
    {
        if (!_loaded) return;

        SelectionDisabler.Enable(this);
        _loaded = false;
        _builder.OnBuildComplete -= HandleBuildComplete;
        _builder.OnBuildFailure -= HandleBuildFailure;
        _builder.Destroy();
        _builder = null;
    }
}