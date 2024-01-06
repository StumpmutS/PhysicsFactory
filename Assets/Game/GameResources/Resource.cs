using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceData Data { get; private set; }
    
    public void Init(ResourceData data)
    {
        Data = data;
    }
}