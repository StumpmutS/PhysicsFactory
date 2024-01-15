using UnityEngine;
using UnityEngine.Events;

public class Placeable : MonoBehaviour, ISellable, ISaveable<SaveableObjectSaveData>
{
    public PlacedData Data { get; private set; }
    public float SalePrice => SupplyCalculator.CalculatePrice(Data.Price, this, Data.SaleMultiplier);

    public UnityEvent OnInitialized = new();

    public void Init(PlacedData data)
    {
        Data = data;
        transform.localScale = data.TransformData.LocalScale;
        OnInitialized.Invoke();
    }

    private void Start()
    {
        PlaceableManager.Instance.AddPlaceable(this);
    }

    private void OnDestroy()
    {
        PlaceableManager.Instance.RemovePlaceable(this);
    }

    public void Save(SaveableObjectSaveData data, AssetRefCollection assetRefCollection)
    {
        data.PlacedSaveData = new PlacedSaveData(Data);
    }
}
