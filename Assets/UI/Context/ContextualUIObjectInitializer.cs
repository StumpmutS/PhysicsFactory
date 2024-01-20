using UnityEngine;

public class ContextualUIObjectInitializer : MonoBehaviour
{
    [SerializeField] private bool useStartingContext;
    [SerializeField, ShowIf(nameof(useStartingContext), true, 4)] private ContextData contextData;
    [SerializeField] private ContextualUIObjectData data;

    private void Awake()
    {
        ContextualUIObjectBuilder.BuildObject(gameObject, data, useStartingContext ? contextData : null);
    }
}