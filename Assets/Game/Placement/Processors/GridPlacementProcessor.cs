using UnityEngine;
using Utility.Scripts;

[CreateAssetMenu(menuName = "Placement/Processors/Grid")]
public class GridPlacementProcessor : PlacementProcessor
{
    public override void Process(PlacementProcessingData data)
    {
        if (!data.Grid.GetIntersectedCellPosition(MainCameraRef.Cam.ScreenPointToRay(Input.mousePosition),
                YLevelManager.Instance.YLevel, out var position)) return;
        
        data.Position = position;
    }
}