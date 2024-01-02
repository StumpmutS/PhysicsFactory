using System.Globalization;
using TMPro;
using UnityEngine;

public class LevelSupplyPersistenceHandler : MonoBehaviour, ILoadable<LevelData>
{
    [SerializeField] private TMP_InputField supplyInputField;

    public LoadingInfo Load(LevelData data, AssetRefCollection assetRefCollection)
    {
        supplyInputField.text = data.SupplyData.Supply.ToString(CultureInfo.InvariantCulture);
        
        return LoadingInfo.Completed(supplyInputField.text, ELoadCompletionStatus.Succeeded);
    }
}