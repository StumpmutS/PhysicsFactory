using System;
using Object = UnityEngine.Object;

public class Builder
{
    private BuildingPlacementData _buildingPlacementData;
    private PlacementProcessingData _processingData;

    public Builder(Grid3D grid, BuildingPlacementData buildingPlacementData)
    {
        _buildingPlacementData = buildingPlacementData;
        var preview = Object.Instantiate(_buildingPlacementData.PreviewPrefab);
        //preview.gameObject.SetActive(false);
        _processingData = new PlacementProcessingData(grid, preview, _buildingPlacementData.PlacementRestrictions, GenerateRestrictionInfo);
        
        InputTranslationManager.Instance.OnResetDown.AddListener(HandleReset);
        InputTranslationManager.Instance.OnInteractNonUIUp.AddListener(HandleInteract);
    }

    public event Action OnBuildComplete = delegate { };
    public event Action<RestrictionFailureInfo> OnBuildFailure = delegate { };

    public void Tick()
    {
        foreach (var processor in _buildingPlacementData.PlacementProcessors)
        {
            processor.Process(_processingData);
        }
    }

    private void HandleInteract()
    {
        Tick();
        _processingData.SelectedPositions.Push(_processingData.Position);

        if (_processingData.SelectedPositions.Count < _buildingPlacementData.AnchorCellAmount) return;
        
        if (!TryCompleteBuild()) _processingData.SelectedPositions.Pop();
    }

    private void HandleReset()
    {
        if (_processingData.SelectedPositions.Count > 0) _processingData.SelectedPositions.Pop();
    }

    private PlacementRestrictionInfo GenerateRestrictionInfo() => new (_processingData.Preview, _buildingPlacementData.Price);
    
    private bool TryCompleteBuild()
    {
        var failureInfo = new RestrictionFailureInfo();
        if (!RestrictionHelper.TryPassRestrictions(_buildingPlacementData.PlacementRestrictions, GenerateRestrictionInfo(), failureInfo))
        {
            OnBuildFailure.Invoke(failureInfo);
            return false;
        }
        
        _processingData.Preview.Place(_buildingPlacementData);
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