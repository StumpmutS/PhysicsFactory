using UnityEngine;
using Utility.Scripts;

public abstract class SignedIntegerSelectorListener : MonoBehaviour
{
    [SerializeField] private SignedIntegerSelector integerSelector;
    
    private void Awake()
    {
        integerSelector.OnChanged.AddListener(HandleChange);
    }

    private void HandleChange(object _, SignedInt value)
    {
        UpdateValue(value);
    }

    protected abstract void UpdateValue(SignedInt value);
}