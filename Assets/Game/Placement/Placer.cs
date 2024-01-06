using System;
using Object = UnityEngine.Object;

public class Placer
{
    private PlacementData _placementData;
    private PlacementProcessingData _processingData;

    public Placer(Grid3D grid, PlacementData placementData)
    {
        _placementData = placementData;
        var preview = Object.Instantiate(_placementData.PreviewPrefab);
        _processingData = new PlacementProcessingData(grid, preview, _placementData.PlacementRestrictions, GenerateRestrictionInfo);
        
        InputTranslationManager.Instance.OnResetDown.AddListener(HandleReset);
        InputTranslationManager.Instance.OnInteractNonUIUp.AddListener(HandleInteract);
    }

    public event Action OnBuildComplete = delegate { };
    public event Action<RestrictionFailureInfo> OnBuildFailure = delegate { };

    public void Tick()
    {
        foreach (var processor in _placementData.PlacementProcessors)
        {
            processor.Process(_processingData);
        }
    }

    private void HandleInteract()
    {
        Tick();
        _processingData.SelectedPositions.Push(_processingData.Position);

        if (_processingData.SelectedPositions.Count < _placementData.AnchorCellAmount) return;
        
        if (!TryCompleteBuild()) _processingData.SelectedPositions.Pop();
    }

    private void HandleReset()
    {
        if (_processingData.SelectedPositions.Count > 0) _processingData.SelectedPositions.Pop();
    }

    private PlacementRestrictionInfo GenerateRestrictionInfo() => new (_processingData.Preview, _placementData.Price);
    
    private bool TryCompleteBuild()
    {
        var failureInfo = new RestrictionFailureInfo();
        if (!RestrictionHelper.TryPassRestrictions(_placementData.PlacementRestrictions, GenerateRestrictionInfo(), failureInfo))
        {
            OnBuildFailure.Invoke(failureInfo);
            return false;
        }
        
        _processingData.Preview.Place(_placementData);
        OnBuildComplete.Invoke();
        return true;
    }

    public void Destroy()
    {
        InputTranslationManager.Instance.OnResetDown.RemoveListener(HandleReset);
        InputTranslationManager.Instance.OnInteractNonUIUp.RemoveListener(HandleInteract);
        Object.Destroy(_processingData.Preview.gameObject);
    }
}