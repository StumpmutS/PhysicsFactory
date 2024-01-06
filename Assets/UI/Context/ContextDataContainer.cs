using UnityEngine;

public class ContextDataContainer : MonoBehaviour
{
    [SerializeField] private ContextData data;
    public ContextData Data => data;
}