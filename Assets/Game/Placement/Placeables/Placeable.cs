using UnityEngine;
using UnityEngine.Events;

public class Placeable : MonoBehaviour, ISellable, ISaveable<PlaceableSaveData>
{
    [SerializeField] private IdentifiableObject identifiableObject;
    
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

    public void Save(PlaceableSaveData data, AssetRefCollection assetRefCollection)
    {
        data.PlacedSaveData = new PlacedSaveData(Data);
        data.IdentifiableObjectSaveData ??= new IdentifiableObjectSaveData(-1);
        identifiableObject.Save(data.IdentifiableObjectSaveData, assetRefCollection);
    }
}