using UnityEngine;
using Utility.Scripts;

public abstract class SignedFloatSelectorListener : MonoBehaviour
{
    [SerializeField] private SignedFloatSelector floatSelector;
    
    private void Awake()
    {
        floatSelector.OnChanged.AddListener(HandleChange);
    }

    private void HandleChange(object _, SignedFloat value)
    {
        UpdateValue(value);
    }

    protected abstract void UpdateValue(SignedFloat value);
}