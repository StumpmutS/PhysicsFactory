using UnityEngine;

public class Cell3D : MonoBehaviour
{
    public Cell3DData Data { get; private set; }

    public void Init(Cell3DData data)
    {
        Data = data;
    }
}