using UnityEngine;

public class Extractable : MonoBehaviour
{
    [SerializeField] private ExtractionData data;

    public ExtractionData Extract()
    {
        return data;
    }
}