using UnityEngine;

public class PlacedBuildingInfo
{
    public float Volume { get; private set; }
    public float Price { get; private set; }

    public PlacedBuildingInfo(float volume, float price)
    {
        Volume = volume;
        Price = price;
    }
}