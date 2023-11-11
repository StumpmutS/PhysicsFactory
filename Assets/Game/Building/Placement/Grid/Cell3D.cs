using UnityEngine;

public class Cell3D : MonoBehaviour
{
    public Cell3DInfo Info { get; private set; }

    public void Init(Cell3DInfo info)
    {
        Info = info;
    }
}